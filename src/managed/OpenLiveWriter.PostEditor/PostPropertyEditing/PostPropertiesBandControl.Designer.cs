using System.Drawing;
using System.Windows.Forms;
using OpenLiveWriter.Controls;
using OpenLiveWriter.PostEditor.PostPropertyEditing.CategoryControl;

namespace OpenLiveWriter.PostEditor.PostPropertyEditing
{
    partial class PostPropertiesBandControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.table = new System.Windows.Forms.TableLayoutPanel();
            this.labelPageOrder = new System.Windows.Forms.Label();
            this.categoryDropDown = new OpenLiveWriter.PostEditor.PostPropertyEditing.CategoryControl.CategoryDropDownControlM1();
            this.textTags = new OpenLiveWriter.Controls.AutoCompleteTextbox();
            this.datePublishDate = new OpenLiveWriter.PostEditor.PostPropertyEditing.PublishDateTimePicker();
            this.labelPageParent = new System.Windows.Forms.Label();
            this.comboPageParent = new OpenLiveWriter.PostEditor.PostPropertyEditing.PageParentComboBox();
            this.textPageOrder = new OpenLiveWriter.Controls.NumericTextBox();
            this.panelShadow = new System.Windows.Forms.Panel();
            this.linkViewAll = new System.Windows.Forms.LinkLabel();
            this.labelPublish = new System.Windows.Forms.Label();
            this.labelCategories = new System.Windows.Forms.Label();
            this.labelTags = new System.Windows.Forms.Label();
            this.buttonPublish = new System.Windows.Forms.Button();
            this.table.SuspendLayout();
            this.SuspendLayout();
            //
            // table — vertical layout for right sidebar
            //
            this.table.BackColor = System.Drawing.Color.Transparent;
            this.table.ColumnCount = 1;
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.table.RowCount = 10;
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle()); // 0: Publish label
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle()); // 1: Publish button
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle()); // 2: Date picker
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle()); // 3: Categories label
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle()); // 4: Category dropdown
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle()); // 5: Tags label
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle()); // 6: Tags textbox
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle()); // 7: Page parent label (hidden for posts)
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle()); // 8: Page parent combo (hidden for posts)
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F)); // 9: filler
            this.table.Controls.Add(this.labelPublish, 0, 0);
            this.table.Controls.Add(this.buttonPublish, 0, 1);
            this.table.Controls.Add(this.datePublishDate, 0, 2);
            this.table.Controls.Add(this.labelCategories, 0, 3);
            this.table.Controls.Add(this.categoryDropDown, 0, 4);
            this.table.Controls.Add(this.labelTags, 0, 5);
            this.table.Controls.Add(this.textTags, 0, 6);
            this.table.Controls.Add(this.labelPageParent, 0, 7);
            this.table.Controls.Add(this.comboPageParent, 0, 8);
            this.table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.table.Location = new System.Drawing.Point(0, 0);
            this.table.Name = "table";
            this.table.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.table.Size = new System.Drawing.Size(260, 600);
            this.table.TabIndex = 0;
            //
            // labelPublish — section header
            //
            this.labelPublish.AutoSize = true;
            this.labelPublish.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPublish.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelPublish.ForeColor = System.Drawing.Color.FromArgb(68, 68, 68);
            this.labelPublish.Location = new System.Drawing.Point(10, 8);
            this.labelPublish.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.labelPublish.Name = "labelPublish";
            this.labelPublish.Size = new System.Drawing.Size(240, 17);
            this.labelPublish.Text = "Publish";
            //
            // buttonPublish
            //
            this.buttonPublish.BackColor = System.Drawing.Color.FromArgb(37, 99, 235);
            this.buttonPublish.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonPublish.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPublish.FlatAppearance.BorderSize = 0;
            this.buttonPublish.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.buttonPublish.ForeColor = System.Drawing.Color.White;
            this.buttonPublish.Location = new System.Drawing.Point(10, 31);
            this.buttonPublish.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.buttonPublish.MinimumSize = new System.Drawing.Size(0, 32);
            this.buttonPublish.Name = "buttonPublish";
            this.buttonPublish.Size = new System.Drawing.Size(240, 32);
            this.buttonPublish.TabIndex = 10;
            this.buttonPublish.Text = "Publish";
            this.buttonPublish.UseVisualStyleBackColor = false;
            this.buttonPublish.Cursor = System.Windows.Forms.Cursors.Hand;
            //
            // datePublishDate
            //
            this.datePublishDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.datePublishDate.Format = DateTimePickerFormat.Custom;
            this.datePublishDate.Location = new System.Drawing.Point(10, 71);
            this.datePublishDate.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.datePublishDate.Name = "datePublishDate";
            this.datePublishDate.RightToLeftLayout = true;
            this.datePublishDate.ShowCheckBox = true;
            this.datePublishDate.Size = new System.Drawing.Size(240, 20);
            this.datePublishDate.TabIndex = 2;
            //
            // labelCategories — section header
            //
            this.labelCategories.AutoSize = true;
            this.labelCategories.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCategories.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelCategories.ForeColor = System.Drawing.Color.FromArgb(68, 68, 68);
            this.labelCategories.Location = new System.Drawing.Point(10, 103);
            this.labelCategories.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.labelCategories.Name = "labelCategories";
            this.labelCategories.Size = new System.Drawing.Size(240, 17);
            this.labelCategories.Text = "Categories";
            //
            // categoryDropDown
            //
            this.categoryDropDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.categoryDropDown.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.categoryDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categoryDropDown.FormattingEnabled = true;
            this.categoryDropDown.IntegralHeight = false;
            this.categoryDropDown.Items.AddRange(new object[] { "", "", "" });
            this.categoryDropDown.Location = new System.Drawing.Point(10, 126);
            this.categoryDropDown.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.categoryDropDown.Name = "categoryDropDown";
            this.categoryDropDown.Size = new System.Drawing.Size(240, 21);
            this.categoryDropDown.TabIndex = 0;
            //
            // labelTags — section header
            //
            this.labelTags.AutoSize = true;
            this.labelTags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTags.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.labelTags.ForeColor = System.Drawing.Color.FromArgb(68, 68, 68);
            this.labelTags.Location = new System.Drawing.Point(10, 159);
            this.labelTags.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.labelTags.Name = "labelTags";
            this.labelTags.Size = new System.Drawing.Size(240, 17);
            this.labelTags.Text = "Tags";
            //
            // textTags
            //
            this.textTags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textTags.DefaultText = null;
            this.textTags.ForeColor = System.Drawing.SystemColors.GrayText;
            this.textTags.Location = new System.Drawing.Point(10, 182);
            this.textTags.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.textTags.Name = "textTags";
            this.textTags.ShowButton = true;
            this.textTags.Size = new System.Drawing.Size(240, 20);
            this.textTags.TabIndex = 1;
            //
            // labelPageParent (hidden for normal posts, visible for pages)
            //
            this.labelPageParent.AutoSize = true;
            this.labelPageParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPageParent.Location = new System.Drawing.Point(10, 214);
            this.labelPageParent.Margin = new System.Windows.Forms.Padding(0);
            this.labelPageParent.Name = "labelPageParent";
            this.labelPageParent.Size = new System.Drawing.Size(240, 13);
            this.labelPageParent.TabIndex = 3;
            this.labelPageParent.Text = "Page parent:";
            //
            // comboPageParent
            //
            this.comboPageParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboPageParent.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboPageParent.FormattingEnabled = true;
            this.comboPageParent.IntegralHeight = false;
            this.comboPageParent.Location = new System.Drawing.Point(10, 227);
            this.comboPageParent.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.comboPageParent.Name = "comboPageParent";
            this.comboPageParent.Size = new System.Drawing.Size(240, 20);
            this.comboPageParent.TabIndex = 4;
            //
            // labelPageOrder (kept but hidden — accessed via View All)
            //
            this.labelPageOrder.AutoSize = true;
            this.labelPageOrder.Location = new System.Drawing.Point(0, 0);
            this.labelPageOrder.Name = "labelPageOrder";
            this.labelPageOrder.Size = new System.Drawing.Size(62, 13);
            this.labelPageOrder.TabIndex = 5;
            this.labelPageOrder.Text = "Page order:";
            this.labelPageOrder.Visible = false;
            //
            // textPageOrder (kept but hidden — accessed via View All)
            //
            this.textPageOrder.Location = new System.Drawing.Point(0, 0);
            this.textPageOrder.Name = "textPageOrder";
            this.textPageOrder.Size = new System.Drawing.Size(63, 20);
            this.textPageOrder.TabIndex = 6;
            this.textPageOrder.Visible = false;
            //
            // panelShadow — left border line
            //
            this.panelShadow.BackColor = System.Drawing.Color.FromArgb(218, 218, 218);
            this.panelShadow.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelShadow.Location = new System.Drawing.Point(0, 0);
            this.panelShadow.Name = "panelShadow";
            this.panelShadow.Size = new System.Drawing.Size(1, 600);
            this.panelShadow.TabIndex = 1;
            //
            // linkViewAll
            //
            this.linkViewAll.ActiveLinkColor = Color.FromArgb(85, 142, 213);
            this.linkViewAll.AutoSize = true;
            this.linkViewAll.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkViewAll.LinkColor = Color.FromArgb(85, 142, 213);
            this.linkViewAll.Location = new System.Drawing.Point(10, 0);
            this.linkViewAll.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.linkViewAll.Name = "linkViewAll";
            this.linkViewAll.Size = new System.Drawing.Size(43, 13);
            this.linkViewAll.TabIndex = 7;
            this.linkViewAll.TabStop = true;
            this.linkViewAll.Text = "View all";
            this.linkViewAll.VisitedLinkColor = Color.FromArgb(85, 142, 213);
            this.linkViewAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkViewAll_LinkClicked);
            //
            // PostPropertiesBandControl — now a right sidebar
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(249, 250, 251);
            this.Controls.Add(this.table);
            this.Controls.Add(this.linkViewAll);
            this.Controls.Add(this.panelShadow);
            this.Name = "PostPropertiesBandControl";
            this.Size = new System.Drawing.Size(260, 600);
            this.table.ResumeLayout(false);
            this.table.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel table;
        private CategoryDropDownControlM1 categoryDropDown;
        private AutoCompleteTextbox textTags;
        private System.Windows.Forms.Label labelPageOrder;
        private PublishDateTimePicker datePublishDate;
        private System.Windows.Forms.Label labelPageParent;
        private PageParentComboBox comboPageParent;
        private NumericTextBox textPageOrder;
        private Panel panelShadow;
        private LinkLabel linkViewAll;
        private System.Windows.Forms.Label labelPublish;
        private System.Windows.Forms.Label labelCategories;
        private System.Windows.Forms.Label labelTags;
        private System.Windows.Forms.Button buttonPublish;
    }
}
