using System;
using System.Collections;
using System.Collections.Generic;

namespace DictionaryLib
{
    /// <summary>
    /// Class which implements IDictionary interface using Red-Black trees.
    /// </summary>
    /// <typeparam name="TKey"> Type of Key.</typeparam>
    /// <typeparam name="TValue"> Value of Key.</typeparam>
    public class DictionaryRB<TKey, TValue> : IDictionary<TKey, TValue>
        where TKey : IComparable, IComparable<TKey>, IEquatable<TKey>
    {
        /// <summary>
        /// Root of Red-Black tree.
        /// </summary>
        private NodeRB<TKey, TValue> root;

        /// <summary>
        /// The leaf node for Reb-Black tree
        /// This node does not contain any data.
        /// </summary>
        private static NodeRB<TKey, TValue> nilNode = new NodeRB<TKey, TValue>
        {
            Left = null,
            Right = null,
            Parent = null,
            Color = NodeColor.Black
        };

        /// <summary>
        /// Creates new instance of dictionary.
        /// </summary>
        public DictionaryRB()
        {
            this.root = nilNode;
            this.Count = 0;
        }

        /// <summary>
        /// Gets or sets the value of the element with the given key.
        /// </summary>
        /// <param name="key"> Key.</param>
        /// <returns> Returns value.</returns>
        public TValue this[TKey key]
        {
            //gets the value with the specified key
            get
            {
                NodeRB<TKey, TValue> node = this.GetRBNode(key);

                //if element is found return the value

                if (node != null)
                {
                    return node.Value;
                }

                //else throw exception
                else
                {
                    throw new KeyNotFoundException("Element with the given key doesn't exist in the dictioanry.");
                }
            }

            //sets the value 
            set
            {
                NodeRB<TKey, TValue> node = this.GetRBNode(key);

                //if the element is found then set the value 
                if (node != null)
                {
                    node.Value = value;
                }

                //else throw an exception
                else
                {
                    this.Insert(key, value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the count of Dictionary.
        /// </summary>
        public int Count
        {
            get; private set;
        }

        /// <summary>
        /// Gets the boolean value which indicates if the Dictionary is readonly
        /// </summary>
        public bool IsReadOnly
        {
            //gets the value of IsReadOnly
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the collection containing keys of dictioanry elements.
        /// </summary>
        public ICollection<TKey> Keys
        {
            get
            {
                List<TKey> keyList = new List<TKey>();
                List<NodeRB<TKey, TValue>> list = this.GetAllElements();

                //iterating through the dictioanry
                for (var counter = list.GetEnumerator(); counter.MoveNext() != false;)
                {
                    keyList.Add(counter.Current.Key);
                }

                return keyList;
            }
        }

        /// <summary>
        /// Gets the collection containing values of dictioanry elements
        /// </summary>
        public ICollection<TValue> Values
        {
            get
            {
                List<TValue> valueList = new List<TValue>();
                List<NodeRB<TKey, TValue>> list = this.GetAllElements();

                //iterating through the dictioanry
                for (var counter = list.GetEnumerator(); counter.MoveNext() != false;)
                {
                    valueList.Add(counter.Current.Value);
                }

                return valueList;
            }
        }


        /// <summary>
        /// Adds new item with the dictionary if there is no any other element with item's key.
        /// </summary>
        /// <param name="item"> Item. </param>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            //calls Insert for adding new item to the dictionary
            this.Insert(item.Key, item.Value);
        }

        /// <summary>
        /// Adds new element with the given key and value to the dictionary if there is
        /// no any other element with that key.
        /// </summary>
        /// <param name="key"> Key.</param>
        /// <param name="value"> Value.</param>
        public void Add(TKey key, TValue value)
        {
            //calls insert function for adding new item to the dictionary
            this.Insert(key, value);
        }

        /// <summary>
        /// Clears the dictionary.
        /// </summary>
        public void Clear()
        {
            this.root = null;
            this.Count = 0;
        }

        /// <summary>
        /// Indicates if the given item exists in the dictionary.
        /// </summary>
        /// <param name="item"> Item.</param>
        /// <returns> Returns true if the  given item exists in the dictionary. </returns>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return this.ContainsKey(item.Key);
        }

        /// <summary>
        /// Indicates if an element with the given key exists in the dictionary.
        /// </summary>
        /// <param name="key"> Key.</param>
        /// <returns> Returns true if the element with the given key exists in the dictioanry
        /// and false;otherwise
        /// </returns>
        public bool ContainsKey(TKey key)
        {
            return this.GetRBNode(key) != null;
        }

        /// <summary>
        /// Copies the elements of the DictionaryRB to an array, starting at arrayIndex
        /// </summary>
        /// <param name="array"> Array.</param>
        /// <param name="arrayIndex"> ArrayIndex. </param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            //if the given array is null
            //then throw an ArgumentNullException
            if (array == null)
            {
                throw new ArgumentNullException("Array cannot be null");
            }

            //if the passed arrayIndex is negative
            //then throw an IndexOutOfRangeException
            if (arrayIndex < 0)
            {
                throw new IndexOutOfRangeException("ArrayIndex must be non-negative.");
            }

            //if the array size is not big enouth
            //then throw Exception messaging about it
            if ((array.Length - arrayIndex) < this.Count)
            {
                throw new ArgumentException("Array size is not enough for storing Dictionary elements");
            }

            List<NodeRB<TKey, TValue>> list = this.GetAllElements();
            for (var counter = list.GetEnumerator(); counter.MoveNext() != false;)
            {
                array.SetValue(counter.Current, arrayIndex++);
            }
        }

        /// <summary>
        /// Gets enumerator of dictionary.
        /// </summary>
        /// <returns> Returns enumerator of dictioanr. </returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return
                ((IEnumerable<KeyValuePair<TKey, TValue>>)this.GetAllElements()).GetEnumerator();
        }

        /// <summary>
        /// Removes item if it exists in dictionary.
        /// </summary>
        /// <param name="item"> Item. </param>
        /// <returns> Returns true,if the item is  removed from the dictionary and false;otherwise</returns>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return this.Delete(item.Key);
        }

        /// <summary>
        /// Removes element with the given key if that element exists in dictionary.
        /// </summary>
        /// <param name="key"> Key. </param>
        /// <returns> Returns true,if the element with given key is removed from the dictionary and false;otherwise</returns>
        public bool Remove(TKey key)
        {
            return this.Delete(key);
        }

        /// <summary>
        /// Stores the value of the element with the given key in the value parameter
        /// if corresponding element exists in the dictionary.
        /// </summary>
        /// <param name="key"> Key.</param>
        /// <param name="value"> Value.</param>
        /// <returns> Returns false if the element with the given key exists in the dictionary and false;otherwise.</returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            NodeRB<TKey, TValue> node = this.GetRBNode(key);

            //if node is found then set the value and return true
            if (node != null)
            {
                value = node.Value;
                return true;
            }

            //otherwise return false
            else
            {
                value = default(TValue);
                return false;
            }
        }

