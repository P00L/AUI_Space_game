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
using AuiSpaceGame.Controller;

namespace AuiSpaceGame.View
{
    /// <summary>
    /// Interaction logic for SetupGamePage.xaml
    /// </summary>
    public partial class SetupGamePage : Page
    {
        private Game Game;
        private Animation CurrentAnimation;

        public SetupGamePage()
        {
            Game = new Game();
            InitializeComponent();
            animationSequence.ItemsSource = Game.AnimationsSequence;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new StartingPage());
        }

        private void gameName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Game.Name = e.Source.ToString();
        }


        private void addAnimation_Click(object sender, RoutedEventArgs e)
        {
            CurrentAnimation = new Asteroid(Lane.Left, Speed.Low);
            Game.AnimationsSequence.Add(CurrentAnimation); //TODO controllare


            animationSequence.ScrollIntoView(CurrentAnimation);


            squareTopLeft.IsEnabled = false;
            squareTopRight.IsEnabled = false;
            squareBottomLeft.IsEnabled = false;
            squareBottomRight.IsEnabled = false;
            laneLeft.IsEnabled = true;
            laneMiddle.IsEnabled = true;
            laneRight.IsEnabled = true;

            laneLeft.Background = Brushes.Yellow;
            laneMiddle.Background = Brushes.AliceBlue;
            laneRight.Background = Brushes.AliceBlue;

            animationAsteroid.IsEnabled = true;
            animationLogicBlock.IsEnabled = true;
            animationAsteroid.IsChecked = true;
            animationLogicBlock.IsChecked = false;

            lowSpeed.IsEnabled = true;
            highSpeed.IsEnabled = true;
            lowSpeed.IsChecked = true;
            highSpeed.IsChecked = false;

            EnableUpDown();
            animationRemove.IsEnabled = true;
            animationSequence.SelectedItem = CurrentAnimation;

        }

        private void animationAsteroid_Checked(object sender, RoutedEventArgs e)
        {
            if (CurrentAnimation != null)
            {
                int index = Game.AnimationsSequence.IndexOf(CurrentAnimation);
                Game.AnimationsSequence.Remove(CurrentAnimation);
                CurrentAnimation = new Asteroid(Lane.Left, Speed.Low);
                Game.AnimationsSequence.Insert(index, CurrentAnimation);

                lowSpeed.IsEnabled = true;
                highSpeed.IsEnabled = true;

                squareTopLeft.IsEnabled = false;
                squareTopRight.IsEnabled = false;
                squareBottomLeft.IsEnabled = false;
                squareBottomRight.IsEnabled = false;
                laneLeft.IsEnabled = true;
                laneMiddle.IsEnabled = true;
                laneRight.IsEnabled = true;

                laneLeft.Background = Brushes.Yellow;
                laneMiddle.Background = Brushes.AliceBlue;
                laneRight.Background = Brushes.AliceBlue;

                animationSequence.SelectedItem = CurrentAnimation;
            }
        }

        private void animationLogicBlock_Checked(object sender, RoutedEventArgs e)
        {
            // TODO
            /* int index = Game.AnimationsSequence.IndexOf(CurrentAnimation);
            Game.AnimationsSequence.Remove(CurrentAnimation);
            CurrentAnimation = new LogicBlock();
            Game.AnimationsSequence.Insert(index, CurrentAnimation);
            */

            lowSpeed.IsEnabled = false;
            highSpeed.IsEnabled = false;

            squareTopLeft.IsEnabled = true;
            squareTopRight.IsEnabled = true;
            squareBottomLeft.IsEnabled = true;
            squareBottomRight.IsEnabled = true;
            laneLeft.IsEnabled = false;
            laneMiddle.IsEnabled = false;
            laneRight.IsEnabled = false;

            squareBottomLeft.Background = Brushes.AliceBlue;
            squareBottomRight.Background = Brushes.AliceBlue;
            squareTopLeft.Background = Brushes.Yellow;
            squareTopRight.Background = Brushes.AliceBlue;
        }

        private void laneLeft_Click(object sender, RoutedEventArgs e)
        {
            Asteroid CurrentAsteroid = (Asteroid)CurrentAnimation;
            CurrentAsteroid.Lane = Lane.Left;

            laneLeft.Background = Brushes.Yellow;
            laneMiddle.Background = Brushes.AliceBlue;
            laneRight.Background = Brushes.AliceBlue;

        }

        private void laneMiddle_Click(object sender, RoutedEventArgs e)
        {
            Asteroid CurrentAsteroid = (Asteroid)CurrentAnimation;
            CurrentAsteroid.Lane = Lane.Middle;

            laneLeft.Background = Brushes.AliceBlue;
            laneMiddle.Background = Brushes.Yellow;
            laneRight.Background = Brushes.AliceBlue;

        }

        private void laneRight_Click(object sender, RoutedEventArgs e)
        {
            Asteroid CurrentAsteroid = (Asteroid)CurrentAnimation;
            CurrentAsteroid.Lane = Lane.Right;

            laneLeft.Background = Brushes.AliceBlue;
            laneMiddle.Background = Brushes.AliceBlue;
            laneRight.Background = Brushes.Yellow;
        }
        private void squareBottomLeft_Click(object sender, RoutedEventArgs e)
        {
            // TODO
            /* LogicBlock CurrentLogicBlock = (LogicBlock)CurrentAnimation;
            CurrentLogicBlock.Lane = Lane.Left; */

            squareBottomLeft.Background = Brushes.Yellow;
            squareBottomRight.Background = Brushes.AliceBlue;
            squareTopLeft.Background = Brushes.AliceBlue;
            squareTopRight.Background = Brushes.AliceBlue;
        }
        private void squareBottomRight_Click(object sender, RoutedEventArgs e)
        {
            // TODO
            /* LogicBlock CurrentLogicBlock = (LogicBlock)CurrentAnimation;
            CurrentLogicBlock.Lane = Lane.Left; */

            squareBottomLeft.Background = Brushes.AliceBlue;
            squareBottomRight.Background = Brushes.Yellow;
            squareTopLeft.Background = Brushes.AliceBlue;
            squareTopRight.Background = Brushes.AliceBlue;
        }
        private void squareTopLeft_Click(object sender, RoutedEventArgs e)
        {
            // TODO
            /* LogicBlock CurrentLogicBlock = (LogicBlock)CurrentAnimation;
            CurrentLogicBlock.Lane = Lane.Left; */

            squareBottomLeft.Background = Brushes.AliceBlue;
            squareBottomRight.Background = Brushes.AliceBlue;
            squareTopLeft.Background = Brushes.Yellow;
            squareTopRight.Background = Brushes.AliceBlue;
        }
        private void squareTopRight_Click(object sender, RoutedEventArgs e)
        {
            // TODO
            /* LogicBlock CurrentLogicBlock = (LogicBlock)CurrentAnimation;
            CurrentLogicBlock.Lane = Lane.Left; */

            squareBottomLeft.Background = Brushes.AliceBlue;
            squareBottomRight.Background = Brushes.AliceBlue;
            squareTopLeft.Background = Brushes.AliceBlue;
            squareTopRight.Background = Brushes.Yellow;
        }

        private void lowSpeed_Checked(object sender, RoutedEventArgs e)
        {
            ((Asteroid)CurrentAnimation).Speed = Speed.Low;
        }

        private void highSpeed_Checked(object sender, RoutedEventArgs e)
        {
            ((Asteroid)CurrentAnimation).Speed = Speed.High;
        }

        private void animationSequence_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (animationSequence.SelectedItem != null)
            {
                CurrentAnimation = (Animation)animationSequence.SelectedItem;
                EnableUpDown();
                if (CurrentAnimation.GetType() == typeof(Asteroid))
                {
                    if (((Asteroid)CurrentAnimation).Lane == Lane.Left)
                    {
                        laneLeft.Background = Brushes.Yellow;
                        laneMiddle.Background = Brushes.AliceBlue;
                        laneRight.Background = Brushes.AliceBlue;
                    }
                    if (((Asteroid)CurrentAnimation).Lane == Lane.Right)
                    {
                        laneLeft.Background = Brushes.AliceBlue;
                        laneMiddle.Background = Brushes.AliceBlue;
                        laneRight.Background = Brushes.Yellow;
                    }
                    if (((Asteroid)CurrentAnimation).Lane == Lane.Middle)
                    {
                        laneLeft.Background = Brushes.AliceBlue;
                        laneMiddle.Background = Brushes.Yellow;
                        laneRight.Background = Brushes.AliceBlue;
                    }
                    if (((Asteroid)CurrentAnimation).Speed == Speed.High)
                    {
                        lowSpeed.IsChecked = false;
                        highSpeed.IsChecked = true;
                    }
                    if (((Asteroid)CurrentAnimation).Speed == Speed.Low)
                    {
                        lowSpeed.IsChecked = true;
                        highSpeed.IsChecked = false;
                    }
                }
            }
        }

        private void animationUp_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentAnimation != null)
            {
                int index = Game.AnimationsSequence.IndexOf(CurrentAnimation);
                Animation tmpAnimation = CurrentAnimation;
                Game.AnimationsSequence.Remove(CurrentAnimation);
                Game.AnimationsSequence.Insert(index - 1, CurrentAnimation);
                animationSequence.SelectedItem = tmpAnimation;
            }
        }

        private void animationDown_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentAnimation != null)
            {
                int index = Game.AnimationsSequence.IndexOf(CurrentAnimation);
                Animation tmpAnimation = CurrentAnimation;
                Game.AnimationsSequence.Remove(CurrentAnimation);
                Game.AnimationsSequence.Insert(index + 1, CurrentAnimation);
                animationSequence.SelectedItem = tmpAnimation;
            }
        }

        private void animationRemove_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentAnimation != null)
            {
                int index = Game.AnimationsSequence.IndexOf(CurrentAnimation);
                int len = Game.AnimationsSequence.Count();
                Game.AnimationsSequence.Remove(CurrentAnimation);
                if (len == 1)
                {
                    animationRemove.IsEnabled = false;
                    Reset();
                    return;
                }
                if (index == 0)
                {
                    CurrentAnimation = Game.AnimationsSequence.ElementAt(index);
                }
                else
                {
                    CurrentAnimation = Game.AnimationsSequence.ElementAt(index - 1);
                }
                animationSequence.SelectedItem = CurrentAnimation;
            }
        }

        private void EnableUpDown()
        {
            if (CurrentAnimation != null)
            {
                int index = Game.AnimationsSequence.IndexOf(CurrentAnimation);
                int len = Game.AnimationsSequence.Count();
                if (index == 0)
                {
                    if (len == 1)
                    {
                        animationUp.IsEnabled = false;
                        animationDown.IsEnabled = false;
                    }
                    else
                    {
                        animationUp.IsEnabled = false;
                        animationDown.IsEnabled = true;
                    }
                }
                else

                    if (index == len - 1)
                {
                    animationUp.IsEnabled = true;
                    animationDown.IsEnabled = false;
                }
                else
                {
                    animationUp.IsEnabled = true;
                    animationDown.IsEnabled = true;
                }
            }
        }

        private void Reset()
        {
            squareTopLeft.IsEnabled = false;
            squareTopRight.IsEnabled = false;
            squareBottomLeft.IsEnabled = false;
            squareBottomRight.IsEnabled = false;
            laneLeft.IsEnabled = false;
            laneMiddle.IsEnabled = false;
            laneRight.IsEnabled = false;

            animationAsteroid.IsEnabled = false;
            animationLogicBlock.IsEnabled = false;
            lowSpeed.IsEnabled = false;
            highSpeed.IsEnabled = false;

            animationAsteroid.IsChecked = false;
            animationLogicBlock.IsChecked = false;
            lowSpeed.IsChecked = false;
            highSpeed.IsChecked = false;
        }

        private void startGame_Click(object sender, RoutedEventArgs e)
        {
            GameState gameState = new GameState();
            Kinect kinect = new Kinect(Game,gameState);
            GameController gameController = new GameController(Game,gameState);
            gameState.GameOn = true;

        }
    }
}
