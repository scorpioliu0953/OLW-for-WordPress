// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using OpenLiveWriter.CoreServices;
using OpenLiveWriter.BlogClient;
using OpenLiveWriter.BlogClient.Detection;
using OpenLiveWriter.CoreServices.Layout;
using OpenLiveWriter.Localization;
using OpenLiveWriter.CoreServices.Marketization;
using OpenLiveWriter.PostEditor.PostHtmlEditing;
using OpenLiveWriter.PostEditor.BlogProviderButtons;

namespace OpenLiveWriter.PostEditor.Configuration.Wizard
{

    internal class WeblogConfigurationWizardPanelBlogType : WeblogConfigurationWizardPanel
    {
        private System.Windows.Forms.Label labelWelcomeText;
        private LinkLabel privacyPolicyLabel;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public WeblogConfigurationWizardPanelBlogType()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            labelHeader.Text = Res.Get(StringId.WizardBlogTypeWhatBlogType);
            labelWelcomeText.Text = Res.Get(StringId.WizardBlogTypeWelcome);

            labelWelcomeText.Text = string.Format(CultureInfo.CurrentCulture, labelWelcomeText.Text, ApplicationEnvironment.ProductNameQualified);
        }

        public override void NaturalizeLayout()
        {
            if (!DesignMode)
            {
                MaximizeWidth(labelWelcomeText);
                LayoutHelper.NaturalizeHeight(labelWelcomeText);
            }
        }

        public override ConfigPanelId? PanelId
        {
            get { return ConfigPanelId.BlogType; }
        }

        public void OnDisplayPanel()
        {
            _userChangedSelection = false;
        }

        public bool UserChangedSelection
        {
            get
            {
                return _userChangedSelection;
            }
        }
        private bool _userChangedSelection;

        // WordPress-only: always false
        public bool IsSharePointBlog
        {
            get { return false; }
        }

        // WordPress-only: always false
        public bool IsGoogleBloggerBlog
        {
            get { return false; }
        }

        public IBlogProviderAccountWizardDescription ProviderAccountWizard
        {
            get { return null; }
        }

        private void UserChangedSelectionHandler(object sender, EventArgs ea)
        {
            _userChangedSelection = true;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelWelcomeText = new System.Windows.Forms.Label();
            this.privacyPolicyLabel = new System.Windows.Forms.LinkLabel();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            //
            // panelMain
            //
            this.panelMain.Controls.Add(this.privacyPolicyLabel);
            this.panelMain.Controls.Add(this.labelWelcomeText);
            //
            // labelWelcomeText
            //
            this.labelWelcomeText.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.labelWelcomeText.Location = new System.Drawing.Point(20, 0);
            this.labelWelcomeText.Name = "labelWelcomeText";
            this.labelWelcomeText.Size = new System.Drawing.Size(332, 40);
            this.labelWelcomeText.TabIndex = 2;
            this.labelWelcomeText.Text = "{0} connects to your WordPress blog using the REST API.";
            //
            // privacyPolicyLabel
            //
            this.privacyPolicyLabel.AutoSize = true;
            this.privacyPolicyLabel.Location = new System.Drawing.Point(23, 167);
            this.privacyPolicyLabel.Name = "privacyPolicyLabel";
            this.privacyPolicyLabel.Size = new System.Drawing.Size(256, 13);
            this.privacyPolicyLabel.TabIndex = 6;
            this.privacyPolicyLabel.TabStop = true;
            this.privacyPolicyLabel.Text = "We follow the privacy policy of the .NET Foundation.";
            this.privacyPolicyLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.privacyPolicyLabel_LinkClicked);
            //
            // WeblogConfigurationWizardPanelBlogType
            //
            this.Name = "WeblogConfigurationWizardPanelBlogType";
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private void privacyPolicyLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShellHelper.LaunchUrl("http://www.dotnetfoundation.org/privacy-policy");
        }
    }
}
