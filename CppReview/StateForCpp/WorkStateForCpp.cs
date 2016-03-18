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
                if (Iswork <= 0)//退出审核状态
                {
                    so.State = new FreeStateForCpp(this);
                }
                else
                {
                    TempArray = SearchArray(strRead[now]);
                    TempPointer = SearchPointer(strRead[now]);
                    TempIter = SearchIter(strRead[now]); 
                    TempMapvar = SearchMapVar(strRead[now]);
                    if (ui.flags.pointerFlag == 1 && TempPointer != "" && !others.IsMatch(strRead[now]))
                    {
                        TempIswork = Iswork;
                        TempRow = now;
                        so.State = new PointerJudgeStateForCpp(this);
                    } 
                    else if (VectorErase.IsMatch(strRead[now]) && !NoVec.IsMatch(strRead[now]))
                    {
                        so.State = new PrintStateForCpp(this, 16);
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
                    else if (ui.flags.mapFlag == 1 && TempMapvar != "")
                    {
                        TempIswork = Iswork;
                        TempRow = now;
                        so.State = new MapCheckStateForCpp(this);
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
            if (temp.Length >= 2)
            {
                Regex str = new Regex("[A-Za-z_0-9]+");
                Match mc;
                mc = str.Match(temp[1]);
                return mc.Value.Trim();
            }
            else { return ""; }
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
