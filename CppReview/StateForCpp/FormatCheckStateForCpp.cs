using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;

namespace CheckReview.StateForCpp
{
    class FormatCheckStateForCpp : AbstractStateForCpp
    {
        public FormatCheckStateForCpp(AbstractStateForCpp state)
        {
            now = TempIsworkStartRow - 1;
            this.so = state.so;
        }

        //确定变量格式是否正确
        public bool IsFormatRight(string str, Regex format)
        {
            Regex val = new Regex(@str);
            while (true)
            {
                if (now < TempRow)
                {
                    if (val.IsMatch(strRead[now]))
                    {
                        if (format.IsMatch(strRead[now]))
                        {
                            now = TempIsworkStartRow - 1;
                            return true;
                        }
                    }
                    now++;
                }
                else { break; }
            }
            now = TempIsworkStartRow - 1;
            return false;
        }

        public override void Process()
        {
            MatchCollection list;
            Regex temp1 = new Regex(@"[)];");
            string content = "";
            int count = 0;
            while (true)
            {
                content = content + strRead[TempRow + count];
                if (temp1.IsMatch(strRead[TempRow + count]))
                {
                    break;
                }
                count++;
            }

            Match other;
            other = Print.Match(content);
            string[] error = other.Value.Trim().Split(',');
            string[] temp = content.Split(',');
            list = FormatVal.Matches(content);

            Regex val = new Regex(@"[a-zA-Z_]+");
            for (int i = 0; i < list.Count; i++)
            {
                Match tempMatch = (Match)list[i];
                MatchCollection valname = val.Matches(temp[i + 1 + error.Length]);
                if (valname.Count != 1)
                {
                    continue;
                }
                string valuename = valname[0].Value.Trim();

                if (tempMatch.Value.Trim() == "%s")
                {
                    if (IsFormatRight(valuename, StringFormatVal))
                    {
                        continue;
                    }
                    else
                    {
                        so.State = new PrintStateForCpp(this, 7);
                    }
                }
                else if (tempMatch.Value.Trim() == "%d")
                {
                    if (IsFormatRight(valuename, IntFormatVal))
                    {
                        continue;
                    }
                    else
                    {
                        so.State = new PrintStateForCpp(this, 7);
                    }
                }
                else if (tempMatch.Value.Trim() == "%f")
                {
                    if (IsFormatRight(valuename, FloatFormatVal))
                    {
                        continue;
                    }
                    else
                    {
                        so.State = new PrintStateForCpp(this, 7);
                    }
                }
            }

            if (so.State.ToString() != "Check2005.StateForCpp.PrintStateForCpp")
            { so.State = new FreeStateForCpp(this); }
            Back();
        }
    }
}
