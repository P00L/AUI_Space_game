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
        bool AmbientAnimationOn;
        public Window1()
        {
            Console.WriteLine("PROAVAAA");
            AmbientAnimationOn = false;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        public Window1(bool ambientAnimationOn)
        {
            Console.WriteLine("PROAVAAA22222222222");
            AmbientAnimationOn = ambientAnimationOn;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            if (AmbientAnimationOn)
                ambientToggleButton.IsChecked = true;
            else
                ambientToggleButton.IsChecked = false;
        }

        private void createGameButton_Click(object sender, RoutedEventArgs e)
        {
            SetupGameWindow SetupGameWindow = new SetupGameWindow(AmbientAnimationOn);
            SetupGameWindow.Show();
            this.Close();
        }

        private void loadGameButton_Click(object sender, RoutedEventArgs e)
        {
            Game Game = Parser.loadGame();
            if (Game != null)
            {       
                SetupGameWindow SetupGameWindow = new SetupGameWindow(Game, AmbientAnimationOn);
                SetupGameWindow.Show();
                this.Close();
            }
        }

        private void ambientToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            AmbientAnimationOn = true;
            // CHIAMATA PHAROS AMBIENTAZIONE ON
        }

        private void ambientToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            AmbientAnimationOn = false;
            // CHIAMATA PHAROS AMBIENTAZIONE OFF
        }
    }
}
