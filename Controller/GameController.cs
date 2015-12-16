using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuiSpaceGame.Model;
namespace AuiSpaceGame.Controller
{

    class GameController
    {

        GameState GameState;

        public GameController(GameState gameState){
            GameState = gameState;

            GameState.GameOnChanged += new EventHandler(GameOnChanged);
            GameState.GamePauseOnChanged += new EventHandler(GamePauseOnChanged);
            GameState.AnimationOnChanged += new EventHandler(AnimationOnChanged);
            GameState.ReinforcementOnChanged += new EventHandler(ReinforcementOnChanged);
            GameState.ExecuteReinforcementChanged += new EventHandler(ExecuteReinforcementChanged);
        }


        public void GameOnChanged(object sender, EventArgs e)
        {

        }

        public void GamePauseOnChanged(object sender, EventArgs e)
        {
        }

        public void AnimationOnChanged(object sender, EventArgs e)
        {
        }

        public void ReinforcementOnChanged(object sender, EventArgs e)
        {
        }

        public void ExecuteReinforcementChanged(object sender, EventArgs e)
        {
        }

        public void NextStepGame() {
        }

    }
}
