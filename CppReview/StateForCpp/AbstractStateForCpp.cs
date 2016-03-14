using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CheckReview.StateForCpp
{

    public abstract class AbstractStateForCpp : AbstractState
    {
        public Solution so;
        public static Regex Pointer = new Regex(@"[\s]*[A-Za-z^char)]+[\s]*[*][\s]*[A-Za-z_]+[\s]*=");
        public static Regex SelfInstancePointer = new Regex(@"Instance()->[a-z][a-zA-Z_]*");
        public static Regex StringValVar = new Regex(@"[A-Za-z]+[A-Za-z_0-9]*[.]StringVal[(][A-Za-z0-9]+[A-Za-z_0-9]*[)]");

        public static Regex Judge1 = new Regex(@"if[\s]*[(](.*)");
        public static Regex Judge2 = new Regex(@"while[\s]*[(](.*)");
        public static Regex Judge3 = new Regex(@"NULL_RETURN_VOID[\s]*[(](.*)");
        public static Regex Judge4 = new Regex(@"NULL_RETURN[\s]*[(](.*)");
        public static Regex Judge5 = new Regex(@"for[\s]*[(](.*)");
        public static Regex Judge6 = new Regex(@"ARGS_ERROR_RETURN_0(.*)");
        public static Regex others = new Regex("char|pKernel|new|NULL|NEW|null|const|TiXmlElement");
        public static Regex LoopStart = new Regex(@"LoopBeginCheck");
        public static Regex LoopCheck = new Regex(@"LoopDoCheck");
        public static Regex LoopFor = new Regex(@"[\s]*for[\s]*[(]");
        public static Regex LoopWhile = new Regex(@"[\s]*while[\s]*[(]");
        public static Regex LoopError = new Regex(@"while[(][0-9][)]");
        public static Regex ObjectLeek1 = new Regex(@"[\s]*pKernel[\s]*->[\s]*CreateFromConfigArgs[(]");
        public static Regex ObjectLeek2 = new Regex(@"[\s]*pKernel[\s]*->[\s]*CreateFromConfig[(]");
        public static Regex ObjectLeek3 = new Regex(@"[\s]*pKernel[\s]*->[\s]*Create[(]");
        public static Regex ObjectLeek4 = new Regex(@"[\s]*pKernel[\s]*->[\s]*CreateArgs[(]");
        public static Regex ObjectLeekCheckRight1 = new Regex(@"[(][\s]*pKernel[\s]*->[\s]*GetScene[(]");
        public static Regex ObjectLeekCheckRight2 = new Regex(@"[(][\s]*scene");
        public static Regex ArrayIsCheck;
        public static Regex Iterator = new Regex(@"iterator.*=");
        public static Regex Sprint = new Regex(@"extend_warning|WriteInfo");
        public static Regex Print = new Regex("[\"].*[\"]");
        public static Regex DoubleYin = new Regex("\"[,](.*)[)][;]");
        public static Regex var = new Regex(@"[A-Za-z]+[A-Za-z_0-9]*");
        public static Regex MapFind = new Regex(@"map.*\[");
        public static Regex sprintf = new Regex(@"sprintf|swprintf");
        public static Regex va_start = new Regex(@"va_start");
        public static Regex constchar = new Regex(@"const[\s]*char*[\s]* .*[\:][\:].*[(].*[)]");
        public static Regex Mem = new Regex(@"memset|memmove|memcpy");
        public static Regex StructCheck = new Regex(@"struct[\s]*[a-zA-Z]+");
        public static Regex Semicolon = new Regex("[;]");
        public static Regex VectorErase = new Regex(@"(erase)[(]");
        public static Regex NoVec = new Regex(@"(map)");
        public static Regex SwitchCheckBegin = new Regex(@"(switch)[\s]*[(]");
        public static Regex SwitchCheckBeginOthers = new Regex(@"{");

        public static Regex FormatVal = new Regex(@"%[a-z]");
        public static Regex StringFormatVal = new Regex(@"char|string");
        public static Regex IntFormatVal = new Regex(@"(int)");
        public static Regex FloatFormatVal = new Regex(@"(float)");

        public static Regex Division = new Regex(@"/");
        public static Regex Division2 = new Regex(@"%");
        public static Regex DivisionOthers = new Regex(@"%(s|f|I|\.|#|l|u|d|p)");
        public static Regex Equal = new Regex(@"=");
        public static Regex Equals = new Regex(@"(==|>=|<=|!=)");
        public static Regex AllocMalloc = new Regex(@"([\s]+new[\s]+|[\s]+malloc[\s]+)");
        public static Regex Stlat = new Regex(@"[.]at[(]");

        public static Regex IteratorIsChecked;

        public override void BraceJudge(string strRead)
        {
            if (Start.IsMatch(strRead))
            {
                Iswork++;
            }
            if (End.IsMatch(strRead))
            {
                Iswork--;
            }
        }
    }

}
