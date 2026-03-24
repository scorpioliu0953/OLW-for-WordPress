using System;
using System.Drawing;
using System.IO;
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

        private static Bitmap BitmapFromBase64(string b64)
        {
            byte[] bytes = Convert.FromBase64String(b64);
            using (var ms = new MemoryStream(bytes))
                return new Bitmap(ms);
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
            this.panelButtons = new TableLayoutPanel();

            this.SuspendLayout();

            var cardBg = Color.White;
            var sidebarBg = Color.FromArgb(243, 244, 246);
            var headingColor = Color.FromArgb(31, 41, 55);
            var subtextColor = Color.FromArgb(107, 114, 128);
            var accentBlue = Color.FromArgb(37, 99, 235);
            var borderColor = Color.FromArgb(209, 213, 219);
            var fieldBorder = Color.FromArgb(209, 213, 219);
            var headingFont = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            var bodyFont = new Font("Segoe UI", 9.5F);
            var smallFont = new Font("Segoe UI", 8.5F);

            // Icons
            var icoPublish = BitmapFromBase64("iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAtUlEQVR4nO2SMQoCMRBFZxbBa3gNyy2s7AVvI3idvYGFeAdrtdBaFLawkCeDCS6yhAR3kYU8GDJMZv7PQEQygwGYAnfgZnlf4p7uTPiI18DVRd2JCTAGLk6wBPYuSlc7W09IYxS6VNUHsHynugN8fQvMReRpPT9t0cRvIAkU0jPFXwyABbCKFbFem4l2BSr77C31mUVL3aiSf9E3qrpJ6Q8ZnPzLJJ5DisHanZNI8WNjJpMZGi8AoagXEwnwVAAAAABJRU5ErkJggg==");
            var icoDraft = BitmapFromBase64("iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAABG0lEQVR4nO2Uv0oDQRCHP0Vbax8gWKVR0a0ibCf4BxYtfQcRmzyBtZhXsI0DihAsXLSRJaiFYCG+gK0voJyschybu3FJE8gHw7AzzPzmbmBgSgMzqaCxbhtYHZWvcB28DNUCxrod4BI9n8Bm8PKQSs4lYivRO+C9ofkScA4MjHVJkdmaryqav9TYXvByARS/cz6KrGkE/kXwcgscAAvA1tgFIm+MILWDMvs1udeG2maB4KWvaZItUMZYdwh04/MkeOlp6lQ7MNZtAKfAYrQzY11nbALAujKWLTBUxvIEgpc74Bj4iHYUvNznLvkr+paxxbX44ybaD8a6dinXqtTWCjxHL5oJKzxqz/UusKw817+TPwUvVxlDTZl0vgEp+UdY47pzFQAAAABJRU5ErkJggg==");
            var icoSecPub = BitmapFromBase64("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAqUlEQVR4nO2QoQ0CQRBFHxdaQGHOQwM4xJSAwyCgBiwnEXSApQdIvsDRAC2cOw+KkEn2QnKwy0oEP1kxf/78+bPw0zCzib+UpkgNAyfgmDIpvgzfgHvKpB8xXgI7YBDqJnCX3BNWkqq2kLRxLvsESY8c7s3AzEZmVkZSeb90TeoPKuAKeGTHvtNfAGNg1hK9zoYDMATOkRBToJY0jyXYAuuw5RPqoPmDF57cmSpsqAeZ/QAAAABJRU5ErkJggg==");
            var icoSecCat = BitmapFromBase64("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAoklEQVR4nN2SMQ4BQRSGP2MTh3ADodFqX8QBZC/gEOh0zqGVPcJ/BS2VwjEkVmTkbbI7CkIhfNV7/8x8mZk8+DatqjCzMTAD2h5dgLmk06uCHXAE9h4NgQGwSc6UQCHpEJusttABtpIKFwZgDfQTQReYAKNU0EDSNT6hmd7FU2BV9YEPCb8vyGr1GcjNLP31lJ7vfRAsfZCeCeIcLN67L3/JDbW4H/tl6sBpAAAAAElFTkSuQmCC");
            var icoSecTag = BitmapFromBase64("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAvUlEQVR4nOWSoQoCURREz+qC4H/YDBosRhn8BIvRbjGZxGTzHwyCiv7BZIvVZvEHtIpFRVlhWR4smwwOPBi49x2G4cKvFX2NpC4wAMqBvQewtr3LDkopP0uAx8A7A0tJ7SwgTvkKsLG9DUWVdANWkhq2r6EEeZoCJ2AhKSoMsP3uoQ+0gFFhQEbPwgBJ790lcADmoRLzNAFqQNP2MwS4Az1J9cDnKjAEOrYv6UGc8uPkkEKAT4G297k5+T+9AA/TMbxmO4/hAAAAAElFTkSuQmCC");

            // ═══════ 發佈卡片 ═══════
            this.panelPublish.BackColor = cardBg;
            this.panelPublish.Padding = new Padding(14, 12, 14, 14);
            this.panelPublish.Dock = DockStyle.Top;
            this.panelPublish.Height = 186;

            this.labelPublish.Text = "  發佈";
            this.labelPublish.Image = icoSecPub;
            this.labelPublish.ImageAlign = ContentAlignment.MiddleLeft;
            this.labelPublish.TextAlign = ContentAlignment.MiddleLeft;
            this.labelPublish.Font = headingFont;
            this.labelPublish.ForeColor = headingColor;
            this.labelPublish.Dock = DockStyle.Top;
            this.labelPublish.Height = 28;

            // ── Buttons side by side ──
            this.panelButtons.ColumnCount = 2;
            this.panelButtons.RowCount = 1;
            this.panelButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            this.panelButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            this.panelButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.panelButtons.Dock = DockStyle.Top;
            this.panelButtons.Height = 42;
            this.panelButtons.Margin = new Padding(0);
            this.panelButtons.BackColor = Color.Transparent;

            this.buttonPublish.BackColor = accentBlue;
            this.buttonPublish.Dock = DockStyle.Fill;
            this.buttonPublish.FlatStyle = FlatStyle.Flat;
            this.buttonPublish.FlatAppearance.BorderSize = 0;
            this.buttonPublish.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.buttonPublish.ForeColor = Color.White;
            this.buttonPublish.Name = "buttonPublish";
            this.buttonPublish.Text = " 發佈";
            this.buttonPublish.Image = icoPublish;
            this.buttonPublish.ImageAlign = ContentAlignment.MiddleLeft;
            this.buttonPublish.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.buttonPublish.TextAlign = ContentAlignment.MiddleCenter;
            this.buttonPublish.UseVisualStyleBackColor = false;
            this.buttonPublish.Cursor = Cursors.Hand;
            this.buttonPublish.Margin = new Padding(0, 0, 4, 0);

            this.buttonDraft.BackColor = cardBg;
            this.buttonDraft.Dock = DockStyle.Fill;
            this.buttonDraft.FlatStyle = FlatStyle.Flat;
            this.buttonDraft.FlatAppearance.BorderColor = borderColor;
            this.buttonDraft.FlatAppearance.BorderSize = 1;
            this.buttonDraft.Font = new Font("Segoe UI", 8.5F);
            this.buttonDraft.ForeColor = Color.FromArgb(55, 65, 81);
            this.buttonDraft.Name = "buttonDraft";
            this.buttonDraft.Text = " 草稿";
            this.buttonDraft.Image = icoDraft;
            this.buttonDraft.ImageAlign = ContentAlignment.MiddleLeft;
            this.buttonDraft.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.buttonDraft.TextAlign = ContentAlignment.MiddleCenter;
            this.buttonDraft.UseVisualStyleBackColor = false;
            this.buttonDraft.Cursor = Cursors.Hand;
            this.buttonDraft.Margin = new Padding(4, 0, 0, 0);

            this.panelButtons.Controls.Add(this.buttonPublish, 0, 0);
            this.panelButtons.Controls.Add(this.buttonDraft, 1, 0);

            this.labelDate.Text = "發佈日期";
            this.labelDate.Font = smallFont;
            this.labelDate.ForeColor = subtextColor;
            this.labelDate.Dock = DockStyle.Top;
            this.labelDate.Height = 28;
            this.labelDate.Padding = new Padding(0, 12, 0, 2);

            this.datePublishDate.Dock = DockStyle.Top;
            this.datePublishDate.Format = DateTimePickerFormat.Custom;
            this.datePublishDate.CustomFormat = "yyyy/MM/dd  HH:mm";
            this.datePublishDate.Font = bodyFont;
            this.datePublishDate.Name = "datePublishDate";
            this.datePublishDate.ShowCheckBox = true;
            this.datePublishDate.Height = 30;

            this.panelPublish.Controls.Add(this.datePublishDate);
            this.panelPublish.Controls.Add(this.labelDate);
            this.panelPublish.Controls.Add(this.panelButtons);
            this.panelPublish.Controls.Add(this.labelPublish);

            // ═══════ 分類卡片 ═══════
            this.panelCategories.BackColor = cardBg;
            this.panelCategories.Padding = new Padding(14, 12, 14, 14);
            this.panelCategories.Dock = DockStyle.Top;
            this.panelCategories.Height = 106;

            this.labelCategories.Text = "  分類";
            this.labelCategories.Image = icoSecCat;
            this.labelCategories.ImageAlign = ContentAlignment.MiddleLeft;
            this.labelCategories.TextAlign = ContentAlignment.MiddleLeft;
            this.labelCategories.Font = headingFont;
            this.labelCategories.ForeColor = headingColor;
            this.labelCategories.Dock = DockStyle.Top;
            this.labelCategories.Height = 28;

            this.categoryDropDown.Dock = DockStyle.Top;
            this.categoryDropDown.DrawMode = DrawMode.OwnerDrawFixed;
            this.categoryDropDown.DropDownStyle = ComboBoxStyle.DropDownList;
            this.categoryDropDown.FormattingEnabled = true;
            this.categoryDropDown.IntegralHeight = false;
            this.categoryDropDown.Items.AddRange(new object[] { "", "", "" });
            this.categoryDropDown.Name = "categoryDropDown";
            this.categoryDropDown.ItemHeight = 30;
            this.categoryDropDown.DropDownHeight = 400;
            this.categoryDropDown.Height = 36;
            this.categoryDropDown.Font = bodyFont;
            this.categoryDropDown.TabIndex = 0;

            this.panelCategories.Controls.Add(this.categoryDropDown);
            this.panelCategories.Controls.Add(this.labelCategories);

            // ═══════ 標籤卡片 ═══════
            this.panelTags.BackColor = cardBg;
            this.panelTags.Padding = new Padding(14, 12, 14, 14);
            this.panelTags.Dock = DockStyle.Top;
            this.panelTags.Height = 106;

            this.labelTags.Text = "  標籤";
            this.labelTags.Image = icoSecTag;
            this.labelTags.ImageAlign = ContentAlignment.MiddleLeft;
            this.labelTags.TextAlign = ContentAlignment.MiddleLeft;
            this.labelTags.Font = headingFont;
            this.labelTags.ForeColor = headingColor;
            this.labelTags.Dock = DockStyle.Top;
            this.labelTags.Height = 28;

            this.textTags.Dock = DockStyle.Top;
            this.textTags.DefaultText = null;
            this.textTags.Name = "textTags";
            this.textTags.ShowButton = true;
            this.textTags.Height = 32;
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

            // ═══════ 檢視全部 ═══════
            this.linkViewAll.LinkBehavior = LinkBehavior.HoverUnderline;
            this.linkViewAll.ActiveLinkColor = accentBlue;
            this.linkViewAll.LinkColor = accentBlue;
            this.linkViewAll.VisitedLinkColor = accentBlue;
            this.linkViewAll.Dock = DockStyle.Top;
            this.linkViewAll.Font = smallFont;
            this.linkViewAll.Height = 28;
            this.linkViewAll.Padding = new Padding(6, 8, 0, 0);
            this.linkViewAll.Name = "linkViewAll";
            this.linkViewAll.TabStop = true;
            this.linkViewAll.Text = "檢視全部屬性...";
            this.linkViewAll.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkViewAll_LinkClicked);

            // ═══════ 左邊框線 ═══════
            this.panelShadow.BackColor = Color.FromArgb(229, 231, 235);
            this.panelShadow.Dock = DockStyle.Left;
            this.panelShadow.Width = 1;
            this.panelShadow.Name = "panelShadow";

            // ═══════ 隱藏 table ═══════
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
        private TableLayoutPanel panelButtons;
    }
}
