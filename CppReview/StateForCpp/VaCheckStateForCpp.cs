using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CheckReview.StateForCpp
{
    class VaCheckStateForCpp : AbstractStateForCpp
    {
        public VaCheckStateForCpp(AbstractStateForCpp state)
        { 
            this.so = state.so; 
        }

        public int flag = 0;

        public void CheckVa()
        {
            Regex va_end = new Regex(@"va_end");  
            if (va_end.IsMatch(strRead[now]))
            { flag = 1; }  
        }

        public override void Process()
        {
            BraceJudge(strRead[now]);
            if (Iswork <= 0)//退出审核状态 发现指针没检查
            {
                so.State = new PrintStateForCpp(this, 12);
            }
            else
            {
                CheckVa();
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
