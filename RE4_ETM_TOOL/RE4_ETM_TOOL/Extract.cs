using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SimpleEndianBinaryIO;

namespace RE4_ETM_TOOL
{
    internal static class Extract
    {
        public static void ExtractFile(string file, Endianness endianness)
        {
            FileInfo fileInfo = new FileInfo(file);

            string baseDirectory = Path.GetDirectoryName(fileInfo.FullName);
            string baseFileName = Path.GetFileNameWithoutExtension(fileInfo.FullName);

            string baseFilePath = Path.Combine(baseDirectory, baseFileName);

            string pattern = "^(00)([0-9]{2})$";
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern, System.Text.RegularExpressions.RegexOptions.CultureInvariant);

            if (regex.IsMatch(baseFileName))
            {
                baseFilePath = Path.Combine(baseDirectory, baseFileName + "_ETM");
            }

            var etm = new EndianBinaryReader(fileInfo.OpenRead(), endianness);
            uint Amount = etm.ReadUInt32();

            if (Amount > 0x10000)
            {
                Console.WriteLine("Invalid ETM file!");
                etm.Close();
                return;
            }

            var idxetm = new FileInfo(Path.Combine(baseDirectory, baseFileName + ".idxetm")).CreateText();
            idxetm.WriteLine("# github.com/JADERLINK/RE4-ETM-TOOL");
            idxetm.WriteLine("# youtube.com/@JADERLINK");
            idxetm.WriteLine("# RE4 ETM TOOL By JADERLINK");
            idxetm.WriteLine();

            Console.WriteLine("Amount: " + Amount);

            if (Amount != 0)
            {
                try
                {
                    Directory.CreateDirectory(baseFilePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error creating directory: " + baseFilePath);
                    Console.WriteLine(ex);
                }

                etm.BaseStream.Position = 32;

                for (int i = 0; i < Amount; i++)
                {
                    uint blockLength = etm.ReadUInt32();
                    uint nameLength = etm.ReadUInt32();
                    etm.BaseStream.Position += 24;
                    byte[] nameb = new byte[32];
                    etm.BaseStream.Read(nameb, 0, nameb.Length);
                    string name = Encoding.GetEncoding(1252).GetString(nameb);
                    name = Utils.ValidFileName(name);

                    byte[] internalFile = new byte[blockLength - 64];
                    etm.BaseStream.Read(internalFile, 0, internalFile.Length);
                    
                    idxetm.WriteLine(name);
                    Console.WriteLine("Extracted: " + name);

                    try
                    {
                        File.WriteAllBytes(Path.Combine(baseFilePath, name), internalFile);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error saving file: " + name);
                        Console.WriteLine(ex);
                    }

                }

            }

            idxetm.Close();
            etm.Close();
        }

    }
}
