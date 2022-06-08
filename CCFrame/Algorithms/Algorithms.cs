using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#region << 算法 >>
/*----------------------------------------------------------------
 * 版权所有 (c) 2022   保留所有权利。
 * CLR版本：4.0.30319.42000
 * 公司名称：
 * 命名空间：CCFrame.Algorithms
 * 文件名：Algorithms
 * 创建者：蔡程健
 * 创建时间：2022/6/8 21:22:27
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

namespace CCFrame.Algorithms
{
    internal class Algorithms
    {
        /// <summary>
        /// 递归方法快速排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="elements"></param>
        public static void QuickSort<T>(T[] elements) where T : IComparable<T>
        {
            void Sort(int start, int end)
            {
                int i = start, j = end;
                var pivot = elements[(start + end) / 2];

                while (i <= j)
                {
                    while (elements[i].CompareTo(pivot) < 0) i++;
                    while (elements[j].CompareTo(pivot) > 0) j--;
                    if (i <= j)
                    {
                        T tmp = elements[i];
                        elements[i] = elements[j];
                        elements[j] = tmp;
                        i++;
                        j--;
                    }
                }
                if (start < j) Sort(start, j);
                if (i < end) Sort(i, end);
            }

            Sort(0, elements.Length - 1);
        }
    }
}
