using System;
using System.Collections.Generic; 
using System.Text;
using System.Text.RegularExpressions;

namespace CheckReview.StateForCpp
{

    //大段注释部分状态
    public class BigNotesStateForCpp : AbstractStateForCpp
    {
        public BigNotesStateForCpp(AbstractStateForCpp state)
        { 
            this.so = state.so; 
        }

        //寻找大段注释结尾
        public bool SearchBigNoteEnd(string strRead)
        {
            if (BNoteEnd.IsMatch(strRead))
            { return true; }
            else { return false; }
        }

        public override void Process()
        {
            if (SearchBigNoteEnd(strRead[now]))
            {
                so.State = new FreeStateForCpp(this);
            }
            else
            {
                now++;
            }
        }
    }

}
