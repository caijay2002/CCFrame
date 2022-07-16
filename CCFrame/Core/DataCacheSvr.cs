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

    public delegate void DataChanged(string key, IData data,object value);

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
                if (value == null) return;
                if (oldData?.Value?.ToString() == value.ToString())
                {
                    return;
                }
                //var newData = oldData;
                //newData.Value = value;

                if (DataChanged != null) DataChanged(key, oldData,value);

                oldData.TimeStamp = DateTime.Now;
                oldData.Value = value;
            }
        }

        public static void InitializeCache(string key, List<IData> dataList)
        {
            DataMap.AddOrUpdate(key, dataList, 
                (oldkey, oldValue) => 
                {
                    dataList.ForEach(item => oldValue.Add(item));
                    return oldValue;
                });
        }
        /// <summary>
        /// 更新缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data">需要新建的值</param>
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
                if (DataChanged != null) DataChanged(key, data,data.Value);

                oldData = data;
            }
            else
            {
                if (DataChanged != null) DataChanged(key, data,data.Value);

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
        public static object GetValue(string key, string address) => DataMap[key]?.FirstOrDefault(x => x.Address == address)?.Value ?? 0;

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public static IData GetData(string key, string address) => DataMap[key]?.FirstOrDefault(x => x.Address == address) ?? null;
    }
}
