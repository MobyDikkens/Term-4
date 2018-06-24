namespace University.Entities.Abstract
{
    public abstract class Person
    {
        private string _name;
        private string _surname;
        private System.DateTime _birthday;

        public abstract object DeepCopy();

        #region Constructors

        public Person(string name, string surname, System.DateTime birthday)
        {
            //  check for correct parametrs
            if ((name == null) || (surname == null) ||
                (birthday == null))
                throw new System.ArgumentException("Cannot initialize person via parametrs");


            this._name = name;
            this._surname = surname;
            this._birthday = birthday;

        }

        #endregion

        #region Properties

        public string Name
        {
            get
            {
                return this._name;
            }
        }

        public string Surname
        {
            get
            {
                return this._surname;
            }
        }

        public System.DateTime Birthday
        {
            get
            {
                return this._birthday;
            }
        }

        public int Age
        {
            get
            {
                System.TimeSpan delta = System.DateTime.Now - _birthday;

                return delta.Days / 365;
            }
        }

        #endregion

        #region Methods

        public virtual void Show()
        {
            System.Console.WriteLine("Name : {0}", _name);
            System.Console.WriteLine("Surname : {0}", _surname);
            System.Console.WriteLine("Birthday : {0}", _birthday);
            System.Console.WriteLine("Age : {0}", Age);
        }

        #endregion

        #region Operators

        public static bool operator ==(Person obj1, object obj2) => obj1.Equals(obj2);

        public static bool operator !=(Person obj1, object obj2) => !obj1.Equals(obj2);



        #endregion

        #region Overriden

        public override bool Equals(object obj)
        {
            if(obj is Person)
            {
                Person person = obj as Person;

                return person._name == this._name &&
                    person._surname == this._surname &&
                    person._birthday == this._birthday;

            }

            return false;
        }

        public override int GetHashCode()
        {
            //string general = "";

            //general += _name;
            //general += _surname;
            //general += System.Convert.ToString(_birthday);


            int hash = 0;
            hash += _name.GetHashCode();
            hash += _surname.GetHashCode();
            hash += _birthday.GetHashCode();

            //hash = general.GetHashCode();


            return hash;
        }

        public override string ToString()
        {
            string general = "Name : {0}\nSurname : {1}\nBirthday : {2}";

            var res = System.String.Format(general, _name, _surname, System.Convert.ToString(_birthday));

            return res;
        }

        #endregion

    }
}
