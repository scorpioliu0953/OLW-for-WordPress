// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using OpenLiveWriter.BlogClient;
using OpenLiveWriter.BlogClient.Clients;
using OpenLiveWriter.Controls;
using OpenLiveWriter.CoreServices;
using OpenLiveWriter.Localization;

namespace OpenLiveWriter.PostEditor.ContentSources.Common
{
    public interface IAuth
    {
        bool IsLoggedIn { get; }

        string Username { get; }
        string AuthToken { get; }

        bool AllowSavePassword { get; }
        bool PasswordRequired(string username);

        bool Login(bool showUI, IWin32Window parent);
        bool Login(string userName, string passWord, bool savePassword, bool ignoreSavedPassword, IWin32Window parent);
        void Logout();

        event EventHandler LoginStatusChanged;

        Bitmap LoginLogo { get; }
        string LoginText { get; }
        string LoginUsernameLabel { get; }
        string LoginPasswordLabel { get; }
        string LoginExampleText { get; }
        string LoginSavePasswordText { get; }
        string ServiceUrl { get; }

    }

    #region YouTubeAuth : IAuth
    // YouTube authentication via GData API is no longer supported.
    // This stub preserves the interface for any remaining references.
    public class YouTubeAuth : IAuth
    {
        [ThreadStatic]
        private static YouTubeAuth _this;

        private YouTubeAuth() { }

        public static YouTubeAuth Instance
        {
            get
            {
                if (_this == null)
                    _this = new YouTubeAuth();
                return _this;
            }
        }

        public string Username { get { return string.Empty; } }
        public string AuthToken { get { return string.Empty; } }
        public bool AllowSavePassword { get { return false; } }
        public bool PasswordRequired(string username) { return false; }
        public bool IsLoggedIn { get { return false; } }

        public bool Login(string username, string password, bool savePassword, bool ignoreSavedPassword, IWin32Window parent) { return false; }
        public bool Login(bool showUI, IWin32Window parent) { return false; }
        public void Logout() { OnLoginStatusChanged(); }

        public event EventHandler LoginStatusChanged;

        public Bitmap LoginLogo { get { return null; } }
        public string LoginText { get { return ""; } }
        public string LoginUsernameLabel { get { return Res.Get(StringId.UsernameLabel); } }
        public string LoginPasswordLabel { get { return Res.Get(StringId.PasswordLabel); } }
        public string LoginExampleText { get { return ""; } }
        public string LoginSavePasswordText { get { return Res.Get(StringId.RememberPassword); } }
        public string ServiceUrl { get { return "http://www.youtube.com"; } }

        protected virtual void OnLoginStatusChanged()
        {
            if (LoginStatusChanged != null)
                LoginStatusChanged(this, EventArgs.Empty);
        }
    }
    #endregion

}
