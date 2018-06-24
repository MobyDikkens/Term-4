using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Gomoku
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

           
        }

        //Initialize
        private void Window_Initialized(object sender, EventArgs e)
        {
            ViewModels.FieldInitializationVM initializationVM = new ViewModels.FieldInitializationVM();
            initializationVM.Initialization(sender, e);
        }
    }
}
