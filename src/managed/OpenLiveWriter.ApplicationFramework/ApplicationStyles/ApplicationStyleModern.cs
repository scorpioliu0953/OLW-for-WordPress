// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.

using System.ComponentModel;
using System.Drawing;
using OpenLiveWriter.CoreServices;

namespace OpenLiveWriter.ApplicationFramework.ApplicationStyles
{
    /// <summary>
    /// Modern clean application style with softer colors and improved readability.
    /// </summary>
    public class ApplicationStyleModern : ApplicationStyle
    {
        private Container components = null;

        public ApplicationStyleModern()
        {
            InitializeComponent();
        }

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
        private void InitializeComponent()
        {
            // Modern clean style - softer blues with better contrast
            this.ActiveSelectionColor = Color.FromArgb(59, 130, 246);       // Tailwind blue-500
            this.ActiveTabBottomColor = Color.FromArgb(239, 246, 255);      // blue-50
            this.ActiveTabHighlightColor = Color.FromArgb(255, 255, 255);   // white
            this.ActiveTabLowlightColor = Color.FromArgb(191, 219, 254);    // blue-200
            this.ActiveTabTextColor = Color.FromArgb(30, 64, 175);          // blue-800
            this.ActiveTabTopColor = Color.FromArgb(219, 234, 254);         // blue-100
            this.AlertControlColor = Color.FromArgb(254, 252, 232);         // yellow-50
            this.BorderColor = Color.FromArgb(203, 213, 225);               // slate-300

            this.DisplayName = "Modern";

            this.InactiveSelectionColor = Color.FromArgb(241, 245, 249);    // slate-100
            this.InactiveTabBottomColor = Color.FromArgb(248, 250, 252);    // slate-50
            this.InactiveTabHighlightColor = Color.FromArgb(255, 255, 255);
            this.InactiveTabLowlightColor = Color.FromArgb(226, 232, 240);  // slate-200
            this.InactiveTabTextColor = Color.FromArgb(71, 85, 105);        // slate-600
            this.InactiveTabTopColor = Color.FromArgb(241, 245, 249);       // slate-100

            this.MenuBitmapAreaColor = Color.FromArgb(241, 245, 249);       // slate-100
            this.MenuSelectionColor = Color.FromArgb(128, 59, 130, 246);    // blue-500 semi-transparent

            // Primary workspace (main area gradients)
            this.PrimaryWorkspaceBottomColor = Color.FromArgb(226, 232, 240);    // slate-200
            this.PrimaryWorkspaceCommandBarBottomBevelFirstLineColor = Color.FromArgb(203, 213, 225); // slate-300
            this.PrimaryWorkspaceCommandBarBottomBevelSecondLineColor = Color.FromArgb(226, 232, 240); // slate-200
            this.PrimaryWorkspaceCommandBarBottomColor = Color.FromArgb(241, 245, 249); // slate-100
            this.PrimaryWorkspaceCommandBarBottomLayoutMargin = 3;
            this.PrimaryWorkspaceCommandBarDisabledTextColor = Color.FromArgb(148, 163, 184); // slate-400
            this.PrimaryWorkspaceCommandBarLeftLayoutMargin = 2;
            this.PrimaryWorkspaceCommandBarRightLayoutMargin = 2;
            this.PrimaryWorkspaceCommandBarSeparatorLayoutMargin = 2;
            this.PrimaryWorkspaceCommandBarTextColor = Color.FromArgb(30, 41, 59); // slate-800
            this.PrimaryWorkspaceCommandBarTopBevelFirstLineColor = Color.Transparent;
            this.PrimaryWorkspaceCommandBarTopBevelSecondLineColor = Color.Transparent;
            this.PrimaryWorkspaceCommandBarTopColor = Color.FromArgb(248, 250, 252); // slate-50
            this.PrimaryWorkspaceCommandBarTopLayoutMargin = 2;
            this.PrimaryWorkspaceTopColor = Color.FromArgb(241, 245, 249);  // slate-100

            // Secondary workspace
            this.SecondaryWorkspaceBottomColor = Color.FromArgb(248, 250, 252); // slate-50
            this.SecondaryWorkspaceTopColor = Color.FromArgb(248, 250, 252);

            // Tool windows
            this.ToolWindowBackgroundColor = Color.FromArgb(37, 99, 235);    // blue-600
            this.ToolWindowBorderColor = Color.FromArgb(29, 78, 216);        // blue-700
            this.ToolWindowTitleBarBottomColor = Color.FromArgb(37, 99, 235); // blue-600
            this.ToolWindowTitleBarTextColor = Color.White;
            this.ToolWindowTitleBarTopColor = Color.FromArgb(59, 130, 246);   // blue-500

            this.WindowColor = Color.White;
            this.WorkspacePaneControlColor = Color.FromArgb(241, 245, 249);   // slate-100
        }
        #endregion

        public override Image PreviewImage
        {
            get
            {
                // Reuse SkyBlue preview for now
                return ResourceHelper.LoadAssemblyResourceBitmap("ApplicationStyles.Images.SkyBlue.png");
            }
        }
    }
}
