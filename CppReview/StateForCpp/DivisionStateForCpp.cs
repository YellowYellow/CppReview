using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CheckReview.StateForCpp
{
    class DivisionStateForCpp : AbstractStateForCpp
    {
        public DivisionStateForCpp(AbstractStateForCpp state)
        {
            this.so = state.so;
        }

        public static Regex val = new Regex(@"^([\s]*[a-zA-Z0-9.]+)");
        public static Regex num = new Regex(@"^([\s]*[0-9.]+)");
        public override void Process()
        {
            string[] temp = { };
            if (Division.IsMatch(strRead[now - 1]))
                temp = strRead[now - 1].Split('/');
            else if (Division2.IsMatch(strRead[now - 1]))
                temp = strRead[now - 1].Split('%');

            if (temp != null)
            {
                Match var = val.Match(temp[1]);
                if (num.IsMatch(var.Value.Trim()))
                {
                    if ((int)var.Value.Trim()[0] == 0)
                    {
                        so.State = new PrintStateForCpp(this, 18);
                    }
                    else
                    {
                        so.State = new FreeStateForCpp(this);
                    }
                }
                else if (var.Value.Trim() != "sizeof" && val.IsMatch(var.Value.Trim()) && temp.Length == 2)
                {
                    now = TempIsworkStartRow;
                    Regex tempRe = new Regex(var.Value.Trim());
                    while (now < TempRow)
                    {
                        if (tempRe.IsMatch(strRead[now]) && IF.IsMatch(strRead[now]))
                        { so.State = new FreeStateForCpp(this); break; }
                        now++;
                    }
                    if (so.State.ToString() != "Check2005.StateForCpp.FreeStateForCpp")
                    {
                        so.State = new PrintStateForCpp(this, 18);
                    }
                }
                else
                {
                    so.State = new FreeStateForCpp(this);
                }
                Back();
            }
        }
    }
}
