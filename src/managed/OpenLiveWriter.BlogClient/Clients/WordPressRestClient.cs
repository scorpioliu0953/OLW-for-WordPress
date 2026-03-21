// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenLiveWriter.Api;
using OpenLiveWriter.BlogClient.Providers;
using OpenLiveWriter.Controls;
using OpenLiveWriter.CoreServices;
using OpenLiveWriter.Extensibility.BlogClient;
using OpenLiveWriter.Localization;

namespace OpenLiveWriter.BlogClient.Clients
{
    [BlogClient("WordPressRest", "WordPress")]
    public class WordPressRestClient : BlogClientBase, IBlogClient
    {
        private readonly string _apiBaseUrl;
        private IBlogClientOptions _clientOptions;
        private readonly string UserAgent = ApplicationEnvironment.UserAgent;

        public WordPressRestClient(Uri postApiUrl, IBlogCredentialsAccessor credentials)
            : base(credentials)
        {
            // postApiUrl should be the wp-json/wp/v2 base URL
            string url = UrlHelper.SafeToAbsoluteUri(postApiUrl);
            if (!url.EndsWith("/", StringComparison.Ordinal))
                url += "/";
            _apiBaseUrl = url;

            BlogClientOptions clientOptions = new BlogClientOptions();
            ConfigureClientOptions(clientOptions);
            _clientOptions = clientOptions;
        }

        private void ConfigureClientOptions(BlogClientOptions clientOptions)
        {
            clientOptions.SupportsCategories = true;
            clientOptions.SupportsMultipleCategories = true;
            clientOptions.SupportsHierarchicalCategories = true;
            clientOptions.SupportsNewCategories = true;
            clientOptions.SupportsSuggestCategories = true;
            clientOptions.SupportsCategoriesInline = true;
            clientOptions.SupportsCategoryIds = true;
            clientOptions.SupportsFileUpload = true;
            clientOptions.SupportsKeywords = true;
            clientOptions.SupportsGetKeywords = true;
            clientOptions.SupportsPages = true;
            clientOptions.SupportsPageParent = true;
            clientOptions.SupportsPageOrder = true;
            clientOptions.SupportsSlug = true;
            clientOptions.SupportsPassword = true;
            clientOptions.SupportsAuthor = true;
            clientOptions.SupportsCommentPolicy = true;
            clientOptions.SupportsPingPolicy = true;
            clientOptions.SupportsExcerpt = true;
            clientOptions.SupportsExtendedEntries = true;
            clientOptions.SupportsCustomDate = true;
            clientOptions.SupportsCustomDateUpdate = true;
            clientOptions.SupportsPostAsDraft = true;
            clientOptions.SupportsPostSynchronization = true;
            clientOptions.SupportsHttps = true;
        }

        #region IBlogClient Properties

        public new string ProtocolName
        {
            get
            {
                try
                {
                    return ((BlogClientAttribute)GetType().GetCustomAttributes(typeof(BlogClientAttribute), false)[0]).ProtocolName;
                }
                catch (IndexOutOfRangeException)
                {
                    Trace.Fail("BlogClientAttribute not found on type " + GetType().FullName);
                    return null;
                }
            }
        }

        public IBlogClientOptions Options
        {
            get { return _clientOptions; }
        }

        public void OverrideOptions(IBlogClientOptions newClientOptions)
        {
            _clientOptions = newClientOptions;
        }

        public bool IsSecure
        {
            get { return (_apiBaseUrl ?? "").StartsWith("https://", StringComparison.OrdinalIgnoreCase); }
        }

        #endregion

        #region Credential Verification

        protected override void VerifyCredentials(TransientCredentials tc)
        {
            try
            {
                JObject result = RestGetJson("users/me?context=edit", tc);
                if (result == null)
                    throw new BlogClientAuthenticationException("WordPress REST API", "Authentication failed");
            }
            catch (BlogClientAuthenticationException)
            {
                throw;
            }
            catch (Exception e)
            {
                if (!BlogClientUIContext.SilentModeForCurrentThread)
                    ShowError(e.Message);
                throw;
            }
        }

