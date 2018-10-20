using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LINQ_ExtensionsLib
{
    /// <summary>
    /// Class for ordered enumerables.
    /// </summary>
    /// <typeparam name="TSource"> Type of source. </typeparam>
    /// <typeparam name="TKey"> Type of Key. </typeparam>
    internal class OrderedSequence<TSource,TKey> : IOrderedEnumerable<TSource>
    {
        /// <summary>
        /// Array.
        /// </summary>
        private List<TSource> array;

        /// <summary>
        /// Key Selector function
        /// </summary>
        private Func<TSource, TKey> keySelector;

        /// <summary>
        /// Comparer
        /// </summary>
        private Comparer<TKey> comparer;

        /// <summary>
        /// Boolean value which indicates the direction of sorting,
        /// </summary>
        private bool descending;

        /// <summary>
        /// Creates new ordered enumerable sequence.
        /// </summary>
        /// <typeparam name="TKey"> Type of Key. </typeparam>
        /// <param name="keySelector"> Key Selector. </param>
        /// <param name="comparer"> Comparer. </param>
        /// <param name="descending"> Descending value. </param>
        /// <returns> Returns new ordered enumerable sequence. </returns>
        public IOrderedEnumerable<TSource> CreateOrderedEnumerable<TKey>(Func<TSource, TKey> keySelector,
            IComparer<TKey> comparer, bool descending)
        {
            OrderedSequence<TSource,TKey> ordered = new OrderedSequence<TSource, TKey>(this.array,keySelector,descending);
            ordered.Sort();
            return ordered;
        }

        /// <summary>
        /// Creates new instance of ordered enumerable. 
        /// </summary>
        /// <param name="source"> Source. </param>
        /// <param name="keySelector"> Key Selector function. </param>
        /// <param name="descending"> Descending value. </param>
        public OrderedSequence(IEnumerable<TSource> source, Func<TSource, TKey> keySelector,bool descending)
        {
            this.array = new List<TSource>(source);
            this.comparer = Comparer<TKey>.Default;
            this.keySelector = keySelector;
            this.descending = descending;
        }

        /// <summary>
        /// Sorts the source.
        /// </summary>
        private void Sort()
        {
            this.MergeSort(this.array,0,this.array.Count - 1);
        }


        /// <summary>
        /// Merges the given sequence.
        /// </summary>
        /// <param name="arr"> Array.</param>
        /// <param name="left"> Left index. </param>
        /// <param name="middle"> Middle index. </param>
        /// <param name="right"> Right index. </param>
        private void Merge(List<TSource> arr, int left, int middle, int right)
        {
            int i, j, k, firstN = middle - left + 1, secondN = right - middle;
            List<TSource> Left = new List<TSource>();
            List<TSource> Right = new List<TSource>();

            for (i = 0; i < firstN; i++)
            {
                Left.Add(arr[left + i]);
            }
            for (j = 0; j < secondN; j++)
            {
                Right.Add(arr[middle + j + 1]);
            }
            i = j = 0; k = left;

            while (i < firstN && j < secondN)
            {
                if (this.CompareKeys(this.keySelector(Left[i]),this.keySelector(Right[j]))>=0)
                {
                    arr[k++] = Left[i++];
                }
                else
                {
                    arr[k++] = Right[j++];
                }
            }
            while (i < firstN)
            {
                arr[k++] = Left[i++];
            }
            while (j < secondN)
            {
                arr[k++] = Right[j++];
            }

        }

        /// <summary>
        /// Sorts the sequence.
        /// </summary>
        /// <param name="arr">Array.</param>
        /// <param name="left"> Left index. </param>
        /// <param name="right"> Right index. </param>
        private void MergeSort(List<TSource> arr, int left, int right)
        {
            if (left < right)
            {
                int middle = left + (right - left) / 2;
                MergeSort(arr, left, middle);
                MergeSort(arr, middle + 1, right);
                Merge(arr, left, middle, right);
            }
        }

        /// <summary>
        /// Compares the keys.
        /// </summary>
        /// <param name="key1"> 1st key. </param>
        /// <param name="key2"> 2nd key. </param>
        /// <returns> Returns the comparison value of keys. </returns>
        private int CompareKeys(TKey key1,TKey key2)
        {
           int comparison = this.comparer.Compare(key1,key2);
            return this.descending == true ? comparison : -comparison;
        }

        /// <summary>
        /// Gets enumerator. 
        /// </summary>
        /// <returns> Returns enumerator.</returns>
        public IEnumerator<TSource> GetEnumerator()
        {
            return this.array.GetEnumerator();
        }

        /// <summary>
        /// Gets enumerator.
        /// </summary>
        /// <returns> Returns enumerator. </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

