using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashSearch
{
    class BadHashTable<T>
    {
        private Lst<T> _nodes;

        private int _lenth;

        public BadHashTable()
        {
            this._lenth = 20000;
            _nodes = new Lst<T>();
            var tmp = _nodes;
            for(int i = 0; i < _lenth; i++)
            {
                tmp.Next = new Lst<T>();
                tmp = tmp.Next;
            }
        }

        public void Add(T value)
        {
            if (value as object != null)
            {
                int hash = GetHash(value);

                Lst<T> tmp = _nodes;

                for (int i = 0; i < hash; i++)
                {
                    tmp = tmp.Next;
                }

                Node<T> curr = tmp.Value;

                if(curr == null)
                {
                    curr = new Node<T>();
                    curr.Value = value;
                }
                else
                {
                    while(curr.Next != null)
                    {
                        curr = curr.Next;
                    }

                    curr.Next = new Node<T>();
                    curr = curr.Next;
                    curr.Value = value;
                }


            }
        }


        public bool Contains(T value)
        {
            if (value as object != null)
            {
                int hash = GetHash(value);

                Lst<T> tmp = _nodes;

                for (int i = 0; i < hash; i++)
                {
                    tmp = tmp.Next;
                }

                Node<T> curr = tmp.Value;

                while (curr != null)
                {
                    if (curr.Value.Equals(value))
                    {
                        return true;
                    }
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
        class Lst<T>
        {
            public Node<T> Value = null;
            public Lst<T> Next = null;
        }
    }
}
