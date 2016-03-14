using System;
using System.Collections.Generic;
using System.Text; 
using System.Text.RegularExpressions;

namespace CheckReview.StateForCpp
{
    class SwitchCheckStateForCpp : AbstractStateForCpp
    {
        public SwitchCheckStateForCpp(AbstractStateForCpp state)
        { 
            this.so = state.so; 
        }

        int flag = 0;

        public void CheckSwitch()
        {
            Regex Case = new Regex(@"case");
            if (Case.IsMatch(strRead[now]))
            { flag = 1; }  
        }

        public override void Process()
        {
            if (Start.IsMatch(strRead[now]))
            {
                Iswork++;
            }
            if (End.IsMatch(strRead[now]))
            {
                Iswork--;
            } 
            if (Iswork == TempIswork) 
            {
                so.State = new PrintStateForCpp(this, 17);
            }
            else
            {
                CheckSwitch();
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
