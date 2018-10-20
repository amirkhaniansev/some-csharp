using System;
using System.Collections;
using System.Collections.Generic;

namespace DictionaryLib
{
    /// <summary>
    /// Dictionary class implementation using AVL tree.
    /// </summary>
    /// <typeparam name="TKey"> Type of key. </typeparam>
    /// <typeparam name="TValue"> Type of value. </typeparam>
    public class DictionaryAVL<TKey, TValue> : IDictionary<TKey, TValue>
        where TKey :IComparable ,IComparable<TKey>,IEquatable<TKey>
    {
        /// <summary>
        /// Root of AVL tree.
        /// </summary>
        private NodeAVL<TKey, TValue> root;

        /// <summary>
        /// Gets the value with the given key.
        /// </summary>
        /// <param name="key"> Key.</param>
        /// <returns> Returns the value with the given key. </returns>
        public TValue this[TKey key]
        {           
            //gets the value
            get
            {
                NodeAVL<TKey, TValue> temp = this.GetNode(key);
                
                //if the key is not present in dictionary
                //throw an exception
                if(temp == null)
                {
                    throw new KeyNotFoundException("The key is not present in Dictionary.");
                }
                return temp.Value;
            }

            //sets the value
            set
            {
                NodeAVL<TKey, TValue> temp = this.GetNode(key);

                //if the key is not present in dictionary
                //throw an exception
                if (temp == null)
                {
                    throw new KeyNotFoundException("The key is not present in Dictionary.");
                }
                temp.Value = value;
            }
        }

        /// <summary>
        /// Gets the amount of items in dictionary.
        /// </summary>
        public int Count
        {
            get;private set;
        }

        /// <summary>
        /// Gets true if dictionary is readonly,otherwise;false.
        /// </summary>
        public bool IsReadOnly
        {
            //gets IsReadOnly value;
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the collection o keys
        /// </summary>
        public ICollection<TKey> Keys
        {
            get
            {
                List<TKey> keyList = new List<TKey>();
                
                //get all nodes
                List<NodeAVL<TKey, TValue>> nodeList = this.GetAllElements();
                for(var counter = nodeList.GetEnumerator();counter.MoveNext()!=false;)
                {
                    keyList.Add(counter.Current.Key);
                }
                return keyList;
            }
        }

        /// <summary>
        /// Gets the collection of values
        /// </summary>
        public ICollection<TValue> Values
        {
            get
            {
                List<TValue> valueList = new List<TValue>();
                
                //get all nodes 
                List<NodeAVL<TKey, TValue>> nodeList = this.GetAllElements();
                for (var counter = nodeList.GetEnumerator(); counter.MoveNext() != false;)
                {
                    valueList.Add(counter.Current.Value);
                }
                return valueList;
            }
        }

        /// <summary>
        /// Adds the item to the dictionary if the item is not present in Dictionary.
        /// </summary>
        /// <param name="item"> Item.</param>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            this.Insert(item.Key, item.Value);
        }

        /// <summary>
        /// Adds the element with specified key and value to the dictionary
        /// if it is not present in there.
        /// </summary>
        /// <param name="key"> Key.</param>
        /// <param name="value"> Value.</param>
        public void Add(TKey key, TValue value)
        {
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
        /// Determines if dictionary contains the given element.
        /// </summary>
        /// <param name="item"> Dictionary element. </param>
        /// <returns> Returns true if dictionary contains the given element and false;otherwise.</returns>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return this.ContainsKey(item.Key);
        }

        /// <summary>
        /// Determines if there is element with the given key.
        /// </summary>
        /// <param name="key"> Key.</param>
        /// <returns> Returns true if element with the given key is found and false;otherwise. </returns>
        public bool ContainsKey(TKey key)
        {
            return this.GetNode(key) != null;
        }

        /// <summary>
        /// Copies the elements of the DictionaryAVL to an array, starting at arrayIndex
        /// </summary>
        /// <param name="array"> Array.</param>
        /// <param name="arrayIndex"> arrayIndex.</param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            //if the given array is null
            //then throw an ArgumentNullException
            if(array == null)
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
            if((array.Length - arrayIndex) < this.Count)
            {
                throw new ArgumentException("Array size is not enough for storing Dictionary elements");
            }

            List<NodeAVL<TKey, TValue>> list = this.GetAllElements();
            for(var counter = list.GetEnumerator();counter.MoveNext()!=false;)
            {
                array.SetValue(counter.Current, arrayIndex++);
            }
        }

