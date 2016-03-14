using System;
using System.Collections.Generic; 
using System.Text;
using System.Text.RegularExpressions;

namespace CheckReview.StateForCpp
{
    //检查迭代器是否被保护状态
    public class IteraterCheckStateForCpp : AbstractStateForCpp
    {
        public IteraterCheckStateForCpp(AbstractStateForCpp state)
        { 
            this.so = state.so; 
        }

        //确定迭代器是否被保护
        public bool IsIterIsJudge(string strRead)
        {
            IteratorIsChecked = new Regex(@"(if|while|for)[\s]*(.*" + @TempIter);
            if (IteratorIsChecked.IsMatch(strRead))
            {
                return true;
            }
            return false;
        }

        public override void Process()
        {
            BraceJudge(strRead[now]);
            if (Iswork <= 0)//退出审核状态 发现迭代器没检查
            {
                so.State = new PrintStateForCpp(this, 6);
            }
            else
            {
                if (IsIterIsJudge(strRead[now]))//迭代器已经被保护
                {
                    so.State = new FreeStateForCpp(this);
                    Back();
                }
                else
                {
                    now++;
                }
            }
        }
    }

}
