using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;

namespace CCFrame.Helper
{
    /// <summary>
    /// 文件帮助类
    /// </summary>
    public class DynamicFileHelper
    {
        public IEnumerable<dynamic> ParseFile(string fileName)
        {
            var retList = new List<dynamic>();
            var fileStream = OpenFile(fileName);
            if (fileStream != null)
            {
                //文本第一行列表字段名
                string[] headerLine = fileStream.ReadLine().Split(',').Select(s => s.Trim()).ToArray();
                while (fileStream.Peek() > 0)
                {
                    string[] dataLine = fileStream.ReadLine().Split(',');
                    dynamic dynamicEntity = new ExpandoObject();
                    for (int i = 0; i < headerLine.Length; i++)
                    {
                        ((IDictionary<string, object>)dynamicEntity).Add(headerLine[i], dataLine[i]);
                    }
                    retList.Add(dynamicEntity);
                }
            }
            return retList;
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private StreamReader OpenFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                return new StreamReader(File.OpenRead(fileName));
            }
            return null;
        }

        /// <summary>
        /// 获取当前文件路径
        /// </summary>
        /// <returns></returns>
        public static string GetDocumentsFolder() =>
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        /// <summary>
        /// 分析文件的编码
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static Encoding GetEncoding(Stream stream)
        {
            if (!stream.CanSeek) throw new ArgumentException("require a stream that can seek");

            Encoding encoding = Encoding.ASCII;

            byte[] bom = new byte[5];
            int nRead = stream.Read(bom, offset: 0, count: 5);
            if (bom[0] == 0xff && bom[1] == 0xfe && bom[2] == 0 && bom[3] == 0)
            {
                Console.WriteLine("UTF-32");
                stream.Seek(4, SeekOrigin.Begin);
                return Encoding.UTF32;
            }
            else if (bom[0] == 0xff && bom[1] == 0xfe)
            {
                Console.WriteLine("UTF-16, little endian");
                stream.Seek(2, SeekOrigin.Begin);
                return Encoding.Unicode;
            }
            else if (bom[0] == 0xfe && bom[1] == 0xff)
            {
                Console.WriteLine("UTF-16, big endian");
                stream.Seek(2, SeekOrigin.Begin);
                return Encoding.BigEndianUnicode;
            }
            else if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf)
            {
                Console.WriteLine("UTF-8");
                stream.Seek(3, SeekOrigin.Begin);
                return Encoding.UTF8;
            }
            stream.Seek(0, SeekOrigin.Begin);
            return encoding;
        }

    }
}
