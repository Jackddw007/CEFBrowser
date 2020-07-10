namespace CefiBrowser
{
    partial class Jdownloading
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonXP1 = new CefiBrowser.ButtonXP();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.DowncontextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Opendownfile = new System.Windows.Forms.ToolStripMenuItem();
            this.Openfolderfile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.CancelDown = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.DowncontextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(2, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 12);
            this.label2.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.buttonXP1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(39, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(129, 32);
            this.panel1.TabIndex = 4;
            // 
            // buttonXP1
            // 
            this.buttonXP1.BackColor = System.Drawing.Color.Transparent;
            this.buttonXP1.BackColorDown = System.Drawing.Color.LightGray;
            this.buttonXP1.BackColorEX = System.Drawing.Color.White;
            this.buttonXP1.BackColorLeave = System.Drawing.Color.White;
            this.buttonXP1.BackColorMove = System.Drawing.Color.Gainsboro;
            this.buttonXP1.FontM = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonXP1.ImageDefault = global::CefiBrowser.Properties.Resources.Down1;
            this.buttonXP1.ImageLeave = null;
            this.buttonXP1.ImageMove = null;
            this.buttonXP1.Location = new System.Drawing.Point(109, -4);
            this.buttonXP1.Name = "buttonXP1";
            this.buttonXP1.Size = new System.Drawing.Size(19, 13);
            this.buttonXP1.TabIndex = 1;
            this.buttonXP1.TextColor = System.Drawing.Color.Black;
            this.buttonXP1.TextEX = "";
            this.buttonXP1.ButtonClick += new System.EventHandler(this.buttonXP1_ButtonClic);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(1, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(35, 31);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // DowncontextMenu
            // 
            this.DowncontextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Opendownfile,
            this.Openfolderfile,
            this.toolStripSeparator1,
            this.CancelDown});
            this.DowncontextMenu.Name = "contextMenuStrip1";
            this.DowncontextMenu.Size = new System.Drawing.Size(149, 76);
            // 
            // Opendownfile
            // 
            this.Opendownfile.Name = "Opendownfile";
            this.Opendownfile.Size = new System.Drawing.Size(148, 22);
            this.Opendownfile.Text = "打开";
            this.Opendownfile.Click += new System.EventHandler(this.Opendownfile_Click);
            // 
            // Openfolderfile
            // 
            this.Openfolderfile.Name = "Openfolderfile";
            this.Openfolderfile.Size = new System.Drawing.Size(148, 22);
            this.Openfolderfile.Text = "打开所在目录";
            this.Openfolderfile.Click += new System.EventHandler(this.Openfolderfile_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // CancelDown
            // 
            this.CancelDown.Name = "CancelDown";
            this.CancelDown.Size = new System.Drawing.Size(148, 22);
            this.CancelDown.Text = "取消";
            this.CancelDown.Click += new System.EventHandler(this.CancelDown_Click);
            // 
            // Jdownloading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Jdownloading";
            this.Size = new System.Drawing.Size(171, 36);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.DowncontextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        public ButtonXP buttonXP1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        public System.Windows.Forms.ContextMenuStrip DowncontextMenu;
        public System.Windows.Forms.ToolStripMenuItem CancelDown;
        public System.Windows.Forms.ToolStripMenuItem Openfolderfile;
        public System.Windows.Forms.ToolStripMenuItem Opendownfile;
    }
}
