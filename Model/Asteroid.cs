using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuiSpaceGame.Model
{
    class Asteroid : Animation
    {
        public int lane { get; set; }
        public float speed { get; set; }
        public float z0 { get; set; }

    }
}
