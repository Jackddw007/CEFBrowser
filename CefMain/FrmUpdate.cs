using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using System.Net;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace CefMain
{
	/// <summary>
	/// Form1 ��ժҪ˵����
	/// </summary>
	public class FrmUpdate : System.Windows.Forms.Form
	{

        /// <summary>
        /// ����������������
        /// </summary>
        private System.ComponentModel.Container components = null;


        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(value);
        }

        public FrmUpdate()
		{
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            SetVisibleCore(false);

            //
            // Windows ���������֧���������
            //
            InitializeComponent();
            

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
		}

		/// <summary>
		/// ������������ʹ�õ���Դ��
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUpdate));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(0, 49);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(399, 27);
            this.progressBar1.Step = 1;
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("΢���ź�", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(2, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "��ǰ���ڸ��£�";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("΢���ź�", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(86, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("΢���ź�", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(356, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "label3";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(0, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(399, 23);
            this.panel2.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("΢���ź�", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(4, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "label4";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(405, 81);
            this.panel1.TabIndex = 12;
            // 
            // FrmUpdate
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(405, 81);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "���¼��";
            this.Load += new System.EventHandler(this.FrmUpdate_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private string updateUrl = string.Empty;
		private string tempUpdatePath = string.Empty;
		XmlFiles updaterXmlFiles = null;
		private int availableUpdate = 0;
		string mainAppExe = "CefiBrowser.exe";
        private ProgressBar progressBar1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Panel panel2;
        private Label label4;
        private Panel panel1;
        private ListView lvUpdateList ;
        private Color offSelectedColor = Color.FromArgb(160, 160, 160); //��ǩδѡ��ʱ����ɫ
        WriteLogs writeLog = new WriteLogs();
        /// <summary>
        /// Ӧ�ó��������ڵ㡣
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new FrmUpdate());
        }

        private void FrmUpdate_Load(object sender, System.EventArgs e)
		{
            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Logs"))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Logs");
            }

            lvUpdateList = new ListView();
            string localXmlFile = Application.StartupPath + "\\UpdateList.xml";
			string serverXmlFile = string.Empty;
	
			try
			{
				//�ӱ��ض�ȡ���������ļ���Ϣ
				updaterXmlFiles = new XmlFiles(localXmlFile );
			}
			catch
			{
                writeLog.WriteLog("�����ļ�����!");
               // MessageBox.Show("�����ļ�����!", "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                StartCefiBrowser();
                
                //this.Close();
                return;
			}
			//��ȡ��������ַ
			updateUrl = updaterXmlFiles.GetNodeValue("//Url");

			AppUpdater appUpdater = new AppUpdater();
			appUpdater.UpdaterUrl = updateUrl + "/UpdateList.xml";

			//�����������,���ظ��������ļ�
			//try
			//{
				tempUpdatePath = Environment.GetEnvironmentVariable("Temp") + "\\"+ "_"+ updaterXmlFiles.FindNode("//Application").Attributes["applicationId"].Value+"_"+"y"+"_"+"x"+"_"+"m"+"_"+"\\";
               if(!appUpdater.DownAutoUpdateFile(tempUpdatePath))
                {

                writeLog.WriteLog("�����������ʧ��, ������ʱ!");
               // MessageBox.Show("�����������ʧ��,������ʱ!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    StartCefiBrowser();
                    //this.Close();
                    return;
                }
			//}
			//catch
			//{

   //             MessageBox.Show("�����������ʧ��,������ʱ!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
   //             StartCefiBrowser();
   //             //this.Close();
   //             return;

			//}

			//��ȡ�����ļ��б�
			Hashtable htUpdateFile = new Hashtable();

			serverXmlFile = tempUpdatePath + "\\UpdateList.xml";
			if(!File.Exists(serverXmlFile))
			{
                StartCefiBrowser();
                return;
			}

			availableUpdate = appUpdater.CheckForUpdate(serverXmlFile,localXmlFile,out htUpdateFile);
            if (availableUpdate > 0)
            {
                for (int i = 0; i < htUpdateFile.Count; i++)
                {
                    string[] fileArray = (string[])htUpdateFile[i];
                    lvUpdateList.Items.Add(new ListViewItem(fileArray));
                }
            }

            if (availableUpdate >0)
            {
                //����и��¾���ʾ���´���
                this.WindowState = FormWindowState.Normal;
               // this.ShowInTaskbar = true;
                SetVisibleCore(true);

                Thread threadDown = new Thread(new ThreadStart(DownUpdateFile));
                threadDown.IsBackground = true;
                threadDown.Start();
            }
            else
            {
                // NtCefilec.Ntfilec.RunAPP = true;
                StartCefiBrowser();
            }
		}

        private void StartCefiBrowser()
        {
            Process process = new Process();
            process.StartInfo.FileName = Directory.GetCurrentDirectory() + @"\" + mainAppExe;
            process.StartInfo.Arguments = "";
            process.Start();
            this.Hide();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias; //���ʱ�������SetSmoothingMode, AntiAliasΪָ��������ݵĳ���
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;

            if (this.WindowState == FormWindowState.Normal)
            {   //  ��TabButton�ĵ���
                this.Padding = new Padding(1);
                e.Graphics.DrawRectangle(new Pen(offSelectedColor), ClientRectangle);
                e.Graphics.DrawLine(new Pen(offSelectedColor), new Point((int)ClientRectangle.Right - 1, ClientRectangle.Y), new Point(ClientRectangle.Right - 1, ClientRectangle.Height));
                e.Graphics.DrawLine(new Pen(offSelectedColor), new Point((int)ClientRectangle.Left, ClientRectangle.Height - 1), new Point(ClientRectangle.Right - 1, ClientRectangle.Height - 1));
            }
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.Padding = new Padding(0);
            }
        }

        private void DownUpdateFile()
        {
            CheckForIllegalCrossThreadCalls = false;
            this.Cursor = Cursors.WaitCursor;
            //mainAppExe = updaterXmlFiles.GetNodeValue("//EntryPoint");
            //Process [] allProcess = Process.GetProcesses();
            //foreach(Process p in allProcess)
            //{

            //	if (p.ProcessName.ToLower() + ".exe" == mainAppExe.ToLower() )
            //	{
            //		for(int i=0;i<p.Threads.Count;i++)
            //			p.Threads[i].Dispose();
            //		p.Kill();
            //		isRun = true;
            //		//break;
            //	}
            //}
            WebClient wcClient = new WebClient();
            for (int i = 0; i < this.lvUpdateList.Items.Count; i++)
            {
                label4.Text = "��" + availableUpdate.ToString() + "���ļ������ڸ��µ�" + (i + 1).ToString() + "��";

                string UpdateFile = lvUpdateList.Items[i].Text.Trim();
                string updateFileUrl = updateUrl + lvUpdateList.Items[i].Text.Trim();
                long fileLength = 0;
                label2.Text = UpdateFile;
                WebRequest webReq = WebRequest.Create(updateFileUrl);
                WebResponse webRes = webReq.GetResponse();
                fileLength = webRes.ContentLength;

                //lbState.Text = "�������ظ����ļ�,���Ժ�...";
                progressBar1.Value = 0;
                progressBar1.Maximum = (int)fileLength;

                try
                {
                    Stream srm = webRes.GetResponseStream();
                    StreamReader srmReader = new StreamReader(srm);
                    byte[] bufferbyte = new byte[fileLength];
                    int allByte = (int)bufferbyte.Length;
                    int startByte = 0;
                    while (fileLength > 0)
                    {
                        Application.DoEvents();
                        int downByte = srm.Read(bufferbyte, startByte, allByte);
                        if (downByte == 0) { break; };
                        startByte += downByte;
                        allByte -= downByte;
                        progressBar1.Value += downByte;

                        float part = (float)startByte / 1024;
                        float total = (float)bufferbyte.Length / 1024;
                        int percent = Convert.ToInt32((part / total) * 100);

                        //this.lvUpdateList.Items[i].SubItems[2].Text = percent.ToString() + "%";
                        label3.Text = percent.ToString() + "%";
                    }

                    string tempPath = tempUpdatePath + UpdateFile;
                    CreateDirtory(tempPath);
                    FileStream fs = new FileStream(tempPath, FileMode.OpenOrCreate, FileAccess.Write);
                    fs.Write(bufferbyte, 0, bufferbyte.Length);
                    srm.Close();
                    srmReader.Close();
                    fs.Close();

                }
                catch (WebException ex)
                {
                    writeLog.WriteLog("�����ļ�����ʧ�ܣ�" + ex.Message.ToString());
                   // MessageBox.Show("�����ļ�����ʧ�ܣ�" + ex.Message.ToString(), "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            InvalidateControl();
            this.Cursor = Cursors.Default;
            writeLog.WriteLog("�ѳɹ�������" + availableUpdate.ToString() + "���ļ�");
            btnFinish_Click();

        }
		//����Ŀ¼
		private void CreateDirtory(string path)
		{
			if(!File.Exists(path))
			{
				string [] dirArray = path.Split('\\'); 
				string temp = string.Empty;
				for(int i = 0;i<dirArray.Length - 1;i++)
				{
					temp += dirArray[i].Trim() + "\\";
					if(!Directory.Exists(temp))
						Directory.CreateDirectory(temp);
				}
			}
		}

		//�����ļ�;
		public void CopyFile(string sourcePath,string objPath)
		{
//			char[] split = @"\".ToCharArray();
			if(!Directory.Exists(objPath))
			{
				Directory.CreateDirectory(objPath);
			}
			string[] files = Directory.GetFiles(sourcePath);
			for(int i=0;i<files.Length;i++)
			{
				string[] childfile = files[i].Split('\\');
				File.Copy(files[i],objPath + @"\" + childfile[childfile.Length-1],true);
			}
			string[] dirs = Directory.GetDirectories(sourcePath);
			for(int i=0;i<dirs.Length;i++)
			{
				string[] childdir = dirs[i].Split('\\');
				CopyFile(dirs[i],objPath + @"\" + childdir[childdir.Length-1]);
			}
		} 

		//�����ɸ��Ƹ����ļ���Ӧ�ó���Ŀ¼
		private void btnFinish_Click()
		{
			try
			{
				CopyFile(tempUpdatePath,Directory.GetCurrentDirectory());
				System.IO.Directory.Delete(tempUpdatePath,true);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message.ToString());
			}
            StartCefiBrowser();
        }

        //���»��ƴ��岿�ֿؼ�����
        private void InvalidateControl()
		{
			//panel2.Location = panel1.Location;
			//panel2.Size = panel1.Size;
			//panel1.Visible = false;
			//panel2.Visible = true;

			//btnNext.Visible = false;
			//btnCancel.Visible = false;
			//btnFinish.Location = btnCancel.Location;
			//btnFinish.Visible = true;
		}


		//�ж���Ӧ�ó����Ƿ���������
		private bool IsMainAppRun()
		{
			string mainAppExe = updaterXmlFiles.GetNodeValue("//EntryPoint");
			bool isRun = false;
			Process [] allProcess = Process.GetProcesses();
			foreach(Process p in allProcess)
			{
				
				if (p.ProcessName.ToLower() + ".exe" == mainAppExe.ToLower() )
				{
					isRun = true;
					//break;
				}
			}
			return isRun;
		}
	}
}