        /// <summary>
        /// Gets the enumerator of dictioanry.
        /// </summary>
        /// <returns> Returns the enumerator of dictionary.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetAllElements().GetEnumerator();
        }

        /// <summary>
        /// Gets the node with the passed key,if it exists.
        /// </summary>
        /// <param name="key"> Key. </param>
        /// <returns> Returns the node with the specified key,if it exist,and null;otherwise.</returns>
        private NodeRB<TKey, TValue> GetRBNode(TKey key)
        {
            //variable for storing CompareTo value
            int comparison;
            NodeRB<TKey, TValue> tempNode = this.root;

            while (tempNode != nilNode)
            {
                //comparing keys
                comparison = key.CompareTo(tempNode.Key);

                //if the key is greater go to the right
                if (comparison > 0)
                {
                    tempNode = tempNode.Right;
                }

                //else if key is less then go to the left
                else if (comparison < 0)
                {
                    tempNode = tempNode.Left;
                }

                //if key is equal to tempNode key then the node is found
                //return the found node
                else
                {
                    return tempNode;
                }
            }

            //return null if the node with the given key is not found
            return null;
        }

        /// <summary>
        /// Gets the collection of all elements in the dictionary.
        /// </summary>
        /// <returns> Returns the collection of all elements in the dictionary.</returns>
        private List<NodeRB<TKey, TValue>> GetAllElements()
        {
            List<NodeRB<TKey, TValue>> list = new List<NodeRB<TKey, TValue>>();
            NodeRB<TKey, TValue> tempNode = this.root, predecessor;

            //doing inorder traversal using James Hiram Morris algorithm
            while (tempNode != nilNode)
            {
                //if the node's left child is nilNode
                //then add node to collection and move to the right
                if (tempNode.Left == nilNode)
                {
                    list.Add(tempNode);
                    tempNode = tempNode.Right;
                }

                //otherwise need to find the inorder predecessor of the tempNode
                else
                {
                    predecessor = tempNode.Left;
                    while (predecessor.Right != nilNode && predecessor.Right != nilNode)
                    {
                        predecessor = predecessor.Right;
                    }

                    //if inorder predecessor doesn't have right child then make tempNode its right child
                    if (predecessor.Right == nilNode)
                    {
                        predecessor.Right = tempNode;
                        tempNode = tempNode.Left;
                    }

                    //if predecessor has right child than add it to the list
                    //and move to the right
                    else
                    {
                        predecessor.Right = nilNode;
                        list.Add(tempNode);
                        tempNode = tempNode.Right;
                    }
                }
            }

            //return the list containing dictionary elements.
            return list;
        }

        /// <summary>
        /// Inserts new node to the tree with the given key and value,if there is no
        /// other element with that key
        /// </summary>
        /// <param name="key"> Key. </param>
        /// <param name="value"> Value. </param>
        private void Insert(TKey key, TValue value)
        {
            NodeRB<TKey, TValue> newNode = new NodeRB<TKey, TValue>(key, value, NodeColor.Red,null,nilNode,nilNode);
            NodeRB<TKey, TValue> temp = this.root, newNodeParent = nilNode;
            int comparison;

            //First of all we have to find the parent of newNode
            while (temp != nilNode)
            {
                //storing the parent
                //we need to do this because in the last iteration node will become nillNode
                newNodeParent = temp;

                comparison = key.CompareTo(temp.Key);

                //if the key is less than the key go to the left subtree
                if (comparison == -1)
                {
                    temp = temp.Left;
                }

                //else if the key is greater than the key go to the right subtree
                else if (comparison == 1)
                {
                    temp = temp.Right;
                }

                //otherwise if keys are equal
                //keys equality means that element with that key already exists in the dictionary
                //hence throw an exception
                else
                {
                    throw new Exception("Element with that key already exists in the dictionary");
                    
                }
            }

            //set the parent of newNode
            newNode.Parent = newNodeParent;

            //if newNode doesn't have parents then it becomes the root of the tree
            if (newNode.Parent == nilNode)
            {
                this.root = newNode;
            }

            //else if the key is less then becomes the left child
            else if (newNode.Key.CompareTo(newNodeParent.Key) < 0)
            {
                newNode.Parent.Left = newNode;
            }

            //otherwise newNode becomes its parent right child
            else
            {
                newNode.Parent.Right = newNode;
            }

            newNode.Color = NodeColor.Red;

            //incrementing the Count property of dictionary because new item is added
            ++this.Count;

            //fix tree after insertion
            this.InsertFixup(newNode);
        }

        /// <summary>
        /// Fixes the Red-Black tree after insertion
        /// </summary>
        /// <param name="node"> Node. </param>
        private void InsertFixup(NodeRB<TKey, TValue> node)
        {
            NodeRB<TKey, TValue> temp;

            //starting Red-Black fixing process
            while (node != this.root && node.Parent.Color == NodeColor.Red)
            {
                //if node's parent is a left child
                if (node.Parent == node.Parent.Parent.Left)
                {
                    //temp becomes the uncle of node
                    temp = node.Parent.Parent.Right;

                    //if the color of node's uncle is red
                    if (temp.Color == NodeColor.Red)
                    {
                        //the color of node's parent and node's uncle becomes black
                        node.Parent.Color = temp.Color = NodeColor.Black;

                        //the color of node's grandparent becomes red    
                        node.Parent.Parent.Color = NodeColor.Red;

                        //node becomes the grandparent of node
                        node = node.Parent.Parent;
                    }

                    //else if the color of node's uncle is not red and node is a right child
                    else
                    {
                        if (node == node.Parent.Right)
                        {
                            //node becomes its parent
                            node = node.Parent;

                            //left rotate starting with node
                            this.LeftRotate(node);
                        }

                        //otherwise node's parent color becomes black,grandparent's color becomes red
                        //and rotate to the right starting with node's grandparent
                        node.Parent.Color = NodeColor.Black;
                        node.Parent.Parent.Color = NodeColor.Red;

                        this.RightRotate(node.Parent.Parent);
                    }
                }

                //otherwise if node's parent is a right child
                else
                {
                    //temp becomes the uncle of node
                    temp = node.Parent.Parent.Left;

                    //if the color of node's uncle is red
                    if (temp.Color == NodeColor.Red)
                    {
                        //the color of node's parent and uncle becomes black
                        node.Parent.Color = temp.Color = NodeColor.Black;

                        //the color of node's grandparent becomes red
                        node.Parent.Parent.Color = NodeColor.Red;

                        //node becomes the grandparent of node
                        node = node.Parent.Parent;
                    }

                    //else if the color of node's uncle is not red and node is a left child
                    else
                    {
                        if (node == node.Parent.Left)
                        {
                            //node becomes its parent
                            node = node.Parent;

                            //rotate the subtree starting with node to the right
                            this.RightRotate(node);
                        }

                        //otherwise
                        //the color of node's parent becomes black,the color of grandparent becomes red
                        node.Parent.Color = NodeColor.Black;
                        node.Parent.Parent.Color = NodeColor.Red;

                        //rotate the subtree starting with node's grandparent to the left
                        this.LeftRotate(node.Parent.Parent);
                    }
                }
            }

            //set the color of the root as black 
            //because the color of the root of Red-Black tree must be black.
            this.root.Color = NodeColor.Black;
        }

        /// <summary>
        /// Rotates the tree  starting with the given subtree to the left.
        /// </summary>
        /// <param name="node"> Node.</param>
        private void LeftRotate(NodeRB<TKey, TValue> node)
        {
            //setting the Right child of node to temp
            NodeRB<TKey, TValue> temp = node.Right;

            //temp's left child becomes the right child of node
            node.Right = temp.Left;

            //if the right child of node has left child
            if (temp.Left != nilNode) 
            {
                temp.Left.Parent = node;
            }

            temp.Parent = node.Parent;

            //if node doesn't have parent then right child of node becomes the root of tree
            if (node.Parent == nilNode)
            {
                this.root = temp;
            }

            //else if node is a left child then right child of node becomes the
            //left child of node's parent
            else if (node == node.Parent.Left)
            {
                node.Parent.Left = temp;
            }

            //otherwise becomes right child
            else
            {
                node.Parent.Right = temp;
            }

            //putting node on the left of temp
            temp.Left = node;
            node.Parent = temp;
        }

        /// <summary>
        /// Rotates the tree with the given subtree to the right
        /// </summary>
        /// <param name="node"></param>
        private void RightRotate(NodeRB<TKey, TValue> node)
        {
            //setting the left child of node to temp
            NodeRB<TKey, TValue> temp = node.Left;

            //temp's right child becomes the left child of node
            node.Left = temp.Right;

            //if the left child of node doesn't have right child
            if (temp.Right != nilNode)
            {
                temp.Right.Parent = node;
            }

            temp.Parent = node.Parent;

            //if node doesn't have parent then left child of node becomes the root of the tree
            if (node.Parent == nilNode)
            {
                this.root = temp;
            }

            //else if node is a right child
            else if (node == node.Parent.Right)
            {
                node.Parent.Right = temp;
            }

            //otherwis if node is a left child
            else
            {
                node.Parent.Left = temp;
            }

            //putting node on the right of its left child
            temp.Left = node;
            node.Parent = temp;
        }

        /// <summary>
        /// Deletes the element with specified key from the dictionary if it exists there.
        /// </summary>
        /// <param name="key"> Key.</param>
        private bool Delete(TKey key)
        {
            NodeRB<TKey, TValue> node = this.GetRBNode(key);

            //if there is no element with the given key then return
            if (node == null)
            {
                return false;
            }

            //if the node is found then delete

            NodeRB<TKey, TValue> temp = node, fixupNode;
            NodeColor tempInitialColor = temp.Color;

            //if node doesn't have left child
            if (node.Left == nilNode)
            {
                //fixupNode becomes the same as the node's right child
                fixupNode = node.Right;

                //transplant node and node's right child
                this.Transplant(node, node.Right);
            }

            //else if node has left child but doesn't have right child
            else if (node.Right == nilNode)
            {
                //make fixupNode node's left child
                fixupNode = node.Left;

                //transplant node and node's left child
                this.Transplant(node, node.Left);
            }

            //otherwise if node  have 2 children
            else
            {
                //temp becomes the same as the leftmost node of node's righ subtree
                temp = MinimumKeyNode(node.Right);

                //update tempInitialColor
                tempInitialColor = temp.Color;

                //update fixupNode
                fixupNode = temp.Right;

                //if node is the right child of updated temp
                //make temp the parent of updated fixupNode
                if (temp.Parent == node)
                {
                    fixupNode.Parent = temp;
                }

                //otherwise 
                else
                {
                    //transplant temp and the right child of temp
                    this.Transplant(temp, temp.Right);

                    //the right child of node becomes the right child of temp
                    temp.Right = node.Right;

                    //temp becomes the parent of temp right
                    temp.Right.Parent = temp;
                }

                //transplant node and temp subtrees
                this.Transplant(node, temp);

                //update corresponding properties of temp
                temp.Left = node.Left;
                temp.Left.Parent = temp;
                temp.Color = node.Color;
            }
            //if the initial color of temp is black then red-black tree needs to be fixed
            //the new tree must save the properties of red-black tree
            if (tempInitialColor == NodeColor.Black)
            {
                this.DeleteFixup(fixupNode);
            }

            //decrement Count property of dictionary because one item is removed
            --this.Count;
            return true;
        }

        /// <summary>
        /// Fixes the red-black tree after deletion of a node.
        /// </summary>
        /// <param name="node"> Node. </param>
        private void DeleteFixup(NodeRB<TKey, TValue> fixupNode)
        {
            NodeRB<TKey, TValue> temp;
            //iterating all over the tree to fix up
            while (fixupNode != this.root && fixupNode.Color == NodeColor.Black)
            {
                //if fixupNode is a left child
                if (fixupNode == fixupNode.Parent.Left)
                {
                    //save the sibling of fixupNode
                    temp = fixupNode.Parent.Right;

                    //if the color of fixupNode's sibling is red
                    if (temp.Color == NodeColor.Red)
                    {
                        //make the sibling's color black,parent's color - red
                        temp.Color = NodeColor.Black;
                        fixupNode.Parent.Color = NodeColor.Red;

                        //rotate the subtree starting with the parent of fixupNode
                        this.LeftRotate(fixupNode.Parent);

                        //update the sibling
                        temp = fixupNode.Parent.Right;

                    }

                    if (temp == nilNode)
                    {
                        return;
                    }
                    //if (temp != nilNode)
                    //if both of temp's children are black

                    if (temp.Left.Color == NodeColor.Black && temp.Right.Color == NodeColor.Black)
                    {
                        temp.Color = NodeColor.Red;

                        //continue with the parent
                        fixupNode = fixupNode.Parent;
                    }

                    //if only the right child of temp is black
                    else
                    {
                        if (temp.Right.Color == NodeColor.Black)
                        {
                            //update colors
                            temp.Left.Color = NodeColor.Black;
                            temp.Color = NodeColor.Red;

                            //rotate the subtree starting with temp
                            this.RightRotate(temp);

                            //update temp
                            temp = fixupNode.Parent.Right;
                        }


                        //otherwise 
                        //update colors
                        temp.Color = fixupNode.Parent.Color;
                        fixupNode.Parent.Color = temp.Right.Color = NodeColor.Black;

                        //rotate the subtree starting with the parent of fixupNode
                        this.LeftRotate(fixupNode.Parent);
                        //store root in temp
                        temp = this.root;
                    }
                }
                //otherwise if fixupNode is a right child
                //doing the same only exchanging right and left because here we have symmetry
                else
                {

                    temp = fixupNode.Parent.Left;
                    if (temp == nilNode)
                    {
                        return;
                    }
                    //if sibling of fixupNode is red
                    if (temp.Color == NodeColor.Red)
                    {
                        //update colors
                        temp.Color = NodeColor.Black;
                        fixupNode.Parent.Color = NodeColor.Red;

                        //rotate to the right starting with the parent
                        this.RightRotate(fixupNode.Parent);

                        //update sibling
                        temp = fixupNode.Parent.Left;
                    }

                    //if the sibling's children are black
                    if (temp.Left.Color == NodeColor.Black && temp.Right.Color == NodeColor.Black)
                    {
                        temp.Color = NodeColor.Red;
                        fixupNode = fixupNode.Parent;
                    }

                    //if only the left child of sibling is black
                    else
                    {
                        if (temp.Left.Color == NodeColor.Black)
                        {
                            //update colors
                            temp.Right.Color = NodeColor.Black;
                            temp.Color = NodeColor.Red;

                            //rotate the subtree starting with the sibling
                            this.LeftRotate(temp);

                            //update sibling
                            temp = fixupNode.Parent.Left;
                        }

                        //setting colors
                        temp.Color = fixupNode.Parent.Color;
                        fixupNode.Parent.Color = temp.Left.Color = NodeColor.Black;

                        //rotate the subtree starting with sibling
                        this.RightRotate(temp);

                        //update sibling
                        temp = this.root;
                    }
                }
                temp.Color = NodeColor.Black;
            }
        }

        /// <summary>
        /// Transplants one subtree with another subtree.
        /// </summary>
        /// <param name="first"> First subtree.</param>
        /// <param name="second"> Second subtree.</param>
        private void Transplant(NodeRB<TKey,TValue> firstSubTree,NodeRB<TKey,TValue> secondSubTree)
        {
            //if first subtree has no parent then make the second subtree the root of the tree
            if(firstSubTree.Parent == nilNode)
            {
                this.root = secondSubTree;
            }

            //else if first subtree is a left child 
            //then make second subtree the left child of first subtree's parent
            else if(firstSubTree == firstSubTree.Parent.Left)
            {
                firstSubTree.Parent.Left = secondSubTree;
            }

            //otherwise if first subtree is a right child
            //then make second subtree the right child of first subtree's parent
            else
            {
                firstSubTree.Parent.Right = secondSubTree;
            }

            //first subtree's parent becomes second subtree's parent
            secondSubTree.Parent = firstSubTree.Parent;
        }

        /// <summary>
        /// Gets the element containing minimum key in the given subtree.
        /// </summary>
        /// <param name="node"> Node.</param>
        /// <returns> Returns the element containing minimum key in the given subtree. </returns>
        private  static NodeRB<TKey,TValue> MinimumKeyNode(NodeRB<TKey,TValue> node)
        {
            NodeRB<TKey, TValue> temp = node;

            //moving to the leftmost node in the given tree
            //because in binary search tree the minimal key is in the leftmost node
            while(temp.Left != nilNode)
            {
                temp = temp.Left;
            }

            return temp;
        }
    }
}

