using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparta_TextRpg
{
    internal class Utility
    {

        public static void ShowTite()
        {
            Console.WriteLine("□■■■■■■■□■□□■■■■■■□□■■□□□□■■■■■■■■■□□□□□■□□□□□■□□■■■■■■□□□■□□■■■■■■■□□■");
            Console.WriteLine("□■□□■■□□□■□□□□□□□■□□■■□□□□■■□□□□□■■□□■■■■■■■□□■□□■■■■■■□□□■□□■■■■■■■□□■");
            Console.WriteLine("□■□□■■□□□■■■□□□□□■□□■■□□□□■■□□□□□■■□□□□■■■□□□□■□□■□□□□□□□□■□□□□□■□□□□□■");
            Console.WriteLine("□■□□■■□□□■□□□□□□■■□□■■■■□□■■□□□□□■■□□□■■■■■□□□■■■■□□□□■■■■■□□□□■■□□■■■■");
            Console.WriteLine("□■■■■■■■■■□□□□□■■■□□■■□□□□■■■■■■■■■□□■■□□□■■□□■■■■□□□□□□□□■□□□■■■■□□□□■");
            Console.WriteLine("□□□□□□□□□■□□□■■■□□□□■■□□□□□□□□□□□□□□□■■□□□■■□□■□□■■■■■■■■□■□□■■■□■■■□□■");
            Console.WriteLine("□□■■■■■■■■□□■■□□□□□□□■□□□□□□□□□□□□□□□□■■■■■□□□■□□■■■■■■■□□■□■■■□□□■■□□■");
            Console.WriteLine("□□□□□□□□□■□□□□■■■■■■■■□□■■■■■■■■■■■■□□□■■■□□□□■□□□□■□□□□□□■□□□□■□□□□□□■");
            Console.WriteLine("□□□□□□□□□■□□□□□□□□□□□■□□□□□□□□■□□□□□□□□■□□□□□□■□□□□■□□□□□□■□□□□■□□□□□□■");
            Console.WriteLine("□□□■■■■■■■□□□□□□□□□□□■□□□□□□□□■□□□□□□□□■□□□□□□□□□□□■□□□□□□□□□□□■□□□□□□□");
            Console.WriteLine("□□□■□□□□□□□□□□□□□□□□□■□□□□□□□□■□□□□□□□□■□□□□□□□□□□□■□□□□□□□□□□□■□□□□□□□");
            Console.WriteLine("□□□■■■■■■■■□□□□□□□□□□■□□□□□□□□■□□□□□□□□■■■■■■■■□□□□■■■■■■■■□□□□■■■■■■■■");
        }

        public static void ShowStartLogo()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("                       !  .            :                ");
            Console.WriteLine("                       $  =           .:                ");
            Console.WriteLine("                       ;#, $          .;                ");
            Console.WriteLine("                       #@; =          .*             ");
            Console.WriteLine("                      .#@* #          .=.               ");
            Console.WriteLine("                      !@@@;@~         :@~           ");
            Console.WriteLine("                      *@@@@@=         @@;               ");
            Console.WriteLine("                     ~@@@@@@@~        @@@            ");
            Console.WriteLine("            $@#-     ;@@@@@@$,       $@@@:          ");
            Console.WriteLine("            ,        :@@@@@@#~       $@@@:              ");
            Console.WriteLine("           ;@=       ~@@@@@@$.       $@@@:              ");
            Console.WriteLine("          ~@@@*      ~@@@@@@#!.!~:*,*@@@@*      .. .    ");
            Console.WriteLine("         .@@@@@~     ~@@@@@@@@@@@@@@@@@@@@$.   *@@@@  ");
            Console.WriteLine("         @@@@@@:     ~@@@@@@@@@@@@@@@@@@@@=.   !@@@* ");
            Console.WriteLine("         @@@@@@=     ~@@@@@@@: @@@@ -#@@@@@@@@@@@@@: ");
            Console.WriteLine("         @@! @@=     ~@@@@@@@!,@@@@-:@@@@@@@@@@@@@@:");
            Console.WriteLine("         @@; @@$     ~@@@@@@@@@@@@@@@@@@@@@@@@@@!*@:    ");
            Console.WriteLine("         @@#=@@#:.:.-!@@@@@@@@@@-=@@@@@@@@@@@@@@.,@:    ");
            Console.WriteLine("         @@@@@@@@-@-=@@@@@@@@@@@-=@@@@@@@@@@@@@@.,@:  ");
            Console.WriteLine("    @@@@@*-@@@@@@@*-@@@@@@@@@*-@@@@@@@@@*-@@@@@@@*-@@@@@@@@@@@*!- ");
            Console.WriteLine("    @@@@@;@@@@@@@@; @@@@@@@@@; @@@@@@@@@; @@@@@@@; @@@@@@@@@@@: ");
            Console.WriteLine("        @@@@@@@@@@@@@@@@@@@@@*:   ::@@@@@@@@@@@@@@@@@@@@@@: ");
            Console.WriteLine("        @@@@@@@    @@@@@@@@@@,      ,@@@@@@@@@    @@@@@@@@: ");
            Console.WriteLine("        @@@@@@@    @@@@@@@@@!        @@@@@@@@@    @@@@@@@@: ");
            Console.WriteLine("        @@@@@@@@@@@@@@@@@@@@!        @@@@@@@@@@@@@@@@@@@@@: ");
            Console.WriteLine("        @@@@@@@@@@@@@@@@@@@@!        @@@@@@@@@@@@@@@@@@@@@; ");
            Console.WriteLine("        @@@@@@@@@@@@@@@@@@@@!        @@@@@@@@@@@@@@@@@@@@@: ");
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.ResetColor();
        }
        public static void ShowTitle(string title)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(title);
            Console.ResetColor();
        }

        public static void PrintTextHighlights(string s1, string s2, string s3 = "", ConsoleColor color = ConsoleColor.Yellow)
        {
            Console.Write(s1);
            Console.ForegroundColor = color;
            Console.Write(s2);
            Console.ResetColor();
            Console.WriteLine(s3);
        }
        public static int GetPrintableLength(string str)
        {
            int length = 0;
            foreach (char c in str)
            {
                if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
                {
                    length += 2; // 한글과 같은 넓은 문자에 대해 길이를 2로 취급
                }
                else
                {
                    length += 1; // 나머지 문자에 대해 길이를 1로 취급
                }
            }

            return length;
        }

        public static string PadRightForMixedText(string str, int totalLength)
        {
            // 가나다
            // 111111
            int currentLength = GetPrintableLength(str);
            int padding = totalLength - currentLength;
            return str.PadRight(str.Length + padding);
        }
    }
}
