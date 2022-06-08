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

        //Func<int, int> f1 = x => x + 1;
        //Func<int, int> f2 = x => x + 2;
        //Func<int, int> f3 = Compose(f1, f2);
        //var x1 = f3(39); 返回值42
        /// <summary>
        /// 高阶函数以函数作为参数返回一个函数或者返回2个函数
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="f1">可以有两个不同的类型</param>
        /// <param name="f2"></param>
        /// <returns></returns>
        public static Func<T1, TResult> Compose<T1, T2, TResult>(
            Func<T1, T2> f1, //可以有两个不同的类型，一个输入T1，另一个输出T2
            Func<T2, TResult> f2) =>//输入T2与f1的输出类型相同
            a => f2(f1(a));
    }
}
