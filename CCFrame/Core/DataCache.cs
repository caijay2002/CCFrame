using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：DataCache
// 创 建 者：蔡程健
// 创建时间：22/6/30 13:27:37
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame.Core
{
    public delegate void DataItemChanged(object data);

    public class DataCache : SoftCacheBase
    {
        public event DataItemChanged DataChanged;
    }
}
