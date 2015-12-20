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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SetupGamePage());
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Game Game = new Game();
            Game.AnimationsSequence.Add(new Asteroid(Lane.Right, Speed.High));
            Game.AnimationsSequence.Add(new Asteroid(Lane.Middle, Speed.High));
            Game.AnimationsSequence.Add(new Asteroid(Lane.Left, Speed.High));
            
            this.NavigationService.Navigate(new SetupGamePage(Game));
        }
    }
}
