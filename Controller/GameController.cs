using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using AuiSpaceGame.Model;
using AuiSpaceGame.Utilities;

namespace AuiSpaceGame.Controller
{

    public class GameController
    {

        GameState GameState;
        Game Game;
        Timer Timer;

        public GameController(Game game, GameState gameState)
        {
            GameState = gameState;
            Game = game;
            GameState.GameOnChanged += new EventHandler(GameOnChanged);
            GameState.GamePauseOnChanged += new EventHandler(GamePauseOnChanged);
            GameState.AnimationOnChanged += new EventHandler(AnimationOnChanged);
            GameState.ReinforcementOnChanged += new EventHandler(ReinforcementOnChanged);
            GameState.ExecuteReinforcementChanged += new EventHandler(ExecuteReinforcementChanged);
            Timer = new Timer();
            Timer.Elapsed += OnElapsedTimer;
            Timer.AutoReset = false;
        }

        /// <summary>
        /// Handles the start of the game and its end 
        /// </summary>
        public void GameOnChanged(object sender, EventArgs e)
        {
            if (GameState.GameOn)
            {
                Console.WriteLine("GAME ON");
                APIServer.LuminousCarpetRequest("5");
                //TODO chiamata pharos CHECK SPEGNERE LUCI!!!
                APIServer.HueRequest("#000000", "front", "1");
                APIServer.HueRequest("#000000", "middle", "1");
                APIServer.HueRequest("#000000", "rear", "1");

                //let's start the introductory video
                Timer.Interval = Constant.TIntroVideo;
                APIServer.ShowVideoOnScreenRequest("FirstScreen", "Introduction.mp4"); //TODO AGGIUNGERE IL VIDEO
                Timer.Start();
            }
            else
            {
                Console.WriteLine("GAME OFF");
                APIServer.LuminousCarpetRequest("5");
                //TODO spegnere il video sullo schermo
                APIServer.HueRequest("#FFFFFF", "front", "100");
                APIServer.HueRequest("#FFFFFF", "middle", "100");
                APIServer.HueRequest("#FFFFFF", "rear", "100");
            }
        }

        public void GamePauseOnChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the start of an animation and its end
        /// </summary>
        public void AnimationOnChanged(object sender, EventArgs e)
        {
            if (GameState.AnimationOn) // false--->true start of animation
            {
                Console.WriteLine("ANIMATION ON " + GameState.AnimationId + "  {0:HH: mm: ss.fff}", DateTime.Now);
                Animation animation = Game.AnimationsSequence.ElementAt(GameState.AnimationId);

                GameState.ExecuteReinforcementChanged -= ExecuteReinforcementChanged;
                if (animation.GetType() == typeof(Asteroid))
                {
                    GameState.ExecuteReinforcement = true;
                }
                else if (animation.GetType() == typeof(LogicBlock))
                {
                    GameState.ExecuteReinforcement = false;
                }
                GameState.ExecuteReinforcementChanged += ExecuteReinforcementChanged;

                animation.StartingAnimationTime = DateTime.Now.AddMilliseconds(Constant.TPharos);
                Timer.Interval = animation.AnimationDuration.TotalMilliseconds;
                //TODO chiamate pharos & C
                if (animation.GetType() == typeof(Asteroid))
                {
                    APIServer.ShowVideoOnScreenRequest("FirstScreen", "Space.mp4");
                    APIServer.LuminousCarpetRequest("6");
                    APIServer.LuminousCarpetRequest(((Asteroid)animation).ToString()); //TODO CHECK

                }
                else if (animation.GetType() == typeof(LogicBlock))
                {
                    LogicBlock animationLogicBlock = (LogicBlock)animation;
                    APIServer.ShowVideoOnScreenRequest("FirstScreen", "LogicBlock-" + animationLogicBlock.Shapes[animationLogicBlock.Target].Figure + "-" + animationLogicBlock.Shapes[animationLogicBlock.Target].Color + ".mp4");
                    string LogicBlockString = animationLogicBlock.ToString();
                    string[] ShapesString = LogicBlockString.Split(';');
                   // System.Threading.Thread.Sleep(5000); //TODO!!! tempo del video!!!
                    for (int i = 0; i < ShapesString.Length; i++)
                    {
                        APIServer.LuminousCarpetRequest(ShapesString[i]);
                    }
                }
                Timer.Start();
            }
            else // true--->false end of animation
            {
                Console.WriteLine("ANIMATION OFF " + GameState.AnimationId + "  {0:HH: mm: ss.fff}", DateTime.Now);
                APIServer.LuminousCarpetRequest("5");
                if (GameState.ExecuteReinforcement)
                {
                    GameState.ReinforcementOn = true;
                }
                else
                {
                    NextAnimation();
                }
            }
        }

