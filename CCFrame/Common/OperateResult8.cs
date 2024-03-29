﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


#region << 文 件 说 明 >>
/*----------------------------------------------------------------
// 文件名称：OperateResult8
// 创 建 者：蔡程健
// 创建时间：22/2/24 11:55:09
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame
{
    public class OperateResult<T1, T2, T3, T4, T5, T6, T7, T8> : OperateResult
    {
        public OperateResult()
        {
        }

        public OperateResult(string msg) : base(msg)
        {

        }

        public OperateResult(int err, string msg) : base(err, msg)
        {

        }

        public T1 Content1 { get; set; }
        public T2 Content2 { get; set; }
        public T3 Content3 { get; set; }
        public T4 Content4 { get; set; }
        public T5 Content5 { get; set; }
        public T6 Content6 { get; set; }
        public T7 Content7 { get; set; }
        public T8 Content8 { get; set; }

        /// <summary>
        /// 返回一个检查结果对象，可以进行自定义的数据检查
        /// </summary>
        /// <param name="check">检查的委托方法</param>
        /// <returns></returns>
        public OperateResult<T1, T2, T3, T4, T5, T6, T7, T8> Check(Func<T1, T2, T3, T4, T5, T6, T7, T8, OperateResult> check)
        {
            if (base.IsSuccess)
            {
                OperateResult result = check(this.Content1, this.Content2, this.Content3, this.Content4, this.Content5, this.Content6, this.Content7, this.Content8);
                if (!result.IsSuccess)
                {
                    return OperateResult.CreateFailedResult<T1, T2, T3, T4, T5, T6, T7, T8>(result);
                }
            }
            return (OperateResult<T1, T2, T3, T4, T5, T6, T7, T8>)this;
        }

        /// <summary>
        /// 返回一个检查结果对象，可以进行自定义的数据检查
        /// </summary>
        /// <param name="check">检查的委托方法</param>
        /// <param name="message">检查失败的错误消息</param>
        /// <returns></returns>
        public OperateResult<T1, T2, T3, T4, T5, T6, T7, T8> Check(Func<T1, T2, T3, T4, T5, T6, T7, T8, bool> check, string message = "All content data check failed")
        {
            if (!base.IsSuccess)
            {
                return (OperateResult<T1, T2, T3, T4, T5, T6, T7, T8>)this;
            }
            if (check(this.Content1, this.Content2, this.Content3, this.Content4, this.Content5, this.Content6, this.Content7, this.Content8))
            {
                return (OperateResult<T1, T2, T3, T4, T5, T6, T7, T8>)this;
            }
            return new OperateResult<T1, T2, T3, T4, T5, T6, T7, T8>(message);
        }

        #region 指定接下来要做的是内容，当前对象如果成功，就返回接下来的执行结果，如果失败，就返回当前对象本身。
        public OperateResult<TResult> Then<TResult>(Func<T1, T2, T3, T4, T5, T6, T7, T8, OperateResult<TResult>> func) =>
            (base.IsSuccess ? func(this.Content1, this.Content2, this.Content3, this.Content4, this.Content5, this.Content6, this.Content7, this.Content8) : OperateResult.CreateFailedResult<TResult>(this));

        public OperateResult<TResult1, TResult2> Then<TResult1, TResult2>(Func<T1, T2, T3, T4, T5, T6, T7, T8, OperateResult<TResult1, TResult2>> func) =>
            (base.IsSuccess ? func(this.Content1, this.Content2, this.Content3, this.Content4, this.Content5, this.Content6, this.Content7, this.Content8) : OperateResult.CreateFailedResult<TResult1, TResult2>(this));

        public OperateResult<TResult1, TResult2, TResult3> Then<TResult1, TResult2, TResult3>(Func<T1, T2, T3, T4, T5, T6, T7, T8, OperateResult<TResult1, TResult2, TResult3>> func) =>
            (base.IsSuccess ? func(this.Content1, this.Content2, this.Content3, this.Content4, this.Content5, this.Content6, this.Content7, this.Content8) : OperateResult.CreateFailedResult<TResult1, TResult2, TResult3>(this));

        public OperateResult<TResult1, TResult2, TResult3, TResult4> Then<TResult1, TResult2, TResult3, TResult4>(Func<T1, T2, T3, T4, T5, T6, T7, T8, OperateResult<TResult1, TResult2, TResult3, TResult4>> func) =>
            (base.IsSuccess ? func(this.Content1, this.Content2, this.Content3, this.Content4, this.Content5, this.Content6, this.Content7, this.Content8) : OperateResult.CreateFailedResult<TResult1, TResult2, TResult3, TResult4>(this));

        public OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5> Then<TResult1, TResult2, TResult3, TResult4, TResult5>(Func<T1, T2, T3, T4, T5, T6, T7, T8, OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5>> func) =>
            (base.IsSuccess ? func(this.Content1, this.Content2, this.Content3, this.Content4, this.Content5, this.Content6, this.Content7, this.Content8) : OperateResult.CreateFailedResult<TResult1, TResult2, TResult3, TResult4, TResult5>(this));

        public OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6> Then<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6>(Func<T1, T2, T3, T4, T5, T6, T7, T8, OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6>> func) =>
            (base.IsSuccess ? func(this.Content1, this.Content2, this.Content3, this.Content4, this.Content5, this.Content6, this.Content7, this.Content8) : OperateResult.CreateFailedResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6>(this));

        public OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7> Then<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7>(Func<T1, T2, T3, T4, T5, T6, T7, T8, OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7>> func) =>
            (base.IsSuccess ? func(this.Content1, this.Content2, this.Content3, this.Content4, this.Content5, this.Content6, this.Content7, this.Content8) : OperateResult.CreateFailedResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7>(this));

        public OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8> Then<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8>(Func<T1, T2, T3, T4, T5, T6, T7, T8, OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8>> func) =>
            (base.IsSuccess ? func(this.Content1, this.Content2, this.Content3, this.Content4, this.Content5, this.Content6, this.Content7, this.Content8) : OperateResult.CreateFailedResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8>(this));

        public OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9> Then<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9>(Func<T1, T2, T3, T4, T5, T6, T7, T8, OperateResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9>> func) =>
            (base.IsSuccess ? func(this.Content1, this.Content2, this.Content3, this.Content4, this.Content5, this.Content6, this.Content7, this.Content8) : OperateResult.CreateFailedResult<TResult1, TResult2, TResult3, TResult4, TResult5, TResult6, TResult7, TResult8, TResult9>(this));
        #endregion
    }
}
