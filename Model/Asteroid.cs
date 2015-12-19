using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuiSpaceGame.Model
{
    public class Asteroid : Animation
    {
        private double speed;

        public double Lane { get; set; }
        public double Speed
        {
            get { return speed; }
            set
            {
                speed = value;
                AnimationDuration = TimeSpan.FromMilliseconds(((Constant.Square * 2) * 1000) / speed);
            }
        }
        public double Z0 { get; set; }

        public Asteroid(double lane, double speed)
        {
            Lane = lane;
            Speed = speed;
            Z0 = Constant.ZCarpet + Constant.ZLittleSpace + Constant.Square;
            AnimationDuration = TimeSpan.FromMilliseconds(((Constant.Square * 2) * 1000) / Speed);
        }

    }
}
