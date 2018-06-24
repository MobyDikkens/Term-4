using Sheludko.Objective.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sheludko
{
    class Program
    {

        private const string _emptyExam = "0";
        private const string _exam = "1";
        private const string _show = "2";
        private const string _sort = "3";
        private const string _showSort = "4";

        static void Main(string[] args)
        {
            Person p1 = new Person();
            Person p2 = new Person("Name1", "Surname1", new DateTime(1, 2, 3, 4, 5, 6, 7));


            Student s1 = new Student();
            Student s2 = new Student(p2, Education.Master, 120021);

            Student test = new Student(new Person { Name = "Student", Surname = "Of KPI" }, Education.Master, 12312);


            p1.PrintFullInfo();
            p2.PrintFullInfo();

            s1.PrintFullInfo();
            s2.PrintFullInfo();

            Examination tmp = null;

            string command = "";

            do
            {
                try
                {
                    Console.WriteLine("Commands : \n\tAdd Default Exam : 0\n\tAdd Parametrized Exam : 1\n\tShow Info : 2\n\tGet all above : 3\n\tShow sorted : 4\n");

                    command = Console.ReadLine();

                    switch (command)
                    {
                        case _emptyExam:
                            {
                                tmp = new Examination();
                                break;
                            }
                        case _exam:
                            {


                                Console.WriteLine("IsExam : ");
                                bool isExam = Convert.ToBoolean(Console.ReadLine());

                                Console.WriteLine("Examinator FIO:");
                                string examinator = Console.ReadLine();

                                Console.WriteLine("Term : ");
                                int term = Convert.ToInt32(Console.ReadLine());

                                Console.WriteLine("Subject : ");
                                string subject = Console.ReadLine();

                                Console.WriteLine("Date : ");
                                DateTime date = Convert.ToDateTime(Console.ReadLine());

                                Console.WriteLine("Enter Points :");
                                int points = Convert.ToInt32(Console.ReadLine());

                                tmp = new Examination(term, subject, date);
                                tmp.ExaminatorFIO = examinator;
                                tmp.Points = points;

                                break;
                            }
                        case _show:
                            {
                                test.PrintFullInfo();
                                break;
                            }
                        case _sort:
                            {
                                Console.WriteLine("Enter Points : ");
                                int points = Convert.ToInt32(Console.ReadLine());

                                var res = test.GetExamsAbove(points);
                                

                                while(res.MoveNext())
                                {
                                    //var exam = res.Current;

                                    //Console.WriteLine("\n\tExaminationFIO : {0}\n\tIsExam : {1}\n\tPassingDate : {2}\n\tPoints : {3}\n\tSubject : {4}\t\n",
                                    //exam.ExaminatorFIO, exam.IsExam, exam.PassingDate, exam.Points, exam.Subject);

                                    Console.WriteLine(res.Current.ToString());
                                }

                                break;
                            }
                        case _showSort:
                            {
                                foreach(var exam in test.Sort())
                                {
                                    // Console.WriteLine("\n\tExaminationFIO : {0}\n\tIsExam : {1}\n\tPassingDate : {2}\n\tPoints : {3}\n\tSubject : {4}\t\nTerm : {5}",
                                    //exam.ExaminatorFIO, exam.IsExam, exam.PassingDate, exam.Points, exam.Subject, exam.Term);

                                    Console.WriteLine(exam.ToString());
                                }
                                break;
                            }
                        default:
                            {
                                continue;
                            }
                    }


                }
                catch
                {
                    continue;
                }



                Examination[] curr = new Examination[1];
                curr[0] = tmp;

                test.AddExams(curr);


            }
            while (true);


            Console.ReadKey();

        }
    }
}
