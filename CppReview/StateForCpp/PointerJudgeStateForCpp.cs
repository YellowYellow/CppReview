using System;
using System.Collections.Generic; 
using System.Text;  
using System.Text.RegularExpressions;

namespace CheckReview.StateForCpp
{
    //核对指针是否检查状态
    public class PointerJudgeStateForCpp : AbstractStateForCpp
    {
        public PointerJudgeStateForCpp(AbstractStateForCpp state)
        {
            this.so = state.so;
        }

        public int flag = 0;

        public void CheckPointer()
        {
            Regex pointer = new Regex(@TempPointer);
            if (Judge1.IsMatch(strRead[now]) || Judge2.IsMatch(strRead[now]) || Judge3.IsMatch(strRead[now]) || Judge4.IsMatch(strRead[now]))
            {
                while (true)
                {
                    if (pointer.IsMatch(strRead[now]))
                    { flag = 1; }
                    if (right.IsMatch(strRead[now]))
                    { break; }
                    now++;
                }
            }
        }

        public override void Process()
        {
            BraceJudge(strRead[now]);
            if (Iswork <= 0)//退出审核状态 发现指针没检查
            {
                so.State = new PrintStateForCpp(this, 1);
            }
            else
            {
                CheckPointer();
                if (flag == 1)
                {
                    so.State = new FreeStateForCpp(this);
                    flag = 0;
                    Back();
                }
                else { now++; }
            }
        }

    }
   
}
