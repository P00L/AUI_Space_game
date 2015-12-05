using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuiSpaceGame.Model
{
    class GameState
    {
        private int AnimationId { get; set; }
        private Boolean ExecuteReinforcement { get; set; }
        private Boolean AnimationOn { get; set; }
        private Boolean GameOn { get; set; }
        private Boolean GamePause { get; set; }
    }
}
