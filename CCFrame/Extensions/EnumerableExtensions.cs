using System;
using System.Collections.Generic;

#region <<  >>
/*----------------------------------------------------------------
// 文件名称：EnumerableExtensions
// 创 建 者：蔡程健
// 创建时间：22/5/29 10:01:43
// 文件版本：
// ===============================================================
// 功能描述：
//		
//
//----------------------------------------------------------------*/
#endregion
namespace CCFrame.Extensions
{

    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Where1<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            foreach (T item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<T> Where2<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return Where2Impl(source, predicate);
        }

        private static IEnumerable<T> Where2Impl<T>(IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (T item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<T> Where3<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return Iterator();

            IEnumerable<T> Iterator()
            {
                foreach (T item in source)
                {
                    if (predicate(item))
                    {
                        yield return item;
                    }
                }
            }
        }
    }
}
