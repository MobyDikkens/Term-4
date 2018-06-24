using System;

namespace HeapSort
{
    class Program
    {
        static void Main(string[] args)
        {
            /*int lenth = 1000;
            int[] array = new int[lenth];
            Random r = new Random();

 
            for(int i = 0; i < lenth; i++)
            {
                array[lenth - i - 1] = r.Next();
            }



            SortingCenter center = new SortingCenter(array);
            array = center.Sort;


            foreach(var tmp in array)
            {
                System.Console.WriteLine(tmp);
            }



            System.Console.WriteLine("Comparison:{0}",center.Comparison);
            System.Console.WriteLine("Execution time:{0}",center.Execution.Milliseconds);
            System.Console.WriteLine("Permutations:{0}",center.Permutations);
            */
            int lenth = 10;
            int[] array = {21,4,6,2,54,11,54,8,65,1};
            Random r = new Random();

            

 /* 
            for(int i = 0; i < lenth; i++)
            {
                array[lenth - 1 - i] = r.Next();
            }
*/

            SortingCenter center = new SortingCenter(array);
            array = center.HeapSort;

            foreach(var tmp in array)
            System.Console.WriteLine(tmp);

            //System.Console.WriteLine("Comparison:{0}",center.Comparison);
            //System.Console.WriteLine("Execution time:{0}",center.Execution.Milliseconds);
            //System.Console.WriteLine("Permutations:{0}",center.Permutations);
            //ystem.Console.WriteLine("Assimptotic:{0}",center.Assimptotic);

        }
    }
}
