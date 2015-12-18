using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using AuiSpaceGame.Model;
namespace AuiSpaceGame.Controller
{

    class GameController
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
            if (GameState.GameOn) {
                GameState.AnimationOn = true;
            }
            else
            {
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
                Animation animation = Game.AnimationsSequence.ElementAt(GameState.AnimationId);
                animation.StartingAnimationTime = DateTime.Now.AddMilliseconds(Constant.TPharos);
                Console.WriteLine(animation.AnimationDuration);
                Timer.Interval = animation.AnimationDuration.Milliseconds;
                //TODO chiamate pharos & C
                Timer.Start();
            }
            else // true--->false end of animation
            {
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
                Timer.Interval = Constant.TReinforcement;
                //TODO chiamata pharos & c
                Timer.Start();

            }
            else //reinforcement ended
            {
                NextAnimation();
            }
        }

        public void ExecuteReinforcementChanged(object sender, EventArgs e)
        {
        }

        private void OnElapsedTimer(object sender, ElapsedEventArgs e)
        {
            if (GameState.AnimationOn) //end of animation
            {
                GameState.AnimationOn = false;
            }
            if (GameState.ReinforcementOn) //end of animation
            {
                GameState.ReinforcementOn = false;
            }
        }


        public void NextStepGame() {
        }

        private void NextAnimation()
        {
            if (GameState.RedoAnimation)
            {
                GameState.RedoAnimation = false;
                GameState.AnimationOn = true;
            }
            else //animation is over and no reinforcement
            {
                if (CheckEndGame())
                {
                    GameState.GameOn = false;
                }
                else
                {
                    GameState.AnimationId += 1;
                }

            }
        }

        private Boolean CheckEndGame()
        {
            if (GameState.AnimationId < Game.AnimationsSequence.Count)
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
