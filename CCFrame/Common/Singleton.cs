using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


#region << 单例基类 >>
/*----------------------------------------------------------------
// 文件名称：Singleton
// 创 建 者：蔡程健
// 创建时间：22/2/21 17:04:47
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame
{
    public class Singleton<T> where T : new()
    {
        protected static T _instance = default(T);

        private static readonly object syncRoot = new object();

        public static T GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new T();
                        }
                    }
                }
                return _instance;
            }
        }
    }
}
