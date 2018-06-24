using System;

namespace BTree
{
    [Serializable]
    class Element<T> : IComparable where T : IComparable<T>
    {
        public T Value { get; set; }

        public Node<T> LeftNode { get; set; }

        public Node<T> RightNode { get; set; }


        #region IComparable implementations

        public int CompareTo(object other)
        {
            Element<T> toCompare = other as Element<T>;

            if(toCompare == null)
            {
                return -1;
            }


            return this.Value.CompareTo(toCompare.Value);
        }

        #endregion
    }
}
