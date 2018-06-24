using System;

namespace BTree
{
    [Serializable]
    class Node<T> where T : IComparable<T>
    {
        public Element<T>[] Elements { get; set; }

        public Node<T> Father { get; set; }

        public bool IsRoot
        {
            get
            {
                return Father == null;
            }
        }

        public Node(int t, Node<T> father)
        {
            if(t <= 0)
            {
                throw new ArgumentException("t parameter must be above 0");
            }

            //after creating all elements will be null
            Elements = new Element<T>[2 * t - 1];
            //if father == null - it is the root node
            Father = father;
        }

        public Element<T> this[int index]
        {
            get
            {
                if(index < 0 || index >= Elements.Length)
                {
                    throw new IndexOutOfRangeException();
                }

                return Elements[index];
            }

            set
            {
                if (index < 0 || index >= Elements.Length)
                {
                    throw new IndexOutOfRangeException();
                }

                Elements[index] = value;
            }
        }

        /// <summary>
        /// Returns the capacity of not null elements
        /// </summary>
        public int Capacity
        {
            get
            {
                int capacity = 0;
                for(int i = 0; i < Elements.Length; i++)
                {
                    if(Elements[i] != null)
                    {
                        capacity++;
                    }
                }

                return capacity;
            }
        }

        public override bool Equals(object obj)
        {
            Node<T> other = obj as Node<T>;

            if (other == null || other.Capacity != Capacity)
                return false;

            for(int i = 0; i < Capacity; i++)
            {
                if (Elements[i].Value.CompareTo(other.Elements[i].Value) != 0)
                    return false;
            }
            return true;
        }

        public void Delete(int position)
        {
            if(position < Capacity)
            {
                if(position == Capacity - 1)
                {
                    Elements[position] = null;
                }
                else
                {
                    for(int i = position; i < Capacity - 1; i++)
                    {
                        Elements[i] = Elements[i + 1];
                    }
                }
            }
        }

        public bool IsLeaf
        {
            get
            {
                for(int i = 0; i < Capacity; i++)
                {
                    if(this[i].LeftNode != null || this[i].RightNode != null)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

    }
}
