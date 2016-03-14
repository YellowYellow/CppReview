using System;
using System.Collections.Generic; 
using System.Text; 
using System.Text.RegularExpressions;

namespace CheckReview.StateForCpp
{

    //核对string变量index是否保护 检查状态
    public class StringValvarStateForCpp : AbstractStateForCpp
    {
        public StringValvarStateForCpp(AbstractStateForCpp state)
        {
            now = TempIsworkStartRow; 
            this.so = state.so; 
        }

        //确定数组界限是否被判断
        public bool IsStringValIndexIsJudge(string strRead)
        {
            Regex StringValIsCheck1 = new Regex(@"(if)[\s]*[(](.*)(" + @TempStringValvar + ")");
            Regex StringValIsCheck2 = new Regex(@"(for)[\s]*[(](.*)(" + @TempStringValIndex + ")");
            if (StringValIsCheck1.IsMatch(strRead) || StringValIsCheck2.IsMatch(strRead))
            {
                return true;
            }
            return false;
        }

        public override void Process()
        {
            if (now < TempRow)
            {
                if (IsStringValIndexIsJudge(strRead[now]))
                {
                    TempStringValvar = "";
                    TempStringValIndex = "";
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
                so.State = new PrintStateForCpp(this, 9);
            }
        }
    }
   
}
