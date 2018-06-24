using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Thailand
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int previous = -1;

        public MainWindow()
        {
            InitializeComponent();
        }



        private void CalculateWay()
        {

        }


        private void Window_Initialized(object sender, EventArgs e)
        {
            var vm = (ViewModels.MainWindowVM)this.FindResource("MWVM");

            vm.MainWindow = this;

        }
    }
}
