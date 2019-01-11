using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using Chords;

namespace gr2latin
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length >= 1)
            {
                StreamReader sr = new StreamReader(args[0]);
                StreamWriter sw = new StreamWriter("out_" + args[0]);
                ConvertLyrics cl = new ConvertLyrics("..\\..\\dics\\wikiGr2latin.xml");

                while (!sr.EndOfStream)
                {
                    string Line = sr.ReadToEnd();

                    Line = cl.Convert(Line);

                    sw.Write(Line);
                    Console.Write(Line);
                }
                sr.Close();
                sw.Close();
            }
        }
    }
}
