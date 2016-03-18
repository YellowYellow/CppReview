 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Threading;
using System.Xml;
using CCWin;

namespace CheckReview
{
    public partial class Form1 : CCSkinMain
    {
        public NewListView listview1;       //指针没检查list
        public NewListView listview2;       //循环没保护list
        public NewListView listview3;       //硬编码list
        public NewListView listview4;       //对象可能泄露list
        public NewListView listview5;       //数组没保护list
        public NewListView listview6;       //迭代器没保护list
        public NewListView listview7;       //打印字符格式核对

        public List<NewListView> errorTypeForCpp = new List<NewListView>();  
        public Dictionary<string,ToolStripMenuItem> MenuItem;
        public Thread main;
        public Label filenum;
        public System.Windows.Forms.Timer timer;
        public ProgressBar progress;
        public int flag = 0;
        public int FileNum = 0;
        public Flags flags = new Flags();
        public ComboBox game_version = new ComboBox();
        public static string GameName;
        public static string GameVersion;

        public class Flags
        {
            public int pointerFlag = 0;
            public int loopFlag = 0;
            public int objectleekFlag = 0;
            public int numFlag = 0;
            public int arrayFlag = 0;
            public int iterFlag = 0;
            public int formatFlag = 0;
            public int vastartFlag = 0;
            public int sprintfFlag = 0;
            public int mapFlag = 0;
            public int constnullFlag = 0;
            public int structFlag = 0;
            public int earseFlag = 0;


            public int dbFlag = 0;
        }

        public BackGoundThread BackThread = new BackGoundThread();

        private delegate void VoidDelegate();

        public Form1()
        {
            InitializeComponent();
            loadXml();
            UIinit();
            tabInit();
        }

        public void loadXml()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("./res/ErrorForCpp.xml"); //加载xml文件

            XmlNodeList nodeList = xmlDoc.SelectSingleNode("ErrorType").ChildNodes;

            //遍历所有子节点
            foreach (XmlNode xn in nodeList)
            {
                XmlElement xe = (XmlElement)xn; //将子节点类型转换为XmlElement类型
                if (xe.Name == "pointer")
                {
                    if (xe.InnerText == "1") { flags.pointerFlag = 1; }
                    else { flags.pointerFlag = 0; }
                }
                if (xe.Name == "loop")
                {
                    if (xe.InnerText == "1") { flags.loopFlag = 1; }
                    else { flags.loopFlag = 0; }
                }
                if (xe.Name == "objectleek")
                {
                    if (xe.InnerText == "1") { flags.objectleekFlag = 1; }
                    else { flags.objectleekFlag = 0; }
                }
                if (xe.Name == "num")
                {
                    if (xe.InnerText == "1") { flags.numFlag = 1; }
                    else { flags.numFlag = 0; }
                }
                if (xe.Name == "array")
                {
                    if (xe.InnerText == "1") { flags.arrayFlag = 1; }
                    else { flags.arrayFlag = 0; }
                }
                if (xe.Name == "iter")
                {
                    if (xe.InnerText == "1") { flags.iterFlag = 1; }
                    else { flags.iterFlag = 0; }
                }
                if (xe.Name == "format")
                {
                    if (xe.InnerText == "1") { flags.formatFlag = 1; }
                    else { flags.formatFlag = 0; }
                }
                if (xe.Name == "struct")
                {
                    if (xe.InnerText == "1") { flags.structFlag = 1; }
                    else { flags.structFlag = 0; }
                }
                if (xe.Name == "map")
                {
                    if (xe.InnerText == "1") { flags.mapFlag = 1; }
                    else { flags.mapFlag = 0; }
                }
                if (xe.Name == "vastart")
                {
                    if (xe.InnerText == "1") { flags.vastartFlag = 1; }
                    else { flags.vastartFlag = 0; }
                }
                if (xe.Name == "constnull")
                {
                    if (xe.InnerText == "1") { flags.constnullFlag = 1; }
                    else { flags.constnullFlag = 0; }
                }
                if (xe.Name == "earse")
                {
                    if (xe.InnerText == "1") { flags.earseFlag = 1; }
                    else { flags.earseFlag = 0; }
                }
                if (xe.Name == "sprintf")
                {
                    if (xe.InnerText == "1") { flags.sprintfFlag = 1; }
                    else { flags.sprintfFlag = 0; }
                }
            }
        }

        //UI初始化
        public void UIinit()
        {
            listview1 = this.newListView1;
            listview1.View = View.Details;
            listview2 = this.newListView2;
            listview2.View = View.Details;
            listview3 = this.newListView3;
            listview3.View = View.Details;
            listview4 = this.newListView4;
            listview4.View = View.Details;
            listview5 = this.newListView5;
            listview5.View = View.Details;
            listview6 = this.newListView6;
            listview6.View = View.Details;
            listview7 = this.newListView7;
            listview7.View = View.Details;
            errorTypeForCpp.Add(listview1);
            errorTypeForCpp.Add(listview2);
            errorTypeForCpp.Add(listview3);
            errorTypeForCpp.Add(listview4);
            errorTypeForCpp.Add(listview5);
            errorTypeForCpp.Add(listview6);
            errorTypeForCpp.Add(listview7);
            timer = this.timer1;
            filenum = this.label1;
            progress = this.progressBar;
            BackThread.ui = this;
            BackThread.progress = this.progress;
            BackThread.timer = this.timer;


        }

