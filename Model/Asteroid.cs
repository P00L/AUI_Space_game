using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuiSpaceGame.Model;

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
                UpdateImage();
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
                UpdateImage();
                NotifyPropertyChanged("image");
            }
        }
        public double Z0 { get; set; }

        public Asteroid(double lane, double speed)
        {
            Lane = lane;
            Speed = speed;
            Z0 = Constant.ZLittleSpace + Constant.Square - Constant.Delta;
            AnimationDuration = TimeSpan.FromMilliseconds(((Constant.Square * 2) * 1000) / Speed);
            UpdateImage();

        }

        private void UpdateImage()
        {
            string NewImage = "Asteroids/Asteroid-";

            if (speed == AuiSpaceGame.Model.Speed.Low)
                NewImage += "low-";
            else if (speed == AuiSpaceGame.Model.Speed.High)
                NewImage += "high-";

            if (lane == AuiSpaceGame.Model.Lane.Left)
                NewImage += "left";
            else if (lane == AuiSpaceGame.Model.Lane.Middle)
                NewImage += "middle";
            else if (lane == AuiSpaceGame.Model.Lane.Right)
                NewImage += "right";

            NewImage += ".png";
            Image = NewImage;
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            string LaneString = "";
            if (this.lane == Model.Lane.Left)
                LaneString = "Left";
            else if (this.lane == Model.Lane.Middle)
                LaneString = "Middle";
            else if (this.lane == Model.Lane.Right)
                LaneString = "Right";

            string SpeedString = "";
            if (this.speed == Model.Speed.High)
                SpeedString = "100";
            else if (this.speed == Model.Speed.Low)
                SpeedString = "50";

            return "Asteroid-" + LaneString + "-" + SpeedString;
        }


    }
}
