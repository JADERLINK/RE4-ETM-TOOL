using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RE4_ETM_TOOL
{
    internal static class Extract
    {
        public static void ExtractFile(string file)
        {
            FileInfo fileInfo = new FileInfo(file);
            string baseName = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length);
            string baseDiretory = fileInfo.DirectoryName;

            var etm = new BinaryReader(fileInfo.OpenRead());
            uint Amount = etm.ReadUInt32();

            var idxetm = new FileInfo(baseDiretory + "\\" + baseName + ".idxetm").CreateText();
            idxetm.WriteLine("# github.com/JADERLINK/RE4-ETM-TOOL");
            idxetm.WriteLine("# youtube.com/@JADERLINK");
            idxetm.WriteLine("# RE4 ETM TOOL By JADERLINK");

            Console.WriteLine("Amount: " + Amount);

            if (Amount != 0)
            {
                Directory.CreateDirectory(baseDiretory + "\\" + baseName);

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
                        File.WriteAllBytes(baseDiretory + "\\" + baseName + "\\" + name, internalFile);
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
