using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Gomoku.ViewModels
{
    public class FieldInitializationVM : INotifyPropertyChanged
    {
        

        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        //Private objects to bind
        private EventHandler _initialization;


        //Properties name
        private const string InitializationProperty = "Initialization";

        //Properties to bind
        public EventHandler Initialization
        {
            get
            {
                return _initialization;
            }
        }



        //Constructor
        public FieldInitializationVM()
        {
            _initialization = Initialization_Handler;
        }

        #region Button Logic

        //tmp color
        private static byte _currentPlayer = Model.PlayersInfo.Player;

        //show if the cell is pushed or not
        private byte[,] _field = null;
        private Button[,] _buttons = null;


        //Initialization all grid cells
        private void Initialization_Handler(object sender, EventArgs e)
        {
            _field = new byte[19, 19];
            _buttons = new Button[19, 19];

            //set all nodes to Empty
            for(int i = 0; i < 19; i++)
            {
                for(int j = 0; j < 19; j++)
                {
                    _field[i, j] = Model.PlayersInfo.Empty;
                }
            }

            //Get window and field
            Window window = sender as Window;
            Grid field = (Grid)window.FindName("Field");

            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < 19; j++)
                {
                    Button b = new Button();
                    b.Click += B_Click;
                    b.Background = Brushes.White;
                    b.Tag = Convert.ToString(i) + "," + Convert.ToString(j);
                    Grid.SetRow(b, i);
                    Grid.SetColumn(b, j);

                    field.Children.Add(b);
                    _buttons[i, j] = b;
                }
            }
        }

        //Button click event
        private void B_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;

                string[] tags = Convert.ToString(button.Tag).Split(',');

                int i = Convert.ToInt32(tags[0]);
                int j = Convert.ToInt32(tags[1]);

                //check if the cell has not already pushed!
                if (_field[i, j]==Model.PlayersInfo.Empty)
                {
                    if(_currentPlayer == Model.PlayersInfo.Player)
                    {

                        button.Background = Model.PlayersInfo.GetColor(_currentPlayer);

                        _field[i, j] = _currentPlayer;
                        _currentPlayer = Model.PlayersInfo.AI;

                        if (Model.Hevristic.Calculate(_field) == Model.Hevristic.AIWin)
                        {
                            MessageBox.Show("AI has WON");
                        }
                        else if (Model.Hevristic.Calculate(_field) == Model.Hevristic.PlayerWin)
                        {
                            MessageBox.Show("Player has WON");
                        }

                        //ai
                        Model.Coordinates step = Model.MinMax.GetCoordinates(_field, _currentPlayer);


                        _buttons[step.X, step.Y].Background = Model.PlayersInfo.GetColor(_currentPlayer);

                        _field[step.X, step.Y] = _currentPlayer;
                        _currentPlayer = Model.PlayersInfo.Player;

                        if(Model.Hevristic.Calculate(_field) == Model.Hevristic.AIWin)
                        {
                            MessageBox.Show("AI has WON");
                        }
                        else if(Model.Hevristic.Calculate(_field) == Model.Hevristic.PlayerWin)
                        {
                            MessageBox.Show("Player has WON");
                        }

                    }
   
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        #endregion

    }
}