        /// <summary>
        /// Handles the start of the reinforcement and its end
        /// </summary>
        public void ReinforcementOnChanged(object sender, EventArgs e)
        {
            if (GameState.ReinforcementOn) //reinforecmente started
            {
                Console.WriteLine("REINFORCEMENT ON " + "  {0:HH: mm: ss.fff}", DateTime.Now);
                Timer.Interval = Constant.TReinforcement;
                //TODO chiamata pharos & c
                APIServer.ShowVideoOnScreenRequest("FirstScreen", "Reinforcement.mp4");
                APIServer.HueRequest("#FF0000", "front", "50");
                APIServer.HueRequest("#FFCC00", "middle", "50");
                APIServer.HueRequest("#FFFF00", "rear", "50");
                APIServer.LuminousCarpetRequest("7");
                Timer.Start();

            }
            else //reinforcement ended
            {
                Console.WriteLine("REINFORCEMENT OFF " + "  {0:HH: mm: ss.fff}", DateTime.Now);
                //TODO chiamata pharos CHECK SPEGNERE LUCI!!!
                APIServer.HueRequest("#000000", "front", "1");
                APIServer.HueRequest("#000000", "middle", "1");
                APIServer.HueRequest("#000000", "rear", "1");
                APIServer.LuminousCarpetRequest("5");
                NextAnimation();
            }
        }

        /// <summary>
        /// Handles the changes to ExecuteReinforcement, when needed
        /// </summary>
        public void ExecuteReinforcementChanged(object sender, EventArgs e)
        {
            Console.WriteLine("ExecuteReinforcementChanged");
            Animation animation = Game.AnimationsSequence.ElementAt(GameState.AnimationId);
            if (animation.GetType() == typeof(LogicBlock))
            {
                if (GameState.ExecuteReinforcement)
                {
                    Console.WriteLine("REINFORCEMENT END due to target reached before expire of timer" + "{0:HH: mm: ss.fff}", DateTime.Now);
                    Timer.Stop();
                    GameState.AnimationOn = false;
                }
            }
        }

        /// <summary>
        /// Handles the event fired by a timer
        /// </summary>
        private void OnElapsedTimer(object sender, ElapsedEventArgs e)
        {
            if (GameState.AnimationOn) //end of animation
            {
                Console.WriteLine("ANIMATION END " + "  {0:HH: mm: ss.fff}", DateTime.Now);
                GameState.AnimationOn = false;
            }
            else if (GameState.ReinforcementOn) //end of reinforcmente
            {
                Console.WriteLine("REINFORCEMENT END " + "{0:HH: mm: ss.fff}", DateTime.Now);
                GameState.ReinforcementOn = false;
            }
            else //end of introductory video
            {
                Console.WriteLine("INTRO-VIDEO END " + "{0:HH: mm: ss.fff}", DateTime.Now);
                GameState.AnimationOn = true;
            }
        }

        //TODO togliere!? che è?!?!
        public void NextStepGame()
        {
        }

        /// <summary>
        /// Manages the transition between an animation and the other
        /// </summary>
        private void NextAnimation()
        {
            if (GameState.RedoAnimation)
            {
                Console.WriteLine("redo");
                GameState.RedoAnimation = false;
                GameState.AnimationOn = true;
            }
            else //animation is over and no reinforcement
            {
                if (CheckEndGame())
                {
                    Console.WriteLine("end game");
                    GameState.GameOn = false;
                }
                else
                {
                    Console.WriteLine("next animation");
                    GameState.AnimationId += 1;
                    GameState.AnimationOn = true;
                }

            }
        }

        //Check whether the game is ended or not
        private Boolean CheckEndGame()
        {
            if (GameState.AnimationId < (Game.AnimationsSequence.Count - 1))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
