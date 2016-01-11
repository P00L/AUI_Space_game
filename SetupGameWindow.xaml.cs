﻿using AuiSpaceGame.Controller;
using AuiSpaceGame.Model;
using AuiSpaceGame.Model.Parser;
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

namespace AuiSpaceGame
{
    /// <summary>
    /// Interaction logic for SetupGameWindow.xaml
    /// </summary>
    public partial class SetupGameWindow : Window
    {

        private Game Game;
        private Animation CurrentAnimation;
        private int SquareTmp;
        private Dictionary<string, ImageBrush> squareDic;

        public SetupGameWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            Game = new Game();
            InitializeComponent();
            Reset();
            //string aaa = "Images/LogicBlocks/LogicBlock-Red-Square.png";
            //squareBottomLeftImage.ImageSource = new BitmapImage(new Uri(@aaa, UriKind.Relative));
            animationSequence.ItemsSource = Game.AnimationsSequence;
        }

        public SetupGameWindow(Game game)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            Game = game;
            InitializeComponent();
            Reset();
            gameName.Text = Game.Name;
            childName.Text = Game.Child;
            therapistName.Text = Game.Therapist;
            animationSequence.ItemsSource = Game.AnimationsSequence;
            if (game.AnimationsSequence.Count > 0)
            {
                CurrentAnimation = Game.AnimationsSequence.ElementAt(0);
                startGame.IsEnabled = true;
                animationSequence.SelectedItem = CurrentAnimation;
                animationSequence.Focus();
                animationRemove.IsEnabled = true;
            }

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Window1 Window1 = new Window1();
            Window1.Show();
            this.Close();
        }

        private void gameName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Game.Name = gameName.Text;
        }

        private void childName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Game.Child = childName.Text;
        }
        private void therapistName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Game.Therapist = therapistName.Text;
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
            animationAsteroid.Checked -= animationAsteroid_Checked;
            animationAsteroid.IsChecked = true;
            animationAsteroid.Checked += animationAsteroid_Checked;
            animationLogicBlock.IsChecked = false;

            lowSpeed.IsEnabled = true;
            highSpeed.IsEnabled = true;
            lowSpeed.IsChecked = true;
            highSpeed.IsChecked = false;

            EnableUpDown();
            animationRemove.IsEnabled = true;
            animationSequence.Focus();
            animationSequence.SelectedItem = CurrentAnimation;
            startGame.IsEnabled = true;

        }

        private void animationAsteroid_Checked(object sender, RoutedEventArgs e)
        {
            int index = Game.AnimationsSequence.IndexOf(CurrentAnimation);
            Game.AnimationsSequence.Remove(CurrentAnimation);
            CurrentAnimation = new Asteroid(Lane.Left, Speed.Low);
            Game.AnimationsSequence.Insert(index, CurrentAnimation);
            animationSequence.Focus();
            animationSequence.SelectedItem = CurrentAnimation;
        }

        private void animationLogicBlock_Checked(object sender, RoutedEventArgs e)
        {
            int index = Game.AnimationsSequence.IndexOf(CurrentAnimation);
            Game.AnimationsSequence.Remove(CurrentAnimation);
            CurrentAnimation = new LogicBlock();
            Game.AnimationsSequence.Insert(index, CurrentAnimation);
            animationSequence.Focus();
            animationSequence.SelectedItem = CurrentAnimation;
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
            string imageUri = "";
            int index = 0;
            foreach (Model.Shape s in ((LogicBlock)CurrentAnimation).Shapes)
            {
                imageUri = "Images/LogicBlocks/LogicBlock-" + s.Color + "-" + s.Figure;
                if (s.X == SquareCoordinate.Left && s.Z == SquareCoordinate.Bottom)
                {
                    SquareTmp = index;
                    imageUri += "-selected";
                }
                if (index == ((LogicBlock)CurrentAnimation).Target)
                    imageUri += "-target";
                imageUri += ".png";
                index += 1;
                squareDic[s.X + "-" + s.Z].ImageSource = new BitmapImage(new Uri(@imageUri, UriKind.Relative));
            }

            shapeColorSquare();
        }

        private void squareBottomRight_Click(object sender, RoutedEventArgs e)
        {
            string imageUri = "";
            int index = 0;
            foreach (Model.Shape s in ((LogicBlock)CurrentAnimation).Shapes)
            {
                imageUri = "Images/LogicBlocks/LogicBlock-" + s.Color + "-" + s.Figure;
                if (s.X == SquareCoordinate.Right && s.Z == SquareCoordinate.Bottom)
                {
                    SquareTmp = index;
                    imageUri += "-selected";
                }
                if (index == ((LogicBlock)CurrentAnimation).Target)
                    imageUri += "-target";
                imageUri += ".png";
                index += 1;
                squareDic[s.X + "-" + s.Z].ImageSource = new BitmapImage(new Uri(@imageUri, UriKind.Relative));
            }

            shapeColorSquare();

        }

        private void squareTopLeft_Click(object sender, RoutedEventArgs e)
        {
            string imageUri = "";
            int index = 0;
            foreach (Model.Shape s in ((LogicBlock)CurrentAnimation).Shapes)
            {
                imageUri = "Images/LogicBlocks/LogicBlock-" + s.Color + "-" + s.Figure;
                if (s.X == SquareCoordinate.Left && s.Z == SquareCoordinate.Top)
                {
                    SquareTmp = index;
                    imageUri += "-selected";
                }
                if (index == ((LogicBlock)CurrentAnimation).Target)
                    imageUri += "-target";
                imageUri += ".png";
                index += 1;
                squareDic[s.X + "-" + s.Z].ImageSource = new BitmapImage(new Uri(@imageUri, UriKind.Relative));
            }

            shapeColorSquare();

        }

        private void squareTopRight_Click(object sender, RoutedEventArgs e)
        {
            string imageUri = "";
            int index = 0;
            foreach (Model.Shape s in ((LogicBlock)CurrentAnimation).Shapes)
            {
                imageUri = "Images/LogicBlocks/LogicBlock-" + s.Color + "-" + s.Figure;
                if (s.X == SquareCoordinate.Right && s.Z == SquareCoordinate.Top)
                {
                    SquareTmp = index;
                    imageUri += "-selected";
                }
                if (index == ((LogicBlock)CurrentAnimation).Target)
                    imageUri += "-target";
                imageUri += ".png";
                index += 1;
                squareDic[s.X + "-" + s.Z].ImageSource = new BitmapImage(new Uri(@imageUri, UriKind.Relative));
            }

            shapeColorSquare();

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
            Reset();
            if (animationSequence.SelectedItem != null)
            {
                CurrentAnimation = (Animation)animationSequence.SelectedItem;
                EnableUpDown();
                //ASTEROID ANIMATION
                if (CurrentAnimation.GetType() == typeof(Asteroid))
                {
                    if (((Asteroid)CurrentAnimation).Lane == Lane.Left)
                    {
                        laneLeft.Background = Brushes.Yellow;
                        laneMiddle.Background = Brushes.AliceBlue;
                        laneRight.Background = Brushes.AliceBlue;
                    }
                    else
                    if (((Asteroid)CurrentAnimation).Lane == Lane.Middle)
                    {
                        laneLeft.Background = Brushes.AliceBlue;
                        laneMiddle.Background = Brushes.Yellow;
                        laneRight.Background = Brushes.AliceBlue;
                    }
                    else
                    if (((Asteroid)CurrentAnimation).Lane == Lane.Right)
                    {
                        laneLeft.Background = Brushes.AliceBlue;
                        laneMiddle.Background = Brushes.AliceBlue;
                        laneRight.Background = Brushes.Yellow;
                    }

                    if (((Asteroid)CurrentAnimation).Speed == Speed.Low)
                    {
                        lowSpeed.IsChecked = true;
                        highSpeed.IsChecked = false;
                    }
                    else
                    if (((Asteroid)CurrentAnimation).Speed == Speed.High)
                    {
                        lowSpeed.IsChecked = false;
                        highSpeed.IsChecked = true;
                    }

                    animationAsteroid.IsEnabled = true;
                    animationLogicBlock.IsEnabled = true;

                    lowSpeed.IsEnabled = true;
                    highSpeed.IsEnabled = true;

                    squareTopLeft.IsEnabled = false;
                    squareTopRight.IsEnabled = false;
                    squareBottomLeft.IsEnabled = false;
                    squareBottomRight.IsEnabled = false;
                    laneLeft.IsEnabled = true;
                    laneMiddle.IsEnabled = true;
                    laneRight.IsEnabled = true;

                    colorBlue.IsEnabled = false;
                    colorBlue.IsChecked = false;
                    colorRed.IsEnabled = false;
                    colorRed.IsChecked = false;
                    colorYellow.IsEnabled = false;
                    colorYellow.IsChecked = false;

                    shapeCircle.IsEnabled = false;
                    shapeCircle.IsChecked = false;
                    shapeSquare.IsEnabled = false;
                    shapeSquare.IsChecked = false;
                    shapeTriangle.IsEnabled = false;
                    shapeTriangle.IsChecked = false;

                    animationAsteroid.Checked -= animationAsteroid_Checked;
                    animationAsteroid.IsChecked = true;
                    animationAsteroid.Checked += animationAsteroid_Checked;

                }
                //LOGIC BLOCK ANIMATION
                else if (CurrentAnimation.GetType() == typeof(LogicBlock))
                {
                    //Fake invocation of click button
                    squareDic = new Dictionary<string, ImageBrush>();
                    int index = 0;
                    string stringa = "";
                    string imageUri = "";

                    foreach (Model.Shape s in ((LogicBlock)CurrentAnimation).Shapes)
                    {
                        imageUri = "Images/LogicBlocks/LogicBlock-" + s.Color + "-" + s.Figure;
                        if (s.X == SquareCoordinate.Left && s.Z == SquareCoordinate.Top)
                        {
                            stringa = s.X + "-" + s.Z;
                            squareDic.Add(stringa, squareTopLeftImage);
                            SquareTmp = index;
                            imageUri += "-selected";
                        }
                        if (s.X == SquareCoordinate.Right && s.Z == SquareCoordinate.Top)
                        {
                            stringa = s.X + "-" + s.Z;
                            squareDic.Add(stringa, squareTopRightImage);
                        }
                        if (s.X == SquareCoordinate.Left && s.Z == SquareCoordinate.Bottom)
                        {
                            stringa = s.X + "-" + s.Z;
                            squareDic.Add(stringa, squareBottomLeftImage);
                        }
                        if (s.X == SquareCoordinate.Right && s.Z == SquareCoordinate.Bottom)
                        {
                            stringa = s.X + "-" + s.Z;
                            squareDic.Add(stringa, squareBottomRightImage);
                        }
                        if (index == ((LogicBlock)CurrentAnimation).Target)
                            imageUri += "-target";
                        index += 1;
                        imageUri += ".png";
                        squareDic[s.X + "-" + s.Z].ImageSource = new BitmapImage(new Uri(@imageUri, UriKind.Relative));
                    }

                    shapeColorSquare();

                    colorBlue.IsEnabled = true;
                    colorRed.IsEnabled = true;
                    colorYellow.IsEnabled = true;

                    shapeCircle.IsEnabled = true;
                    shapeTriangle.IsEnabled = true;
                    shapeSquare.IsEnabled = true;

                    animationAsteroid.IsEnabled = true;
                    animationLogicBlock.IsEnabled = true;

                    lowSpeed.IsEnabled = false;
                    lowSpeed.IsChecked = false;
                    highSpeed.IsEnabled = false;
                    highSpeed.IsChecked = false;

                    squareTopLeft.IsEnabled = true;
                    squareTopRight.IsEnabled = true;
                    squareBottomLeft.IsEnabled = true;
                    squareBottomRight.IsEnabled = true;
                    laneLeft.IsEnabled = false;
                    laneMiddle.IsEnabled = false;
                    laneRight.IsEnabled = false;

                    targetAnimationButton.IsEnabled = true;

                    animationLogicBlock.Checked -= animationLogicBlock_Checked;
                    animationLogicBlock.IsChecked = true;
                    animationLogicBlock.Checked += animationLogicBlock_Checked;
                }
            }
        }

        private void animationUp_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentAnimation != null)
            {
                int index = Game.AnimationsSequence.IndexOf(CurrentAnimation);
                Game.AnimationsSequence.Remove(CurrentAnimation);
                Game.AnimationsSequence.Insert(index - 1, CurrentAnimation);
                animationSequence.SelectedItem = CurrentAnimation;
                animationSequence.ScrollIntoView(CurrentAnimation);
                animationSequence.Focus();
            }
        }

        private void animationDown_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentAnimation != null)
            {
                int index = Game.AnimationsSequence.IndexOf(CurrentAnimation);
                Game.AnimationsSequence.Remove(CurrentAnimation);
                Game.AnimationsSequence.Insert(index + 1, CurrentAnimation);
                animationSequence.SelectedItem = CurrentAnimation;
                animationSequence.ScrollIntoView(CurrentAnimation);
                animationSequence.Focus();
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
                    startGame.IsEnabled = false;
                    Reset();
                    return;
                }
                if (index == 0)
                {
                    CurrentAnimation = Game.AnimationsSequence.ElementAt(index);
                }
                else
                {
                    if (index == (len - 1))
                        CurrentAnimation = Game.AnimationsSequence.ElementAt(index - 1);
                    else
                        CurrentAnimation = Game.AnimationsSequence.ElementAt(index);
                }
                animationSequence.SelectedItem = CurrentAnimation;
                animationSequence.Focus();
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
            laneMiddle.Background = Brushes.AliceBlue;
            laneRight.Background = Brushes.AliceBlue;
            laneLeft.Background = Brushes.AliceBlue;
            squareBottomLeftImage.ImageSource = new BitmapImage(new Uri(@"Images/LogicBlocks/none.png", UriKind.Relative)); ;
            squareBottomRightImage.ImageSource = new BitmapImage(new Uri(@"Images/LogicBlocks/none.png", UriKind.Relative)); ;
            squareTopLeftImage.ImageSource = new BitmapImage(new Uri(@"Images/LogicBlocks/none.png", UriKind.Relative)); ;
            squareTopRightImage.ImageSource = new BitmapImage(new Uri(@"Images/LogicBlocks/none.png", UriKind.Relative)); ;

            colorBlue.IsEnabled = false;
            colorBlue.IsChecked = false;
            colorRed.IsEnabled = false;
            colorRed.IsChecked = false;
            colorYellow.IsEnabled = false;
            colorYellow.IsChecked = false;

            shapeCircle.IsEnabled = false;
            shapeCircle.IsChecked = false;
            shapeSquare.IsEnabled = false;
            shapeSquare.IsChecked = false;
            shapeTriangle.IsEnabled = false;
            shapeTriangle.IsChecked = false;

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

            targetAnimationButton.IsEnabled = false;

            animationAsteroid.IsChecked = false;
            animationLogicBlock.IsChecked = false;
            lowSpeed.IsChecked = false;
            highSpeed.IsChecked = false;
        }

        private void startGame_Click(object sender, RoutedEventArgs e)
        {
            GameState gameState = new GameState();
            Kinect kinect = new Kinect(Game, gameState);
            GameController gameController = new GameController(Game, gameState);
            gameState.GameOn = true;

        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Parser.saveGame(Game);
            //TODO segnalare gioco salvato
            //TODO gestire attivazione pulsante "save"
        }

        private void shapeTriangle_Checked(object sender, RoutedEventArgs e)
        {
            LogicBlock CurrentLogicBlock = (LogicBlock)CurrentAnimation;
            Model.Shape shape = CurrentLogicBlock.Shapes[SquareTmp];
            shape.Figure = FigureShape.Triangle;
            string imageUri = "Images/LogicBlocks/LogicBlock-" + shape.Color + "-" + shape.Figure + "-selected";
            //Forcing the refresh of the image
            if (SquareTmp == CurrentLogicBlock.Target)
            {
                CurrentLogicBlock.Target = SquareTmp;
                imageUri += "-target";
            }
            imageUri += ".png";
            squareDic[shape.X + "-" + shape.Z].ImageSource = new BitmapImage(new Uri(@imageUri, UriKind.Relative));

        }

        private void shapeSquare_Checked(object sender, RoutedEventArgs e)
        {
            LogicBlock CurrentLogicBlock = (LogicBlock)CurrentAnimation;
            Model.Shape shape = CurrentLogicBlock.Shapes[SquareTmp];
            shape.Figure = FigureShape.Square;
            string imageUri = "Images/LogicBlocks/LogicBlock-" + shape.Color + "-" + shape.Figure + "-selected";
            //Forcing the refresh of the image
            if (SquareTmp == CurrentLogicBlock.Target)
            {
                CurrentLogicBlock.Target = SquareTmp;
                imageUri += "-target";
            }
            imageUri += ".png";
            squareDic[shape.X + "-" + shape.Z].ImageSource = new BitmapImage(new Uri(@imageUri, UriKind.Relative));

        }

        private void shapeCircle_Checked(object sender, RoutedEventArgs e)
        {
            LogicBlock CurrentLogicBlock = (LogicBlock)CurrentAnimation;
            Model.Shape shape = CurrentLogicBlock.Shapes[SquareTmp];
            shape.Figure = FigureShape.Circle;
            string imageUri = "Images/LogicBlocks/LogicBlock-" + shape.Color + "-" + shape.Figure + "-selected";
            //Forcing the refresh of the image

            if (SquareTmp == CurrentLogicBlock.Target)
            {
                CurrentLogicBlock.Target = SquareTmp;
                imageUri += "-target";
            }
            imageUri += ".png";
            squareDic[shape.X + "-" + shape.Z].ImageSource = new BitmapImage(new Uri(@imageUri, UriKind.Relative));

        }

        private void colorYellow_Checked(object sender, RoutedEventArgs e)
        {
            LogicBlock CurrentLogicBlock = (LogicBlock)CurrentAnimation;
            Model.Shape shape = CurrentLogicBlock.Shapes[SquareTmp];
            shape.Color = Colour.Yellow;
            string imageUri = "Images/LogicBlocks/LogicBlock-" + shape.Color + "-" + shape.Figure + "-selected";
            //Forcing the refresh of the image
            if (SquareTmp == CurrentLogicBlock.Target)
            {
                CurrentLogicBlock.Target = SquareTmp;
                imageUri += "-target";
            }
            imageUri += ".png";
            squareDic[shape.X + "-" + shape.Z].ImageSource = new BitmapImage(new Uri(@imageUri, UriKind.Relative));

        }

        private void colorRed_Checked(object sender, RoutedEventArgs e)
        {
            LogicBlock CurrentLogicBlock = (LogicBlock)CurrentAnimation;
            Model.Shape shape = CurrentLogicBlock.Shapes[SquareTmp];
            shape.Color = Colour.Red;
            string imageUri = "Images/LogicBlocks/LogicBlock-" + shape.Color + "-" + shape.Figure + "-selected";
            //Forcing the refresh of the image
            if (SquareTmp == CurrentLogicBlock.Target)
            {
                CurrentLogicBlock.Target = SquareTmp;
                imageUri += "-target";
            }
            imageUri += ".png";
            squareDic[shape.X + "-" + shape.Z].ImageSource = new BitmapImage(new Uri(@imageUri, UriKind.Relative));
        }

        private void colorBlue_Checked(object sender, RoutedEventArgs e)
        {
            LogicBlock CurrentLogicBlock = (LogicBlock)CurrentAnimation;
            Model.Shape shape = CurrentLogicBlock.Shapes[SquareTmp];
            shape.Color = Colour.Blue;
            string imageUri = "Images/LogicBlocks/LogicBlock-" + shape.Color + "-" + shape.Figure + "-selected";
            //Forcing the refresh of the image
            if (SquareTmp == CurrentLogicBlock.Target)
            {
                CurrentLogicBlock.Target = SquareTmp;
                imageUri += "-target";
            }
            imageUri += ".png";
            squareDic[shape.X + "-" + shape.Z].ImageSource = new BitmapImage(new Uri(@imageUri, UriKind.Relative));

        }

        private void shapeColorSquare()
        {
            if (((LogicBlock)CurrentAnimation).Shapes[SquareTmp].Color == Colour.Red)
            {
                colorBlue.IsChecked = false;
                colorYellow.IsChecked = false;
                colorRed.IsChecked = true;
            }
            else
            if (((LogicBlock)CurrentAnimation).Shapes[SquareTmp].Color == Colour.Blue)
            {
                colorBlue.IsChecked = true;
                colorYellow.IsChecked = false;
                colorRed.IsChecked = false;
            }
            else
            if (((LogicBlock)CurrentAnimation).Shapes[SquareTmp].Color == Colour.Yellow)
            {
                colorBlue.IsChecked = false;
                colorYellow.IsChecked = true;
                colorRed.IsChecked = false;
            }

            if (((LogicBlock)CurrentAnimation).Shapes[SquareTmp].Figure == FigureShape.Circle)
            {
                shapeCircle.IsChecked = true;
                shapeTriangle.IsChecked = false;
                shapeSquare.IsChecked = false;
            }
            else
            if (((LogicBlock)CurrentAnimation).Shapes[SquareTmp].Figure == FigureShape.Square)
            {
                shapeCircle.IsChecked = false;
                shapeTriangle.IsChecked = false;
                shapeSquare.IsChecked = true;
            }
            else
            if (((LogicBlock)CurrentAnimation).Shapes[SquareTmp].Figure == FigureShape.Triangle)
            {
                shapeCircle.IsChecked = false;
                shapeTriangle.IsChecked = true;
                shapeSquare.IsChecked = false;
            }

        }

        private void targetAnimationButton_Click(object sender, RoutedEventArgs e)
        {
            LogicBlock CurrentLogicBlock = (LogicBlock)CurrentAnimation;
            Model.Shape shapeOld = CurrentLogicBlock.Shapes[CurrentLogicBlock.Target];
            Model.Shape shapeNew = CurrentLogicBlock.Shapes[SquareTmp];
            CurrentLogicBlock.Target = SquareTmp;
            string imageUriNew = "Images/LogicBlocks/LogicBlock-" + shapeNew.Color + "-" + shapeNew.Figure + "-selected-target.png";
            string imageUriOld = "Images/LogicBlocks/LogicBlock-" + shapeOld.Color + "-" + shapeOld.Figure + ".png";
            squareDic[shapeOld.X + "-" + shapeOld.Z].ImageSource = new BitmapImage(new Uri(imageUriOld, UriKind.Relative));
            squareDic[shapeNew.X + "-" + shapeNew.Z].ImageSource = new BitmapImage(new Uri(imageUriNew, UriKind.Relative));
        }
    }
}