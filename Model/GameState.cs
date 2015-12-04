using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuiSpaceGame.Model
{
    class GameState
    {
        private int animationId { get; set; }
        private Boolean executeReinforcement { get; set; }
        private Boolean animationOn { get; set; }
        private Boolean GameOn { get; set; }
        private Boolean GamePause { get; set; }
    }
}
