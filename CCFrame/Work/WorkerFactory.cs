using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region << 工作者创建工厂 >>
/*----------------------------------------------------------------
// 文件名称：WorkerFactory
// 创 建 者：蔡程健
// 创建时间：22/6/9 11:56:13
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame.Work
{
    public class WorkerFactory
    {
        /// <summary>
        /// 创建采集工作者
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IWorker CreateWorker(string name)
        {
            IWorker driver = null;

            switch (name)
            {
                case "MXDriver":
                    driver = new MXPLCWorker();
                    break;
            }
            return driver;
        }
    }
}
