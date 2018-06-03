using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom
{
    class CList
    {
        public string text;
        public void ToList(string[] str, string[] delim)
        {
            text = "";
            if (str[1] == delim[1])
            {
                int j = 1;
                for (var i = 2; i < str.Length; ++i)
                {
                    text += j.ToString() + ". " + str[i] + "\n";
                    ++j;
                }
            }
            else
                for (var i = 2; i < str.Length; ++i)
                {
                    text += str[i] + "\n";
                }
        }
    }
}
