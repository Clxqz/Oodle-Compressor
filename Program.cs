using Clxqz_Compressor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;


namespace ClxqzCompressor
{
    internal class Program
    {
        public static int fileLength = 0;
        public static string filePath = "";
        public static void Main(string[] args)
        {
            Console.Title = "[Clxqz Compresspr] An Oodle Compressor/Decompressor Tool, To Compress/Decompress Uasset's By Clxqz.";
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.Write("Type A Name For Your Compressed Or Decompressed File: ");
            string name = Console.ReadLine();
            Console.Write("Type Compress/Decompress: ");
            string input = Console.ReadLine();
            string path = Path.GetDirectoryName(args[0]);
            string filename = Path.GetFileNameWithoutExtension(args[0]);
            filePath = path + "\\" + filename + ".uasset";
            if (input == "Compress" || input == "compress")
            {
                //simple oodle compressor
                int start = 0;
                while (true)
                {
                    if (start < args.Length)
                    {
                        byte[] readDecompressBytes = File.ReadAllBytes(args[start]);
                        string bytes = System.Text.Encoding.UTF8.GetString(Oodle.Compress(readDecompressBytes));
                        string compressedHex = Convert.ToHexString(Oodle.Compress(readDecompressBytes));
                        var stream = new FileStream(args.ToString(), FileMode.Create, FileAccess.ReadWrite);
                        WriteHexStringToFile(compressedHex, stream);
                        stream.Close();
                        start = +1;
                    }
                    if (start == args.Length)
                    {
                        FileInfo info = new FileInfo(args.ToString());
                        string filePath = info.DirectoryName + $"/compressed{name}.uasset";
                        info.MoveTo(filePath);
                        Console.WriteLine("Compressed Uasset");
                        break;
                    }
                }
            }
            else if(input == "Decompress" || input == "decompress") 
            {
                //decompressing tool isnt fully done yet this can only decompress some asset as i havent worked on away to get a working decompressed size
                GetFileSize.GetFileSizeOnDisk(filePath);
                int start = 0;
                int decompressedSize = GetFileSize.sizeCompressed;
                bool booling = true;
                while(booling)
                {

                    try
                    {
                        while (true)
                        {
                            if (start < args.Length)
                            {
                                byte[] readDecompressBytes = File.ReadAllBytes(args[start]);
                                string bytes = System.Text.Encoding.UTF8.GetString(Oodle.Decompress(readDecompressBytes, decompressedSize));
                                string compressedHex = Convert.ToHexString(Oodle.Decompress(readDecompressBytes, decompressedSize));
                                var stream = new FileStream(args.ToString(), FileMode.Create, FileAccess.ReadWrite);
                                WriteHexStringToFile(compressedHex, stream);
                                stream.Close();
                                start = +1;

                            }
                            if (start == args.Length)
                            {
                                
                                FileInfo info = new FileInfo(args.ToString());
                                string filePath = info.DirectoryName + $"/decompressed{name}.uasset";
                                info.MoveTo(filePath);
                                booling = false;
                                break;
                                
                            }
                        }
                        
                    }
                    catch (Exception e)
                    {
                        string messagge = ($" There Was A Problem Finding The Oodle DecompressedSize : {e.Message}");
                        decompressedSize += 1;
                    }
                    if(booling == false)
                    {
                        Console.WriteLine($"[1] Size Of Decompressed Uasset In Bytes {decompressedSize}");
                        Console.WriteLine("[2] Decompressed Uasset");
                        break;
                    }
                    
                }



            }
            

            Console.ReadLine();
        }
        public static void WriteHexStringToFile(string hexString, FileStream stream)
        {
            var twoCharacterBuffer = new StringBuilder();
            var oneByte = new byte[1];
            foreach (var character in hexString.Where(c => c != ' '))
            {
                twoCharacterBuffer.Append(character);

                if (twoCharacterBuffer.Length == 2)
                {
                    oneByte[0] = (byte)Convert.ToByte(twoCharacterBuffer.ToString(), 16);
                    stream.Write(oneByte, 0, 1);
                    twoCharacterBuffer.Clear();
                }
            }
        }
    }
}