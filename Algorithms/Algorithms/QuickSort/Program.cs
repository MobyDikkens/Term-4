using System;

namespace QuickSort
{
    class Program
    {

         class Params
        {
            public IComparable[] Array {get;set;}
            public int Left {get;set;}

            public int Right {get;set;}
        }

        static void Main(string[] args)
        {
            IComparable[] array = new IComparable[100000];
            Random r = new Random();
            for(int i = 0; i < array.Length; i++)
                array[i] = r.Next();

            DateTime past = DateTime.Now;
            Quicksort(array, 0, array.Length - 1);
            TimeSpan time = DateTime.Now - past;
            
            System.Console.WriteLine(time.Milliseconds);

        }

         public static void Quicksort(IComparable[] elements, int left, int right)
        {
            int i = left, j = right;
            IComparable pivot = elements[(left + right) / 2];
 
            while (i <= j)
            {
                while (elements[i].CompareTo(pivot) < 0)
                {
                    i++;
                }
 
                while (elements[j].CompareTo(pivot) > 0)
                {
                    j--;
                }
 
                if (i <= j)
                {
                    // Swap
                    IComparable tmp = elements[i];
                    elements[i] = elements[j];
                    elements[j] = tmp;
 
                    i++;
                    j--;
                }
            }
 
            // Recursive calls
            if (left < j)
            {

                Quicksort(elements, left, j);
            }
 
            if (i < right)
            {
                Quicksort(elements, i, right);
            }
        }
 
 
    }
}
