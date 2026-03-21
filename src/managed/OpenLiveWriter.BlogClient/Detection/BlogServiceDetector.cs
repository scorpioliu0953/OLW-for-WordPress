// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.

using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using mshtml;
using OpenLiveWriter.BlogClient.Clients;
using OpenLiveWriter.BlogClient.Providers;
using OpenLiveWriter.Controls;
using OpenLiveWriter.CoreServices;
using OpenLiveWriter.CoreServices.Diagnostics;
using OpenLiveWriter.CoreServices.Progress;
using OpenLiveWriter.Extensibility.BlogClient;
using OpenLiveWriter.HtmlParser.Parser;
using OpenLiveWriter.Localization;

namespace OpenLiveWriter.BlogClient.Detection
{
    public class BlogServiceDetector : BlogServiceDetectorBase
    {
        private IBlogSettingsAccessor _blogSettings;

        public BlogServiceDetector(IBlogClientUIContext uiContext, Control hiddenBrowserParentControl, IBlogSettingsAccessor blogSettings, IBlogCredentialsAccessor credentials)
            : base(uiContext, hiddenBrowserParentControl, blogSettings.Id, blogSettings.HomepageUrl, credentials)
        {
            _blogSettings = blogSettings;
        }

        protected override object DetectBlogService(IProgressHost progressHost)
        {
            using (BlogClientUIContextSilentMode uiContextScope = new BlogClientUIContextSilentMode()) //suppress prompting for credentials
            {
                try
                {
                    // Step 1: Get the weblog homepage DOM for blog name and manifest detection
                    IHTMLDocument2 weblogDOM = GetWeblogHomepageDOM(progressHost);

                    // Scan for a writer editing manifest url
                    if (_manifestDownloadInfo == null && weblogDOM != null)
                    {
                        string manifestUrl = WriterEditingManifest.DiscoverUrl(_homepageUrl, weblogDOM);
                        if (manifestUrl != String.Empty)
                            _manifestDownloadInfo = new WriterEditingManifestDownloadInfo(manifestUrl);
                    }

                    // Step 2: Detect WordPress REST API
                    UpdateProgress(progressHost, 50, Res.Get(StringId.ProgressVerifyingInterface));
                    bool detectionSucceeded = AttemptWordPressRestApiDetection();

                    if (!detectionSucceeded)
                    {
                        ReportError(MessageId.WeblogDetectionUnexpectedError,
                            "Could not detect a WordPress REST API at this URL. Please ensure the site is running WordPress 4.7+ and the REST API is accessible.");
                    }

                    // finished
                    UpdateProgress(progressHost, 100, String.Empty);
                }
                catch (OperationCancelledException)
                {
                    // WasCancelled == true
                }
                catch (BlogClientOperationCancelledException)
                {
                    Cancel();
                    // WasCancelled == true
                }
                catch (BlogAccountDetectorException ex)
                {
                    if (ApplicationDiagnostics.AutomationMode)
                        Trace.WriteLine(ex.ToString());
                    else
                        Trace.Fail(ex.ToString());
                    // ErrorOccurred == true
                }
                catch (Exception ex)
                {
                    // ErrorOccurred == true
                    Trace.Fail(ex.Message, ex.ToString());
                    ReportError(MessageId.WeblogDetectionUnexpectedError, ex.Message);
                }

                return this;
            }
        }

        /// <summary>
        /// Attempts to detect the WordPress REST API by probing /wp-json/wp/v2/.
        /// Derives the REST API base URL from the homepage URL.
        /// </summary>
        private bool AttemptWordPressRestApiDetection()
        {
            // Build candidate REST API URLs to probe
            string homepageUrl = _homepageUrl.TrimEnd('/');
            string[] candidateUrls = new string[]
            {
                homepageUrl + "/wp-json/wp/v2/",
                homepageUrl + "/index.php?rest_route=/wp/v2/",
            };

            foreach (string candidateUrl in candidateUrls)
            {
                try
                {
                    // Probe the REST API root
                    string testUrl = candidateUrl;
                    if (!testUrl.EndsWith("/", StringComparison.Ordinal))
                        testUrl += "/";

                    HttpWebRequest request = HttpRequestHelper.CreateHttpWebRequest(testUrl + "types", true);
                    request.Method = "GET";
                    request.Accept = "application/json";
                    request.UserAgent = ApplicationEnvironment.UserAgent;
                    request.Timeout = 15000;

                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            // WordPress REST API detected!
                            _clientType = "WordPressRest";
                            _serviceName = "WordPress";
                            _postApiUrl = candidateUrl;

                            // Verify credentials and get blog info
                            BlogAccountDetector blogAccountDetector = new BlogAccountDetector(
                                _clientType, _postApiUrl, _credentials);

                            if (blogAccountDetector.ValidateService())
                            {
                                _usersBlogs = blogAccountDetector.UsersBlogs;
                                if (_usersBlogs.Length >= 1)
                                {
                                    _hostBlogId = _usersBlogs[0].Id;
                                    if (string.IsNullOrEmpty(_blogName) || _blogName == _homepageUrl)
                                        _blogName = _usersBlogs[0].Name;
                                }
                            }
                            else
                            {
                                AuthenticationErrorOccurred = blogAccountDetector.Exception is BlogClientAuthenticationException;
                                if (blogAccountDetector.ErrorMessageType != MessageId.None)
                                    ReportErrorAndFail(blogAccountDetector.ErrorMessageType, blogAccountDetector.ErrorMessageParams);
                            }

                            return true;
                        }
                    }
                }
                catch (WebException)
                {
                    // This candidate URL didn't work, try next
                    continue;
                }
                catch (Exception)
                {
                    continue;
                }
            }

