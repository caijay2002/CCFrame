using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Security.Principal;

#region << Windows用户 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2022   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 公司名称：
 * 命名空间：CodeCollect
 * 文件名：WindowsPrincipal
 * 创建者：蔡程健
 * 创建时间：2022/6/24 20:11:47
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
    internal class WindowsPrincipal
    {

        /// <summary>
        /// 显示用户信息
        /// </summary>
        /// <returns></returns>
        public static WindowsIdentity ShowIdentityInformation()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            if (identity == null)
            {
                Console.WriteLine("not a Windows Identity");
                return null;
            }

            identity.AddClaim(new Claim("Age", "25"));


            Console.WriteLine($"IdentityType: {identity}");
            Console.WriteLine($"Name: {identity.Name}");
            Console.WriteLine($"Authenticated: {identity.IsAuthenticated}");
            Console.WriteLine($"Authentication Type: {identity.AuthenticationType}");
            Console.WriteLine($"Anonymous? {identity.IsAnonymous}");
            Console.WriteLine($"Access Token: {identity.AccessToken.DangerousGetHandle()}");
            Console.WriteLine();
            return identity;
        }
    }
}
