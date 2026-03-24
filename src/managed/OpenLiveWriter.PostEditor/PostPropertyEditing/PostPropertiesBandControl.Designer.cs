using System.Drawing;
using System.Windows.Forms;
using OpenLiveWriter.Controls;
using OpenLiveWriter.PostEditor.PostPropertyEditing.CategoryControl;

namespace OpenLiveWriter.PostEditor.PostPropertyEditing
{
    partial class PostPropertiesBandControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.categoryDropDown = new CategoryDropDownControlM1();
            this.textTags = new AutoCompleteTextbox();
            this.datePublishDate = new PublishDateTimePicker();
            this.labelPageOrder = new Label();
            this.labelPageParent = new Label();
            this.comboPageParent = new PageParentComboBox();
            this.textPageOrder = new NumericTextBox();
            this.linkViewAll = new LinkLabel();
            this.buttonPublish = new Button();
            this.buttonDraft = new Button();
            this.table = new TableLayoutPanel();
            this.panelShadow = new Panel();
            this.panelPublish = new Panel();
            this.panelCategories = new Panel();
            this.panelTags = new Panel();
            this.labelPublish = new Label();
            this.labelCategories = new Label();
            this.labelTags = new Label();
            this.labelDate = new Label();

            this.SuspendLayout();

            var cardBg = Color.White;
            var sidebarBg = Color.FromArgb(243, 244, 246);
            var headingColor = Color.FromArgb(31, 41, 55);
            var subtextColor = Color.FromArgb(107, 114, 128);
            var accentBlue = Color.FromArgb(37, 99, 235);
            var borderColor = Color.FromArgb(209, 213, 219);
            var headingFont = new Font("Segoe UI", 11F, FontStyle.Bold);
            var bodyFont = new Font("Segoe UI", 9.5F);
            var smallFont = new Font("Segoe UI", 8.5F);

            // ═══════ 發佈卡片 ═══════
            this.panelPublish.BackColor = cardBg;
            this.panelPublish.Padding = new Padding(14, 12, 14, 14);
            this.panelPublish.Dock = DockStyle.Top;
            this.panelPublish.Height = 180;

            this.labelPublish.Text = "發佈";
            this.labelPublish.Font = headingFont;
            this.labelPublish.ForeColor = headingColor;
            this.labelPublish.Dock = DockStyle.Top;
            this.labelPublish.Height = 30;

            this.buttonPublish.BackColor = accentBlue;
            this.buttonPublish.Dock = DockStyle.Top;
            this.buttonPublish.FlatStyle = FlatStyle.Flat;
            this.buttonPublish.FlatAppearance.BorderSize = 0;
            this.buttonPublish.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.buttonPublish.ForeColor = Color.White;
            this.buttonPublish.Height = 38;
            this.buttonPublish.Name = "buttonPublish";
            this.buttonPublish.Text = "  \u2191  發佈文章";
            this.buttonPublish.TextAlign = ContentAlignment.MiddleCenter;
            this.buttonPublish.UseVisualStyleBackColor = false;
            this.buttonPublish.Cursor = Cursors.Hand;

            this.buttonDraft.BackColor = cardBg;
            this.buttonDraft.Dock = DockStyle.Top;
            this.buttonDraft.FlatStyle = FlatStyle.Flat;
            this.buttonDraft.FlatAppearance.BorderColor = borderColor;
            this.buttonDraft.FlatAppearance.BorderSize = 1;
            this.buttonDraft.Font = bodyFont;
            this.buttonDraft.ForeColor = Color.FromArgb(55, 65, 81);
            this.buttonDraft.Height = 34;
            this.buttonDraft.Name = "buttonDraft";
            this.buttonDraft.Text = "  \u270E  儲存草稿";
            this.buttonDraft.TextAlign = ContentAlignment.MiddleCenter;
            this.buttonDraft.UseVisualStyleBackColor = false;
            this.buttonDraft.Cursor = Cursors.Hand;
            this.buttonDraft.Margin = new Padding(0, 6, 0, 0);

            this.labelDate.Text = "發佈日期";
            this.labelDate.Font = smallFont;
            this.labelDate.ForeColor = subtextColor;
            this.labelDate.Dock = DockStyle.Top;
            this.labelDate.Height = 26;
            this.labelDate.Padding = new Padding(0, 10, 0, 2);

            this.datePublishDate.Dock = DockStyle.Top;
            this.datePublishDate.Format = DateTimePickerFormat.Custom;
            this.datePublishDate.CustomFormat = "yyyy/MM/dd  HH:mm";
            this.datePublishDate.Font = bodyFont;
            this.datePublishDate.Name = "datePublishDate";
            this.datePublishDate.ShowCheckBox = true;
            this.datePublishDate.Height = 28;

            this.panelPublish.Controls.Add(this.datePublishDate);
            this.panelPublish.Controls.Add(this.labelDate);
            this.panelPublish.Controls.Add(this.buttonDraft);
            this.panelPublish.Controls.Add(this.buttonPublish);
            this.panelPublish.Controls.Add(this.labelPublish);

