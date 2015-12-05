using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuiSpaceGame.Model
{
    class Game
    {
        public List<Animation> AnimationsSequence { get; set; }
        public Child Child { get; set; }
        public TimeSpan GameDuration { get; set; }
    }
}