        private void ShowError(string error)
        {
            ShowErrorHelper helper =
                new ShowErrorHelper(BlogClientUIContext.ContextForCurrentThread, MessageId.UnexpectedErrorLogin,
                                    new object[] { error });
            if (BlogClientUIContext.ContextForCurrentThread != null)
                BlogClientUIContext.ContextForCurrentThread.Invoke(new ThreadStart(helper.Show), null);
            else
                helper.Show();
        }

        private class ShowErrorHelper
        {
            private readonly IWin32Window _owner;
            private readonly MessageId _messageId;
            private readonly object[] _args;

            public ShowErrorHelper(IWin32Window owner, MessageId messageId, object[] args)
            {
                _owner = owner;
                _messageId = messageId;
                _args = args;
            }

            public void Show()
            {
                DisplayMessage.Show(_messageId, _owner, _args);
            }
        }

        #endregion

        #region REST HTTP Helpers

        private string GetAuthHeader(TransientCredentials tc)
        {
            string credentials = tc.Username + ":" + tc.Password;
            return "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));
        }

        private JObject RestGetJson(string endpoint, TransientCredentials tc)
        {
            string url = _apiBaseUrl + endpoint;
            HttpWebRequest request = HttpRequestHelper.CreateHttpWebRequest(url, true);
            request.Method = "GET";
            request.Accept = "application/json";
            request.UserAgent = UserAgent;
            request.Headers["Authorization"] = GetAuthHeader(tc);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                string json = reader.ReadToEnd();
                return JObject.Parse(json);
            }
        }

