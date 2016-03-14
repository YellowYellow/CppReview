using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CheckReview.StateForCpp
{

    //空闲状态
    public class FreeStateForCpp : AbstractStateForCpp
    {
        public FreeStateForCpp(Solution so, Form1 form1)
        {
            this.so = so;
            ui = form1;
            now = 0;
            LoopCheckFlag = 0;
            WorkEnterFlag = 0;
            Iswork = 0;
            TempIswork = 0;
            TempPointer = "";
            TempRow = 0;
            TempArray = "";
            TempIter = "";
            TempIsworkStartRow = 0;
            TempStringValIndex = "";
            TempStringValvar = "";
            TempMapvar = "";
            TempStruct = "";
        }

        public FreeStateForCpp(AbstractStateForCpp state)
        {
            this.so = state.so;
        }

        public override void Process()
        {
            if (SearchBigNotesStart(strRead[now]))
            {
                so.State = new BigNotesStateForCpp(this);
            }
            else
            {
                strRead[now] = SearchSmallNotes(strRead[now]);

                if (Iswork < 0)//判断记录TempIsworkStartRow的时刻
                {
                    WorkEnterFlag = 1;
                }
                if (Start.IsMatch(strRead[now]))
                {
                    Iswork++;
                }
                if (End.IsMatch(strRead[now]))
                {
                    Iswork--;
                }
                if (Iswork > 0 && WorkEnterFlag == 1)//进入审核状态
                {
                    TempIsworkStartRow = now;
                }
                WorkEnterFlag = 0;

                TempStruct = SearchStruct(strRead[now]);
                if (Iswork > 0)//进入审核状态
                {
                    so.State = new WorkStateForCpp(this);
                }
                else if (constchar.IsMatch(strRead[now]))
                {
                    so.State = new PrintStateForCpp(this, 13);
                }
                else if (TempStruct != "")
                {
                    TempIswork = Iswork;
                    TempRow = now + 1;
                    so.State = new StructCheckStateForCpp(this);
                }
            }
            now++;
        }

        //获得结构体的名称
        public string SearchStruct(string strRead)
        {
            Match mc;
            if (StructCheck.IsMatch(strRead) && !Semicolon.IsMatch(strRead))
            {
                mc = StructCheck.Match(strRead);
                return getStructVar(mc.Value.Trim());
            }
            return "";
        }

        //获得结构体的名称
        public String getStructVar(string Struct)
        {
            string[] temp = Struct.Split(' ');
            Regex str = new Regex("[A-Za-z_]+");
            Match mc;
            mc = str.Match(temp[1]);
            return mc.Value.Trim();
        }

    }
}
