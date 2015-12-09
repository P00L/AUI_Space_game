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

        public int AnimationId { get; set; }
        public Boolean ReinforcementOn { get; set; }
        public Boolean ExecuteReinforcement { get; set; } // TODO default = true;
        public Boolean AnimationOn { get; set; }
        public Boolean GameOn { get; set; } //TODO quando setto a false -> setto tutto a false
        public Boolean GamePause { get; set; }
    }
}
