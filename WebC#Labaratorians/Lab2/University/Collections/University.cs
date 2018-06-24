using System;
using University.Entities.Abstract;
using University.Entities.Objective;
using System.Collections.Generic;
using System.Collections;

namespace University.Collections
{
    public class University : IEnumerable<Person>
    {
        private List<Person> _persons;

        private EventHandler _add = null;
        private EventHandler _remove = null;

        public event EventHandler OnAdd
        {
            add
            {
                if(_add == null)
                {
                    _add = value;
                }
                else
                {
                    _add += value;
                }
            }
            remove
            {
                if(_add != null)
                {
                    _add -= value;
                }
            }
        }
        public event EventHandler OnRemove
        {
            add
            {
                if (_remove == null)
                {
                    _remove = value;
                }
                else
                {
                    _remove += value;
                }
            }
            remove
            {
                if (_remove != null)
                {
                    _remove -= value;
                }
            }
        }



        #region Constructors

        public University()
        {
            _persons = new List<Person>();
        }

        #endregion

        #region Methods

        public void Add(Person person)
        {
            if(person != null)
                _persons.Add(person);

            if(_add != null)
            {
                _add(this, new EventArgs());
            }
        }

        public void Remove(Person person)
        {
            if (person != null)
            {
                if(_persons.Remove(person))
                {
                    if (_add != null)
                    {
                        _remove(this, new EventArgs());
                    }
                }
            }

        }

        public void Show()
        {
            foreach(var person in _persons)
            {
                person.Show();
            }
        }

        public object DeepCopy()
        {
            University university = new University();
            
            foreach(var person in _persons)
            {
                Person copy = person.DeepCopy() as Person;

                university.Add(copy);
            }


            return university;
        }



        #endregion

        #region IEnumarable Implementation

        public IEnumerator<Person> GetEnumerator()
        {
            return _persons.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _persons.GetEnumerator();
        }

        #endregion

    }
}
