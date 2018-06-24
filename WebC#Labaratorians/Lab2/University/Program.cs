using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Entities.Objective;

namespace University
{
    class Program
    {
        static void Main(string[] args)
        {
            University.Collections.University university = new Collections.University();

            Student student = new Student("Test", "1", new DateTime(1, 2, 3, 4, 5, 6), 1, "KPI", 8);
            Enrollee enrollee = new Enrollee("Test", "1", new DateTime(1, 2, 3, 4, 5, 6), 200, 100);
            Lecturer lecturer = new Lecturer("Test", "1", new DateTime(1, 2, 3, 4, 5, 6), "Subject", "KPI");


            university.Add(student);
            university.Add(enrollee);
            university.Add(lecturer);

            university.Show();

            Console.ReadKey();

        }
    }
}
