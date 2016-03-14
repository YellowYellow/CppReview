
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic; 
using System.Text;

namespace CheckReview.StateForCpp
{
    //检查数组的界限是否被保护状态
    public class ArrayOutOfIndexCheckStateForCpp : AbstractStateForCpp
    {
        public ArrayOutOfIndexCheckStateForCpp(AbstractStateForCpp state)
        {
            now = TempIsworkStartRow;  
            this.so = state.so; 
        }

        //确定数组界限是否被判断
        public bool IsArrayIndexIsJudge(string strRead)
        {
            ArrayIsCheck = new Regex(@"(if|for|while)[\s]*[(](.*)" + @TempArray);
            if (ArrayIsCheck.IsMatch(strRead))
            {
                return true;
            }
            return false;
        }

        public override void Process()
        {
            if (now < TempRow)
            {
                if (IsArrayIndexIsJudge(strRead[now]))
                {
                    TempArray = "";
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
                so.State = new PrintStateForCpp(this, 5);
            }
        }
    }

}
