using System;
using System.Collections.Generic;

namespace University.Entities.Objective
{
    public class Lecturer : Abstract.Person
    {
        private List<string> _subjects = null;
        private string _university;


        #region Constructors

        public Lecturer(string name, string surname, DateTime birthday,
            string subject, string university) : base(name, surname, birthday)
        {
            if ((subject == null) || (university == null))
                throw new ArgumentException("Cannot create Student instance via parametrs");

            this._subjects = new List<string>();

            this._subjects.Add(subject);
            this._university = university;
        }

        #endregion

        #region Properties

        public string University
        {
            get
            {
                return this._university;
            }
        }

        public List<string> Subjects
        {
            get
            {
                return this._subjects;
            }
        }

        public string AddSubject
        {
            set
            {
                if(value != null)
                    this._subjects.Add(value);
            }
        }

        #endregion

        #region Methods

        public override void Show()
        {
            base.Show();

            System.Console.WriteLine("Type : {0}", this.GetType());
            System.Console.WriteLine("University : {0}", _university);
            foreach(var subject in _subjects)
            {
                System.Console.WriteLine("Subject : {0}", subject);
            }
        }

        public override object DeepCopy()
        {
            Lecturer lecturer = new Lecturer(Name, Surname, Birthday, _subjects[0], _university);
            
            foreach(var subject in _subjects)
            {
                lecturer.AddSubject = subject;
            }


            return lecturer;
        }

        #endregion

    }
}
