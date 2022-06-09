using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region << 驱动创建工厂 >>
/*----------------------------------------------------------------
// 文件名称：DriverFactory
// 创 建 者：蔡程健
// 创建时间：22/6/9 10:04:12
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame.Driver
{
    public static class DriverFactory
    {
        /// <summary>
        /// 创建驱动
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IDriver CreateDriver(string name)
        {
            IDriver driver = null;

            switch (name)
            {
                case "MXDriver":
                    driver = new MXDriver();
                    break;
            }

            return driver;
        }
    }
}
