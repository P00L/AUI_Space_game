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
using AuiSpaceGame.Model;
using AuiSpaceGame.Model.Parser;

namespace AuiSpaceGame.View
{
    /// <summary>
    /// Interaction logic for StartingPage.xaml
    /// </summary>
    public partial class StartingPage : Page
    {
        public StartingPage()
        {
            InitializeComponent();
        }

        private void createGameButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SetupGamePage());
        }

        private void loadGameButton_Click(object sender, RoutedEventArgs e)
        {
            Game Game = Parser.loadGame();
            if(Game != null)
                this.NavigationService.Navigate(new SetupGamePage(Game));
        }
    }
}
