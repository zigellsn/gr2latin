using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;

namespace Chords
{
    public class ConvertLyrics
    {
        private enum RegexOperation { Replace, Split, Match };

        private struct RegexValues
        {
            public string expression;
            public string replace;
        };

        private List<RegexValues> _regex = new List<RegexValues>();
        private List<RegexValues> _replace = new List<RegexValues>();

        public ConvertLyrics(string patternPath)
        {
            XmlDocument xmldoc = new XmlDocument();
            try
            {
                Path.GetFileName(patternPath);
                xmldoc.Load(patternPath);
            }
            catch
            {
                xmldoc.LoadXml(patternPath);
            }
            _regex = GetRegex(xmldoc, "/conversion/regex");
            _replace = GetRegex(xmldoc, "/conversion/literal");           
        }

        /*public string Convert(Glyph text)
        {
            StringReader sr = new StringReader(text);
            StringBuilder sb = new StringBuilder();

            string line = null;
            while ((line = sr.ReadLine()) != null)
            {
                foreach (RegexValues rv in _regex)
                {
                    Regex r = new Regex(rv.expression);
                    line = r.Replace(line, rv.replace);
                }
                foreach (RegexValues rv in _replace)
                {
                    line = line.Replace(rv.expression, rv.replace);
                }
                sb.AppendLine(line);
            }
            sr.Close();
            return sb.ToString();
        }*/

        public string Convert(string text)
        {
            StringReader sr = new StringReader(text);
            StringBuilder sb = new StringBuilder();

            string line = null;
            while ((line = sr.ReadLine()) != null)
            {
                foreach (RegexValues rv in _regex)
                {
                    Regex r = new Regex(rv.expression);
                    line = r.Replace(line, rv.replace);
                }
                foreach (RegexValues rv in _replace)
                {
                    line = line.Replace(rv.expression, rv.replace);
                }
                sb.AppendLine(line);
            }
            sr.Close();
            return sb.ToString();
        }

        private List<RegexValues> GetRegex(XmlDocument xmldocument, string xpath)
        {
            string expression = null;
            string replace = null;

            List<RegexValues> regex = new List<RegexValues>();

            XmlNodeList nodes = xmldocument.SelectNodes(xpath);
            foreach (XmlNode node in nodes)
            {
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    if (childNode.Name == "expression")
                    {
                        expression = childNode.InnerText;
                    }
                    else if (childNode.Name == "operator")
                    {
                        replace = childNode.InnerText;
                    }
                }
                RegexValues rv = new RegexValues();
                rv.expression = expression;
                rv.replace = replace;
                regex.Add(rv);
            }
            return regex;
        }
    }
}
