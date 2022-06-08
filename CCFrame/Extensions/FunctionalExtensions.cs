using System;

namespace CCFrame.Extensions
{
    #region << 方法扩展类 >>
    /*----------------------------------------------------------------
    // 文件名称：FunctionalExtensions
    // 创 建 者：蔡程健
    // 创建时间：22/5/29 10:01:43
    // 文件版本：
    // ===============================================================
    // 功能描述：
    //		
    //
    //----------------------------------------------------------------*/
    #endregion
    public static class FunctionalExtensions
    {
        /// <summary>
        /// Use方法可以在单个语句中访问并释放资源
        /// 可以将using语句改为方法，扩展名为Use,他是所有实现IDisposable接口的类
        ///new Resource().Use(r => r.Foo());等效于下面的代码
        ///using (var r = new Resource())
        ///{
        ///    r.Foo();
        ///}
        /// </summary>
        /// <typeparam name="T">泛型参数</typeparam>
        /// <param name="item"></param>
        /// <param name="action"></param>
        public static void Use<T>(this T item, Action<T> action)
            where T : IDisposable
        {
            using (item)
            {
                action(item);
            }
        }

        public static Func<T1, TResult> Compose<T1, T2, TResult>(Func<T1, T2> f1, 
            Func<T2, TResult> f2) =>
            a => f2(f1(a));
    }
}
