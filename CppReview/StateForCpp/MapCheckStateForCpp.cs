using System;
using System.Collections.Generic; 
using System.Text;
using System.Text.RegularExpressions;

namespace CheckReview.StateForCpp
{
    public class MapCheckStateForCpp : AbstractStateForCpp
    {
        public MapCheckStateForCpp(AbstractStateForCpp state)
        {
            now = TempIsworkStartRow;
            this.so = state.so; 
        }

        //确定Map界限是否被判断
        public bool IsMapIndexIsJudge(string strRead)
        {
            Regex MapIsCheck = new Regex(@"(if|for|while)[\s]*[(](.*)" + @TempMapvar);
            if (MapIsCheck.IsMatch(strRead))
            {
                return true;
            }
            return false; 
        }

        public override void Process() 
        {
            if (now < TempRow)
            {
                if (IsMapIndexIsJudge(strRead[now]))
                {
                    TempMapvar = "";
                    so.State = new FreeStateForCpp(this);
                    Back();
                }
                else
                {
                    now++;
                }
            }
            else
            {
                so.State = new PrintStateForCpp(this, 10);
            }
        }
    }
}
