using System;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

#region << 显示资源访问权限 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2022   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 公司名称：
 * 命名空间：CodeCollect
 * 文件名：FileAccessControl
 * 创建者：蔡程健
 * 创建时间：2022/6/24 22:09:29
 * 版本：V1.0.0
 * 描述：添加包System.IO.FileSystem.AccessControl
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
    internal class FileAccessControl
    {
        public void Show(string filename)
        {
            using (FileStream stream = File.Open(filename, FileMode.Open))
            {
                FileSecurity securityDescriptor = stream.GetAccessControl();
                AuthorizationRuleCollection rules =
                      securityDescriptor.GetAccessRules(true, true, typeof(NTAccount));

                foreach (AuthorizationRule rule in rules)
                {
                    var fileRule = rule as FileSystemAccessRule;
                    Console.WriteLine($"Access type: {fileRule.AccessControlType}");
                    Console.WriteLine($"Rights: {fileRule.FileSystemRights}");
                    Console.WriteLine($"Identity: {fileRule.IdentityReference.Value}");
                    Console.WriteLine();
                }
            }
        }
    }
}