            // ═══════ 分類卡片 ═══════
            this.panelCategories.BackColor = cardBg;
            this.panelCategories.Padding = new Padding(14, 12, 14, 14);
            this.panelCategories.Dock = DockStyle.Top;
            this.panelCategories.Height = 100;

            this.labelCategories.Text = "分類";
            this.labelCategories.Font = headingFont;
            this.labelCategories.ForeColor = headingColor;
            this.labelCategories.Dock = DockStyle.Top;
            this.labelCategories.Height = 30;

            this.categoryDropDown.Dock = DockStyle.Top;
            this.categoryDropDown.DrawMode = DrawMode.OwnerDrawFixed;
            this.categoryDropDown.DropDownStyle = ComboBoxStyle.DropDownList;
            this.categoryDropDown.FormattingEnabled = true;
            this.categoryDropDown.IntegralHeight = false;
            this.categoryDropDown.Items.AddRange(new object[] { "", "", "" });
            this.categoryDropDown.Name = "categoryDropDown";
            this.categoryDropDown.ItemHeight = 28;
            this.categoryDropDown.Height = 32;
            this.categoryDropDown.Font = bodyFont;
            this.categoryDropDown.TabIndex = 0;

            this.panelCategories.Controls.Add(this.categoryDropDown);
            this.panelCategories.Controls.Add(this.labelCategories);

            // ═══════ 標籤卡片 ═══════
            this.panelTags.BackColor = cardBg;
            this.panelTags.Padding = new Padding(14, 12, 14, 14);
            this.panelTags.Dock = DockStyle.Top;
            this.panelTags.Height = 100;

            this.labelTags.Text = "標籤";
            this.labelTags.Font = headingFont;
            this.labelTags.ForeColor = headingColor;
            this.labelTags.Dock = DockStyle.Top;
            this.labelTags.Height = 30;

            this.textTags.Dock = DockStyle.Top;
            this.textTags.DefaultText = null;
            this.textTags.Name = "textTags";
            this.textTags.ShowButton = true;
            this.textTags.Height = 28;
            this.textTags.Font = bodyFont;
            this.textTags.TabIndex = 1;

            this.panelTags.Controls.Add(this.textTags);
            this.panelTags.Controls.Add(this.labelTags);

            // ═══════ 隱藏控件 ═══════
            this.labelPageOrder.Visible = false;
            this.labelPageOrder.Name = "labelPageOrder";
            this.textPageOrder.Visible = false;
            this.textPageOrder.Name = "textPageOrder";
            this.labelPageParent.Visible = false;
            this.labelPageParent.Name = "labelPageParent";
            this.comboPageParent.Visible = false;
            this.comboPageParent.Name = "comboPageParent";

            // ═══════ 檢視全部連結 ═══════
            this.linkViewAll.LinkBehavior = LinkBehavior.HoverUnderline;
            this.linkViewAll.ActiveLinkColor = accentBlue;
            this.linkViewAll.LinkColor = accentBlue;
            this.linkViewAll.VisitedLinkColor = accentBlue;
            this.linkViewAll.Dock = DockStyle.Top;
            this.linkViewAll.Font = smallFont;
            this.linkViewAll.Height = 28;
            this.linkViewAll.Padding = new Padding(6, 6, 0, 0);
            this.linkViewAll.Name = "linkViewAll";
            this.linkViewAll.TabStop = true;
            this.linkViewAll.Text = "檢視全部屬性...";
            this.linkViewAll.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkViewAll_LinkClicked);

            // ═══════ 左邊框線 ═══════
            this.panelShadow.BackColor = Color.FromArgb(229, 231, 235);
            this.panelShadow.Dock = DockStyle.Left;
            this.panelShadow.Width = 1;
            this.panelShadow.Name = "panelShadow";

            // ═══════ 相容用隱藏 table ═══════
            this.table = new TableLayoutPanel();
            this.table.Visible = false;
            this.table.Size = new Size(0, 0);

            // ═══════ 主控件 ═══════
            this.AutoScaleMode = AutoScaleMode.Inherit;
            this.BackColor = sidebarBg;
            this.Padding = new Padding(1, 8, 8, 8);

            this.Controls.Add(this.linkViewAll);
            this.Controls.Add(this.panelTags);
            this.Controls.Add(this.panelCategories);
            this.Controls.Add(this.panelPublish);
            this.Controls.Add(this.panelShadow);

            this.Name = "PostPropertiesBandControl";
            this.Size = new Size(280, 600);
            this.ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel table;
        private CategoryDropDownControlM1 categoryDropDown;
        private AutoCompleteTextbox textTags;
        private Label labelPageOrder;
        private PublishDateTimePicker datePublishDate;
        private Label labelPageParent;
        private PageParentComboBox comboPageParent;
        private NumericTextBox textPageOrder;
        private Panel panelShadow;
        private LinkLabel linkViewAll;
        private Button buttonPublish;
        private Button buttonDraft;
        private Panel panelPublish;
        private Panel panelCategories;
        private Panel panelTags;
        private Label labelPublish;
        private Label labelCategories;
        private Label labelTags;
        private Label labelDate;
    }
}
