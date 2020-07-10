using System;

namespace CefiBrowser
{
    partial class Jdownload
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.addrPanel2 = new CefiBrowser.AddrPanel();
            this.mDownloadTextBTN1 = new CefiBrowser.mDownloadTextBTN();
            this.buttonXP1 = new CefiBrowser.ButtonXP();
            this.addrPanel1 = new CefiBrowser.AddrPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.OpenFileBtn = new CefiBrowser.ButtonXP();
            this.addrPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // addrPanel2
            // 
            this.addrPanel2.DrBottom = false;
            this.addrPanel2.DrLeft = false;
            this.addrPanel2.DrRight = false;
            this.addrPanel2.DrTop = false;
            this.addrPanel2.Location = new System.Drawing.Point(67, 3);
            this.addrPanel2.mStrText = null;
            this.addrPanel2.Name = "addrPanel2";
            this.addrPanel2.Size = new System.Drawing.Size(177, 18);
            this.addrPanel2.TabIndex = 9;
            this.addrPanel2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AddrPanel2_MouseUp);
            // 
            // mDownloadTextBTN1
            // 
            this.mDownloadTextBTN1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mDownloadTextBTN1.BackColor = System.Drawing.Color.Transparent;
            this.mDownloadTextBTN1.FileName = null;
            this.mDownloadTextBTN1.Location = new System.Drawing.Point(68, 22);
            this.mDownloadTextBTN1.Name = "mDownloadTextBTN1";
            this.mDownloadTextBTN1.Size = new System.Drawing.Size(232, 21);
            this.mDownloadTextBTN1.TabIndex = 8;
            this.mDownloadTextBTN1.Text = "mDownloadTextBTN1";
            this.mDownloadTextBTN1.URL = "";
            this.mDownloadTextBTN1.UseVisualStyleBackColor = false;
            this.mDownloadTextBTN1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MDownloadTextBTN1_MouseClick);
            // 
            // buttonXP1
            // 
            this.buttonXP1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXP1.BackColor = System.Drawing.Color.Transparent;
            this.buttonXP1.BackColorDown = System.Drawing.Color.DarkGray;
            this.buttonXP1.BackColorEX = System.Drawing.Color.White;
            this.buttonXP1.BackColorLeave = System.Drawing.Color.White;
            this.buttonXP1.BackColorMove = System.Drawing.Color.Gainsboro;
            this.buttonXP1.FontM = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonXP1.ImageDefault = global::CefiBrowser.Properties.Resources.close1;
            this.buttonXP1.ImageDown = null;
            this.buttonXP1.ImageLeave = null;
            this.buttonXP1.ImageMove = null;
            this.buttonXP1.Location = new System.Drawing.Point(342, 3);
            this.buttonXP1.Name = "buttonXP1";
            this.buttonXP1.Size = new System.Drawing.Size(20, 20);
            this.buttonXP1.TabIndex = 4;
            this.buttonXP1.TextColor = System.Drawing.Color.Black;
            this.buttonXP1.TextEX = "";
            this.buttonXP1.ButtonClick += new System.EventHandler(this.ButtonXP1_ButtonClick);
            // 
            // addrPanel1
            // 
            this.addrPanel1.Controls.Add(this.label1);
            this.addrPanel1.Controls.Add(this.pictureBox1);
            this.addrPanel1.DrBottom = false;
            this.addrPanel1.DrLeft = false;
            this.addrPanel1.DrRight = false;
            this.addrPanel1.DrTop = false;
            this.addrPanel1.Location = new System.Drawing.Point(3, 3);
            this.addrPanel1.mStrText = null;
            this.addrPanel1.Name = "addrPanel1";
            this.addrPanel1.Size = new System.Drawing.Size(47, 59);
            this.addrPanel1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 9;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(7, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 38);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // OpenFileBtn
            // 
            this.OpenFileBtn.BackColor = System.Drawing.Color.Transparent;
            this.OpenFileBtn.BackColorDown = System.Drawing.Color.Transparent;
            this.OpenFileBtn.BackColorEX = System.Drawing.Color.Transparent;
            this.OpenFileBtn.BackColorLeave = System.Drawing.Color.Transparent;
            this.OpenFileBtn.BackColorMove = System.Drawing.Color.Transparent;
            this.OpenFileBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OpenFileBtn.FontM = new System.Drawing.Font("宋体", 11F);
            this.OpenFileBtn.ImageDefault = null;
            this.OpenFileBtn.ImageDown = null;
            this.OpenFileBtn.ImageLeave = null;
            this.OpenFileBtn.ImageMove = null;
            this.OpenFileBtn.Location = new System.Drawing.Point(66, 45);
            this.OpenFileBtn.Name = "OpenFileBtn";
            this.OpenFileBtn.Size = new System.Drawing.Size(98, 17);
            this.OpenFileBtn.TabIndex = 5;
            this.OpenFileBtn.TextColor = System.Drawing.Color.Black;
            this.OpenFileBtn.TextEX = "在文件夹中显示";
            this.OpenFileBtn.ButtonClick += new System.EventHandler(this.OpenFileBtn_Load);
            // 
            // Jdownload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Controls.Add(this.addrPanel2);
            this.Controls.Add(this.mDownloadTextBTN1);
            this.Controls.Add(this.buttonXP1);
            this.Controls.Add(this.addrPanel1);
            this.Controls.Add(this.OpenFileBtn);
            this.Name = "Jdownload";
            this.Size = new System.Drawing.Size(365, 68);
            this.Load += new System.EventHandler(this.Jdownload_Load);
            this.ClientSizeChanged += new System.EventHandler(this.JSizeChanged);
            this.addrPanel1.ResumeLayout(false);
            this.addrPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }




        #endregion
        public System.Windows.Forms.PictureBox pictureBox1;
        private ButtonXP buttonXP1;
        private ButtonXP OpenFileBtn;
        private AddrPanel addrPanel1;
        public mDownloadTextBTN mDownloadTextBTN1;
        public System.Windows.Forms.Label label1;
        public AddrPanel addrPanel2;
    }
}
