using Sheludko.Entities.Abstract;
using System;

namespace Sheludko.Objective.Entities
{
    /// <summary>
    /// Info about the appropriative exam
    /// </summary>
    public class Examination : IMarkName
    {
        private const int DefaultTerm = 1;
        private const string DefaultSubject = "Math";
        private const string DefaultExaminatorFIO = "Pavlik.V.O";
        private const int DefaultPoints = 100;
        private const bool DefaultIsExam = false;
        private static DateTime DefaultPassingDate = new DateTime(1, 2, 3, 4, 5, 6, 7);

        

        #region Properties

        public int Term { get; set; }

        public string Subject { get; set; }

        public string ExaminatorFIO { get; set; }

        public int Points { get; set; }

        public bool IsExam { get; set; }

        public DateTime PassingDate { get; set; }

        #endregion


        #region Constructors

        public Examination()
        {
            this.Term = DefaultTerm;
            this.Subject = DefaultSubject;
            this.ExaminatorFIO = DefaultExaminatorFIO;
            this.Points = DefaultPoints;
            this.IsExam = DefaultIsExam;
            this.PassingDate = DefaultPassingDate;
        }

        public Examination(int term, string subject, DateTime passingDate) : this()
        {
            this.Term = term;
            this.Subject = subject;
            this.PassingDate = passingDate;
        }

        #endregion


        #region Methods

        public override string ToString()
        {
            string format = "\n\tSubject : {0}\n\tExaminatorFIO : {1}\n\tPoints : {2}\n\tExaminator FIO : {3}\n\tTerm : {4}\n";

            string result = String.Format(format, Subject, ExaminatorFIO, Convert.ToString(Points), ExaminatorFIO, Term);

            return result;
        }

        #endregion


        #region IMarkName implementation

        string IMarkName.EctsScaleName()
        {
            if(Points >= 95)
            {
                return "A";
            }
            else if(Points >= 85)
            {
                return "B";
            }
            else if(Points >= 75)
            {
                return "C";
            }
            else if(Points >= 60)
            {
                return "D";
            }
            else
            {
                return "F";
            }
        }

        string IMarkName.NationalScaleName()
        {
            if (Points >= 95)
            {
                return "відмінно";
            }
            else if (Points >= 85)
            {
                return "добре";
            }
            else if (Points >= 60)
            {
                return "зараховано";
            }
            else
            {
                return "не заразовано";
            }
        }


        #endregion


    }
}
