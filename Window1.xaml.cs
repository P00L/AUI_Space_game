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
using AuiSpaceGame.Utilities;
using System.ComponentModel;

namespace AuiSpaceGame
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        bool AmbientAnimationOn;
        private APIServer APIServer;
        public Window1()
        {
            AmbientAnimationOn = false;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        public Window1(bool ambientAnimationOn)
        {
            AmbientAnimationOn = ambientAnimationOn;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
            if (AmbientAnimationOn)
            {
                ambientToggleButton.IsChecked = true;
            }
            else
            {
                ambientToggleButton.IsChecked = false;
            }
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
            if (AmbientAnimationOn != true)
            {
                AmbientAnimationOn = true;
                if (APIServer == null)
                {
                    APIServer = new APIServer();
                }

                APIServer.LuminousCarpetRequest("Sparkle");
                APIServer.ShowVideoOnScreenRequest("FirstScreen", "Space.mp4");
                APIServer.HueRequest("#2E09C1", "front", "100");
                APIServer.HueRequest("#2E09C1", "middle", "100");
                APIServer.HueRequest("#2E09C1", "rear", "100");
            }
        }

        private void ambientToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            if (AmbientAnimationOn != false)
            {
                AmbientAnimationOn = false;
                if (APIServer == null)
                {
                    APIServer = new APIServer();
                }

                APIServer.LuminousCarpetRequest("Release");
                //TODO spegnere il video sullo schermo
                APIServer.HueRequest("#FFFFFF", "front", "100");
                APIServer.HueRequest("#FFFFFF", "middle", "100");
                APIServer.HueRequest("#FFFFFF", "rear", "100");
            }
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            
        }
    }
}
