using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;

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
    }
}