        public void tabInit()
        {
            if (flags.pointerFlag == 1) { tabPage2.Parent = tabControl1; }
            else { tabPage2.Parent = null; }
            if (flags.loopFlag == 1) { tabPage3.Parent = tabControl1; }
            else { tabPage3.Parent = null; }
            if (flags.objectleekFlag == 1) { tabPage4.Parent = tabControl1; }
            else { tabPage4.Parent = null; }
            if (flags.numFlag == 1) { tabPage5.Parent = tabControl1; }
            else { tabPage5.Parent = null; }
            if (flags.arrayFlag == 1) { tabPage6.Parent = tabControl1; }
            else { tabPage6.Parent = null; }
            if (flags.iterFlag == 1) { tabPage7.Parent = tabControl1; }
            else { tabPage7.Parent = null; }
            if (flags.formatFlag == 1) { tabPage7.Parent = tabControl1; }
            else { tabPage7.Parent = null; }
        }

        //窗体程序退出
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(flag==1)
            {
                main.Abort();
            }
        }

        //按时刷新进度条
        private void timer1_Tick(object sender, EventArgs e)
        {
            filenum.Text = progress.Value + " / " + BackThread.FileNum;
            if (BackThread.progress.Value == BackThread.FileNum)
            {
                this.Invoke(new VoidDelegate(delegate()
                {
                    审核ToolStripMenuItem.Enabled = true;
                    继续ToolStripMenuItem.Enabled = false;
                    暂停ToolStripMenuItem.Enabled = false;
                    停止ToolStripMenuItem.Enabled = false;
                }));
            }
        }

        public class BackGoundThread
        {
            public Form1 ui;
            public ProgressBar progress;
            public int FileNum;
            public System.Windows.Forms.Timer timer;
            Regex Cppfile = new Regex("((\\.cpp)|(\\.h))$");
           // Regex Headfile = new Regex("(.*)(.h)");
            Regex Javafile = new Regex("(\\.java)$");
            Regex Otherfiles = new Regex("SDK");
            public string foldDir;

            public string languageType;

            private delegate void VoidDelegate();

            //进行代码审核判断
            public void Check()
            {
                this.ui.Invoke(new VoidDelegate(delegate()
                {
                    FileNum = 0;
                }));
                GetFilesNum(foldDir);
                this.ui.Invoke(new VoidDelegate(delegate()
                {
                    progress.Maximum = FileNum;
                    timer.Enabled = true;
                }));
                Regex FileType;
                if (languageType == "C++")
                {
                    FileType = Cppfile;
                }
                else { FileType = Javafile; }
                string[] filenames = Directory.GetFileSystemEntries(foldDir);
                for (int i = 0; i < filenames.Length; i++)
                {
                    if (System.IO.File.Exists(filenames[i]))
                    {
                        if (FileType.IsMatch(filenames[i])&&!Otherfiles.IsMatch(filenames[i]))
                        {
                            try
                            {
                                FileStream aFile = new FileStream(filenames[i], FileMode.Open, FileAccess.Read);
                                StreamReader sr = new StreamReader(aFile, Encoding.GetEncoding("gb2312"));

                                Solution solution = new Solution();
                                solution.main(sr, filenames[i], ui, languageType, foldDir.Length);
                                sr.Close();
                                this.ui.Invoke(new VoidDelegate(delegate()
                                {
                                    progress.Value++;
                                }));
                            }
                            catch
                            {
                                this.ui.Invoke(new VoidDelegate(delegate()
                                {
                                    progress.Value++;
                                }));
                            }
                        }
                    }
                    else
                    {
                        CheckLittle(filenames[i], foldDir.Length);
                    }
                }
            }

            //获得文件夹内文件的总个数
            private void GetFilesNum(string fileDir)
            {
                Regex FileType;
                if (languageType == "C++")
                { FileType = Cppfile; }
                else { FileType = Javafile; }
                string[] filenames = Directory.GetFileSystemEntries(fileDir);
                for (int i = 0; i < filenames.Length; i++)
                {
                    if (System.IO.File.Exists(filenames[i]))
                    {
                        if (FileType.IsMatch(filenames[i]) && !Otherfiles.IsMatch(filenames[i]))
                        {
                            this.ui.Invoke(new VoidDelegate(delegate()
                            {
                                FileNum++;
                            }));
                        }
                    }
                    else
                    {
                        GetFilesNum(filenames[i]);
                    }
                }
            }

            public void CheckLittle(string File,int length)
            {
                Regex FileType;
                if (languageType == "C++")
                {
                    FileType = Cppfile;
                }
                else { FileType = Javafile; }
                string[] filenames = Directory.GetFileSystemEntries(File);
                for (int i = 0; i < filenames.Length; i++)
                {
                    if (System.IO.File.Exists(filenames[i]))
                    {
                        if (FileType.IsMatch(filenames[i]) && !Otherfiles.IsMatch(filenames[i]))
                        {
                            try
                            {
                                FileStream aFile = new FileStream(filenames[i], FileMode.Open, FileAccess.Read);
                                StreamReader sr = new StreamReader(aFile, Encoding.GetEncoding("gb2312"));

                                Solution solution = new Solution();
                                solution.main(sr, filenames[i], ui, languageType, length);
                                sr.Close();
                                this.ui.Invoke(new VoidDelegate(delegate()
                                {
                                    progress.Value++;
                                }));
                            }
                            catch
                            {
                                this.ui.Invoke(new VoidDelegate(delegate()
                                {
                                    progress.Value++;
                                }));
                            }
                        }
                    }
                    else
                    {
                        CheckLittle(filenames[i], length);
                    }
                }
            }
        }

        //点击某项 根据路径打开某文件
        private void openfile(object sender, MouseEventArgs e)
        {
            string temp = (sender as NewListView).Name.Substring((sender as NewListView).Name.Length - 1);
            ListViewItem sele = errorTypeForCpp.ToArray()[(int.Parse(temp) - 1)].SelectedItems[0];


            System.Diagnostics.Process.Start(sele.SubItems[2].Text);

            SendKeys.Send("^g");
            SendKeys.Send(sele.SubItems[0].Text.Substring(0, sele.SubItems[0].Text.Length - 1));
            SendKeys.Send("{ENTER}");
        } 

        private void 错误类型ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            errorSelect errorselect = new errorSelect(this);
            errorselect.ShowDialog();
        }

        private void 审核ToolStripMenuItem_Click(object sender, EventArgs e)
        {

                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "请选择文件路径";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.listview7.Items.Clear();
                    this.listview1.Items.Clear();
                    this.listview2.Items.Clear();
                    this.listview3.Items.Clear();
                    this.listview5.Items.Clear();
                    this.listview6.Items.Clear();
                    BackThread.foldDir = dialog.SelectedPath;
                    BackThread.languageType = "C++";
                    FileNum = 0;
                    progress.Value = 0;
                    progress.Minimum = 0;
                    filenum.Text = "正在统计文件个数 稍等";
                    flag = 1;
                    main = new Thread(new ThreadStart(BackThread.Check));
                    main.Start();
                    暂停ToolStripMenuItem.Enabled = true;
                    审核ToolStripMenuItem.Enabled = false;
                    停止ToolStripMenuItem.Enabled = true;
                    继续ToolStripMenuItem.Enabled = false;

                }
            //}
            //else { MessageBox.Show("请先选择游戏名和版本号"); }
      //  }
        }

        private void 暂停ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            main.Suspend();
            继续ToolStripMenuItem.Enabled = true;
            暂停ToolStripMenuItem.Enabled = false;
            停止ToolStripMenuItem.Enabled = true;
            审核ToolStripMenuItem.Enabled = false;
        }

        private void 继续ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            main.Resume();
            暂停ToolStripMenuItem.Enabled = true;
            审核ToolStripMenuItem.Enabled = false;
            停止ToolStripMenuItem.Enabled = true;
            继续ToolStripMenuItem.Enabled = false;
        }

        private void 停止ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                main.Abort();
                timer.Enabled = false;
                BackThread.FileNum = 0; 
                this.listview7.Items.Clear();
                this.listview1.Items.Clear();
                this.listview2.Items.Clear();
                this.listview3.Items.Clear();
                this.listview5.Items.Clear();
                this.listview6.Items.Clear();
                flag = 0;
                审核ToolStripMenuItem.Enabled = true;
                继续ToolStripMenuItem.Enabled = false;
                暂停ToolStripMenuItem.Enabled = false;
                停止ToolStripMenuItem.Enabled = false;
            }
        }

        private void 清空ToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            this.listview7.Items.Clear();
            this.listview1.Items.Clear();
            this.listview2.Items.Clear();
            this.listview3.Items.Clear();
            this.listview5.Items.Clear();
            this.listview6.Items.Clear();
        }






        //private void 输入目录远程审核ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    BackThread.foldDir = "\\172.19.10.117";
        //    if (cToolStripMenuItem1.Checked)
        //    { BackThread.languageType = "C++"; }
        //    else { BackThread.languageType = "Java"; }
        //    FileNum = 0;
        //    progress.Value = 0;
        //    progress.Minimum = 0;
        //    filenum.Text = "正在统计文件个数 稍等";
        //    flag = 1;
        //    main = new Thread(new ThreadStart(BackThread.Check));
        //    main.Start();

        //    FtpWebRequest ReqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://172.19.10.117"));
        //    ReqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
        //    ReqFTP.UseBinary = true;
        //    ReqFTP.Credentials = new NetworkCredential("huangzw", "hzwasdf");
        //    using (FtpWebResponse response = (FtpWebResponse)ReqFTP.GetResponse())
        //    {
        //        using (Stream FtpStream = response.GetResponseStream())
        //        {

        //        }
        //        response.Close();
        //    }
        //}
    }
}
