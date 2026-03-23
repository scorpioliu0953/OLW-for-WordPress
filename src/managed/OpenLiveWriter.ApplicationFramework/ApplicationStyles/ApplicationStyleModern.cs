// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for details.

using System.ComponentModel;
using System.Drawing;
using OpenLiveWriter.CoreServices;

namespace OpenLiveWriter.ApplicationFramework.ApplicationStyles
{
    /// <summary>
    /// Editorial warm style - cream and amber tones for a publishing-focused feel.
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

        public override Font NormalApplicationFont
        {
            get { return new Font("Segoe UI", 9f, FontStyle.Regular, GraphicsUnit.Point); }
        }

        #region Component Designer generated code
        private void InitializeComponent()
        {
            // Editorial warm palette - cream, beige, amber
            this.ActiveSelectionColor = Color.FromArgb(180, 83, 9);          // amber-700
            this.ActiveTabBottomColor = Color.FromArgb(254, 243, 199);       // amber-100
            this.ActiveTabHighlightColor = Color.FromArgb(255, 255, 255);    // white
            this.ActiveTabLowlightColor = Color.FromArgb(253, 230, 138);     // amber-200
            this.ActiveTabTextColor = Color.FromArgb(92, 45, 1);             // amber-900
            this.ActiveTabTopColor = Color.FromArgb(254, 251, 235);          // amber-50
            this.AlertControlColor = Color.FromArgb(254, 243, 199);          // amber-100
            this.BorderColor = Color.FromArgb(212, 196, 176);                // warm border

            this.DisplayName = "Modern";

            this.InactiveSelectionColor = Color.FromArgb(245, 237, 224);     // warm beige
            this.InactiveTabBottomColor = Color.FromArgb(253, 246, 236);     // warm cream
            this.InactiveTabHighlightColor = Color.FromArgb(255, 255, 255);
            this.InactiveTabLowlightColor = Color.FromArgb(237, 228, 215);   // panel
            this.InactiveTabTextColor = Color.FromArgb(120, 113, 108);       // stone-500
            this.InactiveTabTopColor = Color.FromArgb(245, 237, 224);        // warm beige

            this.MenuBitmapAreaColor = Color.FromArgb(237, 228, 215);        // panel
            this.MenuSelectionColor = Color.FromArgb(100, 180, 83, 9);       // amber semi-transparent

            // Primary workspace (toolbar / chrome area)
            this.PrimaryWorkspaceBottomColor = Color.FromArgb(237, 228, 215);                  // panel
            this.PrimaryWorkspaceCommandBarBottomBevelFirstLineColor = Color.FromArgb(212, 196, 176);  // warm border
            this.PrimaryWorkspaceCommandBarBottomBevelSecondLineColor = Color.FromArgb(237, 228, 215); // panel
            this.PrimaryWorkspaceCommandBarBottomColor = Color.FromArgb(245, 237, 224);        // beige
            this.PrimaryWorkspaceCommandBarBottomLayoutMargin = 3;
            this.PrimaryWorkspaceCommandBarDisabledTextColor = Color.FromArgb(188, 175, 160);  // muted warm
            this.PrimaryWorkspaceCommandBarLeftLayoutMargin = 2;
            this.PrimaryWorkspaceCommandBarRightLayoutMargin = 2;
            this.PrimaryWorkspaceCommandBarSeparatorLayoutMargin = 2;
            this.PrimaryWorkspaceCommandBarTextColor = Color.FromArgb(28, 25, 23);             // stone-900
            this.PrimaryWorkspaceCommandBarTopBevelFirstLineColor = Color.Transparent;
            this.PrimaryWorkspaceCommandBarTopBevelSecondLineColor = Color.Transparent;
            this.PrimaryWorkspaceCommandBarTopColor = Color.FromArgb(253, 246, 236);           // cream
            this.PrimaryWorkspaceCommandBarTopLayoutMargin = 2;
            this.PrimaryWorkspaceTopColor = Color.FromArgb(245, 237, 224);                     // beige

            // Secondary workspace
            this.SecondaryWorkspaceBottomColor = Color.FromArgb(253, 246, 236);    // cream
            this.SecondaryWorkspaceTopColor = Color.FromArgb(253, 246, 236);       // cream

            // Tool windows (floating panels)
            this.ToolWindowBackgroundColor = Color.FromArgb(180, 83, 9);      // amber-700
            this.ToolWindowBorderColor = Color.FromArgb(146, 64, 14);         // amber-800
            this.ToolWindowTitleBarBottomColor = Color.FromArgb(180, 83, 9);  // amber-700
            this.ToolWindowTitleBarTextColor = Color.White;
            this.ToolWindowTitleBarTopColor = Color.FromArgb(217, 119, 6);    // amber-600

            this.WindowColor = Color.FromArgb(255, 251, 242);                 // light cream (editor bg)
            this.WorkspacePaneControlColor = Color.FromArgb(245, 237, 224);   // beige
        }
        #endregion

        public override Image PreviewImage
        {
            get
            {
                return ResourceHelper.LoadAssemblyResourceBitmap("ApplicationStyles.Images.SkyBlue.png");
            }
        }
    }
}
