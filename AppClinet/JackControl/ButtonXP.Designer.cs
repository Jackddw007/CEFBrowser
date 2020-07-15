using Microsoft.Win32;

namespace CefiBrowser
{
    partial class ButtonXP
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ButtonXP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "ButtonXP";
            this.Size = new System.Drawing.Size(40, 40);
            this.Click += new System.EventHandler(this.ButtonXP_Click);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.label_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label_MouseDown);
            this.MouseLeave +=  (this.label_MouseLeave);
            this.LostFocus += new System.EventHandler(this.label_LostFocus);
            this.ResumeLayout(false);

        }

        private void ButtonXP_MouseLeave(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
