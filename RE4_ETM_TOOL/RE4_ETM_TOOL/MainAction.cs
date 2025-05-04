using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SimpleEndianBinaryIO;

namespace RE4_ETM_TOOL
{
    internal static class MainAction
    {
        public static void Continue(string[] args, Endianness endianness) 
        {
            bool usingBatFile = false;
            int start = 0;
            if (args.Length > 0 && args[0].ToLowerInvariant() == "-bat")
            {
                usingBatFile = true;
                start = 1;
            }

            for (int i = start; i < args.Length; i++)
            {
                if (File.Exists(args[i]))
                {
                    try
                    {
                        Action(args[i], endianness);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + args[i]);
                        Console.WriteLine(ex);
                    }
                }
                else
                {
                    Console.WriteLine("File specified does not exist: " + args[i]);
                }
            }

            if (args.Length == 0)
            {
                Console.WriteLine("How to use: Drag the file to the executable.");
                Console.WriteLine("For more information read:");
                Console.WriteLine("https://github.com/JADERLINK/RE4-ETM-TOOL");
                Console.WriteLine("Press any key to close the console.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Finished!!!");
                if (!usingBatFile)
                {
                    Console.WriteLine("Press any key to close the console.");
                    Console.ReadKey();
                }
            }

        }

        private static void Action(string file, Endianness endianness)
        {
            FileInfo fileInfo = null;
            try
            {
                fileInfo = new FileInfo(file);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in the path: " + Environment.NewLine + ex);
            }

            if (fileInfo != null)
            {
                Console.WriteLine("File: " + fileInfo.Name);

                if (fileInfo.Extension.ToUpperInvariant() == ".ETM")
                {
                    try
                    {
                        Console.WriteLine("Extract Mode:");
                        Extract.ExtractFile(fileInfo.FullName, endianness);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + Environment.NewLine + ex);
                    }

                }
                else if (fileInfo.Extension.ToUpperInvariant() == ".IDXETM")
                {
                    try
                    {
                        Console.WriteLine("Repack Mode:");
                        Repack.RepackFile(fileInfo.FullName, endianness);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + Environment.NewLine + ex);
                    }
                }
                else
                {
                    Console.WriteLine("The extension is not valid: " + fileInfo.Extension);
                }
            }

        }


    }
}
