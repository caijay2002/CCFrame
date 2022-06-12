using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2022   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 公司名称：
 * 命名空间：CodeCollect
 * 文件名：VersionManage
 * 创建者：蔡程健
 * 创建时间：2022/6/11 21:22:59
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
    internal class VersionManage
    {

        private static readonly StringBuilder outputText = new StringBuilder(1000);
        private static DateTime backDateTo = new DateTime(2022, 2, 1);

        public static void DisplayTypeInfo(Type type)
        {
            if (!type.GetTypeInfo().IsClass)
            {
                return;
            }

            AddToOutput($"{Environment.NewLine}class {type.Name}");

            IEnumerable<LastModifiedAttribute> lastModifiedAttributes = type.GetTypeInfo().GetCustomAttributes().OfType<LastModifiedAttribute>().Where(a => a.DateModified >= backDateTo).ToArray();
            if (lastModifiedAttributes.Count() == 0)
            {
                AddToOutput($"\tNo changes to the class {type.Name}{Environment.NewLine}");
            }
            else
            {
                foreach (LastModifiedAttribute attribute in lastModifiedAttributes)
                {
                    WriteAttributeInfo(attribute);
                }
            }

            AddToOutput("changes to methods of this class:");

            foreach (MethodInfo method in type.GetTypeInfo().DeclaredMembers.OfType<MethodInfo>())
            {
                IEnumerable<LastModifiedAttribute> attributesToMethods = method.GetCustomAttributes()
                    .OfType<LastModifiedAttribute>().Where(a => a.DateModified >= backDateTo).ToArray();

                if (attributesToMethods.Count() > 0)
                {
                    AddToOutput($"{method.ReturnType} {method.Name}()");

                    foreach (Attribute attribute in attributesToMethods)
                    {
                        WriteAttributeInfo(attribute);
                    }
                }
            }
        }

        public static void WriteAttributeInfo(Attribute attribute)
        {
            if (attribute is LastModifiedAttribute lastModifiedAttribute)
            {
                AddToOutput($"\tmodified: {lastModifiedAttribute.DateModified:D}: {lastModifiedAttribute.Changes}");

                if (lastModifiedAttribute.Issues != null)
                {
                    AddToOutput($"\tOutstanding issues: {lastModifiedAttribute.Issues}");
                }
            }
        }

        static void AddToOutput(string text) =>
    //outputText.Append($"{Environment.NewLine}{text}");
    Console.WriteLine($"{Environment.NewLine}{text}");
    }
}
