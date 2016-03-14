using System;
using System.Collections.Generic; 
using System.Text; 
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;


namespace CheckReview
{
    /*enum ERROR_TYPE : int
    {
        POINTER_NOT_CHECK = 1,
        LOOP_NOT_PROTECT = 2,
    };*/

    public class Solution
    {
        public delegate void VoidDelegate();
        public AbstractState State;
        public string filename; 
        public string GameName; 
        public string GameVersion; 
        //审核主程序
        public void main(StreamReader file, string filename,  Form1 form1 , string type,int filelength)
        {
            this.GameName = Form1.GameName;
            this.GameVersion = Form1.GameVersion;
            this.filename = filename; 
            AbstractState.strRead = new List<string>();
            AbstractState.filelength = filelength;
            string strLine = file.ReadLine();
            
            //读取文件内容
            while (strLine != null)
            {
                AbstractState.strRead.Add(strLine);
                strLine = file.ReadLine();
            }
       //选择状态机开始工作
            if (type == "C++")
            {
                State = new StateForCpp.FreeStateForCpp(this, form1);
                while (AbstractState.now < AbstractState.strRead.Count)
                {
                    State.Process();
                }
            } 
 
        }
    }

    public abstract class AbstractState
    {
        public static int TempIsworkStartRow;
        public static int Iswork;
        public static int TempRow;
        public static string TempPointer;
        public static string TempArray;
        public static string TempIter;
        public static int TempIswork;
        public static int WorkEnterFlag;
        public static string TempStringValvar;
        public static string TempStream;
        public static int LoopCheckFlag;
        public static int SBraceFlag;
        public static string TempStringValIndex;
        public static string TempMapvar;
        public static string TempStruct;
        public static int filelength;

        public delegate void VoidDelegate();  
        public static List<string> strRead;
        public static ListViewItem lvi;
        public static Form1 ui;
        public static Regex right = new Regex(@"[)]");
        public static int now;
        public static Regex Start = new Regex(@"[{]");
        public static Regex End = new Regex(@"[}]");
        public static Regex BNoteStart = new Regex(@"([/][*])");
        public static Regex BNoteEnd = new Regex(@"([*][/])");
        public static Regex IF = new Regex(@"[\s]+(if)[\s]*"); 
        public static Regex ArrayMaybeOutOfIndex = new Regex(@"[a-zA-Z]+\[[a-hk-z0-9][a-zA-Z]*\]");
        public static Regex ArrayMaybeOutOfIndexOthers = new Regex(@"len|index|size|Index|Size|Len");
        public static Regex MagicNum = new Regex(@"[1-9][0-9]+");
        //public static Regex route = new Regex(@"(FsGame|FsAuctionLogic|FsVoiceLogic|FsSnsLogic|FsDumpLogic|FsRoomLogic|FsPublogic|FsPlatformLogic|fm_fmod|fm_game|fm_gui|fm_main|fm_movie_m|fm_snaileditor|fm_stublogic|fm_tool|fm_ui3d|utils|define|public).*");
        public static Regex Jroute = new Regex(@"(DSG-Robot|Room-Fight|Room-Manage|RPG-Common|RPG-Fight|RPG-Game|RPG-Gate|RPG-Mail|RPG-Patch).*");
 


        //大括号的判断
        public abstract void BraceJudge(string strRead);

        //回退
        public void Back()
        {
            now = TempRow;
            Iswork = TempIswork;
        }

        //自动忽略小段注释部分
        public string SearchSmallNotes(string strRead)
        {
            string replacement = " ";
            string SNote = "[/][/].*";
            return Regex.Replace(strRead, SNote, replacement);
        }

        //自动忽略大段注释部分
        public bool SearchBigNotesStart(string strRead)
        {
            if (BNoteStart.IsMatch(strRead))
            { return true; }
            else { return false; }
        }

        //获取附近的代码
        public string GetCodeAround(int temprow)
        {
            string temp = "";
            for (int i = 5; i > 0 ; i--)
            {
                temp += "        " + (temprow - i).ToString() + strRead[temprow - i] + "\n";
            }
            for (int j = 0; j < 6; j++)
            {
                if (j == 0)
                {
                    temp += "错误行：" + (temprow + j).ToString() + strRead[temprow + j] + "\n";
                }
                else
                {
                    temp += "        " + (temprow + j).ToString() + strRead[temprow + j] + "\n";
                }
            }
            return temp;
        }

        //获得正确的路径
        public string GetRoute(string file)
        { 
            return file.Substring(filelength); 
        }

        //获得正确的路径
        public string JGetRoute(string file)
        {
            Match rou;
            rou = Jroute.Match(file);
            return rou.Value.Trim();
        }

        public abstract void Process();

    }
  
  
}
