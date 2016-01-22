using AuiSpaceGame.Controller;
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
        private GameState gameState;
        private Animation CurrentAnimation;
        private int SquareTmp;
        private Dictionary<string, ImageBrush> squareDic;
        bool AmbientAnimationOn;

        public SetupGameWindow(bool ambientAnimationOn)
        {
            AmbientAnimationOn = ambientAnimationOn;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            Game = new Game();
            InitializeComponent();
            Reset();
            //string aaa = "Images/LogicBlocks/LogicBlock-Red-Square.png";
            //squareBottomLeftImage.ImageSource = new BitmapImage(new Uri(@aaa, UriKind.Relative));
            animationSequence.ItemsSource = Game.AnimationsSequence;
        }

        public SetupGameWindow(Game game, bool ambientAnimationOn)
        {
            AmbientAnimationOn = ambientAnimationOn;
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

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Window1 Window1 = new Window1(AmbientAnimationOn);
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

        private void addAsteroid_Click(object sender, RoutedEventArgs e)
        {
            CurrentAnimation = new Asteroid(Lane.Left, Speed.Low);
            addAnimationClicked();
        }

        private void addLogicBlock_Click(object sender, RoutedEventArgs e)
        {
            CurrentAnimation = new LogicBlock();
            addAnimationClicked();
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
            laneClicked(Lane.Left);
        }

        private void laneMiddle_Click(object sender, RoutedEventArgs e)
        {
            laneClicked(Lane.Middle);
        }

        private void laneRight_Click(object sender, RoutedEventArgs e)
        {

            laneClicked(Lane.Right);
        }

        private void squareBottomLeft_Click(object sender, RoutedEventArgs e)
        {
            squareClicked(SquareCoordinate.Left, SquareCoordinate.Bottom);
        }

        private void squareBottomRight_Click(object sender, RoutedEventArgs e)
        {
            squareClicked(SquareCoordinate.Right, SquareCoordinate.Bottom);
        }

        private void squareTopLeft_Click(object sender, RoutedEventArgs e)
        {
            squareClicked(SquareCoordinate.Left, SquareCoordinate.Top);
        }

        private void squareTopRight_Click(object sender, RoutedEventArgs e)
        {
            squareClicked(SquareCoordinate.Right, SquareCoordinate.Top);
        }

        private void lowSpeed_Checked(object sender, RoutedEventArgs e)
        {
            speedClicked(Speed.Low);
        }

        private void highSpeed_Checked(object sender, RoutedEventArgs e)
        {
            speedClicked(Speed.High);
        }

        private void animationSequence_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Reset();
            if (animationSequence.SelectedItem != null)
            {
                CurrentAnimation = (Animation)animationSequence.SelectedItem;
                EnableUpDown();
                animationAsteroid.IsEnabled = true;
                animationLogicBlock.IsEnabled = true;
                animationLogicBLockImage.Opacity = 1;
                animationAsteroidImage.Opacity = 1;
                //ASTEROID ANIMATION
                if (CurrentAnimation.GetType() == typeof(Asteroid))
                {
                    Asteroid CurrentAsteroid = (Asteroid)CurrentAnimation;
                    if (CurrentAsteroid.Lane == Lane.Left)
                    {
                        string imageUri = "Images/AsteroidsCarpet/Asteroid-";
                        if (CurrentAsteroid.Speed == Speed.Low)
                            imageUri += "low";
                        else if (CurrentAsteroid.Speed == Speed.High)
                            imageUri += "high";
                        imageUri += ".png";

                        string noImageUri = "Images/AsteroidsCarpet/none.png";
                        laneLeftImage.ImageSource = new BitmapImage(new Uri(@imageUri, UriKind.Relative));
                        laneMiddleImage.ImageSource = new BitmapImage(new Uri(@noImageUri, UriKind.Relative));
                        laneRightImage.ImageSource = new BitmapImage(new Uri(@noImageUri, UriKind.Relative));
                    }
                    else
                    if (CurrentAsteroid.Lane == Lane.Middle)
                    {
                        string imageUri = "Images/AsteroidsCarpet/Asteroid-";
                        if (CurrentAsteroid.Speed == Speed.Low)
                            imageUri += "low";
                        else if (CurrentAsteroid.Speed == Speed.High)
                            imageUri += "high";
                        imageUri += ".png";

                        string noImageUri = "Images/AsteroidsCarpet/none.png";
                        laneLeftImage.ImageSource = new BitmapImage(new Uri(@noImageUri, UriKind.Relative));
                        laneMiddleImage.ImageSource = new BitmapImage(new Uri(@imageUri, UriKind.Relative));
                        laneRightImage.ImageSource = new BitmapImage(new Uri(@noImageUri, UriKind.Relative));
                    }
                    else
                    if (CurrentAsteroid.Lane == Lane.Right)
                    {
                        string imageUri = "Images/AsteroidsCarpet/Asteroid-";
                        if (CurrentAsteroid.Speed == Speed.Low)
                            imageUri += "low";
                        else if (CurrentAsteroid.Speed == Speed.High)
                            imageUri += "high";
                        imageUri += ".png";

                        string noImageUri = "Images/AsteroidsCarpet/none.png";
                        laneLeftImage.ImageSource = new BitmapImage(new Uri(@noImageUri, UriKind.Relative));
                        laneMiddleImage.ImageSource = new BitmapImage(new Uri(@noImageUri, UriKind.Relative));
                        laneRightImage.ImageSource = new BitmapImage(new Uri(@imageUri, UriKind.Relative));
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

                    AsteroidRadioButton();

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

                    lowSpeed.IsEnabled = false;
                    lowSpeed.IsChecked = false;
                    highSpeed.IsEnabled = false;
                    highSpeed.IsChecked = false;

                    logicBlockEnableRadioButton();

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
                    Reset();
                    animationRemove.IsEnabled = false;
                    startGame.IsEnabled = false;
                    startGame.Content = Application.Current.Resources["startGame"];
                    backButton.IsEnabled = true;
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
            laneLeftImage.ImageSource = new BitmapImage(new Uri(@"Images/AsteroidsCarpet/none.png", UriKind.Relative));
            laneMiddleImage.ImageSource = new BitmapImage(new Uri(@"Images/AsteroidsCarpet/none.png", UriKind.Relative));
            laneRightImage.ImageSource = new BitmapImage(new Uri(@"Images/AsteroidsCarpet/none.png", UriKind.Relative));
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

            lowSpeedImage.Opacity = 0.23;
            highSpeedImage.Opacity = 0.23;
            colorBlueImage.Opacity = 0.23;
            colorYellowImage.Opacity = 0.23;
            colorRedImage.Opacity = 0.23;
            shapeSquareImage.Opacity = 0.23;
            shapeTriangleImage.Opacity = 0.23;
            shapeCircleImage.Opacity = 0.23;
            animationAsteroidImage.Opacity = 0.23;
            animationLogicBLockImage.Opacity = 0.23;
            SpeedLabel.Opacity = 0.23;
            colorLabel.Opacity = 0.23;
            shapeLabel.Opacity = 0.23;
        }

        private void startGame_Click(object sender, RoutedEventArgs e)
        {
            if (gameState == null || !gameState.GameOn)
            {
                gameState = new GameState();
                Kinect kinect = new Kinect(Game, gameState);
                GameController gameController = new GameController(Game, gameState);
                gameState.GameOn = true;
                startGame.Content = Application.Current.Resources["endGame"];
                backButton.IsEnabled = false;
            }
            else
            {
                startGame.Content = Application.Current.Resources["startGame"];
                backButton.IsEnabled = true;
                gameState.GameOn = false;
                AmbientAnimationOn = false;
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Parser.saveGame(Game);
            //TODO segnalare gioco salvato
            //TODO gestire attivazione pulsante "save"
        }

        private void shapeTriangle_Checked(object sender, RoutedEventArgs e)
        {
            shapeClicked(FigureShape.Triangle);
        }

        private void shapeSquare_Checked(object sender, RoutedEventArgs e)
        {
            shapeClicked(FigureShape.Square);
        }

        private void shapeCircle_Checked(object sender, RoutedEventArgs e)
        {
            shapeClicked(FigureShape.Circle);
        }

        private void colorYellow_Checked(object sender, RoutedEventArgs e)
        {
            colorClicked(Colour.Yellow);
        }

        private void colorRed_Checked(object sender, RoutedEventArgs e)
        {
            colorClicked(Colour.Red);
        }

        private void colorBlue_Checked(object sender, RoutedEventArgs e)
        {
            colorClicked(Colour.Blue);
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

        private void logicBlockEnableRadioButton()
        {
            lowSpeedImage.Opacity = 0.23;
            highSpeedImage.Opacity = 0.23;
            colorBlueImage.Opacity = 1;
            colorYellowImage.Opacity = 1;
            colorRedImage.Opacity = 1;
            shapeSquareImage.Opacity = 1;
            shapeTriangleImage.Opacity = 1;
            shapeCircleImage.Opacity = 1;
            SpeedLabel.Opacity = 0.23;
            colorLabel.Opacity = 1;
            shapeLabel.Opacity = 1;
        }

        private void AsteroidRadioButton()
        {
            lowSpeedImage.Opacity = 1;
            highSpeedImage.Opacity = 1;
            colorBlueImage.Opacity = 0.23;
            colorYellowImage.Opacity = 0.23;
            colorRedImage.Opacity = 0.23;
            shapeSquareImage.Opacity = 0.23;
            shapeTriangleImage.Opacity = 0.23;
            shapeCircleImage.Opacity = 0.23;
            SpeedLabel.Opacity = 1;
            colorLabel.Opacity = 0.23;
            shapeLabel.Opacity = 0.23;
        }

        private void RemoteControl(object sender, KeyEventArgs e)
        {
            if (gameState != null && gameState.AnimationOn)
            {
                if (e.Key == Key.PageUp)
                {
                    Console.WriteLine("execute reinforement therapist decision");
                    gameState.ExecuteReinforcement = true;
                }
                if (e.Key == Key.PageDown)
                {
                    Console.WriteLine("redo animation therapist decision");
                    gameState.RedoAnimation = true;

                }
            }
        }

        private void addAnimationClicked()
        {
            Game.AnimationsSequence.Add(CurrentAnimation);
            animationSequence.ScrollIntoView(CurrentAnimation);
            animationSequence.Focus();
            animationSequence.SelectedItem = CurrentAnimation;
            animationRemove.IsEnabled = true;
            startGame.IsEnabled = true;
        }
        private void laneClicked(double lane)
        {
            Asteroid CurrentAsteroid = (Asteroid)CurrentAnimation;
            CurrentAsteroid.Lane = lane;

            string imageUri = "Images/AsteroidsCarpet/Asteroid-";
            if (CurrentAsteroid.Speed == Speed.Low)
                imageUri += "low";
            else if (CurrentAsteroid.Speed == Speed.High)
                imageUri += "high";
            imageUri += ".png";

            string noImageUri = "Images/AsteroidsCarpet/none.png";
            if (lane == Lane.Left)
            {
                laneLeftImage.ImageSource = new BitmapImage(new Uri(@imageUri, UriKind.Relative));
                laneMiddleImage.ImageSource = new BitmapImage(new Uri(@noImageUri, UriKind.Relative));
                laneRightImage.ImageSource = new BitmapImage(new Uri(@noImageUri, UriKind.Relative));
            }
            else if (lane == Lane.Middle)
            {
                laneLeftImage.ImageSource = new BitmapImage(new Uri(@noImageUri, UriKind.Relative));
                laneMiddleImage.ImageSource = new BitmapImage(new Uri(@imageUri, UriKind.Relative));
                laneRightImage.ImageSource = new BitmapImage(new Uri(@noImageUri, UriKind.Relative));
            }
            else if (lane == Lane.Right)
            {
                laneLeftImage.ImageSource = new BitmapImage(new Uri(@noImageUri, UriKind.Relative));
                laneMiddleImage.ImageSource = new BitmapImage(new Uri(@noImageUri, UriKind.Relative));
                laneRightImage.ImageSource = new BitmapImage(new Uri(@imageUri, UriKind.Relative));
            }
        }
        private void squareClicked(double x, double z)
        {
            string imageUri = "";
            int index = 0;
            foreach (Model.Shape s in ((LogicBlock)CurrentAnimation).Shapes)
            {
                imageUri = "Images/LogicBlocks/LogicBlock-" + s.Color + "-" + s.Figure;
                if (s.X == x && s.Z == z)
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
        private void speedClicked(double speed)
        {
            Asteroid CurrentAsteroid = (Asteroid)CurrentAnimation;
            CurrentAsteroid.Speed = speed;
            string imageUri = "Images/AsteroidsCarpet/Asteroid-";
            if (speed == Speed.High)
                imageUri += "high";
            else if (speed == Speed.Low)
                imageUri += "low";
            imageUri += ".png";
            if (CurrentAsteroid.Lane == Lane.Left)
                laneLeftImage.ImageSource = new BitmapImage(new Uri(@imageUri, UriKind.Relative));
            else if (CurrentAsteroid.Lane == Lane.Middle)
                laneMiddleImage.ImageSource = new BitmapImage(new Uri(@imageUri, UriKind.Relative));
            else if (CurrentAsteroid.Lane == Lane.Right)
                laneRightImage.ImageSource = new BitmapImage(new Uri(@imageUri, UriKind.Relative));
        }
        private void shapeClicked(string figureShape)
        {
            LogicBlock CurrentLogicBlock = (LogicBlock)CurrentAnimation;
            Model.Shape shape = CurrentLogicBlock.Shapes[SquareTmp];
            shape.Figure = figureShape;
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
        private void colorClicked(string color)
        {
            LogicBlock CurrentLogicBlock = (LogicBlock)CurrentAnimation;
            Model.Shape shape = CurrentLogicBlock.Shapes[SquareTmp];
            shape.Color = color;
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
    }
}