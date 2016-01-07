using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using AuiSpaceGame.Model;
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

        public void GameOnChanged(object sender, EventArgs e)
        {
            if (GameState.GameOn)
            {
                Console.WriteLine("GAME ON");
                GameState.AnimationOn = true;
            }
            else
            {
                Console.WriteLine("GAME OFF");
                //TODO chiamate pharos & c
            }
        }

        public void GamePauseOnChanged(object sender, EventArgs e)
        {

        }

        public void AnimationOnChanged(object sender, EventArgs e)
        {
            if (GameState.AnimationOn) // false--->true start of animation
            {
                Console.WriteLine("ANIMATION ON " + GameState.AnimationId + "  {0:HH: mm: ss.fff}", DateTime.Now);
                Animation animation = Game.AnimationsSequence.ElementAt(GameState.AnimationId);
                //TODO controllare se serve davvero
                GameState.ExecuteReinforcementChanged -= ExecuteReinforcementChanged;
                if (animation.GetType() == typeof(Asteroid))
                {
                    GameState.ExecuteReinforcement = true;
                }
                else if(animation.GetType() == typeof(LogicBlock))
                {
                    GameState.ExecuteReinforcement = false;
                }
                GameState.ExecuteReinforcementChanged += ExecuteReinforcementChanged;

                animation.StartingAnimationTime = DateTime.Now.AddMilliseconds(Constant.TPharos);
                Timer.Interval = animation.AnimationDuration.TotalMilliseconds;
                //TODO chiamate pharos & C
                if(animation.GetType() == typeof(Asteroid))
                {

                }
                else if (animation.GetType() == typeof(LogicBlock))
                {

                }
                Timer.Start();
            }
            else // true--->false end of animation
            {
                Console.WriteLine("ANIMATION OFF " + GameState.AnimationId + "  {0:HH: mm: ss.fff}", DateTime.Now);
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

        public void ReinforcementOnChanged(object sender, EventArgs e)
        {
            if (GameState.ReinforcementOn) //reinforecmente started
            {
                Console.WriteLine("REINFORCEMENT ON " + "  {0:HH: mm: ss.fff}", DateTime.Now);
                Timer.Interval = Constant.TReinforcement;
                //TODO chiamata pharos & c
                Timer.Start();

            }
            else //reinforcement ended
            {
                Console.WriteLine("REINFORCEMENT OFF " + "  {0:HH: mm: ss.fff}", DateTime.Now);
                NextAnimation();
            }
        }

        public void ExecuteReinforcementChanged(object sender, EventArgs e)
        {
            Console.WriteLine("ExecuteReinforcementChanged");
        }

        private void OnElapsedTimer(object sender, ElapsedEventArgs e)
        {
            if (GameState.AnimationOn) //end of animation
            {
                Console.WriteLine("ANIMATION END " + "  {0:HH: mm: ss.fff}", DateTime.Now);
                GameState.AnimationOn = false;
            }
            if (GameState.ReinforcementOn) //end of reinforcmente
            {
                Console.WriteLine("REINFORCEMENT END " + "{0:HH: mm: ss.fff}", DateTime.Now);
                GameState.ReinforcementOn = false;
            }
        }

        public void NextStepGame()
        {
        }

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
