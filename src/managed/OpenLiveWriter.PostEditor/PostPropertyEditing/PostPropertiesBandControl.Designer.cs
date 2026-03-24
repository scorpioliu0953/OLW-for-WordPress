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
            this.categoryDropDown = new OpenLiveWriter.PostEditor.PostPropertyEditing.CategoryControl.CategoryDropDownControlM1();
            this.textTags = new OpenLiveWriter.Controls.AutoCompleteTextbox();
            this.datePublishDate = new OpenLiveWriter.PostEditor.PostPropertyEditing.PublishDateTimePicker();
            this.labelPageOrder = new System.Windows.Forms.Label();
            this.labelPageParent = new System.Windows.Forms.Label();
            this.comboPageParent = new OpenLiveWriter.PostEditor.PostPropertyEditing.PageParentComboBox();
            this.textPageOrder = new OpenLiveWriter.Controls.NumericTextBox();
            this.linkViewAll = new System.Windows.Forms.LinkLabel();
            this.buttonPublish = new System.Windows.Forms.Button();
            this.buttonDraft = new System.Windows.Forms.Button();
            this.table = new System.Windows.Forms.TableLayoutPanel();
            this.panelShadow = new System.Windows.Forms.Panel();

            // Section panels for card-style layout
            this.panelPublish = new System.Windows.Forms.Panel();
            this.panelCategories = new System.Windows.Forms.Panel();
            this.panelTags = new System.Windows.Forms.Panel();
            this.labelPublish = new System.Windows.Forms.Label();
            this.labelCategories = new System.Windows.Forms.Label();
            this.labelTags = new System.Windows.Forms.Label();
            this.labelDate = new System.Windows.Forms.Label();

            this.SuspendLayout();

            // ─── Publish Card ───
            this.panelPublish.BackColor = Color.White;
            this.panelPublish.Padding = new Padding(12);
            this.panelPublish.Margin = new Padding(0, 0, 0, 10);
            this.panelPublish.Dock = DockStyle.Top;
            this.panelPublish.Height = 160;

            this.labelPublish.Text = "Publish";
            this.labelPublish.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.labelPublish.ForeColor = Color.FromArgb(31, 41, 55);
            this.labelPublish.Dock = DockStyle.Top;
            this.labelPublish.Height = 28;
            this.labelPublish.Padding = new Padding(0, 0, 0, 4);

            this.buttonPublish.BackColor = Color.FromArgb(37, 99, 235);
            this.buttonPublish.Dock = DockStyle.Top;
            this.buttonPublish.FlatStyle = FlatStyle.Flat;
            this.buttonPublish.FlatAppearance.BorderSize = 0;
            this.buttonPublish.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            this.buttonPublish.ForeColor = Color.White;
            this.buttonPublish.Height = 34;
            this.buttonPublish.Name = "buttonPublish";
            this.buttonPublish.Text = "Publish";
            this.buttonPublish.UseVisualStyleBackColor = false;
            this.buttonPublish.Cursor = Cursors.Hand;
            this.buttonPublish.Margin = new Padding(0, 0, 0, 6);

            this.buttonDraft.BackColor = Color.White;
            this.buttonDraft.Dock = DockStyle.Top;
            this.buttonDraft.FlatStyle = FlatStyle.Flat;
            this.buttonDraft.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
            this.buttonDraft.FlatAppearance.BorderSize = 1;
            this.buttonDraft.Font = new Font("Segoe UI", 9F);
            this.buttonDraft.ForeColor = Color.FromArgb(55, 65, 81);
            this.buttonDraft.Height = 30;
            this.buttonDraft.Name = "buttonDraft";
            this.buttonDraft.Text = "Save as Draft";
            this.buttonDraft.UseVisualStyleBackColor = false;
            this.buttonDraft.Cursor = Cursors.Hand;

            this.labelDate.Text = "Publish Date";
            this.labelDate.Font = new Font("Segoe UI", 8.25F);
            this.labelDate.ForeColor = Color.FromArgb(107, 114, 128);
            this.labelDate.Dock = DockStyle.Top;
            this.labelDate.Height = 22;
            this.labelDate.Padding = new Padding(0, 6, 0, 2);

            this.datePublishDate.Dock = DockStyle.Top;
            this.datePublishDate.Format = DateTimePickerFormat.Custom;
            this.datePublishDate.Name = "datePublishDate";
            this.datePublishDate.RightToLeftLayout = true;
            this.datePublishDate.ShowCheckBox = true;
            this.datePublishDate.Height = 24;

            // Add to panelPublish (reverse order for Dock.Top stacking)
            this.panelPublish.Controls.Add(this.datePublishDate);
            this.panelPublish.Controls.Add(this.labelDate);
            this.panelPublish.Controls.Add(this.buttonDraft);
            this.panelPublish.Controls.Add(this.buttonPublish);
            this.panelPublish.Controls.Add(this.labelPublish);

            // ─── Categories Card ───
            this.panelCategories.BackColor = Color.White;
            this.panelCategories.Padding = new Padding(12);
            this.panelCategories.Margin = new Padding(0, 0, 0, 10);
            this.panelCategories.Dock = DockStyle.Top;
            this.panelCategories.Height = 90;

            this.labelCategories.Text = "Categories";
            this.labelCategories.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.labelCategories.ForeColor = Color.FromArgb(31, 41, 55);
            this.labelCategories.Dock = DockStyle.Top;
            this.labelCategories.Height = 28;
            this.labelCategories.Padding = new Padding(0, 0, 0, 4);

            this.categoryDropDown.Dock = DockStyle.Top;
            this.categoryDropDown.DrawMode = DrawMode.OwnerDrawFixed;
            this.categoryDropDown.DropDownStyle = ComboBoxStyle.DropDownList;
            this.categoryDropDown.FormattingEnabled = true;
            this.categoryDropDown.IntegralHeight = false;
            this.categoryDropDown.Items.AddRange(new object[] { "", "", "" });
            this.categoryDropDown.Name = "categoryDropDown";
            this.categoryDropDown.Height = 28;
            this.categoryDropDown.Font = new Font("Segoe UI", 9.5F);
            this.categoryDropDown.TabIndex = 0;

            this.panelCategories.Controls.Add(this.categoryDropDown);
            this.panelCategories.Controls.Add(this.labelCategories);

            // ─── Tags Card ───
            this.panelTags.BackColor = Color.White;
            this.panelTags.Padding = new Padding(12);
            this.panelTags.Margin = new Padding(0, 0, 0, 10);
            this.panelTags.Dock = DockStyle.Top;
            this.panelTags.Height = 90;

            this.labelTags.Text = "Tags";
            this.labelTags.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.labelTags.ForeColor = Color.FromArgb(31, 41, 55);
            this.labelTags.Dock = DockStyle.Top;
            this.labelTags.Height = 28;
            this.labelTags.Padding = new Padding(0, 0, 0, 4);

            this.textTags.Dock = DockStyle.Top;
            this.textTags.DefaultText = null;
            this.textTags.Name = "textTags";
            this.textTags.ShowButton = true;
            this.textTags.Height = 28;
            this.textTags.Font = new Font("Segoe UI", 9.5F);
            this.textTags.TabIndex = 1;

            this.panelTags.Controls.Add(this.textTags);
            this.panelTags.Controls.Add(this.labelTags);

            // ─── Hidden controls (accessed via View All) ───
            this.labelPageOrder.Visible = false;
            this.labelPageOrder.Name = "labelPageOrder";
            this.textPageOrder.Visible = false;
            this.textPageOrder.Name = "textPageOrder";
            this.labelPageParent.Visible = false;
            this.labelPageParent.Name = "labelPageParent";
            this.comboPageParent.Visible = false;
            this.comboPageParent.Name = "comboPageParent";

            // ─── View All link ───
            this.linkViewAll.ActiveLinkColor = Color.FromArgb(37, 99, 235);
            this.linkViewAll.LinkBehavior = LinkBehavior.HoverUnderline;
            this.linkViewAll.LinkColor = Color.FromArgb(37, 99, 235);
            this.linkViewAll.Dock = DockStyle.Top;
            this.linkViewAll.Font = new Font("Segoe UI", 8.25F);
            this.linkViewAll.Height = 24;
            this.linkViewAll.Padding = new Padding(4, 4, 0, 0);
            this.linkViewAll.Name = "linkViewAll";
            this.linkViewAll.TabStop = true;
            this.linkViewAll.Text = "View all properties...";
            this.linkViewAll.VisitedLinkColor = Color.FromArgb(37, 99, 235);
            this.linkViewAll.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkViewAll_LinkClicked);

            // ─── Left border separator ───
            this.panelShadow.BackColor = Color.FromArgb(229, 231, 235);
            this.panelShadow.Dock = DockStyle.Left;
            this.panelShadow.Width = 1;
            this.panelShadow.Name = "panelShadow";

            // ─── Dummy table (kept for SharedPropertiesController compat) ───
            this.table = new TableLayoutPanel();
            this.table.Visible = false;
            this.table.Size = new Size(0, 0);

            // ─── Main control ───
            this.AutoScaleMode = AutoScaleMode.Inherit;
            this.BackColor = Color.FromArgb(243, 244, 246);
            this.Padding = new Padding(1, 10, 10, 10);

            // Add in reverse dock order (bottom items first)
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
