using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuiSpaceGame.Model
{
    public class Asteroid : Animation, INotifyPropertyChanged
    {
        private double speed;
        private double lane;
        public event PropertyChangedEventHandler PropertyChanged;

        public double Lane
        {
            get { return lane; }
            set
            {
                lane = value;
                NotifyPropertyChanged("lane");
            }
        }
        public double Speed
        {
            get { return speed; }
            set
            {
                speed = value;
                AnimationDuration = TimeSpan.FromMilliseconds(((Constant.Square * 2) * 1000) / speed);
                NotifyPropertyChanged("speed");
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

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
