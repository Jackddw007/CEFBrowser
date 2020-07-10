namespace CefiBrowser
{
    partial class FaviFrm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            CefiBrowser.TTextBoxBorderRenderStyle tTextBoxBorderRenderStyle1 = new CefiBrowser.TTextBoxBorderRenderStyle();
            CefiBrowser.TTextBoxBorderRenderStyle tTextBoxBorderRenderStyle2 = new CefiBrowser.TTextBoxBorderRenderStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FaviFrm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.NewFavi_Folder = new System.Windows.Forms.Button();
            this.Favi_Save = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.DelLabBT = new System.Windows.Forms.Button();
            this.ItemURL = new CefiBrowser.TxtBoxXP();
            this.ItemTitle = new CefiBrowser.TxtBoxXP();
            this.buttonXP1 = new CefiBrowser.ButtonXP();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "修改标签";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(6, 39);
            this.label2.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "名称";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(6, 66);
            this.label3.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "网址";
            // 
            // treeView1
            // 
            this.treeView1.AllowDrop = true;
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.LabelEdit = true;
            this.treeView1.Location = new System.Drawing.Point(10, 96);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(495, 428);
            this.treeView1.TabIndex = 5;
            this.treeView1.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView1_ItemDrag);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView1_NodeMouseClick);
            this.treeView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView1_DragDrop);
            this.treeView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeView1_DragEnter);
            this.treeView1.LostFocus += new System.EventHandler(this.TreeView1_LostFocus);
            // 
            // NewFavi_Folder
            // 
            this.NewFavi_Folder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NewFavi_Folder.Location = new System.Drawing.Point(9, 529);
            this.NewFavi_Folder.Name = "NewFavi_Folder";
            this.NewFavi_Folder.Size = new System.Drawing.Size(75, 23);
            this.NewFavi_Folder.TabIndex = 6;
            this.NewFavi_Folder.Text = "新建收藏夹";
            this.NewFavi_Folder.UseVisualStyleBackColor = true;
            this.NewFavi_Folder.Click += new System.EventHandler(this.NewFavi_Folder_Click);
            // 
            // Favi_Save
            // 
            this.Favi_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Favi_Save.Location = new System.Drawing.Point(334, 529);
            this.Favi_Save.Name = "Favi_Save";
            this.Favi_Save.Size = new System.Drawing.Size(77, 23);
            this.Favi_Save.TabIndex = 6;
            this.Favi_Save.Text = "保存";
            this.Favi_Save.UseVisualStyleBackColor = true;
            this.Favi_Save.Click += new System.EventHandler(this.Favi_Save_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(429, 529);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(77, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "关闭";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // DelLabBT
            // 
            this.DelLabBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DelLabBT.Location = new System.Drawing.Point(99, 529);
            this.DelLabBT.Name = "DelLabBT";
            this.DelLabBT.Size = new System.Drawing.Size(75, 23);
            this.DelLabBT.TabIndex = 8;
            this.DelLabBT.Text = "删除";
            this.DelLabBT.UseVisualStyleBackColor = true;
            this.DelLabBT.Click += new System.EventHandler(this.DelLabBT_Click);
            // 
            // ItemURL
            // 
            this.ItemURL.AllowReturn = false;
            this.ItemURL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            tTextBoxBorderRenderStyle1.LineColor = System.Drawing.Color.LightGray;
            tTextBoxBorderRenderStyle1.LineWidth = 1F;
            this.ItemURL.BorderRenderStyle = tTextBoxBorderRenderStyle1;
            this.ItemURL.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ItemURL.Location = new System.Drawing.Point(51, 66);
            this.ItemURL.Multiline = true;
            this.ItemURL.Name = "ItemURL";
            this.ItemURL.Size = new System.Drawing.Size(454, 21);
            this.ItemURL.TabIndex = 3;
            this.ItemURL.TextMargin = new System.Windows.Forms.Padding(1);
            this.ItemURL.WordWrap = false;
            // 
            // ItemTitle
            // 
            this.ItemTitle.AllowReturn = false;
            this.ItemTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            tTextBoxBorderRenderStyle2.LineColor = System.Drawing.Color.LightGray;
            tTextBoxBorderRenderStyle2.LineWidth = 1F;
            this.ItemTitle.BorderRenderStyle = tTextBoxBorderRenderStyle2;
            this.ItemTitle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ItemTitle.Location = new System.Drawing.Point(51, 39);
            this.ItemTitle.Multiline = true;
            this.ItemTitle.Name = "ItemTitle";
            this.ItemTitle.Size = new System.Drawing.Size(454, 21);
            this.ItemTitle.TabIndex = 1;
            this.ItemTitle.TextMargin = new System.Windows.Forms.Padding(1);
            this.ItemTitle.WordWrap = false;
            // 
            // buttonXP1
            // 
            this.buttonXP1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXP1.BackColor = System.Drawing.Color.Transparent;
            this.buttonXP1.BackColorDown = System.Drawing.Color.OrangeRed;
            this.buttonXP1.BackColorEX = System.Drawing.SystemColors.Control;
            this.buttonXP1.BackColorLeave = System.Drawing.SystemColors.Control;
            this.buttonXP1.BackColorMove = System.Drawing.Color.Salmon;
            this.buttonXP1.FontM = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonXP1.ImageDefault = global::CefiBrowser.Properties.Resources.close1;
            this.buttonXP1.ImageLeave = null;
            this.buttonXP1.ImageMove = null;
            this.buttonXP1.Location = new System.Drawing.Point(484, 1);
            this.buttonXP1.Name = "buttonXP1";
            this.buttonXP1.Size = new System.Drawing.Size(30, 20);
            this.buttonXP1.TabIndex = 9;
            this.buttonXP1.TextColor = System.Drawing.Color.Black;
            this.buttonXP1.TextEX = "";
            this.buttonXP1.ButtonClick += new System.EventHandler(this.formCloseButton1_Click);
            // 
            // FaviFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(515, 558);
            this.Controls.Add(this.buttonXP1);
            this.Controls.Add(this.DelLabBT);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ItemURL);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Favi_Save);
            this.Controls.Add(this.NewFavi_Folder);
            this.Controls.Add(this.ItemTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FaviFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "收藏夹整理";
            this.Load += new System.EventHandler(this.FaviFrm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Start_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }








        #endregion

        private System.Windows.Forms.Label label1;
        private TxtBoxXP ItemTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private TxtBoxXP ItemURL;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button NewFavi_Folder;
        private System.Windows.Forms.Button Favi_Save;
        private System.Windows.Forms.Button button3;
        private ButtonXP formCloseButton1;
        private System.Windows.Forms.Button DelLabBT;
        private ButtonXP buttonXP1;
    }
}