        private JArray RestGetJsonArray(string endpoint, TransientCredentials tc)
        {
            string url = _apiBaseUrl + endpoint;
            HttpWebRequest request = HttpRequestHelper.CreateHttpWebRequest(url, true);
            request.Method = "GET";
            request.Accept = "application/json";
            request.UserAgent = UserAgent;
            request.Headers["Authorization"] = GetAuthHeader(tc);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                string json = reader.ReadToEnd();
                return JArray.Parse(json);
            }
        }

        private JArray RestGetAllPages(string endpoint, TransientCredentials tc, int perPage = 100)
        {
            JArray allResults = new JArray();
            int page = 1;
            while (true)
            {
                string separator = endpoint.Contains("?") ? "&" : "?";
                string url = _apiBaseUrl + endpoint + separator + "per_page=" + perPage + "&page=" + page;

                HttpWebRequest request = HttpRequestHelper.CreateHttpWebRequest(url, true);
                request.Method = "GET";
                request.Accept = "application/json";
                request.UserAgent = UserAgent;
                request.Headers["Authorization"] = GetAuthHeader(tc);

                try
                {
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        string json = reader.ReadToEnd();
                        JArray pageResults = JArray.Parse(json);
                        foreach (JToken item in pageResults)
                            allResults.Add(item);

                        string totalPages = response.Headers["X-WP-TotalPages"];
                        if (totalPages == null || page >= int.Parse(totalPages, CultureInfo.InvariantCulture))
                            break;
                        page++;
                    }
                }
                catch (WebException ex)
                {
                    HttpWebResponse resp = ex.Response as HttpWebResponse;
                    if (resp != null && resp.StatusCode == HttpStatusCode.BadRequest)
                        break; // page out of range
                    throw;
                }
            }
            return allResults;
        }

        private JObject RestPostJson(string endpoint, TransientCredentials tc, JObject body)
        {
            string url = _apiBaseUrl + endpoint;
            HttpWebRequest request = HttpRequestHelper.CreateHttpWebRequest(url, true);
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            request.Accept = "application/json";
            request.UserAgent = UserAgent;
            request.Headers["Authorization"] = GetAuthHeader(tc);

            byte[] data = Encoding.UTF8.GetBytes(body.ToString(Newtonsoft.Json.Formatting.None));
            request.ContentLength = data.Length;
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(data, 0, data.Length);
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                string json = reader.ReadToEnd();
                return JObject.Parse(json);
            }
        }

        private void RestDelete(string endpoint, TransientCredentials tc)
        {
            string url = _apiBaseUrl + endpoint;
            HttpWebRequest request = HttpRequestHelper.CreateHttpWebRequest(url, true);
            request.Method = "DELETE";
            request.Accept = "application/json";
            request.UserAgent = UserAgent;
            request.Headers["Authorization"] = GetAuthHeader(tc);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                // consume response
            }
        }

        private JObject RestUploadMedia(string endpoint, TransientCredentials tc, string fileName, string contentType, Stream fileStream)
        {
            string url = _apiBaseUrl + endpoint;
            HttpWebRequest request = HttpRequestHelper.CreateHttpWebRequest(url, true);
            request.Method = "POST";
            request.Accept = "application/json";
            request.UserAgent = UserAgent;
            request.Headers["Authorization"] = GetAuthHeader(tc);
            request.Headers["Content-Disposition"] = string.Format(CultureInfo.InvariantCulture, "attachment; filename=\"{0}\"", fileName);
            request.ContentType = contentType ?? "application/octet-stream";

            using (Stream requestStream = request.GetRequestStream())
            {
                byte[] buffer = new byte[8192];
                int bytesRead;
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    requestStream.Write(buffer, 0, bytesRead);
                }
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                string json = reader.ReadToEnd();
                return JObject.Parse(json);
            }
        }

        private T WrapRestCall<T>(string operationName, Func<TransientCredentials, T> action)
        {
            TransientCredentials tc = Login();
            try
            {
                return action(tc);
            }
            catch (WebException ex)
            {
                HttpRequestHelper.LogException(ex);
                HttpWebResponse response = ex.Response as HttpWebResponse;
                if (response != null)
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.Unauthorized:
                        case HttpStatusCode.Forbidden:
                            throw new BlogClientAuthenticationException(response.StatusCode.ToString(), ex.Message, ex);
                        case HttpStatusCode.NotFound:
                            throw new BlogClientPostUrlNotFoundException(_apiBaseUrl, ex.Message);
                        default:
                            // Try to read the error message from the response body
                            string errorMessage = ex.Message;
                            try
                            {
                                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                                {
                                    JObject errorObj = JObject.Parse(reader.ReadToEnd());
                                    string msg = (string)errorObj["message"];
                                    if (!string.IsNullOrEmpty(msg))
                                        errorMessage = msg;
                                }
                            }
                            catch { }
                            throw new BlogClientHttpErrorException(_apiBaseUrl,
                                string.Format(CultureInfo.InvariantCulture, "{0} {1}", (int)response.StatusCode, errorMessage), ex);
                    }
                }
                else
                {
                    throw new BlogClientConnectionErrorException(_apiBaseUrl, ex.Message);
                }
            }
            catch (IOException ex)
            {
                throw new BlogClientIOException(operationName, ex);
            }
        }

        private void WrapRestCall(string operationName, Action<TransientCredentials> action)
        {
            WrapRestCall<object>(operationName, tc => { action(tc); return null; });
        }

        #endregion

        #region Users & Blogs

        public BlogInfo[] GetUsersBlogs()
        {
            return WrapRestCall("GetUsersBlogs", tc =>
            {
                JObject user = RestGetJson("users/me?context=edit", tc);
                string userId = user["id"].ToString();
                string userName = (string)user["name"] ?? (string)user["slug"] ?? "WordPress Blog";

                // Get site info - try the /settings endpoint first, fall back to user info
                string blogName = userName;
                string blogUrl = _apiBaseUrl;
                try
                {
                    // /wp-json/wp/v2/settings requires admin; fall back to site URL from API base
                    JObject settings = RestGetJson("settings", tc);
                    blogName = (string)settings["title"] ?? blogName;
                    blogUrl = (string)settings["url"] ?? blogUrl;
                }
                catch
                {
                    // Non-admin users can't access settings, derive from API URL
                    blogUrl = _apiBaseUrl.Replace("/wp-json/wp/v2/", "/");
                    blogName = userName;
                }

                return new BlogInfo[] { new BlogInfo("1", blogName, blogUrl) };
            });
        }

        public BlogInfo[] GetImageEndpoints()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Categories

        public BlogPostCategory[] GetCategories(string blogId)
        {
            return WrapRestCall("GetCategories", tc =>
            {
                JArray categories = RestGetAllPages("categories", tc);
                List<BlogPostCategory> result = new List<BlogPostCategory>();
                foreach (JObject cat in categories)
                {
                    string id = cat["id"].ToString();
                    string name = (string)cat["name"] ?? "";
                    string parent = cat["parent"] != null ? cat["parent"].ToString() : "0";
                    result.Add(new BlogPostCategory(id, WebUtility.HtmlDecode(name), parent));
                }
                return result.ToArray();
            });
        }

        public string AddCategory(string blogId, BlogPostCategory category)
        {
            return WrapRestCall("AddCategory", tc =>
            {
                JObject body = new JObject();
                body["name"] = category.Name;
                if (!string.IsNullOrEmpty(category.Parent) && category.Parent != "0")
                    body["parent"] = int.Parse(category.Parent, CultureInfo.InvariantCulture);

                JObject result = RestPostJson("categories", tc, body);
                return result["id"].ToString();
            });
        }

        public BlogPostCategory[] SuggestCategories(string blogId, string partialCategoryName)
        {
            return WrapRestCall("SuggestCategories", tc =>
            {
                string encodedSearch = Uri.EscapeDataString(partialCategoryName);
                JArray categories = RestGetJsonArray("categories?search=" + encodedSearch + "&per_page=20", tc);
                List<BlogPostCategory> result = new List<BlogPostCategory>();
                foreach (JObject cat in categories)
                {
                    string id = cat["id"].ToString();
                    string name = (string)cat["name"] ?? "";
                    string parent = cat["parent"] != null ? cat["parent"].ToString() : "0";
                    result.Add(new BlogPostCategory(id, WebUtility.HtmlDecode(name), parent));
                }
                return result.ToArray();
            });
        }

        #endregion

        #region Keywords/Tags

        public BlogPostKeyword[] GetKeywords(string blogId)
        {
            return WrapRestCall("GetKeywords", tc =>
            {
                JArray tags = RestGetAllPages("tags", tc);
                List<BlogPostKeyword> result = new List<BlogPostKeyword>();
                foreach (JObject tag in tags)
                {
                    string name = (string)tag["name"] ?? "";
                    result.Add(new BlogPostKeyword(WebUtility.HtmlDecode(name)));
                }
                return result.ToArray();
            });
        }

        /// <summary>
        /// Resolves keyword names to tag IDs, creating new tags as needed.
        /// </summary>
        private int[] ResolveTagIds(string keywords, TransientCredentials tc)
        {
            if (string.IsNullOrEmpty(keywords))
                return new int[0];

            string[] tagNames = keywords.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            List<int> tagIds = new List<int>();

            foreach (string rawName in tagNames)
            {
                string name = rawName.Trim();
                if (string.IsNullOrEmpty(name))
                    continue;

                // Search for existing tag
                string encodedName = Uri.EscapeDataString(name);
                JArray existing = RestGetJsonArray("tags?search=" + encodedName + "&per_page=100", tc);
                int foundId = -1;
                foreach (JObject tag in existing)
                {
                    string tagName = (string)tag["name"] ?? "";
                    if (string.Equals(WebUtility.HtmlDecode(tagName), name, StringComparison.OrdinalIgnoreCase))
                    {
                        foundId = (int)tag["id"];
                        break;
                    }
                }

                if (foundId >= 0)
                {
                    tagIds.Add(foundId);
                }
                else
                {
                    // Create new tag
                    JObject body = new JObject();
                    body["name"] = name;
                    JObject newTag = RestPostJson("tags", tc, body);
                    tagIds.Add((int)newTag["id"]);
                }
            }

            return tagIds.ToArray();
        }

        /// <summary>
        /// Resolves category names/IDs to category IDs, creating new categories as needed.
        /// </summary>
        private int[] ResolveCategoryIds(BlogPostCategory[] categories, BlogPostCategory[] newCategories, INewCategoryContext newCategoryContext, TransientCredentials tc)
        {
            List<int> categoryIds = new List<int>();

            // Existing categories
            if (categories != null)
            {
                foreach (BlogPostCategory cat in categories)
                {
                    int catId;
                    if (int.TryParse(cat.Id, out catId) && catId > 0)
                    {
                        categoryIds.Add(catId);
                    }
                }
            }

            // New categories
            if (newCategories != null)
            {
                foreach (BlogPostCategory cat in newCategories)
                {
                    JObject body = new JObject();
                    body["name"] = cat.Name;
                    if (!string.IsNullOrEmpty(cat.Parent) && cat.Parent != "0")
                        body["parent"] = int.Parse(cat.Parent, CultureInfo.InvariantCulture);

                    JObject result = RestPostJson("categories", tc, body);
                    string newId = result["id"].ToString();
                    categoryIds.Add(int.Parse(newId, CultureInfo.InvariantCulture));

                    if (newCategoryContext != null)
                    {
                        string parentStr = cat.Parent ?? "0";
                        newCategoryContext.NewCategoryAdded(new BlogPostCategory(newId, cat.Name, parentStr));
                    }
                }
            }

            return categoryIds.ToArray();
        }

        #endregion

        #region Posts

        public BlogPost[] GetRecentPosts(string blogId, int maxPosts, bool includeCategories, DateTime? now)
        {
            return WrapRestCall("GetRecentPosts", tc =>
            {
                string endpoint = string.Format(CultureInfo.InvariantCulture,
                    "posts?context=edit&per_page={0}&orderby=date&order=desc", maxPosts);

                if (now.HasValue)
                    endpoint += "&before=" + Uri.EscapeDataString(now.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture));

                // Include all statuses
                endpoint += "&status=publish,draft,pending,private,future";

                JArray posts = RestGetJsonArray(endpoint, tc);
                List<BlogPost> result = new List<BlogPost>();
                foreach (JObject postJson in posts)
                {
                    result.Add(JsonToBlogPost(postJson, false));
                }
                return result.ToArray();
            });
        }

        public string NewPost(string blogId, BlogPost post, INewCategoryContext newCategoryContext, bool publish, out string etag, out XmlDocument remotePost)
        {
            etag = null;
            remotePost = null;

            return WrapRestCall("NewPost", tc =>
            {
                JObject body = BlogPostToJson(post, publish, tc, newCategoryContext);
                JObject result = RestPostJson("posts", tc, body);
                return result["id"].ToString();
            });
        }

        public bool EditPost(string blogId, BlogPost post, INewCategoryContext newCategoryContext, bool publish, out string etag, out XmlDocument remotePost)
        {
            etag = null;
            remotePost = null;

            return WrapRestCall("EditPost", tc =>
            {
                JObject body = BlogPostToJson(post, publish, tc, newCategoryContext);
                RestPostJson("posts/" + post.Id, tc, body);
                return true;
            });
        }

        public BlogPost GetPost(string blogId, string postId)
        {
            return WrapRestCall("GetPost", tc =>
            {
                JObject postJson = RestGetJson("posts/" + postId + "?context=edit", tc);
                return JsonToBlogPost(postJson, false);
            });
        }

        public void DeletePost(string blogId, string postId, bool publish)
        {
            WrapRestCall("DeletePost", tc =>
            {
                RestDelete("posts/" + postId + "?force=true", tc);
            });
        }

        #endregion

        #region Pages

        public BlogPost GetPage(string blogId, string pageId)
        {
            return WrapRestCall("GetPage", tc =>
            {
                JObject pageJson = RestGetJson("pages/" + pageId + "?context=edit", tc);
                return JsonToBlogPost(pageJson, true);
            });
        }

        public PageInfo[] GetPageList(string blogId)
        {
            return WrapRestCall("GetPageList", tc =>
            {
                JArray pages = RestGetAllPages("pages?context=edit", tc);
                List<PageInfo> result = new List<PageInfo>();
                foreach (JObject page in pages)
                {
                    string id = page["id"].ToString();
                    string title = GetRenderedText(page, "title");
                    DateTime datePublished = ParseRestDate(page, "date_gmt");
                    string parentId = page["parent"] != null ? page["parent"].ToString() : "0";
                    result.Add(new PageInfo(id, title, datePublished, parentId));
                }
                return result.ToArray();
            });
        }

        public BlogPost[] GetPages(string blogId, int maxPages)
        {
            return WrapRestCall("GetPages", tc =>
            {
                string endpoint = string.Format(CultureInfo.InvariantCulture,
                    "pages?context=edit&per_page={0}&status=publish,draft,pending,private,future", maxPages);

                JArray pages = RestGetJsonArray(endpoint, tc);
                List<BlogPost> result = new List<BlogPost>();
                foreach (JObject pageJson in pages)
                {
                    result.Add(JsonToBlogPost(pageJson, true));
                }
                return result.ToArray();
            });
        }

        public string NewPage(string blogId, BlogPost page, bool publish, out string etag, out XmlDocument remotePost)
        {
            etag = null;
            remotePost = null;

            return WrapRestCall("NewPage", tc =>
            {
                JObject body = BlogPostToJson(page, publish, tc, null);
                JObject result = RestPostJson("pages", tc, body);
                return result["id"].ToString();
            });
        }

        public bool EditPage(string blogId, BlogPost page, bool publish, out string etag, out XmlDocument remotePost)
        {
            etag = null;
            remotePost = null;

            return WrapRestCall("EditPage", tc =>
            {
                JObject body = BlogPostToJson(page, publish, tc, null);
                RestPostJson("pages/" + page.Id, tc, body);
                return true;
            });
        }

        public void DeletePage(string blogId, string pageId)
        {
            WrapRestCall("DeletePage", tc =>
            {
                RestDelete("pages/" + pageId + "?force=true", tc);
            });
        }

        #endregion

        #region Authors

        public AuthorInfo[] GetAuthors(string blogId)
        {
            return WrapRestCall("GetAuthors", tc =>
            {
                JArray users = RestGetAllPages("users?who=authors", tc);
                List<AuthorInfo> result = new List<AuthorInfo>();
                foreach (JObject user in users)
                {
                    string id = user["id"].ToString();
                    string name = (string)user["name"] ?? (string)user["slug"] ?? "";
                    result.Add(new AuthorInfo(id, name));
                }
                return result.ToArray();
            });
        }

        #endregion

        #region Media Upload

        public bool? DoesFileNeedUpload(IFileUploadContext uploadContext)
        {
            return null;
        }

        public string DoBeforePublishUploadWork(IFileUploadContext uploadContext)
        {
            return WrapRestCall("UploadMedia", tc =>
            {
                string fileName = uploadContext.PreferredFileName;
                string contentType = MimeHelper.GetContentType(Path.GetExtension(fileName));

                using (Stream fileStream = uploadContext.GetContents())
                {
                    JObject result = RestUploadMedia("media", tc, fileName, contentType, fileStream);
                    string mediaUrl = (string)result["source_url"];
                    return mediaUrl;
                }
            });
        }

        public void DoAfterPublishUploadWork(IFileUploadContext uploadContext)
        {
        }

        #endregion

        #region Authenticated HTTP Request

        public HttpWebResponse SendAuthenticatedHttpRequest(string requestUri, int timeoutMs, HttpRequestFilter filter)
        {
            return BlogClientHelper.SendAuthenticatedHttpRequest(requestUri, filter, CreateCredentialsFilter(requestUri));
        }

        private HttpRequestFilter CreateCredentialsFilter(string requestUri)
        {
            TransientCredentials tc = Login();
            if (tc != null)
                return HttpRequestCredentialsFilter.Create(tc.Username, tc.Password, requestUri, true);
            else
                return null;
        }

        #endregion

        #region JSON <-> BlogPost Conversion

        private JObject BlogPostToJson(BlogPost post, bool publish, TransientCredentials tc, INewCategoryContext newCategoryContext)
        {
            JObject body = new JObject();

            // Title
            body["title"] = post.Title ?? "";

            // Content - combine main + extended
            string content = post.Contents ?? "";
            body["content"] = content;

            // Excerpt
            if (!string.IsNullOrEmpty(post.Excerpt))
                body["excerpt"] = post.Excerpt;

            // Status
            body["status"] = publish ? "publish" : "draft";

            // Slug
            if (!string.IsNullOrEmpty(post.Slug))
                body["slug"] = post.Slug;

            // Password
            if (!string.IsNullOrEmpty(post.Password))
                body["password"] = post.Password;

            // Author
            if (!post.Author.IsEmpty)
            {
                int authorId;
                if (int.TryParse(post.Author.Id, out authorId))
                    body["author"] = authorId;
            }

            // Date
            if (post.HasDatePublishedOverride)
            {
                body["date_gmt"] = post.DatePublishedOverride.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
            }
            else if (post.DatePublished != DateTime.MinValue)
            {
                body["date_gmt"] = post.DatePublished.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
            }

            // Comment status
            if (post.CommentPolicy != BlogCommentPolicy.Unspecified)
            {
                body["comment_status"] = (post.CommentPolicy == BlogCommentPolicy.Open) ? "open" : "closed";
            }

            // Ping status
            if (post.TrackbackPolicy != BlogTrackbackPolicy.Unspecified)
            {
                body["ping_status"] = (post.TrackbackPolicy == BlogTrackbackPolicy.Allow) ? "open" : "closed";
            }

            if (!post.IsPage)
            {
                // Categories
                int[] categoryIds = ResolveCategoryIds(post.Categories, post.NewCategories, newCategoryContext, tc);
                if (categoryIds.Length > 0)
                    body["categories"] = new JArray(categoryIds);

                // Tags
                int[] tagIds = ResolveTagIds(post.Keywords, tc);
                if (tagIds.Length > 0)
                    body["tags"] = new JArray(tagIds);
            }
            else
            {
                // Page parent
                if (!post.PageParent.IsEmpty)
                {
                    int parentId;
                    if (int.TryParse(post.PageParent.Id, out parentId))
                        body["parent"] = parentId;
                }

                // Page order
                if (!string.IsNullOrEmpty(post.PageOrder))
                {
                    int order;
                    if (int.TryParse(post.PageOrder, out order))
                        body["menu_order"] = order;
                }
            }

            return body;
        }

        private BlogPost JsonToBlogPost(JObject postJson, bool isPage)
        {
            BlogPost post = new BlogPost();

            post.Id = postJson["id"].ToString();
            post.IsPage = isPage;

            // Title
            post.Title = GetRawText(postJson, "title");

            // Content
            string content = GetRawText(postJson, "content");
            post.Contents = content;

            // Excerpt
            post.Excerpt = GetRawText(postJson, "excerpt");

            // Permalink
            post.Permalink = (string)postJson["link"] ?? "";

            // Date
            JToken dateGmt = postJson["date_gmt"];
            if (dateGmt != null && dateGmt.Type != JTokenType.Null)
            {
                string dateStr = (string)dateGmt;
                if (!string.IsNullOrEmpty(dateStr))
                {
                    DateTime dt;
                    if (DateTime.TryParseExact(dateStr, "yyyy-MM-ddTHH:mm:ss",
                        CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out dt))
                    {
                        post.DatePublished = dt;
                    }
                }
            }

            // Slug
            post.Slug = (string)postJson["slug"] ?? "";

            // Password
            post.Password = (string)postJson["password"] ?? "";

            // Author
            JToken authorToken = postJson["author"];
            if (authorToken != null && authorToken.Type != JTokenType.Null)
            {
                post.Author = new PostIdAndNameField(authorToken.ToString(), authorToken.ToString());
            }

            // Comment status
            string commentStatus = (string)postJson["comment_status"];
            if (commentStatus == "open")
                post.CommentPolicy = BlogCommentPolicy.Open;
            else if (commentStatus == "closed")
                post.CommentPolicy = BlogCommentPolicy.Closed;

            // Ping status
            string pingStatus = (string)postJson["ping_status"];
            if (pingStatus == "open")
                post.TrackbackPolicy = BlogTrackbackPolicy.Allow;
            else if (pingStatus == "closed")
                post.TrackbackPolicy = BlogTrackbackPolicy.Deny;

            if (!isPage)
            {
                // Categories
                JToken categoriesToken = postJson["categories"];
                if (categoriesToken != null && categoriesToken.Type == JTokenType.Array)
                {
                    List<BlogPostCategory> categories = new List<BlogPostCategory>();
                    foreach (JToken catId in categoriesToken)
                    {
                        categories.Add(new BlogPostCategory(catId.ToString(), catId.ToString()));
                    }
                    post.Categories = categories.ToArray();
                }

                // Tags -> Keywords
                JToken tagsToken = postJson["tags"];
                if (tagsToken != null && tagsToken.Type == JTokenType.Array)
                {
                    List<string> tagNames = new List<string>();
                    foreach (JToken tagId in tagsToken)
                    {
                        tagNames.Add(tagId.ToString());
                    }
                    post.Keywords = string.Join(",", tagNames.ToArray());
                }
            }
            else
            {
                // Page parent
                JToken parentToken = postJson["parent"];
                if (parentToken != null && parentToken.Type != JTokenType.Null)
                {
                    string parentId = parentToken.ToString();
                    if (parentId != "0")
                        post.PageParent = new PostIdAndNameField(parentId, parentId);
                }

                // Page order
                JToken menuOrderToken = postJson["menu_order"];
                if (menuOrderToken != null && menuOrderToken.Type != JTokenType.Null)
                {
                    post.PageOrder = menuOrderToken.ToString();
                }
            }

            return post;
        }

        private string GetRawText(JObject json, string field)
        {
            JToken token = json[field];
            if (token == null || token.Type == JTokenType.Null)
                return "";

            // REST API returns { "raw": "...", "rendered": "..." } in edit context
            if (token.Type == JTokenType.Object)
            {
                string raw = (string)token["raw"];
                return raw ?? "";
            }

            return (string)token ?? "";
        }

        private string GetRenderedText(JObject json, string field)
        {
            JToken token = json[field];
            if (token == null || token.Type == JTokenType.Null)
                return "";

            if (token.Type == JTokenType.Object)
            {
                string rendered = (string)token["rendered"];
                return WebUtility.HtmlDecode(rendered ?? "");
            }

            return WebUtility.HtmlDecode((string)token ?? "");
        }

        private DateTime ParseRestDate(JObject json, string field)
        {
            JToken token = json[field];
            if (token == null || token.Type == JTokenType.Null)
                return DateTime.MinValue;

            string dateStr = (string)token;
            if (string.IsNullOrEmpty(dateStr))
                return DateTime.MinValue;

            DateTime dt;
            if (DateTime.TryParseExact(dateStr, "yyyy-MM-ddTHH:mm:ss",
                CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out dt))
            {
                return dt;
            }

            return DateTime.MinValue;
        }

        #endregion
    }
}
