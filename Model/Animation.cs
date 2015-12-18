using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuiSpaceGame.Model
{
    public abstract class Animation
    {
        public DateTime StartingAnimationTime { get; set; }
        public TimeSpan AnimationDuration { get; set; }

    }
}
