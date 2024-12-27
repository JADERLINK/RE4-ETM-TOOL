using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RE4_ETM_TOOL
{
    internal static class Utils
    {
        public static string ValidFileName(string fileName)
        {
            string res = "";

            foreach (var c in fileName)
            {
                if (char.IsLetterOrDigit(c)
                    || c == '.'
                    || c == ' '
                    || c == '-'
                    || c == '+'
                    || c == '_'
                    || c == '='
                    )
                {
                    res += c;
                }
            }

            if (res.Length > 0 && res[res.Length - 1] == '.')
            {
                res += "error";
            }
            if (res.Length == 0)
            {
                res = "error.error";
            }
            return res;
        }
    }
}
