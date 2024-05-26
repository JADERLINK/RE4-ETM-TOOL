using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RE4_ETM_TOOL
{
    internal static class Repack
    {
        public static void RepackFile(string file)
        {
            StreamReader idx = null;
            BinaryWriter etm = null;
            FileInfo fileInfo = new FileInfo(file);
            string baseName = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length);
            string baseDiretory = fileInfo.DirectoryName;

            try
            {
                idx = new FileInfo(file).OpenText();
                etm = new BinaryWriter(new FileInfo(baseDiretory + "\\" + baseName + ".ETM").Create(), Encoding.GetEncoding(1252));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + Environment.NewLine + ex);
            }

            if (idx != null)
            {
                List<string> files = new List<string>();
                List<string> check = new List<string>();

                string endLine = "";
                while (endLine != null)
                {
                    endLine = idx.ReadLine();

                    if (endLine != null)
                    {
                        endLine = endLine.Trim();

                        if (!( endLine.Length == 0
                            || endLine.StartsWith("#")
                            || endLine.StartsWith("\\")
                            || endLine.StartsWith("/")
                            || endLine.StartsWith(":")
                            ))
                        {
                            string validFile = Utils.ValidFileName(endLine);
                            string toUpper = validFile.ToUpperInvariant();
                            if (!check.Contains(toUpper))
                            {
                                files.Add(validFile);
                                check.Add(toUpper);
                            }
                        }
                       
                    }
                  
                }

                idx.Close();

                int counter = 0;
                etm.Write(new byte[32]); //header

                foreach (var fname in files)
                {
                    string filePath = baseDiretory + "\\" + baseName + "\\" + fname;
                    if (File.Exists(filePath))
                    {
                        //verificação de tamanho do arquivo
                        FileInfo info = new FileInfo(filePath);
                        uint lenght = (uint)info.Length;
                        uint lines = lenght / 32;
                        uint rest = lenght % 32;
                        lines += rest != 0 ? 1u: 0u;
                        uint diff = (lines * 32) - lenght;
                        uint fullLength = (lines + 2) * 32;

                        //verificação do nome
                        byte[] bName = Encoding.GetEncoding(1252).GetBytes(Utils.ValidFileName(fname));
                        bName = bName.Length > 31 ? bName.Take(31).ToArray() : bName;
                        uint nameToEtmLength = (uint)bName.Length + 1;
                        byte[] insertName = new byte[32];
                        bName.CopyTo(insertName, 0);

                        //escreve no arqivo de destino
                        etm.Write((uint)fullLength);
                        etm.Write((uint)nameToEtmLength);
                        etm.BaseStream.Position += 24;
                        etm.Write(insertName);

                        var fileStream = info.OpenRead();
                        fileStream.CopyTo(etm.BaseStream);
                        fileStream.Close();

                        etm.Write(new byte[diff]);

                        counter++;
                        Console.WriteLine("Inserted file: " + fname);
                    }
                    else 
                    {
                        Console.WriteLine("File \"" + fname + "\" does not exist, has not been added to the ETM");
                    }

                }

                etm.BaseStream.Position = 0;
                etm.Write((uint)counter);

                etm.Close();

                Console.WriteLine("Created an ETM with " + counter + " files");
            }


        }
    }
}