        /// <summary>
        /// Gets enumerator of dictionary.
        /// </summary>
        /// <returns> Returns enumerator of dictionary.</returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return
                ((IEnumerable<KeyValuePair<TKey, TValue>>)this.GetAllElements()).GetEnumerator();
        }

        /// <summary>
        /// Removes the given element from the dictionary if it exists.
        /// </summary>
        /// <param name="item"> Item.</param>
        /// <returns> Returns true if the given element exists in the Dictionary and false;otherwise. </returns>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return this.Delete(item.Key);
        }

        /// <summary>
        /// Removes the element with the given key from the Dictionary if its exists.
        /// </summary>
        /// <param name="key"> Key. </param>
        /// <returns> Returns true if the element with the given key exists and false ;otherwise. </returns>
        public bool Remove(TKey key)
        {
            return this.Delete(key);
        }

        /// <summary>
        /// Copies the value with given key to the value parameter.
        /// </summary>
        /// <param name="key"> Key.</param>
        /// <param name="value"> Value.</param>
        /// <returns> Returns true,if the element exists and false;otherwise.</returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            NodeAVL<TKey, TValue> node = this.GetNode(key);

            //if node is not null,i.e the lement with the given key exists
            //copy the value to out value parameter and return true
            if (node != null)
            {
                value = node.Value;
                return true;
            }

            //if node exists with the given key return false;
            else
            {
                value = default(TValue);
                return false;
            }
        }
        
        /// <summary>
        /// Gets Enumerator of the Dictionary.
        /// </summary>
        /// <returns> Returns Enumerator of the Dictionary.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetAllElements().GetEnumerator();
        }

        

        /**********           private helper methods.        *******/

        /// <summary>
        /// Gets node with specified key.
        /// </summary>
        /// <param name="key"> Key of node.</param>
        /// <returns> Returns node with specified key if it exists and null otherwise.</returns>
        private NodeAVL<TKey,TValue> GetNode(TKey key)
        {
            NodeAVL<TKey, TValue> temp = this.root;
            int comparison;
            while(temp!=null)
            {
                //compare the given key with tree node key
                comparison = key.CompareTo(temp.Key);

                //if key is greater than node key
                //go to the right subtree
                if(comparison == 1)
                {
                    temp = temp.Right;
                }

                //else if key is less than node key
                //go to the left subtree
                else if(comparison == -1)
                {
                    temp = temp.Left;
                }
                
                //else return found node.
                else
                {
                    return temp;
                }
            }

            //if no node is found with given key,return null.
            return null;
        }

        /// <summary>
        /// Gets the collection of all nodes in AVL tree.
        /// </summary>
        /// <returns> Returns the collection of all nodes. </returns>
        private List<NodeAVL<TKey,TValue>> GetAllElements()
        {
            List<NodeAVL<TKey, TValue>> list = new List<NodeAVL<TKey, TValue>>();
            NodeAVL<TKey, TValue> temp = this.root, predecessor;

            //doing inorder traversal
            //using James Hiram Morris algorithm
            while(temp != null)
            {
                if(temp.Left == null)
                {
                    list.Add(temp);
                    temp = temp.Right;
                }
                else
                {
                    //finding the inorder predecessor
                    predecessor = temp.Left;
                    while (predecessor.Left != null && predecessor.Right != temp)
                    {
                        predecessor = predecessor.Right;
                    }

                    //temp must become the right child of predecessor
                    if (predecessor.Right == null)
                    {
                        predecessor.Right = temp;
                        temp = temp.Left;
                    }

                    //if predecessor has right child than add it to the list
                    else
                    {
                        predecessor.Right = null;
                        list.Add(temp);
                        temp = temp.Right;
                    }
                } 
            }
            return list; 
        }

        /// <summary>
        /// Rotates the subtree to the left.
        /// </summary>
        /// <param name="sRoot"> Root of the subtree. </param>
        /// <param name="rightChild"> Right child. </param>
        /// <returns> Returns new root of the subtree.</returns>
        private static NodeAVL<TKey,TValue> LeftRotate(NodeAVL<TKey,TValue> sRoot,NodeAVL<TKey,TValue> rightChild)
        {
            //storing the grandchild of sRoot
            NodeAVL<TKey, TValue> temp = rightChild.Left;

            sRoot.Right = temp;

            //if right child has left son
            if(temp != null)
            {
                temp.Parent = sRoot;
            }

            rightChild.Left = sRoot;
            sRoot.Parent = rightChild;

            //first case balance factor of right child is 0
            //can occur only during deletion
            if (rightChild.Balance == 0)
            {
                sRoot.Balance = 1; rightChild.Balance = -1;
            }

            //second case can happen during either deletion or insertion
            else
            {
                sRoot.Balance = rightChild.Balance = 0;
            }

            return rightChild;
        }

        /// <summary>
        /// Rotates the subtree to the right.
        /// </summary>
        /// <param name="sRoot"> Root of the subtree.</param>
        /// <param name="leftChild"> Left child. </param>
        /// <returns> Returns new root of the subtree. </returns>
        private static NodeAVL<TKey,TValue> RightRotate(NodeAVL<TKey,TValue> sRoot,NodeAVL<TKey,TValue> leftChild)
        {
            //storing the grandchild of sRoot in temp
            NodeAVL<TKey, TValue> temp = leftChild.Right;

            sRoot.Left = temp;

            //if left child has right son
            if(temp != null)
            {
                temp.Parent = sRoot;
            }

            leftChild.Right = sRoot;
            sRoot.Parent = leftChild;

            //first case happens only during deletion
            if(leftChild.Balance == 0)
            {
                sRoot.Balance = 1; leftChild.Balance = -1;
            }

            //second case happens during either deletion or insertion
            else
            {
                leftChild.Balance = sRoot.Balance = 0;
            }

            return leftChild;
        }

        /// <summary>
        /// Does double right-left rotation.
        /// </summary>
        /// <param name="sRoot"> Root of the subtree.</param>
        /// <param name="rightChild"> Right Child. </param>
        /// <returns> Returns new root of the subtree.</returns>
        private static NodeAVL<TKey, TValue> RightLeftRotate(NodeAVL<TKey, TValue> sRoot, NodeAVL<TKey, TValue> rightChild)
        {
            NodeAVL<TKey, TValue> temp1, temp2, temp3;

            temp1 = rightChild.Left;
            temp2 = temp1.Right;
            rightChild.Left = temp2;

            //if the grandchild exists
            if(temp2 != null)
            {
                temp2.Parent = rightChild;
            }

            temp1.Right = rightChild;
            rightChild.Parent = temp1;

            temp3 = temp1.Left;
            sRoot.Right = temp3;

            //if the grandchild exists
            if (temp3 != null)
            {
                temp3.Parent = sRoot;
            }
            temp1.Left = sRoot;
            sRoot.Parent = temp1;

            //we need to update balance factors
            //first case,occurs during insertion or deletion
            if(temp1.Balance > 0)
            {
                sRoot.Balance = -1;rightChild.Balance = 0;
            }

            //second case,occurs only during deletion
            else if(temp1.Balance == 0)
                {
                    sRoot.Balance = rightChild.Balance = 0;
                }
            //third case happens with deletion or insertion
                else
                {
                    sRoot.Balance = 0;
                    rightChild.Balance = 1;
                }
            temp1.Balance = 0;
            
            //return new root of the tree
            return temp1;
        }

        /// <summary>
        /// Does double left-right rotation.
        /// </summary>
        /// <param name="sRoot"> Root of the subtree.</param>
        /// <param name="leftChild"> Left Child. </param>
        /// <returns> Returns new root of the subtree.</returns>
        private static NodeAVL<TKey, TValue> LeftRightRotate(NodeAVL<TKey, TValue> sRoot, NodeAVL<TKey, TValue> leftChild)
        {
            NodeAVL<TKey, TValue> temp1, temp2, temp3;

            temp1 = leftChild.Right;
            temp2 = temp1.Left;
            leftChild.Right = temp2;

            //if the grandchild exists
            if (temp2 != null)
            {
                temp2.Parent = leftChild;
            }

            temp1.Left = leftChild;
            leftChild.Parent = temp1;

            temp3 = temp1.Right;
            sRoot.Left = temp3;

            //if the grandchild exists
            if (temp3 != null)
            {
                temp3.Parent = sRoot;
            }
            temp1.Right = sRoot;
            sRoot.Parent = temp1;

            //we need to update balance factors
            //first case,occurs during insertion or deletion
            if (temp1.Balance > 0)
            {
                sRoot.Balance = -1; leftChild.Balance = 0;
            }

            //second case,occurs only during deletion
            else if (temp1.Balance == 0)
            {
                sRoot.Balance = leftChild.Balance = 0;
            }
            //third case happens with deletion or insertion
            else
            {
                sRoot.Balance = 0;
                leftChild.Balance = 1;
            }
            temp1.Balance = 0;

            //return new root of the tree
            return temp1;

        }

        /// <summary>
        /// Inserts new item to the dictionary.
        /// </summary>
        /// <param name="key"> Key.</param>
        /// <param name="value"> Value.</param>
        private void Insert(TKey key,TValue value)
        {
            //constructing new node 
            NodeAVL<TKey, TValue> newNode = new NodeAVL<TKey, TValue>(key,value,0,null,null,null);
            NodeAVL<TKey, TValue> tempNode = this.root;
           // NodeAVL<TKey, TValue> newNodeParent = null;
            int comparison;

            //iterating all over the tree to find the right place for insertion
            while(tempNode != null)
            {
                //save the parent
                newNode.Parent = tempNode;
                comparison = key.CompareTo(tempNode.Key);

                if (comparison < 0)
                {
                    //go to the left subtree
                    tempNode = tempNode.Left;
                }
                else if (comparison > 0)
                {
                    //go to the right suntree
                    tempNode = tempNode.Right;
                }

                //otherwise if key is found throw exception cause key collisions is not allowed
                else
                {
                    throw new Exception("Item with that key already exists in the dictionary.");
                }
            }

            //newNode.Parent = newNodeParent;

            //if new node has no parent then make it the root of the tree
            if(newNode.Parent == null)
            {
                this.root = newNode;
            }

            else if (key.CompareTo(newNode.Parent.Key) < 0)
            {
                //make the new node a left child
                newNode.Parent.Left = newNode;
            }
            else
            {
                //make the new node  a right child
                newNode.Parent.Right = newNode;
            }

            //increment count of the dictionary
            ++this.Count;

            //rebalancing the tree after insertion to keep AVL tree arrangement
            this.InsertFixup(newNode);
        }

        /// <summary>
        /// Rebalances the tree after insertion.
        /// </summary>
        /// <param name="node"> New item. </param>
        private void InsertFixup(NodeAVL<TKey,TValue> node)
        {
            NodeAVL<TKey, TValue> counterParent, newRoot;
            //iterating for rebalancing
            for(var counter = node.Parent;counter != null; counter = node.Parent)
            {
                //if node is the right child of counter
                if (node == counter.Right)
                {
                    //if counter is right heavy
                    if (counter.Balance > 0)
                    {
                        //save the parent of counter
                        counterParent = counter.Parent;

                        //right left case
                        if (node.Balance < 0)
                        {
                            //double rotate
                            newRoot = /*this.*/RightLeftRotate(counter, node);
                        }
                        //right right case
                        else
                        {
                            newRoot = /*this.*/LeftRotate(counter, node);
                        }
                    }

                    else
                    {
                        //if counter is left heavy
                        if (counter.Balance < 0)
                        {
                            counter.Balance = 0; break;
                        }
                        counter.Balance = 1;
                        node = counter;
                        continue;
                    }
                }
                //if node is the left child of counter
                else
                {
                    //if counter is left heavy
                    if (counter.Balance < 0)
                    {
                        //save the parent of counter
                        counterParent = counter.Parent;

                        //left right case
                        if (node.Balance > 0)
                        {
                            newRoot = /*this.*/LeftRightRotate(counter, node);
                        }

                        //left left case
                        else
                        {
                            newRoot = /*this.*/RightRotate(counter, node);
                        }
                    }

                    else
                    {
                        //if counter is right heavy
                        if (counter.Balance > 0)
                        {
                            counter.Balance = 0; break;
                        }
                        counter.Balance = -1;
                        node = counter;
                        continue;
                    }
                }

                //adapting parent link after rotation
                //newRoot is the new root of rotated subtree.
                newRoot.Parent = counterParent;
                if(counterParent != null)
                {
                    //if counter is a left child
                    if(counter == counterParent.Left)
                    {
                        counterParent.Left = newRoot;
                    }

                    //if counter is a right child
                    else
                    {
                        counterParent.Right = newRoot;
                    }
                    break;
                }
                else
                {
                    this.root = newRoot; break;
                }
            }
        }

        /// <summary>
        /// Deletes the item with the specified key.
        /// </summary>
        /// <param name="key"> Key. </param>
        /// <returns> Returns true if node is deleted and false;otherwise. </returns>
        private bool Delete(TKey key)
        {
            NodeAVL<TKey, TValue> deleteNode = this.GetNode(key), fixupNode;

            if(deleteNode == null)
            {
                return false; 
            }

            if(deleteNode.Left == null)
            {
                fixupNode = deleteNode.Right;
                this.Transplant(deleteNode, deleteNode.Right);
            }
            else if (deleteNode.Right == null)
            {
                fixupNode = deleteNode.Left;
                this.Transplant(deleteNode, deleteNode.Left);
            }
            else
            {
                NodeAVL<TKey, TValue> temp = MinimumKeyNode(deleteNode.Right);
                fixupNode = temp.Right;
                if (temp.Parent != deleteNode)
                {
                    this.Transplant(temp, temp.Right);
                    temp.Right = deleteNode.Right;
                    temp.Right.Parent = temp;
                }
                this.Transplant(deleteNode, temp);
                temp.Left = deleteNode.Left;
                temp.Left.Parent = temp;
            }

            //decrement the count of the dictionary
            --this.Count;

            //rebalancing tree after deletion to keep AVL tree arrangement
            if (FindUnBalanced(deleteNode) != null)
            {
                this.DeleteFixup(FindUnBalanced(deleteNode));
            }

            return true;
        }

        /// <summary>
        /// Finds the first unbalanced subtree up to the node.
        /// </summary>
        /// <param name="node"> Node. </param>
        /// <returns> Returns the root of the first unbalanaced subtree up to the node. </returns>
        private static NodeAVL<TKey,TValue> FindUnBalanced(NodeAVL<TKey,TValue> node)
        {
            NodeAVL<TKey, TValue> temp = node;
            while(temp != null)
            {
                if (temp.Balance ==  2 || temp.Balance == -2)
                    return temp;
                temp = temp.Parent;
            }
            return null;
        } 

        /// <summary>
        /// Transplants the two subtrees.
        /// </summary>
        /// <param name="firstSubTree"> First subtree. </param>
        /// <param name="secondSubTree"> Second subtree. </param>
        private void Transplant(NodeAVL<TKey,TValue> firstSubTree,NodeAVL<TKey,TValue> secondSubTree)
        {
            //if first subtree has no parent,hence first subtree is the root of the tree
            if (firstSubTree.Parent == null)
            {
                this.root = secondSubTree;
            }

            //if first subtree is a left child
            else if (firstSubTree == firstSubTree.Parent.Left)
            {
                firstSubTree.Parent.Left = secondSubTree;
            }

            //if first tree is a right child
            else
            {
                firstSubTree.Parent.Right = secondSubTree;
            }

            //if second subtree is not null
            if(secondSubTree != null)
            {
                secondSubTree.Parent = firstSubTree.Parent;
            }
        }

        /// <summary>
        /// Gets the node with minimum key in the tree.
        /// </summary>
        /// <param name=""> Node. </param>
        /// <returns> Returns the node with minimum key. </returns>
        private static NodeAVL<TKey,TValue> MinimumKeyNode(NodeAVL<TKey,TValue> node) 
        {
            NodeAVL<TKey, TValue> temp = node;

            //go to the left most node
            while (temp.Left != null)
            {
                temp = temp.Left;
            }

            return temp;
        }

        /// <summary>
        /// Rebalances tree after insertion.
        /// </summary>
        /// <param name="node"> Node. </param>
        private void DeleteFixup(NodeAVL<TKey,TValue> node)
        {
            NodeAVL<TKey, TValue> counterParent = null, sibling;
            int balance;
            for(var counter = node.Parent;counter != null; counter = counterParent)
            {
                //store the parent of counter in counterParent
                counterParent = counter.Parent;

                //if node is the left child of counter
                if(node == counter.Left)
                {
                    //if counter is right heavy
                    if(counter.Balance > 0)
                    {
                        sibling = counter.Right;
                        balance = sibling.Balance;

                        //right left case
                        if (balance < 0)
                        {
                            //double rotation
                            node = RightLeftRotate(counter, sibling);
                        }

                        //right right case
                        else
                        {
                            //single rotation
                            node = LeftRotate(counter, sibling);
                        }
                    }

                    //adapting parent link after rotation
                    else
                    {
                        if(counter.Balance == 0)
                        {
                            counter.Balance = 1; break;
                        }
                        node = counter;
                        node.Balance = 0;
                        continue;
                    }
                }

                //if node is the right child of counter
                else
                {
                    //if counter is left heavy
                    if(counter.Balance < 0)
                    {
                        sibling = counter.Left;
                        balance = sibling.Balance;

                        //left right case
                        if (balance > 0)
                        {
                            node = LeftRightRotate(counter, sibling);
                        }

                        //left left case
                        else
                        {
                            node = RightRotate(counter, sibling);
                        }
                    }

                    //adapting parent link after rotation
                    else
                    {
                        if(counter.Balance == 0)
                        {
                            counter.Balance = -1;break;
                        }

                        node = counter;
                        node.Balance = 0;
                        continue;
                    }
                }

                //adapting parent link after rotation
                //node is the new root of the subtree
                node.Parent = counterParent;
                
                if(counterParent != null )
                {
                    //if counter is a left child
                    if(counter == counterParent.Left)
                    {
                        counterParent.Left = node;
                    }

                    //if counter is a right child
                    else
                    {
                        counterParent.Right = node;
                    }

                    if(balance == 0)
                    {
                        break;
                    }
                }

                //otherwise
                else
                {
                    //node becomes the root of the total tree
                    this.root = node;
                }
            }
        }
    }
}
