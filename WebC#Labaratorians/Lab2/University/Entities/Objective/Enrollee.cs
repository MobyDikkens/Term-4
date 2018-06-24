using System;

namespace University.Entities.Objective
{
    public class Enrollee : Abstract.Person
    {

        private double _points;
        private double _additionals;


        #region Constructors

        public Enrollee(string name, string surname, DateTime birthday,
            double points, double additionals) : base(name, surname, birthday)
        {
            this._points = points;
            this._additionals = additionals;
        }

        #endregion

        #region Properties

        public double Points
        {
            get
            {
                return this._points;
            }
        }

        public double Additionals
        {
            get
            {
                return this._additionals;
            }
        }



        #endregion

        #region Methods

        public override void Show()
        {
            base.Show();

            System.Console.WriteLine("Type : {0}", this.GetType());
            System.Console.WriteLine("Points : {0}", _points);
            System.Console.WriteLine("Additional Points : {0}", _additionals);
        }

        public override object DeepCopy()
        {
            Enrollee enrollee = new Enrollee(Name, Surname, Birthday, _points, _additionals);


            return enrollee;
        }

        #endregion

    }
}
