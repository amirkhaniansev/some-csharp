using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LINQ_ExtensionsLib
{
    /// <summary>
    /// Class for specifically grouped sequences.
    /// </summary>
    /// <typeparam name="TKey"> Type of Key. </typeparam>
    /// <typeparam name="TSource"> Type of Source.</typeparam>
    internal class GroupedSequence<TKey, TSource> : IEnumerable<Group<TKey, TSource>>
    {
        /// <summary>
        /// Base enumerable sequence.
        /// </summary>
        private IEnumerable<TSource> source;

        /// <summary>
        /// Groups ordered by specific keys.
        /// </summary>
        private List<Group<TKey, TSource>> groups;

        /// <summary>
        /// Key selector function.
        /// </summary>
        private Func<TSource, TKey> keySelector;

        /// <summary>
        /// Equality comparer
        /// </summary>
        private readonly EqualityComparer<TKey> equalityComparer = EqualityComparer<TKey>.Default;

        /// <summary>
        /// Creates new grouped sequence.
        /// </summary>
        /// <param name="source"> Source. </param>
        /// <param name="keySelector"> Key selector function. </param>
        public GroupedSequence(IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            this.source = source;
            this.keySelector = keySelector;
            this.groups = new List<Group<TKey, TSource>>();

            //Groupify the given source.
            this.CreateGroups();
        }

        /// <summary>
        /// Gets enumerator.
        /// </summary>
        /// <returns> Returns enumerator. </returns>
        public IEnumerator<Group<TKey, TSource>> GetEnumerator()
        {
            return this.groups.GetEnumerator();
        }

        /// <summary>
        /// Gets enumerator.
        /// </summary>
        /// <returns> Returns enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Groupifies the given source.
        /// </summary>
        private void CreateGroups()
        {
            TKey key;
            Group<TKey, TSource> tempGroup;
            for (var counter = this.source.GetEnumerator(); counter.MoveNext() != false;)
            {
                key = this.keySelector(counter.Current);
                if (this.Contains(key, out tempGroup))
                {
                    tempGroup.Add(counter.Current);
                }
                else
                {
                    this.groups.Add(new Group<TKey, TSource>(key, counter.Current));
                }
            }
        }

        /// <summary>
        /// Checks if there is group with the given key.
        /// </summary>
        /// <param name="key"> Key.</param>
        /// <param name="gGroup"> Group.</param>
        /// <returns> Returns true if there is gropu with the specified key,and else otherwise. </returns>
        private bool Contains(TKey key, out Group<TKey, TSource> gGroup)
        {
            for (var counter = this.groups.GetEnumerator(); counter.MoveNext() != false;)
            {
                if (this.equalityComparer.Equals(key, counter.Current.Key))
                {
                    gGroup = counter.Current;
                    return true;
                }
            }
            gGroup = null;
            return false;
        }
    }

    /// <summary>
    /// Class for groups specified with unique key.
    /// </summary>
    /// <typeparam name="TKey"> Type of Key. </typeparam>
    /// <typeparam name="TSource"> Type of Source. </typeparam>
    internal class Group<TKey, TSource> : IGrouping<TKey, TSource>
    {
        /// <summary>
        /// Unique key for the group.
        /// </summary>
        private TKey key;

        /// <summary>
        /// Items of the group.
        /// </summary>
        private List<TSource> items;

        /// <summary>
        /// Gets the key of the group.
        /// </summary>
        public TKey Key
        {
            get { return this.key; }
        }

        /// <summary>
        /// Gest the items of group.
        /// </summary>
        public List<TSource> Items
        {
            get { return this.items; }
        }

        /// <summary>
        /// Creates new instance of group.
        /// </summary>
        /// <param name="key"> Key. </param>
        public Group(TKey key)
        {
            this.key = key;
            this.items = new List<TSource>();
        }

        /// <summary>
        /// Creates new instance of group.
        /// </summary>
        /// <param name="key"> Key. </param>
        /// <param name="source"> Source. </param>
        public Group(TKey key, TSource source) : this(key)
        {
            this.items.Add(source);
        }

        /// <summary>
        /// Adds new item to the key.
        /// </summary>
        /// <param name="item"> Item. </param>
        public void Add(TSource item)
        {
            items.Add(item);
        }

        /// <summary>
        /// Gets enumerator.
        /// </summary>
        /// <returns> Returns enumerator.</returns>
        public IEnumerator<TSource> GetEnumerator()
        {
            return this.items.GetEnumerator();
        }

        /// <summary>
        /// Gets enumerator.
        /// </summary>
        /// <returns> Returns enumerator. </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}