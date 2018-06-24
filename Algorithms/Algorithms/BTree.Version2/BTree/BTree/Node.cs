using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTree
{
    class Node<TKey, TValue> where TKey : IComparable<TKey>
    {
        private Element<TKey, TValue>[] _elements;
        private Node<TKey, TValue> _father;

        public Node(int degree, Node<TKey, TValue> father)
        {
            _elements = new Element<TKey, TValue>[degree];
            _father = father;
        }

        public bool IsRoot
        {
            get
            {
                return _father == null;
            }
        }

        public bool IsLeaf
        {
            get
            {
                foreach(var element in _elements)
                {
                    if (!element.IsLeaf)
                        return false;
                }
                return true;
            }
        }

        public int Capacity
        {
            get
            {
                int capacity = 0;
                foreach(var tmp in _elements)
                {
                    capacity++;
                }
                return capacity;
            }
        }
    }
}
