using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTree
{
    class BTree<TKey, TValue> where TKey : IComparable<TKey>
    {
        private Node<TKey, TValue> _root = null;

        private int _degree;
        public BTree(int degree)
        {
            this._degree = degree;
            _root = new Node<TKey, TValue>(degree, null);
        }

        //public TValue Search(TKey key)
        //{

        //}

        public void Add(TKey key, TValue value)
        {
            if(_root == null)
            {
                
            }
        }

    }
}
