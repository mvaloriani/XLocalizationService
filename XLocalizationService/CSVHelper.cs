using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace XLocalizationService
{
    public static class CSVHelper
    {
        public static List<List<String>> Parse(String csvString)
        {

            List<List<String>> csv = new List<List<string>>();

            var rows = csvString.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

            Regex r = new Regex("((?<=\")[^\"]*(?=\"(,|$)+)|(?<=,|^)[^,\"]*(?=,|$))");
            List<String> temp;

            foreach (var row in rows)
            {
                temp = new List<string>();

                if (row != String.Empty)
                {
                    var matches = r.Matches(row);
                    foreach (var m in matches)
                    {
                        temp.Add(m.ToString());
                    }
                    csv.Add(temp);
                }

            }

            return csv;
        }
    }
}