using System;

namespace AssLineParserNET
{
    public class Line
    {
        public String Format = String.Empty;
        public Int32 Layer = 0;
        public TimeSpan Start = new TimeSpan(0, 0, 0);
        public TimeSpan End = new TimeSpan(0, 0, 0);
        public String Style = String.Empty;
        public String Actor = String.Empty;
        public Int32 MarginL = 0;
        public Int32 MarginR = 0;
        public Int32 MarginV = 0;
        public String Effect = String.Empty;
        public String Text = String.Empty;
        public String LineWithoutText = String.Empty;

        public Line(String AssLine)
        {
            Parser(AssLine);
        }

        public String BuildNewLine(TimeSpan NewStart, TimeSpan NewEnd, Boolean Dialogue = true, Int32 NewLayer = 0, String NewStyle = "", String NewActor = "", Int32 NewMarginL = 0, Int32 NewMarginR = 0, Int32 NewMarginV = 0, String NewEffect = "", String NewText = "", String NewTextComment="")
        {
            String NewLine = String.Empty;

            if (Dialogue == true)
            {
                NewLine += "Dialogue: ";
            }
            else
            {
                NewLine += "Comment: ";
            }

            NewLine += NewLayer.ToString() + ",";

            NewLine += NewStart.ToString().Substring(1, NewStart.ToString().Length - 1) + ",";

            NewLine += NewEnd.ToString().Substring(1, NewEnd.ToString().Length - 1) + ",";

            NewLine += NewStyle + ",";

            NewLine += NewActor + ",";

            NewLine += NewMarginL.ToString() + ",";

            NewLine += NewMarginR.ToString() + ",";

            NewLine += NewMarginV.ToString() + ",";

            NewLine += NewEffect + ",";

            NewLine += NewText;

            if (NewTextComment != String.Empty)
            {
                NewLine += " {" + NewTextComment + "}";
            }

            NewLine = NewLine.Trim();

            return NewLine;
        }

        protected void Parser(String AssLine)
        {
            Text = RemoveFromZeroToLastComma(AssLine);

            if (String.IsNullOrWhiteSpace(Text) == false)
                LineWithoutText = AssLine.Replace(Text, String.Empty).Trim(',');
            else
                LineWithoutText = Text;

            String Syntax = RemoveFromLastComma(AssLine);

            Format = Syntax.Split(':')[0].Trim();

            Layer = Convert.ToInt32(Syntax.Split(',')[0].Split(':')[1].Trim());

            Start = TimeSpan.Parse("0" + Syntax.Split(',')[1].Trim());
            End = TimeSpan.Parse("0" + Syntax.Split(',')[2].Trim());

            Style = Syntax.Split(',')[3].Trim();

            Actor = Syntax.Split(',')[4].Trim();

            MarginL = Convert.ToInt32(Syntax.Split(',')[5].Trim());
            MarginR = Convert.ToInt32(Syntax.Split(',')[6].Trim());
            MarginV = Convert.ToInt32(Syntax.Split(',')[7].Trim());

            Effect = Syntax.Split(',')[8].Trim();
        }

        protected Int32 FindLastComma(String s)
        {
            Int32 i = 0;
            Int32 Commas = 0;
            for (i = 0; i < s.Length; i++)
            {
                if (s[i] == ',')
                {
                    Commas++;
                }
                if (Commas == 9)
                {
                    break;
                }
            }
            return i;
        }

        public String RemoveFromZeroToLastComma(String s)
        {
            return s.Remove(0, FindLastComma(s) + 1);
        }

        public String RemoveFromLastComma(String s)
        {
            return s.Remove(FindLastComma(s));
        }
    }
}
