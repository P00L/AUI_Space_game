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
using System.Windows.Shapes;
using AuiSpaceGame.Model;
using AuiSpaceGame.Model.Parser;

namespace AuiSpaceGame
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void createGameButton_Click(object sender, RoutedEventArgs e)
        {
            SetupGameWindow SetupGameWindow = new SetupGameWindow();
            SetupGameWindow.Show();
            this.Close();
        }

        private void loadGameButton_Click(object sender, RoutedEventArgs e)
        {
            Game Game = Parser.loadGame();
            if (Game != null)
            {       
                SetupGameWindow SetupGameWindow = new SetupGameWindow(Game);
                SetupGameWindow.Show();
                this.Close();
            }
        }
    }
}
