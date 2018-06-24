using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BTree
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime past = DateTime.Now;
            BTree<int> tree = new BTree<int>(50);
            for(int i = 0; i < 10000; i++)
            {
                tree.Add(i);
            }
            TimeSpan delta = DateTime.Now - past;
            Console.WriteLine(delta.Seconds);


            Console.WriteLine("Searching");

            int v = -1;

            do
            {
                Console.WriteLine("Key:\n");
                var b = int.TryParse(Console.ReadLine(), out v);
                if (b)
                {
                    Console.WriteLine(tree.Search(v));
                    Console.WriteLine("Comparisons : {0}", tree.SearchComparisons);
                }
            }
            while (v != -1);

            tree.Delete(100);
            Console.WriteLine(tree.SearchComparisons);

            Console.ReadKey();
        }
    }
}