            return false;
        }
    }

    public abstract class BlogServiceDetectorBase : MultipartAsyncOperation, ITemporaryBlogSettingsDetectionContext
    {
        public BlogServiceDetectorBase(IBlogClientUIContext uiContext, Control hiddenBrowserParentControl, string localBlogId, string homepageUrl, IBlogCredentialsAccessor credentials)
            : base(uiContext)
        {
            // save references
            _uiContext = uiContext;
            _localBlogId = localBlogId;
            _homepageUrl = homepageUrl;
            _credentials = credentials;

            // add blog service detection
            AddProgressOperation(
                new ProgressOperation(DetectBlogService),
                35);

            // add settings downloading (note: this operation will be a no-op
            // in the case where we don't succesfully detect a weblog)
            AddProgressOperation(
                new ProgressOperation(DetectWeblogSettings),
                new ProgressOperationCompleted(DetectWeblogSettingsCompleted),
                30);

            // add template downloading (note: this operation will be a no-op in the
            // case where we don't successfully detect a weblog)
            _blogEditingTemplateDetector = new BlogEditingTemplateDetector(uiContext, hiddenBrowserParentControl);
            AddProgressOperation(
                new ProgressOperation(_blogEditingTemplateDetector.DetectTemplate),
                35);
        }

        public BlogInfo[] UsersBlogs
        {
            get { return _usersBlogs; }
        }

        public string ProviderId
        {
            get { return _providerId; }
        }

        public string ServiceName
        {
            get { return _serviceName; }
        }

        public string ClientType
        {
            get { return _clientType; }
        }

        public string PostApiUrl
        {
            get { return _postApiUrl; }
        }

        public string HostBlogId
        {
            get { return _hostBlogId; }
        }

        public string BlogName
        {
            get { return _blogName; }
        }

        public IDictionary OptionOverrides
        {
            get { return _optionOverrides; }
        }

        public IDictionary HomePageOverrides
        {
            get { return _homePageOverrides; }
        }

        public IDictionary UserOptionOverrides
        {
            get { return null; }
        }

        public IBlogProviderButtonDescription[] ButtonDescriptions
        {
            get { return _buttonDescriptions; }
        }

        public BlogPostCategory[] Categories
        {
            get { return _categories; }
        }

        public BlogPostKeyword[] Keywords
        {
            get { return _keywords; }
        }

        public byte[] FavIcon
        {
            get { return _favIcon; }
        }

        public byte[] Image
        {
            get { return _image; }
        }

        public byte[] WatermarkImage
        {
            get { return _watermarkImage; }
        }

        public BlogEditingTemplateFile[] BlogTemplateFiles
        {
            get { return _blogEditingTemplateDetector.BlogTemplateFiles; }
        }

        public Color? PostBodyBackgroundColor
        {
            get { return _blogEditingTemplateDetector.PostBodyBackgroundColor; }
        }

        public bool WasCancelled
        {
            get { return CancelRequested; }
        }

        public bool ErrorOccurred
        {
            get { return _errorMessageType != MessageId.None; }
        }

        public bool AuthenticationErrorOccurred
        {
            get { return _authenticationErrorOccured; }
            set { _authenticationErrorOccured = value; }
        }
        private bool _authenticationErrorOccured = false;

        public bool TemplateDownloadFailed
        {
            get { return _blogEditingTemplateDetector.ExceptionOccurred; }
        }

        IBlogCredentialsAccessor IBlogSettingsDetectionContext.Credentials
        {
            get { return _credentials; }
        }

        string IBlogSettingsDetectionContext.HomepageUrl
        {
            get { return _homepageUrl; }
        }

        public WriterEditingManifestDownloadInfo ManifestDownloadInfo
        {
            get { return _manifestDownloadInfo; }
            set { _manifestDownloadInfo = value; }
        }

        string IBlogSettingsDetectionContext.ClientType
        {
            get { return _clientType; }
            set { _clientType = value; }
        }

        byte[] IBlogSettingsDetectionContext.FavIcon
        {
            get { return _favIcon; }
            set { _favIcon = value; }
        }

        byte[] IBlogSettingsDetectionContext.Image
        {
            get { return _image; }
            set { _image = value; }
        }

        byte[] IBlogSettingsDetectionContext.WatermarkImage
        {
            get { return _watermarkImage; }
            set { _watermarkImage = value; }
        }

        BlogPostCategory[] IBlogSettingsDetectionContext.Categories
        {
            get { return _categories; }
            set { _categories = value; }
        }

        BlogPostKeyword[] IBlogSettingsDetectionContext.Keywords
        {
            get { return _keywords; }
            set { _keywords = value; }
        }

        IDictionary IBlogSettingsDetectionContext.OptionOverrides
        {
            get { return _optionOverrides; }
            set { _optionOverrides = value; }
        }

        IDictionary IBlogSettingsDetectionContext.HomePageOverrides
        {
            get { return _homePageOverrides; }
            set { _homePageOverrides = value; }
        }

        IBlogProviderButtonDescription[] IBlogSettingsDetectionContext.ButtonDescriptions
        {
            get { return _buttonDescriptions; }
            set { _buttonDescriptions = value; }
        }

        public BlogInfo[] AvailableImageEndpoints
        {
            get { return availableImageEndpoints; }
            set { availableImageEndpoints = value; }
        }

        public void ShowLastError(IWin32Window owner)
        {
            if (ErrorOccurred)
            {
                DisplayMessage.Show(_errorMessageType, owner, _errorMessageParams);
            }
            else
            {
                Trace.Fail("Called ShowLastError when no error occurred");
            }
        }

        public static byte[] SafeDownloadFavIcon(string homepageUrl)
        {
            try
            {
                string favIconUrl = UrlHelper.UrlCombine(homepageUrl, "favicon.ico");
                using (Stream favIconStream = HttpRequestHelper.SafeDownloadFile(favIconUrl))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        StreamHelper.Transfer(favIconStream, memoryStream);
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        return memoryStream.ToArray();
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        protected abstract object DetectBlogService(IProgressHost progressHost);

        protected void AttemptUserBlogDetection()
        {
            BlogAccountDetector blogAccountDetector = new BlogAccountDetector(
                _clientType, _postApiUrl, _credentials);

            if (blogAccountDetector.ValidateService())
            {
                BlogInfo blogInfo = blogAccountDetector.DetectAccount(_homepageUrl, _hostBlogId);
                if (blogInfo != null)
                {
                    // save the detected info
                    // TODO: Commenting out next line for Spaces demo tomorrow.
                    // need to decide whether to keep it commented out going forward.
                    // _homepageUrl = blogInfo.HomepageUrl;
                    _hostBlogId = blogInfo.Id;
                    _blogName = blogInfo.Name;
                }

                // always save the list of user's blogs
                _usersBlogs = blogAccountDetector.UsersBlogs;
            }
            else
            {
                AuthenticationErrorOccurred = blogAccountDetector.Exception is BlogClientAuthenticationException;
                ReportErrorAndFail(blogAccountDetector.ErrorMessageType, blogAccountDetector.ErrorMessageParams);
            }
        }

        protected IHTMLDocument2 GetWeblogHomepageDOM(IProgressHost progressHost)
        {
            // try download the weblog home page
            UpdateProgress(progressHost, 25, Res.Get(StringId.ProgressAnalyzingHomepage));
            string responseUri;
            IHTMLDocument2 weblogDOM = HTMLDocumentHelper.SafeGetHTMLDocumentFromUrl(_homepageUrl, out responseUri);
            if (responseUri != null && responseUri != _homepageUrl)
            {
                _homepageUrl = responseUri;
            }
            if (weblogDOM != null)
            {
                // default the blog name to the title of the document
                if (weblogDOM.title != null)
                {
                    _blogName = weblogDOM.title;

                    // drop anything to the right of a "|", as it usually is a site name
                    int index = _blogName.IndexOf("|", StringComparison.OrdinalIgnoreCase);
                    if (index > 0)
                    {
                        string newname = _blogName.Substring(0, index).Trim();
                        if (newname != String.Empty)
                            _blogName = newname;
                    }
                }
            }

            return weblogDOM;
        }

        protected void CopySettingsFromProvider(IBlogProvider blogAccountProvider)
        {
            _providerId = blogAccountProvider.Id;
            _serviceName = blogAccountProvider.Name;
            _clientType = blogAccountProvider.ClientType;
            _postApiUrl = ProcessPostUrlMacros(blogAccountProvider.PostApiUrl);
        }

        private string ProcessPostUrlMacros(string postApiUrl)
        {
            return postApiUrl.Replace("<username>", _credentials.Username);
        }

        private object DetectWeblogSettings(IProgressHost progressHost)
        {
            using (BlogClientUIContextSilentMode uiContextScope = new BlogClientUIContextSilentMode()) //supress prompting for credentials
            {
                // no-op if we don't have a blog-id to work with
                if (HostBlogId == String.Empty)
                    return this;

                try
                {
                    // detect settings
                    BlogSettingsDetector blogSettingsDetector = new BlogSettingsDetector(this);
                    blogSettingsDetector.DetectSettings(progressHost);
                }
                catch (OperationCancelledException)
                {
                    // WasCancelled == true
                }
                catch (BlogClientOperationCancelledException)
                {
                    Cancel();
                    // WasCancelled == true
                }
                catch (Exception ex)
                {
                    Trace.Fail("Unexpected error occurred while detecting weblog settings: " + ex.ToString());
                }

                return this;
            }
        }

        private void DetectWeblogSettingsCompleted(object result)
        {
            // no-op if we don't have a blog detected
            if (HostBlogId == String.Empty)
                return;

            // get the editing template directory
            string blogTemplateDir = BlogEditingTemplate.GetBlogTemplateDir(_localBlogId);

            // set context for template detector
            BlogAccount blogAccount = new BlogAccount(ServiceName, ClientType, PostApiUrl, HostBlogId);
            _blogEditingTemplateDetector.SetContext(blogAccount, _credentials, _homepageUrl, blogTemplateDir, _manifestDownloadInfo, false, _providerId, _optionOverrides, null, _homePageOverrides);

        }

        protected void UpdateProgress(IProgressHost progressHost, int percent, string message)
        {
            if (CancelRequested)
                throw new OperationCancelledException();

            progressHost.UpdateProgress(percent, 100, message);
        }

        protected void ReportError(MessageId errorMessageType, params object[] errorMessageParams)
        {
            _errorMessageType = errorMessageType;
            _errorMessageParams = errorMessageParams;
        }

        protected void ReportErrorAndFail(MessageId errorMessageType, params object[] errorMessageParams)
        {
            ReportError(errorMessageType, errorMessageParams);
            throw new BlogAccountDetectorException();
        }

        protected class BlogAccountDetectorException : ApplicationException
        {
            public BlogAccountDetectorException() : base("Blog account detector did not succeed")
            {
            }
        }

        /// <summary>
        /// Blog account we are scanning
        /// </summary>
        protected string _localBlogId;
        protected string _homepageUrl;
        protected WriterEditingManifestDownloadInfo _manifestDownloadInfo = null;
        protected IBlogCredentialsAccessor _credentials;

        // BlogTemplateDetector
        private BlogEditingTemplateDetector _blogEditingTemplateDetector;

        /// <summary>
        /// Results of scanning
        /// </summary>
        protected string _providerId = String.Empty;
        protected string _serviceName = String.Empty;
        protected string _clientType = String.Empty;
        protected string _postApiUrl = String.Empty;
        protected string _hostBlogId = String.Empty;
        protected string _blogName = String.Empty;

        protected BlogInfo[] _usersBlogs = new BlogInfo[] { };

        // if we are unable to detect these values then leave them null
        // as an indicator that their values are "unknown" vs. "empty"
        // callers can then choose to not overwrite any existing settings
        // in this case
        protected IDictionary _homePageOverrides = null;
        protected IDictionary _optionOverrides = null;
        private BlogPostCategory[] _categories = null;
        private BlogPostKeyword[] _keywords = null;
        private byte[] _favIcon = null;
        private byte[] _image = null;
        private byte[] _watermarkImage = null;
        private IBlogProviderButtonDescription[] _buttonDescriptions = null;

        // error info
        private MessageId _errorMessageType;
        private object[] _errorMessageParams;
        protected IBlogClientUIContext _uiContext;
        private BlogInfo[] availableImageEndpoints;
    }
}
