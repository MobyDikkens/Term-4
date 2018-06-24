using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BTree
{
    [Serializable]
    class BTree<T> : ICollection<T> where T : IComparable<T>
    {
        private int _t;
        private Node<T> _root;

        public int SearchComparisons { get; set; }

        public int AddComparisons { get; set; }

        public Node<T> Root
        {
            get
            {
                return _root;
            }
        }

        public BTree(int t)
        {
            if (t <= 0)
            {
                throw new ArgumentException("t parameter must be above 0");
            }

            this._t = t;
            //null in Node creation because the node is root node
            this._root = new Node<T>(t, null);
        }

        #region ICollection Implementation

        public void Add(T value)
        {
            #region Create Element instanse

            Element<T> element = new Element<T>();
            element.Value = value;

            #endregion



            if (_root.Capacity == 0)
            {
                _root[0] = element;
            }
            else
            {

                InsertionInfo info = FindPlace(_root, value);

                //if the element exist
                //return
                if (info.Neighboor != null && info.Neighboor.Value.CompareTo(value) == 0)
                {
                    return;
                }

                if(info.Node.Capacity == 2*_t - 1)
                {
                    var node = info.Node;
                    Reallocate(ref node);
                    info = FindPlace(_root, value);
                }

                //we need to reallocate all elements after position
                if (info.Position < info.Node.Capacity)
                {
                    //insert after neighbour
                    //this situation could be just if 
                    //the info.Neighbour is alone
                    if(info.Neighboor.Value.CompareTo(value) < 0)
                    {
                        info.Node[info.Position + 1] = element;
                    }
                    //insert before 
                    else
                    {
                        for (int i = info.Node.Capacity - 1; i >= info.Position; i--)
                        {
                            Swap(ref info.Node, i, i + 1);
                        }
                        info.Node[info.Position] = element;
                    }

                    
                }
                //the info.Neighbour will always be null
                else
                {
                    info.Node[info.Position] = element;
                }

                //if the amount of value is max
                //need to reallocate the node
                if (info.Node.Capacity == 2 * _t - 1)
                {
                    var needRealocate = info.Node;
                    Reallocate(ref needRealocate);
                }



            }

        }

        public bool Contains(T value)
        {
            throw new NotImplementedException();
        }

        public void Delete(T value)
        {
            var node = _root;
            var res = FindNode(ref node, value);

            //if the value does not exist
            if(res == -1)
            {
                return;                                
            }

            //get the element
            var element = node[res];


            //1 scenario
            if(node.IsLeaf)
            {
                //delete element
                node.Delete(res);

                if(node.Capacity < _t - 1)
                {
                    if(element.LeftNode.Capacity >= _t)
                    {

                    }
                }
            }
            //2scenario
            else
            {

            }

            //must add all elements to the current node
            if(node[res].LeftNode != null)
            {

            }

            //must rebuild
            //there are 2 scenarious
            //1 the node has childs
            //2 the node no childs
            if(node.Capacity <= _t - 1)
            {

            }


        }

        private void DeleteFromNode(Node<T> node, int position)
        {
            //get the element to delete
            var element = node[position];

            //1 scenario
            if (node.IsLeaf)
            {

            }
            //2scenario
            else
            {
                //delete element from node
                node.Delete(position);

                Element<T> toInsert = null;

                //delete last element from the child node
                //left or right
                if(element.LeftNode != null)
                {
                    //get new element to insert
                    toInsert = element.LeftNode[element.LeftNode.Capacity - 1];
                    DeleteFromNode(element.LeftNode, element.LeftNode.Capacity - 1);
                }
                else if(element.RightNode != null)
                {
                    //get new element to insert
                    toInsert = element.RightNode[0];
                    DeleteFromNode(element.RightNode, 0);
                }


                
            }

        }

            private int FindNode(ref Node<T> node, T value)
        {
            int postion = 0;

            for(int i = 0; i < node.Capacity; i++)
            {
                if(node[i].Value.CompareTo(value) <= 0)
                {
                    postion = i;
                }
                else
                {
                    break;
                }
            }

            if(node[postion].Value.CompareTo(value) == 0)
            {
                return postion;
            }

            if(node[postion].Value.CompareTo(value) < 0 && node[postion].RightNode != null)
            {
                node = node[postion].RightNode;
                return FindNode(ref node, value);
            }

            if (node[postion].Value.CompareTo(value) > 0 && node[postion].LeftNode != null)
            {
                node = node[postion].LeftNode;
                return FindNode(ref node, value);
            }

            return -1;
        }

        public T Search(T value)
        {
            SearchComparisons = 0;

            T result = default(T);

            result = InterpoliationSearch(_root, value);

            return result;

        }

        private T InterpoliationSearch(Node<T> node, T value)
        {

            int left = 0;
            int right = node.Capacity - 1;

            int mid = 0;
            int state = -1;

            while(node[left].Value.CompareTo(value) < 0 && node[right].Value.CompareTo(value) > 0)
            {

                SearchComparisons += 2;
                mid = left + (right - left) * (value.GetHashCode() - node[left].Value.GetHashCode()) /
                    (node[right].Value.GetHashCode() - node[left].Value.GetHashCode());

                //loop
                if (mid == state)
                {
                    SearchComparisons += 2;

                    Node<T> deep = null;
                    //go deeper
                    if (node[mid].Value.CompareTo(value) > 0)
                    {
                        deep = node[mid].LeftNode;
                    }
                    else
                    {
                        deep = node[mid].RightNode;
                    }

                    if (deep != null)
                    {
                        return InterpoliationSearch(deep, value);
                    }
                    else
                    {
                        return default(T);
                    }

                }


                if (node[mid].Value.CompareTo(value) < 0)
                {
                    left = mid + 1;
                }
                else if (node[mid].Value.CompareTo(value) > 0)
                {
                    right = mid - 1;
                }
                else
                {
                    return node[mid].Value;
                }

                SearchComparisons += 2;

                state = mid;


            }

            if(node[left].Value.CompareTo(value) > 0)
            {
                SearchComparisons++;
                if (node[left].LeftNode != null)
                {
                    SearchComparisons++;
                    return InterpoliationSearch(node[left].LeftNode, value);
                }
            }

            if(node[right].Value.CompareTo(value) < 0)
            {
                SearchComparisons++;
                if (node[right].RightNode != null)
                {
                    SearchComparisons++;
                    return InterpoliationSearch(node[right].RightNode, value);
                }
            }

            if(node[left].Value.CompareTo(value) == 0)
            {
                SearchComparisons++;
                return node[left].Value;
            }
            else if(node[right].Value.CompareTo(value) == 0)
            {
                SearchComparisons++;
                return node[right].Value;
            }

            return default(T);


        }

        #endregion


        #region Helpers


        private InsertionInfo FindPlace(Node<T> node, T value)
        {
            InsertionInfo info = new InsertionInfo();

            int position = 0;

            for(int i = 0; i < node.Capacity; i++)
            {
                //find first element upper than value
                if(node[position].Value.CompareTo(value) > 0)
                {
                    position = i;
                    break;
                }
                position = i;
            }

            //fill the info about searching
            info.Neighboor = node[position];
            info.Node = node;
            info.Position = position;


            Node<T> toDiscover = null;

            //discover left node
            if (node[position].Value.CompareTo(value) > 0)
            {
                toDiscover = node[position].LeftNode;
            }
            //discover right node
            else
            {
                toDiscover = node[position].RightNode;
            }

            if (toDiscover != null)
            {
                info = FindPlace(toDiscover, value);
            }


            return info;
        }



        private void Swap(ref Node<T> node, int i, int j)
        {
            var tmp = node[i];
            node[i] = node[j];
            node[j] = tmp;
        }

        private void Reallocate(ref Node<T> node, Node<T> child = null)
        {
            int mediana = node.Capacity / 2;

            //node is root
            if(node.Father == null)
            {
                //create new root node
                Node<T> newRoot = new Node<T>(_t, null);
                //create left and right nodes
                Node<T> left = new Node<T>(_t, newRoot);
                Node<T> right = new Node<T>(_t, newRoot);

                //split elements between nodes
                for(int i = 0; i < mediana; i++)
                {
                    left[i] = node[i];
                    right[i] = node[mediana + 1 + i];
                }
                //create root element
                Element<T> element = new Element<T>();
                element.Value = node[mediana].Value;
                element.LeftNode = left;
                element.RightNode = right;

                //add element
                newRoot[0] = element;

                //initialize new root
                _root = newRoot;

                if(child != null)
                {
                    bool stateLeft = SearchChild(_root[0].LeftNode, child);
                    bool stateRight = SearchChild(_root[0].RightNode, child);
                    if(stateLeft)
                    {
                        node = _root[0].LeftNode;
                    }
                    if(stateRight)
                    {
                        node = _root[0].RightNode;
                    }

                }

            }
            else
            {
                //ok can raise the element
                if(node.Father.Capacity < 2*_t - 1)
                {
                    //drop node into 2 parts
                    Node<T> left = new Node<T>(_t, node.Father);
                    Node<T> right = new Node<T>(_t, node.Father);

                    //fill nodes
                    for(int i = 0; i < mediana; i++)
                    {
                        left[i] = node[i];
                        right[i] = node[mediana + 1 + i];
                    }

                    //create element to raise
                    Element<T> mediumElement = new Element<T>();
                    mediumElement.Value = node[mediana].Value;
                    mediumElement.LeftNode = left;
                    mediumElement.RightNode = right;

                    if (child != null)
                    {
                        bool stateLeft = SearchChild(mediumElement.LeftNode, child);
                        bool stateRight = SearchChild(mediumElement.RightNode, child);
                        if (stateLeft)
                        {
                            node = mediumElement.LeftNode;
                        }
                        if (stateRight)
                        {
                            node = mediumElement.RightNode;
                        }

                    }

                    //insert(raise)
                    var fatherNode = node.Father;
                    InsertToTheNode(ref fatherNode, mediumElement);

  
                }
                //not ok should drop father node and then drop desire node
                else
                {
                    var fatherNode = node.Father;
                    //reallocate father node
                    Reallocate(ref fatherNode, node);
                    node.Father = fatherNode;
                    Reallocate(ref node);
                }
            }

        }

        private bool SearchChild(Node<T> father, Node<T> child)
        {

            for(int i = 0; i < father.Capacity; i++)
            {
                if(father[i].LeftNode != null)
                {
                    if (father[i].LeftNode.Equals(child) || father[i].RightNode.Equals(child))
                        return true;
                }

            }

            return false;
        }


        /// <summary>
        /// Returns the position in the node
        /// where element is located
        /// </summary>
        /// <param name="node"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        private void InsertToTheNode(ref Node<T> node, Element<T> element)
        {
            //position now equals next element after the last in the array
            int position = node.Capacity;

            for(int i = 0; i < node.Capacity; i++)
            {
                if(node[i].Value.CompareTo(element.Value) > 0)
                {
                    position = i;
                    break;
                }
            }
            
            //need to swap elements after position
            if(node.Capacity - 1 > position)
            {
                for (int i = node.Capacity - 1; i >= position; i--)
                {
                    Swap(ref node, i, i + 1);
                }
                node[position] = element;

            }
            else
            {
                node[position] = element;
            }

            if (position - 1 >= 0)
            {
                node[position - 1].RightNode = element.LeftNode;
            }

            if (position + 1 < node.Capacity)
            {
                node[position + 1].LeftNode = element.RightNode;
            }

        }

        #endregion


        #region InsertingInfo Class

        private struct InsertionInfo
        {
            public Element<T> Neighboor;
            public Node<T> Node;
            public int Position;
        }

        #endregion
    }
}
