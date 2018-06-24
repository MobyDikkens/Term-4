using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTree
{
    class Element<TKey, TValue> : IComparable<Element<TKey, TValue>> where TKey : IComparable<TKey>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public Node<TKey, TValue> LeftNode { get; set; }

        public Node<TKey, TValue> RightNode { get; set; }

        public int CompareTo(Element<TKey, TValue> other)
        {
            return Key.CompareTo(other.Key);
        }

        public bool IsLeaf
        {
            get
            {
                return LeftNode == null && RightNode == null;
            }
        }
    }
}
