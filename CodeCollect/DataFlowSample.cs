using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;


#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2022   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 公司名称：
 * 命名空间：CodeCollect
 * 文件名：DataFlowSample
 * 创建者：蔡程健
 * 创建时间：2022/6/21 21:15:43
 * 版本：V1.0.0
 * 描述：
 *
 * ----------------------------------------------------------------
 * 修改人：
 * 时间：
 * 修改说明：
 *
 * 版本：V1.0.1
 *----------------------------------------------------------------*/
#endregion << 版 本 注 释 >>

namespace CodeCollect
{
    internal class DataFlowSample
    {
        //static void Main()
        //{
        //    var target = SetupPipeline();
        //    target.Post(".");
        //    Console.ReadLine();
        //}
        /// <summary>
        /// 获取.cs为扩展名的文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetFileNames(string path)
        {
            foreach (var fileName in Directory.EnumerateFiles(path, "*.cs"))
            {
                yield return fileName;
            }
        }
        /// <summary>
        /// 得到文件中的每一行
        /// </summary>
        /// <param name="fileNames"></param>
        /// <returns></returns>
        public static IEnumerable<string> LoadLines(IEnumerable<string> fileNames)
        {
            foreach (var fileName in fileNames)
            {
                using (FileStream stream = File.OpenRead(fileName))
                {
                    var reader = new StreamReader(stream);
                    string line = null;
                    while ((line = reader.ReadLine()) != null)
                    {
                        //WriteLine($"LoadLines {line}");
                        yield return line;
                    }
                }
            }
        }

        /// <summary>
        /// 接收一个lines集合作为参数，并返回一个单词列表
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetWords(IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                string[] words = line.Split(' ', ';', '(', ')', '{', '}', '.', ',');
                foreach (var word in words)
                {
                    if (!string.IsNullOrEmpty(word))
                        yield return word;
                }
            }
        }

        public static ITargetBlock<string> SetupPipeline()
        {
            var fileNamesForPath = new TransformBlock<string, IEnumerable<string>>(
              path => GetFileNames(path));

            var lines = new TransformBlock<IEnumerable<string>, IEnumerable<string>>(
              fileNames => LoadLines(fileNames));

            var words = new TransformBlock<IEnumerable<string>, IEnumerable<string>>(
              lines2 => GetWords(lines2));

            var display = new ActionBlock<IEnumerable<string>>(
              coll =>
              {
                  foreach (var s in coll)
                  {
                      Console.WriteLine(s);
                  }
              });


            fileNamesForPath.LinkTo(lines);
            lines.LinkTo(words);
            words.LinkTo(display);
            return fileNamesForPath;
        }
    }
}
