using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();
            BadHashTable<int> table = new BadHashTable<int>();
            DateTime past = DateTime.Now;
            for(int i = 0; i < 50000; i++)
            {
                var tmp = r.Next();
                table.Add(tmp);
            }
            TimeSpan now = DateTime.Now - past;
            Console.WriteLine("{0}:{1}",now.Minutes,now.Milliseconds);
            int value = 0;

            DateTime time1;
            TimeSpan delta;

            while(value != -1)
            {
                
                value = Convert.ToInt32(Console.ReadLine());
                time1 = DateTime.Now;
                Console.WriteLine(table.Contains(value));
                delta = DateTime.Now - time1;
                Console.WriteLine("Time:{0}",delta.Milliseconds);
                //Console.WriteLine("Comparison:{0}",table.Comparisons);
            }

        }
    }
}
