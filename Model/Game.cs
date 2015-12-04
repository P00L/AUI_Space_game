using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuiSpaceGame.Model
{
    class Game
    {
        public Animation[] AnimationsSequence { get; set; }
        public Child Child { get; set; }
        public DateTime GameDuration { get; set; }
    }
}
