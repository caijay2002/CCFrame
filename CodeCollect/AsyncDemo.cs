using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;


#region << 版 本 注 释 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2022   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 公司名称：
 * 命名空间：CodeCollect
 * 文件名：AsyncDemo
 * 创建者：蔡程健
 * 创建时间：2022/6/10 21:40:34
 * 版本：V1.0.0
 * 描述：
 *
 * ----------------------------------------------------------------
 * 修改人：
 * 时间：
 * 修改说明：
 *
 * 版本：V1.0.1
 *----------------------------------------------------------------*/
#endregion << 版 本 注 释 >>

namespace CodeCollect
{
    [LastModified("19 Jul 2017", "updated for C# 7 and .NET Core 2")]
    internal class AsyncDemo
    {
        private const string url = "http://www.cninnovation.com";

        //static async Task Main()
        //{
        //    //SynchronizedAPI();
        //    //AsynchronousPattern();
        //    CallerWithAsync();
        //    EventBasedAsyncPattern();
        //    await TaskBasedAsyncPatternAsync();
        //    Console.ReadLine();
        //}

        /// <summary>
        /// 调用异步方法
        /// 可以使用await关键字来调用返回任务的异步方法GreetingAsync，await需要有async修饰符声明的方法。
        /// </summary>
        private static async void CallerWithAsync()
        {
            TraceThreadAndTask($"started {nameof(CallerWithAsync)}");
            string result = await GreetingAsync("Stephanie");
            Console.WriteLine(result);
            TraceThreadAndTask($"ended {nameof(CallerWithAsync)}");
        }


        /// <summary>
        /// 基于事件的异步委托
        /// DownloadStringCompleted事件，这是UI线程，可以直接从事件处理UI元素
        /// </summary>
        private static void EventBasedAsyncPattern()
        {
            Console.WriteLine(nameof(EventBasedAsyncPattern));
            using (var client = new WebClient())
            {
                client.DownloadStringCompleted += (sender, e) =>
                {
                    Console.WriteLine(e.Result.Substring(0, 100));
                };
                client.DownloadStringAsync(new Uri(url));
                Console.WriteLine();
            }
        }

        /// <summary>
        /// 基于任务的异步模式
        /// </summary>
        /// <returns></returns>
        private static async Task TaskBasedAsyncPatternAsync()
        {
            Console.WriteLine(nameof(TaskBasedAsyncPatternAsync));
            using (var client = new WebClient())
            {
                //await关键字会接触线程（UI）的阻塞，完成其他任务，当方法完成后处理
                string content = await client.DownloadStringTaskAsync(url);
                Console.WriteLine(content.Substring(0, 100));
                Console.WriteLine();
            }
        }

        /// <summary>
        /// 基于任务的异步模式指定，后缀名Async，返回一个任务，Task.Run返回一个字符串 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        static Task<string> GreetingAsync(string name) =>
            Task.Run(() =>   //Task.Run<string>
            {
                TraceThreadAndTask($"running {nameof(GreetingAsync)}");
                return Greeting(name);
            });

        /// <summary>
        /// 延续任务
        /// ContinueWith任务完成后调用
        /// </summary>
        private static void CallerWithContinuationTask()
        {
            TraceThreadAndTask($"started {nameof(CallerWithContinuationTask)}");
            var t1 = GreetingAsync("Stephanie");
            t1.ContinueWith(t => //任务完成后调用
            {
                string result = t.Result;//可以访问任务返回的结果
                Console.WriteLine(result);
                TraceThreadAndTask($"ended {nameof(CallerWithContinuationTask)}");
            });
        }


        /// <summary>
        /// 使用组合器，并行运行
        /// 把每个异步方法都返回给结果赋值TASK，并行运行，这样可以运行更快
        /// </summary>
        private static async void MultipleAsyncMethodsWithCombinators1()
        {
            Task<string> t1 = GreetingAsync("Stephanie");
            Task<string> t2 = GreetingAsync("Matthias");
            await Task.WhenAll(t1, t2);//等待2个任务都完成。
            Console.WriteLine($"Finished both methods.{Environment.NewLine} Result 1: {t1.Result}{Environment.NewLine} Result 2: {t2.Result}");
        }

        /// <summary>
        /// 顺序调用多个异步方法
        /// </summary>
        private static async void MultipleAsyncMethods()
        {
            string s1 = await GreetingAsync("Stephanie");
            string s2 = await GreetingAsync("Matthias");
            Console.WriteLine($"Finished both methods.{Environment.NewLine} Result 1: {s1}{Environment.NewLine} Result 2: {s2}");
        }

        /// <summary>
        /// 使用AggregatedException信息
        /// 得到所有任务异常
        /// </summary>
        private static async void ShowAggregatedException()
        {
            Task taskResult = null;
            try
            {
                Task t1 = ThrowAfter(2000, "first");
                Task t2 = ThrowAfter(1000, "second");
                await (taskResult = Task.WhenAll(t1, t2));
            }
            catch (Exception ex)
            {
                // just display the exception information of the first task that is awaited within WhenAll
                Console.WriteLine($"handled {ex.Message}");
                foreach (var ex1 in taskResult.Exception.InnerExceptions)
                {
                    Console.WriteLine($"inner exception {ex1.Message} from task {ex1.Source}");
                }
            }
        }

        static async Task ThrowAfter(int ms, string message)
        {
            await Task.Delay(ms);
            throw new Exception(message);
        }


        static string Greeting(string name)
        {
            TraceThreadAndTask($"running {nameof(Greeting)}");
            Task.Delay(3000).Wait();
            return $"Hello, {name}";
        }

        public static void TraceThreadAndTask(string info)
        {
            string taskInfo = Task.CurrentId == null ? "no task" : "task " + Task.CurrentId;

            Console.WriteLine($"{info} in thread {Thread.CurrentThread.ManagedThreadId} and {taskInfo}");
        }
    }
}
