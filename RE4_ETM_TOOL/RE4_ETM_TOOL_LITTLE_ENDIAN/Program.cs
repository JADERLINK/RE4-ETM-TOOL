using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RE4_ETM_TOOL_LITTLE_ENDIAN
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("# RE4 ETM TOOL (LITTLE ENDIAN)");
            Console.WriteLine("# By JADERLINK");
            Console.WriteLine("# youtube.com/@JADERLINK");
            Console.WriteLine("# github.com/JADERLINK");
            Console.WriteLine("# VERSION 1.1.1 (2025-05-04)");

            RE4_ETM_TOOL.MainAction.Continue(args, SimpleEndianBinaryIO.Endianness.LittleEndian);
        }
    }
}
