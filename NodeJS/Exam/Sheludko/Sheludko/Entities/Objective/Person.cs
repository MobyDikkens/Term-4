using System;

namespace Sheludko.Objective.Entities
{
    /// <summary>
    /// Incapsulate logic about
    /// the appropriative person
    /// </summary>
    public class Person
    {
        private const string DefaultName = "Victor";
        private const string DefalutSurname = "Pavlik";
        private static DateTime DefaultBirthday = new DateTime(1, 2, 3, 4, 5, 6, 7);


        #region Properties

        /// <summary>
        /// The Person`s name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Person`s surname
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// The Person`s birthday
        /// </summary>
        public DateTime Birthday { get; private set; }

        /// <summary>
        /// Gets or sets Birthday year
        /// </summary>
        public int Year
        {
            get
            {
                return Birthday.Year;
            }
            set
            {
                //get the current birthday
                var tmp = Birthday;

                //change to the appropriative year
                Birthday = new DateTime(value, tmp.Month, tmp.Day, tmp.Hour, tmp.Minute, tmp.Second);

            }
        }

        #endregion


        #region Constructors

        public Person()
        {
            Name = DefaultName;
            Surname = DefalutSurname;
            Birthday = DefaultBirthday;
        }

        public Person(string name, string surname, DateTime birthday)
        {
            this.Name = name;
            this.Surname = surname;
            this.Birthday = birthday;
        }

        #endregion

        #region Methods

        public virtual void PrintFullInfo()
        {
            Console.WriteLine("******************************************************************************************");

            string format = "Name : {0} \nSurname : {1} \nBirthday : {2}\n";

            string info = String.Format(format, Name, Surname, Convert.ToString(Birthday));

            Console.WriteLine(info);

            //Console.WriteLine("******************************************************************************************");
        }


        public override bool Equals(object obj)
        {
            if(obj is Person)
            {
                Person other = obj as Person;


                return other.Birthday == this.Birthday &&
                    other.Name == this.Name &&
                    other.Surname == this.Surname;
            }

            return false;
        }


        #endregion

        #region Operators

        public static bool operator ==(Person person, object other) => person.Equals(other);
        public static bool operator !=(Person person, object other) => !person.Equals(other);


        #endregion


    }
}
