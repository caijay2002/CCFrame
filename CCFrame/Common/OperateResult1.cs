using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region << 操作结果的泛类 >>
/*----------------------------------------------------------------
// 文件名称：OperateResult1
// 创 建 者：蔡程健
// 创建时间：21/8/3 16:14:39
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame
{
    public class OperateResult<T> : OperateResult
    {
        public T Content { get; set; }

        public OperateResult()
        {
        }

        public OperateResult(string msg) : base(msg)
        {
        }

        public OperateResult(int err, string msg) : base(err, msg)
        {
        }

        /// <summary>
        /// 返回一个检查结果对象，可以进行自定义的数据检查
        /// </summary>
        /// <param name="check">检查的委托方法</param>
        /// <returns></returns>
        public OperateResult<T> Check(Func<T, OperateResult> check)
        {
            if (base.IsSuccess)
            {
                OperateResult result = check(this.Content);
                if (!result.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<T>(result);
                }
            }
            return (OperateResult<T>)this;
        }

        /// <summary>
        /// 返回一个检查结果对象，可以进行自定义的数据检查
        /// </summary>
        /// <param name="check">检查的委托方法</param>
        /// <param name="message">检查失败的错误消息</param>
        /// <returns></returns>
        public OperateResult<T> Check(Func<T, bool> check, string message = "All content data check failed")
        {
            if (!base.IsSuccess)
            {
                return (OperateResult<T>)this;
            }
            if (check(this.Content))
            {
                return (OperateResult<T>)this;
            }
            return new OperateResult<T>(message);
        }

        #region 指定接下来要做的是内容，当前对象如果成功，就返回接下来的执行结果，如果失败，就返回当前对象本身。
        public OperateResult Then(Func<T, OperateResult> func) =>
            (base.IsSuccess ? func(this.Content) : this);

        public OperateResult<TResult> Then<TResult>(Func<T, OperateResult<TResult>> func) =>
            (base.IsSuccess ? func(this.Content) : OperateResult.CreateFailedResult<TResult>(this));

        public OperateResult<TResult1, TResult2> Then<TResult1, TResult2>(Func<T, OperateResult<TResult1, TResult2>> func) =>
            (base.IsSuccess ? func(this.Content) : OperateResult.CreateFailedResult<TResult1, TResult2>(this));


        public OperateResult<TResult1, TResult2, TResult3> Then<TResult1, TResult2, TResult3>(Func<T, OperateResult<TResult1, TResult2, TResult3>> func) =>
            (base.IsSuccess ? func(this.Content) : OperateResult.CreateFailedResult<TResult1, TResult2, TResult3>(this));

        public OperateResult<TResult1, TResult2, TResult3, TResult4> Then<TResult1, TResult2, TResult3, TResult4>(Func<T, OperateResult<TResult1, TResult2, TResult3, TResult4>> func) =>
            (base.IsSuccess ? func(this.Content) : OperateResult.CreateFailedResult<TResult1, TResult2, TResult3, TResult4>(this));

        public OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5> Then<TResult1, TResult2, TResult3, TResult4, TResult5>(Func<T, OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5>> func) =>
            (base.IsSuccess ? func(this.Content) : OperateResult.CreateFailedResult<TResult1, TResult2, TResult3, TResult4, TResult5>(this));

        public OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6> Then<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6>(Func<T, OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6>> func) =>
            (base.IsSuccess ? func(this.Content) : OperateResult.CreateFailedResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6>(this));

        public OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7> Then<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7>(Func<T, OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7>> func) =>
            (base.IsSuccess ? func(this.Content) : OperateResult.CreateFailedResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7>(this));

        public OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8> Then<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8>(Func<T, OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8>> func) =>
            (base.IsSuccess ? func(this.Content) : OperateResult.CreateFailedResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8>(this));

        public OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9> Then<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9>(Func<T, OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9>> func) =>
            (base.IsSuccess ? func(this.Content) : OperateResult.CreateFailedResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9>(this));

        #endregion





    }
}
