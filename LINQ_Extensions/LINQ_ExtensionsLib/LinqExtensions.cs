using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ_ExtensionsLib
{
    /// <summary>
    /// Class containing extension methods
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        /// Makes new enumerable sequence from the given enumerable sequence using the given selector.
        /// </summary>
        /// <typeparam name="TSource"> Type of Source. </typeparam>
        /// <typeparam name="TResult"> Type of Result.</typeparam>
        /// <param name="source"> Source. </param>
        /// <param name="selector"> Selector function. </param>
        /// <returns> Returns new sequence projected with the help of given selector. </returns>
        public static IEnumerable<TResult> ExtensionSelect<TSource, TResult>(this IEnumerable<TSource> source,
            Func<TSource, TResult> selector)
        {
            //if source is null,then throw an exception
            if (source == null)
            {
                throw  new ArgumentNullException("Source is null");
            }

            //if selector is null,then throw an exception
            if (selector == null)
            {
                throw  new ArgumentNullException("Selector is null");
            }

            //if no argument is null then return new sequence.
            return new Sequence<TSource,TResult>(source,selector);
        }


        /// <summary>
        /// Constructs new enumerable sequence from the given sequence with the given predicate.
        /// </summary>
        /// <typeparam name="TSource"> Type of Source. </typeparam>
        /// <param name="source"> Source.</param>
        /// <param name="predicate"> Predicate. </param>
        /// <returns> Returns new enumerable sequence constructed from the given sequence with the predicate. </returns>
        public static IEnumerable<TSource> ExtensionWhere<TSource>(this IEnumerable<TSource> source,
            Func<TSource, int, bool> predicate)
        {
            //if source is null,then throw an exception
            if (source == null)
            {
                throw  new  ArgumentNullException("Source is null.");
            }

            //if predicate is null,then throw an exception
            if (predicate == null)
            {
                throw new ArgumentNullException("Predicate is null");
            }

            int index = -1;
            //if no argument is null ,then iterate all over the source and select elements where predicate is satisfied.
            for (var counter = source.GetEnumerator(); counter.MoveNext() != false;)
            {
                if (predicate(counter.Current, ++index))
                {
                    yield return counter.Current;
                }
            }
        }

        /// <summary>
        /// Constructs new enumerable sequence from the given sequence with the given predicate.
        /// </summary>
        /// <typeparam name="TSource"> Type of Source. </typeparam>
        /// <param name="source"> Source. </param>
        /// <param name="predicate"> Predicate. </param>
        /// <returns> Returns new enumerable sequence constructed from the given sequence with the predicate. </returns>
        public static IEnumerable<TSource> ExtensionWhere<TSource>(this IEnumerable<TSource> source,
            Func<TSource, bool> predicate)
        {
            //if source is null,then throw an exception
            if (source == null)
            {
                throw  new ArgumentNullException("Source is null.");
            }

            //if predicate is null,then throw an exception
            if (predicate == null)
            {
                throw  new ArgumentNullException("Predicate is null.");
            }

            //if no argument is null,iterate all over the source and select the items where  predicate is satisfied.
            for (var counter = source.GetEnumerator(); counter.MoveNext() != false;)
            {
                if (predicate(counter.Current))
                {
                    yield return counter.Current;
                }
            }
        }

        /// <summary>
        /// Makes list from the given enumerable sequence.
        /// </summary>
        /// <typeparam name="TSource"> Type of Source.</typeparam>
        /// <param name="source"> Source.</param>
        /// <returns> Returns list made from the enumerable sequence. </returns>
        public static List<TSource> ExtensionToList<TSource>(this IEnumerable<TSource> source)
        {
            //if source is null,then throw an exception
            if (source == null)
            {
                throw new ArgumentNullException("Source is null");
            }

            var resultList = new List<TSource>();

            //if source is not null,iterate all over the source to add elements to list.
            for (var counter = source.GetEnumerator(); counter.MoveNext() != false;)
            {
                resultList.Add(counter.Current);
            }
            
            //return the result
            return resultList;
        }

        /// <summary>
        /// Makes new dictionary from the given enumerable sequence with the help of the key selector function.
        /// </summary>
        /// <typeparam name="TSource"> Type of Source. </typeparam>
        /// <typeparam name="TKey"> Type of Key. </typeparam>
        /// <param name="source"> Source. </param>
        /// <param name="keySelector"> Key selector function.</param>
        /// <returns> Returns dictionary constructed with the given enumerable sequence. </returns>
        public static Dictionary<TKey, TSource> ExtensionToDictionary<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            //if source is null,then throw an exception.
            if (source == null)
            {
                throw  new ArgumentNullException("Source is null");
            }

            //if key selector is null,then throw an exception.
            if (keySelector == null)
            {
                throw  new ArgumentNullException("Key selector is null");
            }

            //if no argument is null,then start constructing dictionary
            var dictionary = new Dictionary<TKey,TSource>();

            for (var counter = source.GetEnumerator(); counter.MoveNext() != false;)
            {
                dictionary.Add(keySelector(counter.Current),counter.Current);
            }

            return dictionary;
        }

        /// <summary>
        /// Constructs new ordered enumerable sequence from the given source with the keySelector.
        /// </summary>
        /// <typeparam name="TSource"> Type of Source. </typeparam>
        /// <typeparam name="TKey"> Type of Key. </typeparam>
        /// <param name="source"> Source. </param>
        /// <param name="keySelector"> Key Selector. </param>
        /// <param name="descending"> Specifies the direction of order. </param>
        /// <returns> Returns new ordered enumerable sequence. </returns>
        public static IOrderedEnumerable<TSource> ExtensionOrderBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,bool descending)
        {
            //if source is null,then throw an exception
            if (source == null)
            {
                throw  new ArgumentNullException("Source is null");
            }

            //if key selector is null,then throw an exception
            if (keySelector == null)
            {
                throw  new ArgumentNullException("Key selector is null.");
            }


                return new OrderedSequence<TSource, TKey>(source, keySelector, descending).CreateOrderedEnumerable(
                    keySelector, null, descending);
        }

        /// <summary>
        /// Constructs new grouped enumerable sequence.
        /// </summary>
        /// <typeparam name="TSource"> Type of Source. </typeparam>
        /// <typeparam name="TKey"> Type of Key.</typeparam>
        /// <param name="source"> Source. </param>
        /// <param name="keySelector"> Key selector function. </param>
        /// <returns></returns>
       public static IEnumerable<IGrouping<TKey, TSource>> ExtensionGroupBy<TSource, TKey>(
            this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (source == null)
            {
                throw  new ArgumentException("Source is null.");
            }

            if (keySelector == null)
            {
                throw  new ArgumentException("Key selector is null");
            }

            return  new GroupedSequence<TKey,TSource>(source,keySelector);
        }
    }
}
