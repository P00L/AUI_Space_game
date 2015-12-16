using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuiSpaceGame.Model
{
    class GameState
    {
        //gameon:	TRUE-->	gamePau TRUE
        //          FALSE           FALSE-->animationOn TRUE (esclusivi)
        //                                              FALSE
        //                                  reinforcementOn TRUE
        //                                                  FALSE
        
        //TODO game pause????

        public event EventHandler GameOnChanged;
        public event EventHandler GamePauseOnChanged;
        public event EventHandler AnimationOnChanged;
        public event EventHandler ReinforcementOnChanged;
        public event EventHandler ExecuteReinforcementChanged;

        private Boolean gameOn = false;
        private Boolean gamePause = false;
        private Boolean animationOn = false;
        private Boolean reinforcementOn = false;
        private Boolean executeReinforcement = true; //se il bambino colpisce l'asteroide diventa false
        private Boolean redoAnimation = false;
        private int animationId = 0;

        public int AnimationId
        {
            get
            {
                return animationId;
            }
            set
            {
                animationId = value;
            }
        }

        public Boolean RedoAnimation
        {
            get
            {
                return redoAnimation;
            }
            set
            {
                redoAnimation = value;
            }
        }

        public Boolean ReinforcementOn
        {
            get
            {
                return reinforcementOn;
            }
            set
            {
                reinforcementOn = value;
                OnReinforcementOnChanged(EventArgs.Empty);
            }
        }

        public Boolean ExecuteReinforcement
        {
            get
            {
                return executeReinforcement;
            }
            set
            {
                executeReinforcement = value;
                OnExecuteReinforcementChanged(EventArgs.Empty);
            }
        } 
        public Boolean AnimationOn
        {
            get
            {
                return animationOn;
            }
            set
            {
                animationOn = value;
                OnAnimationOnChanged(EventArgs.Empty);
            }
        }
        public Boolean GameOn
        {
            get
            {
                return gameOn;
            }
            set
            {
                gameOn = value;
                if (!gameOn)
                {
                    animationOn = false;
                    reinforcementOn = false;
                    executeReinforcement = true;
                    animationOn = false;
                    gamePause = false;
                }
                OnGameOnChanged(EventArgs.Empty);
            }
        }
        public Boolean GamePause
        {
            get
            {
                return gamePause;
            }
            set
            {
                gamePause = value;
                OnGamePauseOnChanged(EventArgs.Empty);
            }

        }

        protected virtual void OnGameOnChanged(EventArgs e)
        {
            GameOnChanged?.Invoke(this, e);
        }

        protected virtual void OnGamePauseOnChanged(EventArgs e)
        {
            GamePauseOnChanged?.Invoke(this, e);
        }
        protected virtual void OnAnimationOnChanged(EventArgs e)
        {
            AnimationOnChanged?.Invoke(this, e);
        }
        protected virtual void OnReinforcementOnChanged(EventArgs e)
        {
            ReinforcementOnChanged?.Invoke(this, e);
        }
        protected virtual void OnExecuteReinforcementChanged(EventArgs e)
        {
            ExecuteReinforcementChanged?.Invoke(this, e);
        }
    }
}
