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
                Image = "Asteroid," + speed.ToString() +","+ lane.ToString() + ".png";
                NotifyPropertyChanged("image");
            }
        }
        public double Speed
        {
            get { return speed; }
            set
            {
                speed = value;
                AnimationDuration = TimeSpan.FromMilliseconds(((Constant.Square * 2) * 1000) / speed);
                Image = "Asteroid," + speed.ToString() + "," + lane.ToString() + ".png";
                NotifyPropertyChanged("image");
            }
        }
        public double Z0 { get; set; }

        public Asteroid(double lane, double speed)
        {
            Lane = lane;
            Speed = speed;
            Z0 = Constant.ZCarpet + Constant.ZLittleSpace + Constant.Square;
            AnimationDuration = TimeSpan.FromMilliseconds(((Constant.Square * 2) * 1000) / Speed);
            Image = "Asteroid," + speed.ToString() + "," + lane.ToString() + ".png";

        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
