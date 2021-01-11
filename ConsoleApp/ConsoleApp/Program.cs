using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        static bool FileSaved = false;
        static long Bytes;
        static List<string> Lines = new List<string>();
        static string FileName;
        static string Path;


        static void Main(string[] args)
        {
            Console.WriteLine("Enter text line and press Enter for new line. Press Ctrl + P to Save file");

            ConsoleKeyInfo Key;

            StringBuilder Line = new StringBuilder();

            do
            {
                Key = Console.ReadKey();

                if (((Key.Modifiers & ConsoleModifiers.Control) != 0) && Key.Key == ConsoleKey.P)
                {
                    SaveFile();
                }
                else if (Key.Key == ConsoleKey.Enter)
                {

                    Lines.Add(Line.ToString());
                    Line.Clear();
                    Console.WriteLine();

                }
                else
                {
                    Line.Append(Key.KeyChar);
                }

            } while (!FileSaved);

            Bytes = BytesDefine();

            Console.WriteLine($"File successfully saved. {Bytes} bytes");
            Console.ReadKey();
        }

        /// <summary>
        /// Сохраняет файл.
        /// </summary>
        /// <param name="Path"></param>
        private static void SaveFile()
        {
            FileName = FileNameDefine();
            Path = Directory.GetCurrentDirectory() + @"\" + $"{FileName}.txt";

            try
            {
                using (StreamWriter streamWriter = new StreamWriter(Path, false, System.Text.Encoding.Default))
                {
                    foreach (var line in Lines)
                    {
                        streamWriter.WriteLine(line);
                    }
                }
                FileSaved = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// Определяет имя файла.
        /// </summary>
        /// <returns></returns>
        private static string FileNameDefine()
        {
            return DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
        }

        /// <summary>
        /// Определяет размер файла в байтах.
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        private static long BytesDefine()
        {
            FileInfo fileInfo = new FileInfo(Path);
            if (fileInfo.Exists)
            {
                return fileInfo.Length;
            }
            else
            {
                throw new Exception("File not found");
            }

        }
    }
}
