using System;

namespace DictionaryLib
{
    /// <summary>
    /// Node class Red Black tree node
    /// </summary>
    /// <typeparam name="TKey"> Type of key. </typeparam>
    /// <typeparam name="TValue"> Type of value. </typeparam>
    internal class NodeRB<TKey,TValue> 
        where TKey:IComparable,IComparable<TKey>,IEquatable<TKey> 
    {
        /// <summary>
        /// Key of the node.
        /// </summary>
        public TKey Key
        {
            get;set;
        }

        /// <summary>
        /// Value of the node.
        /// </summary>
        public TValue Value
        {
            get;set;
        }

        /// <summary>
        /// Left child of the node.
        /// </summary>
        public NodeRB<TKey, TValue> Left
        {
            get; set;
        }


        /// <summary>
        /// Right child of node.
        /// </summary>
        public NodeRB<TKey,TValue> Right
        {
            get;set;
        }

        /// <summary>
        /// Parent of the node
        /// </summary>
        public NodeRB<TKey,TValue> Parent
        {
            get;set;
        }

        /// <summary>
        /// Color of the node.
        /// </summary>
        internal NodeColor Color
        {
            get; set;
        }

        /// <summary>
        /// Creates new instance of Red-Black node tree.
        /// </summary>
        public NodeRB()
        {
            this.Color = NodeColor.Black;
            this.Right = this.Left = this.Parent = null;
        }

        /// <summary>
        /// Creates new instance of red-black tree node
        /// </summary>
        /// <param name="key"> Key of node.</param>
        /// <param name="value"> Value of node.</param>
        /// <param name="color"> Color of node.</param>
        /// <param name="parent"> Parent of node.</param>
        /// <param name="left"> Left child of node.</param>
        /// <param name="right"> Right child of node.</param>
        public NodeRB(TKey key,TValue value,NodeColor color = NodeColor.Black,
            NodeRB<TKey,TValue> parent = null,NodeRB<TKey,TValue> left = null,NodeRB<TKey,TValue> right = null)
        {
            this.Key = key;
            this.Value = value;
            this.Color = color;
            this.Left = left;
            this.Right = right;
            this.Parent = parent;
        }

      
    }
}
