using System;
using System.Collections;
using System.Collections.Generic;

namespace LINQ_ExtensionsLib
{

    /// <summary>
    /// Sequence class for Select.
    /// </summary>
    /// <typeparam name="TSource"> Type of Source. </typeparam>
    /// <typeparam name="TResult"> Type of Result. </typeparam>
    internal class Sequence<TSource, TResult> : IEnumerable<TResult>
    {
        /// <summary>
        /// Base sequence for this class.
        /// </summary>
        private IEnumerable<TSource> baseEnumerable;

        /// <summary>
        /// Selector function.
        /// </summary>
        private Func<TSource, TResult> selector;


        /// <summary>
        /// Creates new instance of Sequence class.
        /// </summary>
        /// <param name="source"> Source. </param>
        /// <param name="selector"> Selector. </param>
        public Sequence(IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            this.baseEnumerable = source;
            this.selector = selector;
        }

        public IEnumerable<TSource> BaseEnumerable
        {
            get { return this.baseEnumerable; }
        }

        /// <summary>
        /// Gets enumerator for Sequence class.
        /// </summary>
        /// <returns> Returns enumerator of Sequence class. </returns>
        public IEnumerator<TResult> GetEnumerator()
        {
            return new Enumerator<TResult>(this);
        }

        /// <summary>
        /// Gets enumerator for Sequence class.
        /// </summary>
        /// <returns> Returns enumerator of Sequence class. </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Enumerator class for Sequence class.
        /// </summary>
        /// <typeparam name="T"> Type of generic argument.</typeparam>
        class Enumerator<T> : IEnumerator<T>
        {
            /// <summary>
            /// Sequence
            /// </summary>
            private Sequence<TSource, T> enumerable;

            /// <summary>
            /// Current element.
            /// </summary>
            private T current;

            /// <summary>
            /// Enumerator.
            /// </summary>
            private IEnumerator<TSource> iterator;

            /// <summary>
            /// Creates new instance of Enumerator.
            /// </summary>
            /// <param name="source"> </param>
            public Enumerator(Sequence<TSource, T> source)
            {
                this.enumerable = source;
                this.current = default(T);
                this.iterator = source.baseEnumerable.GetEnumerator();
            }

            /// <summary>
            /// Gets the current.
            /// </summary>
            public T Current
            {
                get { return this.current; }
            }

            /// <summary>
            /// Gets the current.
            /// </summary>
            object IEnumerator.Current
            {
                get { return this.current; }
            }

            /// <summary>
            /// Dispose method for Enumerator.
            /// </summary>
            public void Dispose()
            {
            }

            /// <summary>
            /// Moves to the next element if it exists.
            /// </summary>
            /// <returns> Returns true if next element exists. </returns>
            public bool MoveNext()
            {
                while (this.iterator.MoveNext())
                {
                    this.current = this.enumerable.selector(this.iterator.Current);
                    return true;

                }
                return false;
            }

            public void Reset()
            {
                this.current = default(T);
                this.iterator.Reset();
            }
        }
    }
}