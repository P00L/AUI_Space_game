using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuiSpaceGame.Model
{
    abstract class Animation
    {
        public DateTime startingAnimationTime { get; set; }
        public DateTime animationDuration { get; set; }

    }
}
