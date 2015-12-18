using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuiSpaceGame.Model
{
    class Asteroid : Animation
    {
        public double Lane { get; set; }
        public double Speed { get; set; }
        public double Z0 { get; set; }

        public Asteroid(double lane, double speed)
        {
            Lane = lane;
            Speed = speed;
            Z0 = Constant.ZCarpet + Constant.ZLittleSpace + Constant.Square;
            AnimationDuration = TimeSpan.FromMilliseconds((Constant.Square * 2) / Speed);
            Console.WriteLine(AnimationDuration);
        }

    }
}
