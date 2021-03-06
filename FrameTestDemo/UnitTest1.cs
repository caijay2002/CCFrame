using Microsoft.VisualStudio.TestTools.UnitTesting;
using CCFrame;

namespace FrameTestDemo
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
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

        [TestMethod]
        public void TestMethod2()
        {

        }
    }
}
