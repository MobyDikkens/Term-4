using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashSearch
{
    class HashTable<T>
    {
        private Node<T>[] _nodes;

        private int _lenth;

        public int Comparisons { get; private set; }
        
        public HashTable()
        {
            this._lenth = 20000;
            _nodes = new Node<T>[this._lenth];
        }

        public void Add(T value)
        {
            if(value as object != null)
            {
                int hash = GetHash(value);

                if (_nodes[hash] == null)
                {
                    _nodes[hash] = new Node<T>();
                    _nodes[hash].Value = value;

                }
                else
                {
                    Node<T> toInsert = _nodes[hash];

                    while (toInsert.Next != null)
                    {
                        toInsert = toInsert.Next;
                    }

                    toInsert.Next = new Node<T>();
                    toInsert = toInsert.Next;
                    toInsert.Value = value;
                }

            }
        }

        public bool Contains(T value)
        {
            Comparisons = 0;
            if(value as object != null)
            {
                int hash = GetHash(value);

                Node<T> node = _nodes[hash];


                if (node != null)
                {
                    do
                    {
                        if (node.Value.Equals(value))
                        {
                            Comparisons++;
                            return true;
                        }
                        Comparisons ++;
                        node = node.Next;
                    }
                    while (node != null);
                }

            }
            return false;
        }

        private int GetHash(T value)
        {
            return Math.Abs(value.GetHashCode() % this._lenth);
        }


        //  Typical list node
        class Node<T>
        {
            public T Value;
            public Node<T> Next = null;
        }
    }
}
