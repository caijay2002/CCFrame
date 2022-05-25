using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCFrame.Language;


#region << 操作结果的类 >>
/*----------------------------------------------------------------
// 文件名称：OperateResult
// 创 建 者：蔡程健
// 创建时间：21/8/3 15:57:26
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion

namespace CCFrame
{
    /// <summary>
    /// 操作结果的类
    /// </summary>
    public class OperateResult
    {
        /// <summary>
        /// 故障代码
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        public OperateResult() { }

        /// <summary>
        /// 操作结果
        /// </summary>
        /// <param name="msg">消息</param>
        public OperateResult(string msg) { this.Message = msg; }

        /// <summary>
        /// 操作结果
        /// </summary>
        /// <param name="err"></param>
        /// <param name="msg"></param>
        public OperateResult(int err, string msg)
        {
            this.ErrorCode = err;
            this.Message = msg;
        }

        #region Convert
        public OperateResult<T> Convert<T>(T content) =>
        (this.IsSuccess ? CreateSuccessResult<T>(content) : CreateFailedResult<T>(this));

        public OperateResult<T1, T2> Convert<T1, T2>(T1 content1, T2 content2) =>
        (this.IsSuccess ? CreateSuccessResult<T1, T2>(content1, content2) : CreateFailedResult<T1, T2>(this));

        public OperateResult<T1, T2, T3> Convert<T1, T2, T3>(T1 content1, T2 content2, T3 content3) =>
        (this.IsSuccess ? CreateSuccessResult<T1, T2, T3>(content1, content2, content3) : CreateFailedResult<T1, T2, T3>(this));

        public OperateResult<T1, T2, T3, T4> Convert<T1, T2, T3, T4>(T1 content1, T2 content2, T3 content3, T4 content4) =>
        (this.IsSuccess ? CreateSuccessResult<T1, T2, T3, T4>(content1, content2, content3, content4) : CreateFailedResult<T1, T2, T3, T4>(this));

        public OperateResult<T1, T2, T3, T4, T5> Convert<T1, T2, T3, T4, T5>(T1 content1, T2 content2, T3 content3, T4 content4, T5 content5) =>
        (this.IsSuccess ? CreateSuccessResult<T1, T2, T3, T4, T5>(content1, content2, content3, content4, content5) : CreateFailedResult<T1, T2, T3, T4, T5>(this));

        public OperateResult<T1, T2, T3, T4, T5, T6> Convert<T1, T2, T3, T4, T5, T6>(T1 content1, T2 content2, T3 content3, T4 content4, T5 content5, T6 content6) =>
        (this.IsSuccess ? CreateSuccessResult<T1, T2, T3, T4, T5, T6>(content1, content2, content3, content4, content5, content6) : CreateFailedResult<T1, T2, T3, T4, T5, T6>(this));

        public OperateResult<T1, T2, T3, T4, T5, T6, T7> Convert<T1, T2, T3, T4, T5, T6, T7>(T1 content1, T2 content2, T3 content3, T4 content4, T5 content5, T6 content6, T7 content7) =>
        (this.IsSuccess ? CreateSuccessResult<T1, T2, T3, T4, T5, T6, T7>(content1, content2, content3, content4, content5, content6, content7) : CreateFailedResult<T1, T2, T3, T4, T5, T6, T7>(this));

        public OperateResult<T1, T2, T3, T4, T5, T6, T7, T8> Convert<T1, T2, T3, T4, T5, T6, T7, T8>(T1 content1, T2 content2, T3 content3, T4 content4, T5 content5, T6 content6, T7 content7, T8 content8) =>
        (this.IsSuccess ? CreateSuccessResult<T1, T2, T3, T4, T5, T6, T7, T8>(content1, content2, content3, content4, content5, content6, content7, content8) : CreateFailedResult<T1, T2, T3, T4, T5, T6, T7, T8>(this));

        public OperateResult<T1, T2, T3, T4, T5, T6, T7, T8, T9> Convert<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 content1, T2 content2, T3 content3, T4 content4, T5 content5, T6 content6, T7 content7, T8 content8, T9 content9) =>
        (this.IsSuccess ? CreateSuccessResult<T1, T2, T3, T4, T5, T6, T7, T8, T9>(content1, content2, content3, content4, content5, content6, content7, content8, content9) : CreateFailedResult<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this));

        #endregion

        #region ConvertFailed

        /// <summary>
        /// 将当前的结果对象转换到指定泛型的结果类对象，直接返回指定泛型的失败结果类对象
        /// </summary>
        /// <typeparam name="T">结果类型</typeparam>
        /// <returns></returns>
        public OperateResult<T> ConvertFailed<T>() => CreateFailedResult<T>(this);

        public OperateResult<T1, T2> ConvertFailed<T1, T2>() => CreateFailedResult<T1, T2>(this);

        public OperateResult<T1, T2, T3> ConvertFailed<T1, T2, T3>() =>
        CreateFailedResult<T1, T2, T3>(this);

        public OperateResult<T1, T2, T3, T4> ConvertFailed<T1, T2, T3, T4>() =>
        CreateFailedResult<T1, T2, T3, T4>(this);

        public OperateResult<T1, T2, T3, T4, T5> ConvertFailed<T1, T2, T3, T4, T5>() =>
        CreateFailedResult<T1, T2, T3, T4, T5>(this);

        public OperateResult<T1, T2, T3, T4, T5, T6> ConvertFailed<T1, T2, T3, T4, T5, T6>() =>
        CreateFailedResult<T1, T2, T3, T4, T5, T6>(this);

        public OperateResult<T1, T2, T3, T4, T5, T6, T7> ConvertFailed<T1, T2, T3, T4, T5, T6, T7>() =>
        CreateFailedResult<T1, T2, T3, T4, T5, T6, T7>(this);

        public OperateResult<T1, T2, T3, T4, T5, T6, T7, T8> ConvertFailed<T1, T2, T3, T4, T5, T6, T7, T8>() =>
        CreateFailedResult<T1, T2, T3, T4, T5, T6, T7, T8>(this);

        public OperateResult<T1, T2, T3, T4, T5, T6, T7, T8, T9> ConvertFailed<T1, T2, T3, T4, T5, T6, T7, T8, T9>() =>
        CreateFailedResult<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this);

        #endregion

        public void CopyErrorFromOther<TResult>(TResult result) where TResult : OperateResult
        {
            if (result != null)
            {
                this.ErrorCode = result.ErrorCode;
                this.Message = result.Message;
            }
        }

        #region 创建并返回一个成功的结果对象  CreateSuccessResult
        public static OperateResult CreateSuccessResult(string message) => new OperateResult
        {
            IsSuccess = true,
            ErrorCode = 0,
            Message = message
        };

        public static OperateResult CreateSuccessResult() => new OperateResult
        {
            IsSuccess = true,
            ErrorCode = 0,
            Message = StringResources.Language.SuccessText
        };

        public static OperateResult<T> CreateSuccessResult<T>(T value) => new OperateResult<T>
        {
            IsSuccess = true,
            ErrorCode = 0,
            Message = StringResources.Language.SuccessText,
            Content = value
        };

        public static OperateResult<T1, T2> CreateSuccessResult<T1, T2>(T1 value1, T2 value2) =>
        new OperateResult<T1, T2>
        {
            IsSuccess = true,
            ErrorCode = 0,
            Message = StringResources.Language.SuccessText,
            Content1 = value1,
            Content2 = value2
        };

        public static OperateResult<T1, T2, T3> CreateSuccessResult<T1, T2, T3>(T1 value1, T2 value2, T3 value3) =>
        new OperateResult<T1, T2, T3>
        {
            IsSuccess = true,
            ErrorCode = 0,
            Message = StringResources.Language.SuccessText,
            Content1 = value1,
            Content2 = value2,
            Content3 = value3
        };

        public static OperateResult<T1, T2, T3, T4> CreateSuccessResult<T1, T2, T3, T4>(T1 value1, T2 value2, T3 value3, T4 value4) =>
        new OperateResult<T1, T2, T3, T4>
        {
            IsSuccess = true,
            ErrorCode = 0,
            Message = StringResources.Language.SuccessText,
            Content1 = value1,
            Content2 = value2,
            Content3 = value3,
            Content4 = value4
        };
    
        public static OperateResult<T1, T2, T3, T4, T5> CreateSuccessResult<T1, T2, T3, T4, T5>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5) =>
        new OperateResult<T1, T2, T3, T4, T5>
        {
            IsSuccess = true,
            ErrorCode = 0,
            Message = StringResources.Language.SuccessText,
            Content1 = value1,
            Content2 = value2,
            Content3 = value3,
            Content4 = value4,
            Content5 = value5
        };

        public static OperateResult<T1, T2, T3, T4, T5, T6> CreateSuccessResult<T1, T2, T3, T4, T5, T6>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6) => 
        new OperateResult<T1, T2, T3, T4, T5, T6> { 
            IsSuccess = true,
            ErrorCode = 0,
            Message = StringResources.Language.SuccessText,
            Content1 = value1,
            Content2 = value2,
            Content3 = value3,
            Content4 = value4,
            Content5 = value5,
            Content6 = value6
        };

        public static OperateResult<T1, T2, T3, T4, T5, T6, T7> CreateSuccessResult<T1, T2, T3, T4, T5, T6, T7>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7) =>
        new OperateResult<T1, T2, T3, T4, T5, T6, T7>
        {
            IsSuccess = true,
            ErrorCode = 0,
            Message = StringResources.Language.SuccessText,
            Content1 = value1,
            Content2 = value2,
            Content3 = value3,
            Content4 = value4,
            Content5 = value5,
            Content6 = value6,
            Content7 = value7
        };

        public static OperateResult<T1, T2, T3, T4, T5, T6, T7, T8> CreateSuccessResult<T1, T2, T3, T4, T5, T6, T7, T8>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8) =>
        new OperateResult<T1, T2, T3, T4, T5, T6, T7, T8>
        {
            IsSuccess = true,
            ErrorCode = 0,
            Message = StringResources.Language.SuccessText,
            Content1 = value1,
            Content2 = value2,
            Content3 = value3,
            Content4 = value4,
            Content5 = value5,
            Content6 = value6,
            Content7 = value7,
            Content8 = value8
        };

        public static OperateResult<T1, T2, T3, T4, T5, T6, T7, T8, T9> CreateSuccessResult<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8, T9 value9) =>
        new OperateResult<T1, T2, T3, T4, T5, T6, T7, T8, T9>
        {
            IsSuccess = true,
            ErrorCode = 0,
            Message = StringResources.Language.SuccessText,
            Content1 = value1,
            Content2 = value2,
            Content3 = value3,
            Content4 = value4,
            Content5 = value5,
            Content6 = value6,
            Content7 = value7,
            Content8 = value8,
            Content9 = value9
        };

        #endregion

        #region 创建并返回一个失败的结果对象

        public static OperateResult CreateFailedResult(OperateResult result) => new OperateResult
        {
            ErrorCode = result.ErrorCode,
            Message = result.Message
        };

        /// <summary>
        /// 创建并返回一个失败的结果对象，该对象复制另一个结果对象的错误信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static OperateResult<T> CreateFailedResult<T>(OperateResult result) => new OperateResult<T>
        {
            ErrorCode = result.ErrorCode,
            Message = result.Message
        };

        public static OperateResult<T1, T2> CreateFailedResult<T1, T2>(OperateResult result) =>
            new OperateResult<T1, T2>
            {
                ErrorCode = result.ErrorCode,
                Message = result.Message
            };

        public static OperateResult<T1, T2, T3> CreateFailedResult<T1, T2, T3>(OperateResult result) =>
            new OperateResult<T1, T2, T3>
            {
                ErrorCode = result.ErrorCode,
                Message = result.Message
            };

        public static OperateResult<T1, T2, T3, T4> CreateFailedResult<T1, T2, T3, T4>(OperateResult result) =>
            new OperateResult<T1, T2, T3, T4>
            {
                ErrorCode = result.ErrorCode,
                Message = result.Message
            };

        public static OperateResult<T1, T2, T3, T4, T5> CreateFailedResult<T1, T2, T3, T4, T5>(OperateResult result) =>
            new OperateResult<T1, T2, T3, T4, T5>
            {
                ErrorCode = result.ErrorCode,
                Message = result.Message
            };

        public static OperateResult<T1, T2, T3, T4, T5, T6> CreateFailedResult<T1, T2, T3, T4, T5, T6>(OperateResult result) =>
            new OperateResult<T1, T2, T3, T4, T5, T6>
            {
                ErrorCode = result.ErrorCode,
                Message = result.Message
            };

        public static OperateResult<T1, T2, T3, T4, T5, T6, T7> CreateFailedResult<T1, T2, T3, T4, T5, T6, T7>(OperateResult result) =>
            new OperateResult<T1, T2, T3, T4, T5, T6, T7>
            {
                ErrorCode = result.ErrorCode,
                Message = result.Message
            };

        public static OperateResult<T1, T2, T3, T4, T5, T6, T7, T8> CreateFailedResult<T1, T2, T3, T4, T5, T6, T7, T8>(OperateResult result) =>
            new OperateResult<T1, T2, T3, T4, T5, T6, T7, T8>
            {
                ErrorCode = result.ErrorCode,
                Message = result.Message
            };

        public static OperateResult<T1, T2, T3, T4, T5, T6, T7, T8, T9> CreateFailedResult<T1, T2, T3, T4, T5, T6, T7, T8, T9>(OperateResult result) =>
            new OperateResult<T1, T2, T3, T4, T5, T6, T7, T8, T9>
            {
                ErrorCode = result.ErrorCode,
                Message = result.Message
            };
        #endregion

        #region Then 指定接下来要做的是内容
        /// <summary>
        /// 指定接下来要做的是内容，当前对象如果成功，就返回接下来的执行结果，如果失败，就返回当前对象本身。
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public OperateResult Then(Func<OperateResult> func) =>
        (this.IsSuccess ? func() : this);

        public OperateResult<T> Then<T>(Func<OperateResult<T>> func) =>
        (this.IsSuccess ? func() : CreateFailedResult<T>(this));

        public OperateResult<T1, T2> Then<T1, T2>(Func<OperateResult<T1, T2>> func) =>
        (this.IsSuccess ? func() : CreateFailedResult<T1, T2>(this));

        public OperateResult<T1, T2, T3> Then<T1, T2, T3>(Func<OperateResult<T1, T2, T3>> func) =>
        (this.IsSuccess ? func() : CreateFailedResult<T1, T2, T3>(this));

        public OperateResult<T1, T2, T3, T4> Then<T1, T2, T3, T4>(Func<OperateResult<T1, T2, T3, T4>> func) =>
        (this.IsSuccess ? func() : CreateFailedResult<T1, T2, T3, T4>(this));

        public OperateResult<T1, T2, T3, T4, T5> Then<T1, T2, T3, T4, T5>(Func<OperateResult<T1, T2, T3, T4, T5>> func) =>
        (this.IsSuccess ? func() : CreateFailedResult<T1, T2, T3, T4, T5>(this));

        public OperateResult<T1, T2, T3, T4, T5, T6> Then<T1, T2, T3, T4, T5, T6>(Func<OperateResult<T1, T2, T3, T4, T5, T6>> func) =>
        (this.IsSuccess ? func() : CreateFailedResult<T1, T2, T3, T4, T5, T6>(this));

        public OperateResult<T1, T2, T3, T4, T5, T6, T7> Then<T1, T2, T3, T4, T5, T6, T7>(Func<OperateResult<T1, T2, T3, T4, T5, T6, T7>> func) =>
        (this.IsSuccess ? func() : CreateFailedResult<T1, T2, T3, T4, T5, T6, T7>(this));

        public OperateResult<T1, T2, T3, T4, T5, T6, T7, T8> Then<T1, T2, T3, T4, T5, T6, T7, T8>(Func<OperateResult<T1, T2, T3, T4, T5, T6, T7, T8>> func) =>
        (this.IsSuccess ? func() : CreateFailedResult<T1, T2, T3, T4, T5, T6, T7, T8>(this));

        public OperateResult<T1, T2, T3, T4, T5, T6, T7, T8, T9> Then<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Func<OperateResult<T1, T2, T3, T4, T5, T6, T7, T8, T9>> func) =>
        (this.IsSuccess ? func() : CreateFailedResult<T1, T2, T3, T4, T5, T6, T7, T8, T9>(this));


        #endregion


        public string ToErrorMessage() =>
            $"{StringResources.Language.ErrorCode}:{this.ErrorCode}{Environment.NewLine}{StringResources.Language.TextDescription}:{this.Message}";

    }
}
