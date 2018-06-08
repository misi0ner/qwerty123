using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom
{
    class CTable
    {
        public string text;
        public void ToTable(string[] str, string[] delim)
        {
            text = "";
            if (str[1].Contains(delim[1]))
            {
                var s1 = str[1].Replace(delim[1] + " ", "");
                if (s1.Contains(delim[2]))
                {
                    var s2 = s1.Replace(" " + delim[1] + " ", "\t").Replace(" " + delim[2] + " ", "\t");
                    text += s2 + '\n';
                    for (int i = 2; i < str.Length; ++i)
                    {
                        var s3 = str[i].Replace(" ", "\t");
                        text += s3 + '\n';
                    }
                }
            }
            var ts = text.Split('\t', '\n');
            int len = 0;
            foreach (var x in ts)
                if (x.Length > len)
                    len = x.Length;
            text = "";
            int cnt = 0;
            foreach (var x in ts)
            {
                text += x.PadRight(len) + '\t';
                ++cnt;
                if (cnt == 3)
                {
                    cnt = 0;
                    text += '\n';
                }
            }
        }
    }
}
