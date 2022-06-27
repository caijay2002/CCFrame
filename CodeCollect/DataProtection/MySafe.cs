using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;

#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2022   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 公司名称：
 * 命名空间：CodeCollect.DataProtection
 * 文件名：MySafe
 * 创建者：蔡程健
 * 创建时间：2022/6/24 21:37:34
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

namespace CodeCollect.DataProtection
{
    public class MySafe
    {
        private IDataProtector _protector;
        public MySafe(IDataProtectionProvider provider) =>
            _protector = provider.CreateProtector("MySafe.MyProtection.v2");

        public string Encrypt(string input) => _protector.Protect(input);

        public string Decrypt(string encrypted) => _protector.Unprotect(encrypted);
    }
}
