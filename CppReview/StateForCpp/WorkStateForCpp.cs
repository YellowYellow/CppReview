using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CheckReview.StateForCpp
{
    //审核状态 
    public class WorkStateForCpp : AbstractStateForCpp
    {
        public WorkStateForCpp(AbstractStateForCpp state)
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
                BraceJudge(strRead[now]);
                SearchLoop(strRead[now]);
                if (Iswork <= 0)//退出审核状态
                {
                    so.State = new FreeStateForCpp(this);
                }
                else
                {
                    TempArray = SearchArray(strRead[now]);
                    TempPointer = SearchPointer(strRead[now]);
                    TempIter = SearchIter(strRead[now]);
                    TempStringValvar = SearchStringVal(strRead[now]);
                    TempMapvar = SearchMapVar(strRead[now]);
                    if (ui.flags.pointerFlag == 1 && TempPointer != "" && !others.IsMatch(strRead[now]))
                    {
                        TempIswork = Iswork;
                        TempRow = now;
                        so.State = new PointerJudgeStateForCpp(this);
                    }
                    else if ((ui.flags.loopFlag == 1) && ((LoopFor.IsMatch(strRead[now]) || LoopWhile.IsMatch(strRead[now])) && !LoopError.IsMatch(strRead[now]) && LoopCheckFlag != 1))
                    {
                        so.State = new PrintStateForCpp(this, 2);
                    }
                    else if (VectorErase.IsMatch(strRead[now]) && !NoVec.IsMatch(strRead[now]))
                    {
                        so.State = new PrintStateForCpp(this, 16);
                    }
                    else if ((ui.flags.objectleekFlag == 1) && (ObjectLeek1.IsMatch(strRead[now]) || ObjectLeek2.IsMatch(strRead[now]) || ObjectLeek3.IsMatch(strRead[now]) || ObjectLeek4.IsMatch(strRead[now])))
                    {
                        if (ObjectLeekCheckRight1.IsMatch(strRead[now]) || ObjectLeekCheckRight2.IsMatch(strRead[now]))
                        { }
                        else
                        {
                            so.State = new PrintStateForCpp(this, 3);
                        }
                    }
                    else if ((ui.flags.numFlag == 1) && IF.IsMatch(strRead[now]) && MagicNum.IsMatch(strRead[now]))
                    {
                        so.State = new PrintStateForCpp(this, 4);
                    }
                    else if ((ui.flags.arrayFlag == 1) && ArrayMaybeOutOfIndex.IsMatch(strRead[now]) && !ArrayMaybeOutOfIndexOthers.IsMatch(strRead[now]) && Iswork == 1 && TempArray != "")
                    {
                        TempIswork = Iswork;
                        TempRow = now;
                        so.State = new ArrayOutOfIndexCheckStateForCpp(this);
                    }
                    else if (ui.flags.iterFlag == 1 && Iterator.IsMatch(strRead[now]) && !IF.IsMatch(strRead[now]) && !LoopFor.IsMatch(strRead[now]) && TempIter != "")
                    {
                        TempIswork = Iswork;
                        TempRow = now;
                        so.State = new IteraterCheckStateForCpp(this);
                    }
                    else if (ui.flags.formatFlag == 1 && Sprint.IsMatch(strRead[now]))
                    {
                        TempIswork = Iswork;
                        TempRow = now;
                        so.State = new FormatCheckStateForCpp(this);


                        //Regex Others = new Regex(@"[(][\s]*[a-zA-Z_]+[\s]*,[\s]*[a-zA-Z_]+[\s]*[)]");
                        //string[] temp = strRead[now].Split('%');
                        //Match va;
                        //Regex Brace = new Regex("[(].*[)]");
                        //va = DoubleYin.Match(strRead[now]);
                        //string[] temp2 = va.Value.Trim().Split(',');
                        //string temp3 = va.Value.Trim();
                        //MatchCollection a;
                        //a = Others.Matches(temp3);
                        //if (temp.Length != (temp2.Length - a.Count))
                        //{
                        //    so.State = new PrintStateForCpp(this, 7);
                        //}
                    }
                    else if (SelfInstancePointer.IsMatch(strRead[now]))
                    {
                        so.State = new PrintStateForCpp(this, 8);
                    }
                    else if (ui.flags.mapFlag == 1 && TempMapvar != "")
                    {
                        TempIswork = Iswork;
                        TempRow = now;
                        so.State = new MapCheckStateForCpp(this);
                    }
                    else if (ui.flags.sprintfFlag == 1 && sprintf.IsMatch(strRead[now]))
                    {
                        so.State = new PrintStateForCpp(this, 11);
                    }
                    else if (ui.flags.vastartFlag == 1 && va_start.IsMatch(strRead[now]))
                    {
                        TempIswork = Iswork;
                        TempRow = now;
                        so.State = new VaCheckStateForCpp(this);
                    }
                    else if (Mem.IsMatch(strRead[now]))
                    {
                        TempIswork = Iswork;
                        TempRow = now;
                        so.State = new PrintStateForCpp(this, 14);
                    }
                    else if (SwitchCheckBegin.IsMatch(strRead[now]) && !SwitchCheckBeginOthers.IsMatch(strRead[now]))
                    {
                        TempIswork = Iswork;
                        TempRow = now;
                        so.State = new SwitchCheckStateForCpp(this);
                    }
                    else if ((Division.IsMatch(strRead[now]) || Division2.IsMatch(strRead[now])) && !DivisionOthers.IsMatch(strRead[now]) && !Print.IsMatch(strRead[now]))
                    {
                        TempIswork = Iswork;
                        TempRow = now;
                        so.State = new DivisionStateForCpp(this); //18
                    }
                    else if (IF.IsMatch(strRead[now]) && Equal.IsMatch(strRead[now]) && !Equals.IsMatch(strRead[now]))
                    {
                        TempIswork = Iswork;
                        TempRow = now;
                        so.State = new PrintStateForCpp(this,19); //18
                    }
                    else if (AllocMalloc.IsMatch(strRead[now]))
                    {
                        TempIswork = Iswork;
                        TempRow = now;
                        so.State = new PrintStateForCpp(this, 20); //18
                    }
                    else if (Stlat.IsMatch(strRead[now]))
                    {
                        TempIswork = Iswork;
                        TempRow = now;
                        so.State = new PrintStateForCpp(this, 21); //18
                    }
                }
            }
            now++;
        }

        //找map变量
        public string SearchMapVar(string strRead)
        {
            Match mc;
            if (MapFind.IsMatch(strRead))
            {
                mc = MapFind.Match(strRead);
                return getMapVar(mc.Value.Trim());
            }
            return "";
        }

        //找map变量名
        public string getMapVar(string map)
        {
            string[] temp = map.Split('[');
            Regex str = new Regex("[A-Za-z_]+");
            Match mc;
            mc = str.Match(temp[0]);
            return mc.Value.Trim();
        }

        //找指针
        public string SearchPointer(string strRead)
        {
            Match mc;
            if (Pointer.IsMatch(strRead) && !others.IsMatch(strRead))
            {
                mc = Pointer.Match(strRead);
                return getPointerVar(mc.Value.Trim());
            }
            return "";
        }

        //找LoopCheck
        public void SearchLoop(string strRead)
        {
            if (LoopStart.IsMatch(strRead))
            {
                LoopCheckFlag++;
            }
            if (LoopCheck.IsMatch(strRead))
            {
                LoopCheckFlag--;
            }
        }

        //找数组
        public string SearchArray(string strRead)
        {
            Match mc;
            if (ArrayMaybeOutOfIndex.IsMatch(strRead))
            {
                mc = ArrayMaybeOutOfIndex.Match(strRead);
                return getArrayIndex(mc.Value.Trim());
            }
            return "";
        }

        //找迭代器
        public string SearchIter(string strRead)
        {
            Match mc;
            if (Iterator.IsMatch(strRead))
            {
                mc = Iterator.Match(strRead);
                return getIterVar(mc.Value.Trim());
            }
            return "";
        }

        //找ValList变量名
        public string SearchStringVal(string strRead)
        {
            Match mc;
            if (StringValVar.IsMatch(strRead))
            {
                mc = StringValVar.Match(strRead);
                TempStringValIndex = getStringValIndex(mc.Value.Trim());
                return getStringVar(mc.Value.Trim());
            }
            return "";
        }

        //获得VarList变量名
        public String getStringVar(string StringVal)
        {
            string[] temp = StringVal.Split('.');
            Regex str = new Regex("[A-Za-z_]+");
            Match mc;
            mc = str.Match(temp[0]);
            return mc.Value.Trim();
        }

        //获得指针变量的名称
        public String getPointerVar(string Pointer)
        {
            string[] temp = Pointer.Split('*');
            Regex str = new Regex("[A-Za-z_]+");
            Match mc;
            mc = str.Match(temp[1]);
            return mc.Value.Trim();
        }

        //获得数组index值的名称
        public String getArrayIndex(string Array)
        {
            string[] temp = Array.Split('[');
            Regex str = new Regex("[A-Za-z_0-9]+");
            Match mc;
            mc = str.Match(temp[1]);
            return mc.Value.Trim();
        }

        //获得StringVal的index
        public String getStringValIndex(string StringVal)
        {
            string[] temp = StringVal.Split('(');
            Regex str = new Regex("[A-Za-z_0-9]+");
            Match mc;
            mc = str.Match(temp[1]);
            return mc.Value.Trim();
        }

        //获得迭代器变量名称
        public String getIterVar(string Iter)
        {
            string[] temp = Iter.Split(' ');
            Regex str = new Regex("[A-Za-z_]+");
            Match mc;
            mc = str.Match(temp[1]);
            return mc.Value.Trim();
        }


    }

}
