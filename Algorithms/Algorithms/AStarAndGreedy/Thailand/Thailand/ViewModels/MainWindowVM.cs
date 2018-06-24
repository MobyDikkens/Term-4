using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Input;
using Thailand.Search;

namespace Thailand.ViewModels
{
    class MainWindowVM : INotifyPropertyChanged
    {
        //event fires if the property is changed
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow MainWindow { get; set; }


        private string _length = null;
        private string _from = null;
        private string _to = null;
        private bool __astartChecked;
        private bool _greedyChecked;
        private ICommand _search;
        private ICommand _setCommand;
        private string _state = "from";


        //the name of properties
        private const string FromProperty = "From";
        private const string ToProperty = "To";
        private const string AStartChecheckedPropery = "AStarChecked";
        private const string GreedyChecheckedPropery = "GreedyChecked";
        private const string SearchProperty = "Search";
        private const string SetCommandProperty = "SetCommand";
        private const string LengthProperty = "Length";

        #region Binding Properties

        //command to search
        public ICommand Search
        {
            get
            {
                return _search;
            }
            
            set
            {
                _search = value;
                PropertyChanged(this, new PropertyChangedEventArgs(SearchProperty));
            }
        }

        public string From
        {
            get
            {
                return _from;
            }

            set
            {
                _from = value;
                PropertyChanged(this, new PropertyChangedEventArgs(FromProperty));
            }
        }

        public string To
        {
            get
            {
                return _to;
            }

            set
            {
                _to = value;
                PropertyChanged(this, new PropertyChangedEventArgs(ToProperty));
            }
        }


        public bool AStarChecked
        {
            get
            {
                return __astartChecked;
            }

            set
            {
                __astartChecked = value;
                if (value)
                {
                    Search = new Commands.AStarSearchCommand(CanExecute, ShowRouteAstar);
                }
                PropertyChanged(this, new PropertyChangedEventArgs(AStartChecheckedPropery));
            }
        }

        public bool GreedyChecked
        {
            get
            {
                return _greedyChecked;
            }

            set
            {
                _greedyChecked = value;

                if (value)
                {
                    Search = new Commands.GreedySearchCommand(CanExecute, ShowRouteGreedy);
                }
                PropertyChanged(this, new PropertyChangedEventArgs(GreedyChecheckedPropery));
            }
        }

        public ICommand SetCommand
        {
            get
            {
                return _setCommand;
            }
            set
            {
                _setCommand = value;
                PropertyChanged(this, new PropertyChangedEventArgs(SetCommandProperty));
            }
        }

        public string Length
        {
            get
            {
                return _length;
            }

            set
            {
                _length = value;
                PropertyChanged(this, new PropertyChangedEventArgs(LengthProperty));
            }
        }

        #endregion

        public MainWindowVM()
        {
            //set first algo as greedy
            _search = new Commands.GreedySearchCommand(CanExecute, ShowRouteGreedy);
            _setCommand = new Commands.SetCommand(SetValue);
        }

        /// <summary>
        /// Validate current state
        /// if the command can execute
        /// </summary>
        /// <returns></returns>
        private bool CanExecute()
        {
            return To != null && From != null && (__astartChecked || _greedyChecked);
        }

        /// <summary>
        /// Set value on the from
        /// to labels
        /// </summary>
        /// <param name="value"></param>
        private void SetValue(string value)
        {
            if(_state == "from")
            {
                _state = "to";
                From = value;
            }
            else
            {
                To = value;
                _state = "from";
            }
        }

        private void ShowRouteAstar(int from, int to)
        {
            Thailand.Search.RouteInfo route = Thailand.Search.AStart.FindRoute(from, to);

            Length = Convert.ToString(route.Length);

            Thread thread = new Thread((start) => ShowRoute(route));

            thread.Start();
        }

        private void ShowRouteGreedy(int from, int to)
        {
            Thailand.Search.RouteInfo route = Thailand.Search.Greedy.FindRoute(from, to);

            Length = Convert.ToString(route.Length);

            Thread thread = new Thread((start) => ShowRoute(route));

            thread.Start();
        }

        private void ShowRoute(object obj)
        {
            RouteInfo route = (RouteInfo)obj;

            foreach(var town in route.Towns)
            {
                this.MainWindow.Dispatcher.InvokeAsync(() =>
                {
                    string name = Helpers.TownResolver.GetName(town);
                    RadioButton rb = (RadioButton)this.MainWindow.FindName(name);
                    rb.IsChecked = true;
                });
                Thread.Sleep(1000);
            }
        }


    }
}
