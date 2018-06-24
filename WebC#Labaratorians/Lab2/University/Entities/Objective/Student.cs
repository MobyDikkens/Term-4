using System;

namespace University.Entities.Objective
{
    public class Student : Abstract.Person
    {
        private int _id;
        private string _university;
        private int _term;

        #region Constructors

        public Student(string name, string surname, DateTime birthday,
            int id, string university, int term) : base(name, surname, birthday)
        {
            if (university == null)
                throw new ArgumentException("Cannot create Student instance via parametrs");

            this._id = id;
            this._university = university;
            this._term = term;
        }

        #endregion

        #region Properties

        public int Id
        {
            get
            {
                return this._id;
            }
        }

        public string University
        {
            get
            {
                return this._university;
            }
        }

        public int Term
        {
            get
            {
                return this._term;
            }
        }

        #endregion

        #region Methods

        public override void Show()
        {
            base.Show();

            System.Console.WriteLine("Type : {0}" + this.GetType());
            System.Console.WriteLine("Id : {0}", _id);
            System.Console.WriteLine("University : {0}", _university);
            System.Console.WriteLine("Term : {0}", _term);
        }

        public override object DeepCopy()
        {
            Student student = new Student(Name, Surname, Birthday, _id, _university, _term);


            return student;
        }

        #endregion

    }
}
