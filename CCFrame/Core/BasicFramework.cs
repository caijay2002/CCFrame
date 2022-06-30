using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region << 软件数据缓存 >>
/*----------------------------------------------------------------
// 文件名称：BasicFramework
// 创 建 者：蔡程健
// 创建时间：22/6/30 13:26:29
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame.Core
{
    /// <summary>
    /// 软件缓存基类
    /// </summary>
    public abstract class SoftCacheBase
    {
        /// <summary>
        /// 数据项值
        /// </summary>
        protected ConcurrentDictionary<string, object> DataItems = new ConcurrentDictionary<string, object>();

        /// <summary>
        /// 添加或者更新值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public virtual void UpdateValue(string key, object value)
        {
            DataItems.AddOrUpdate(key, value, (oldKey, oldValue) => value);
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual object GetValue(string key)
        {
            if (DataItems.ContainsKey(key))
            {
                return DataItems[key];
            }
            else
                return "";
        }
    }
}
