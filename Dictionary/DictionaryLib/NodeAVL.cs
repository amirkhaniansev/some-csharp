using System;

namespace DictionaryLib
{
    /// <summary>
    /// Node class for AVL tree
    /// </summary>
    /// <typeparam name="TKey"> Type of key.</typeparam>
    /// <typeparam name="TValue"> Type of value.</typeparam>
    internal class NodeAVL<TKey,TValue> 
        where TKey:IComparable,IComparable<TKey>,IEquatable<TKey>
    {
        /// <summary>
        /// Key of node.
        /// </summary>
        public TKey Key
        {
            get;private set;
        }

        /// <summary>
        /// Data stored in node.
        /// </summary>
        public TValue Value
        {
            get;set;
        }

        /// <summary>
        /// Specifies the difference of childs heights.
        /// </summary>
        public sbyte Balance
        {
            get;set;
        }

        /// <summary>
        /// Parent node.
        /// </summary>
        public NodeAVL<TKey,TValue> Parent
        {
            get; set;
        } 

        /// <summary>
        /// Left child of treenode.
        /// </summary>
        public NodeAVL<TKey,TValue> Left
        {
            get;set;
        }

        /// <summary>
        /// Right child of treenode.
        /// </summary>
        public NodeAVL<TKey,TValue> Right
        {
            get; set; 
        }

        /// <summary>
        /// Contsructs new instance of NodeAVL
        /// </summary>
        /// <param name="key"> Key.</param>
        /// <param name="data"> Data.</param>
        /// <param name="balance"> Balance.</param>
        /// <param name="parent"> Parent node.</param>
        /// <param name="left"> Left child.</param>
        /// <param name="right"> Right child.</param>
        public NodeAVL(TKey key,TValue data,sbyte balance = 0,NodeAVL<TKey,TValue> parent = null,
            NodeAVL<TKey,TValue> left = null,NodeAVL<TKey,TValue> right = null)
        {
            this.Key = key;
            this.Value = data;
            this.Balance = balance;
            this.Parent = parent;
            this.Left = left;
            this.Right = right;
        }
    }
}
