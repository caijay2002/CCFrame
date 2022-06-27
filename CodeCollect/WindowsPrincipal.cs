using System;
using System.Collections.Generic;
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
    internal class WindowsPrincipalHelper
    {

        /// <summary>
        /// 显示用户信息，身份类型，名称，身份验证类型，其他值
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

        /// <summary>
        /// Windows Principal 代码示例验证用户是否属于内置角色
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static WindowsPrincipal ShowPrincipal(WindowsIdentity identity)
        {
            Console.WriteLine("Show principal information");
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            if (principal == null)
            {
                Console.WriteLine("not a Windows Principal");
                return null;
            }
            Console.WriteLine($"Users? {principal.IsInRole(WindowsBuiltInRole.User)}");
            Console.WriteLine($"Administrators? {principal.IsInRole(WindowsBuiltInRole.Administrator)}");
            Console.WriteLine();
            return principal;
        }

        /// <summary>
        /// 使用声明
        /// </summary>
        /// <param name="claims"></param>
        public static void ShowClaims(IEnumerable<Claim> claims)
        {
            Console.WriteLine("Claims");
            foreach (var claim in claims)
            {
                Console.WriteLine($"Subject: {claim.Subject}");
                Console.WriteLine($"Issuer: {claim.Issuer}");
                Console.WriteLine($"Type: {claim.Type}");
                Console.WriteLine($"Value type: {claim.ValueType}");
                Console.WriteLine($"Value: {claim.Value}");
                foreach (var prop in claim.Properties)
                {
                    Console.WriteLine($"\tProperty: {prop.Key} {prop.Value}");
                }
                Console.WriteLine();
            }
        }
    }
}
