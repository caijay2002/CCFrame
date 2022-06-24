using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCFrame.Command.Data;

#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：DataCacheSvr
// 创 建 者：蔡程健
// 创建时间：22/5/29 19:19:33
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame.Core
{

    public delegate void DataChanged(string key, IData data);

    public static class DataCacheSvr
    {
        /// <summary>
        /// 数据缓存
        /// </summary>
        public static ConcurrentDictionary<string, List<IData>> DataMap = new ConcurrentDictionary<string, List<IData>>();
        /// <summary>
        /// 数据改变
        /// </summary>
        public static event DataChanged DataChanged;
        /// <summary>
        /// 更新缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="address"></param>
        /// <param name="value"></param>
        public static void UpdateCache(string key, string address, object value)
        {
            if (DataMap.ContainsKey(key))
            {
                var oldData = DataMap[key].FirstOrDefault(x => x.Address == address);
                //if (oldData == null || oldData.Value?.Equals(value))
                if (oldData.Value?.ToString() == value.ToString())
                {
                    return;
                }
                var newData = oldData;
                newData.Value = value;

                if (DataChanged != null) DataChanged(key, newData);

                oldData.TimeStamp = DateTime.Now;
                oldData.Value = value;
            }
        }

        public static void InitializeCache(string key, List<IData> dataList)
        {
            DataMap.AddOrUpdate(key, dataList, (oldkey, oldValue) => { return dataList; });
        }
        /// <summary>
        /// 更新缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public static void UpdateCache(string key, IData data)
        {
            data.TimeStamp = DateTime.Now;
            if (DataMap.ContainsKey(key))
            {
                var oldData = DataMap[key].FirstOrDefault(x => x.Address == data.Address);
                if (oldData == null || oldData.Value == data.Value)
                {
                    return;
                }
                if (DataChanged != null) DataChanged(key, data);

                oldData = data;
            }
            else
            {
                if (DataChanged != null) DataChanged(key, data);

                DataMap.TryAdd(key, new List<IData>() { data });
            }
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<IData> GetDataList(string key) => DataMap[key] ?? null;

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public static object GetValue(string key, string address) => DataMap[key]?.FirstOrDefault(x => x.Address == address)?.Value ?? null;

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public static IData GetData(string key, string address) => DataMap[key]?.FirstOrDefault(x => x.Address == address) ?? null;
    }
}
