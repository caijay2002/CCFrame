using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCFrame;

#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：OperateResultDemo
// 创 建 者：蔡程健
// 创建时间：22/5/25 10:45:35
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace FrameTestDemo
{
    public static class OperateResultDemo
    {
        public static void CreatResult()
        {
            var ret1 = OperateResult.CreateFailedResult<int, int>(new OperateResult());
            ret1.Content1 = 10;
            ret1.Content2 = 11;
            ret1.IsSuccess = true;
            var ret2 = new OperateResult();

            var ret3 = ret1.Check((a, b) =>
            {
                ret1.Content1 = a;
                ret1.Content2 = b;
                ret1.IsSuccess = true;
                return ret1;
            });

            var ret4 = ret3.Convert<int, int, string>(123, 123, "157");

            var ret5 = ret4.Then((a, b, c) =>
            {

                return ret4;
            });
        }
    }
}
