using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace CheckReview.StateForCpp
{
    class StructCheckStateForCpp : AbstractStateForCpp
    {
        public StructCheckStateForCpp(AbstractStateForCpp state)
        {
            this.so = state.so;
        }

        int flag = 0;

        public void CheckStruct()
        {
            Regex Creator = new Regex(@TempStruct);
            if (Creator.IsMatch(strRead[now]))
            { flag = 1; }
        }

        public override void Process()
        {
            BraceJudge(strRead[now]);
            if (Iswork <= 0)//退出审核状态 结构体没构造器
            {
                so.State = new PrintStateForCpp(this, 15);
                TempStruct = "";
                Back();
            }
            else
            {
                CheckStruct();
                if (flag == 1)
                {
                    so.State = new FreeStateForCpp(this);
                    flag = 0;
                    TempStruct = "";
                    Back();
                }
                else { now++; }
            }
        }
    }
}
