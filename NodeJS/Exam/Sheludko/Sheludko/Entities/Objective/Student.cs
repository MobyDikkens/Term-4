using System;
using System.Collections.Generic;

namespace Sheludko.Objective.Entities
{
    /// <summary>
    /// Descrybes the appropriative student
    /// </summary>
    public class Student : Person
    {
        private const Education DefaultLevel = Education.Bachelor;
        private const string DefaultGroup = "DefaultGroup";
        private const int DefaultId = 1;


        #region Properties

        public Education Level { get; set; }
        public string Group { get; set; }
        public int Id { get; set; }
        public List<Examination> PassedSubjects { get; private set; }
        public double Avarage
        {
            get
            {
                int summ = 0;

                foreach(var subject in PassedSubjects)
                {
                    summ += subject.Points;
                }


                return PassedSubjects.Count > 0 ? summ / PassedSubjects.Count : 0;

            }
        }

        #endregion


        #region Constructors

        public Student() : base()
        {
            this.Level = DefaultLevel;
            this.Group = DefaultGroup;
            this.Id = DefaultId;

            this.PassedSubjects = new List<Examination>();
        }

        public Student(Person person, Education level, int id) : base(person.Name, person.Surname, person.Birthday)
        {
            this.Level = level;
            this.Group = DefaultGroup;
            this.Id = id;

            this.PassedSubjects = new List<Examination>();
        }


        #endregion


        #region Methods

        public void AddExams(Examination[] examList)
        {
            foreach(var exam in examList)
            {
                PassedSubjects.Add(exam);
            }
        }

        public override string ToString()
        {
            string format = "Name : {0}\nSurname : {1}\nGroup : {2}\n";

            string result = String.Format(format, Name, Surname, Group);


            return result;
        }

        public override void PrintFullInfo()
        {
            base.PrintFullInfo();

            string format1 = "Group : {0}\n Id : {1}\nAvarage : {2}\n";
            Console.WriteLine(String.Format(format1, Group, Id, Avarage));

            Console.WriteLine("Passed Subjects:");

            string format2 = "\t{0} : {1}\n";


            foreach (var subject in PassedSubjects)
            {
                Console.WriteLine(String.Format(format2, subject.Subject, subject.Points));
            }

            Console.WriteLine("******************************************************************************************");
        }

        /// <summary>
        /// Returns enumerator
        /// for each exam that is above points
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public IEnumerator<Examination> GetExamsAbove(int points)
        {
            return PassedSubjects.FindAll((p)=>p.Points >= points).GetEnumerator();
        }

        /// <summary>
        /// Retrns sorted array of the exams
        /// </summary>
        /// <returns></returns>
        public Examination[] Sort()
        {
            List<Examination> sorted = new List<Examination>();

            foreach(var exam in PassedSubjects)
            {
                sorted.Add(exam);
            }

            sorted.Sort((e1, e2) =>
            {
                return e1.Term - e2.Term;
            });

            return sorted.ToArray();
        }


        #endregion

    }
}
