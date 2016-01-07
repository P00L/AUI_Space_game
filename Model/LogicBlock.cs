﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuiSpaceGame.Model;

namespace AuiSpaceGame.Model
{
    public class LogicBlock : Animation, INotifyPropertyChanged
    {
        private int target;
        private Shape[] shapes;
        public event PropertyChangedEventHandler PropertyChanged;

        public int Target
        {
            get { return target; }
            set
            {
                target = value;
                UpdateImage();
                NotifyPropertyChanged("target");
            }
        }
        public Shape[] Shapes
        {
            get { return shapes; }
            set
            {
                shapes = value;
                UpdateImage();
                NotifyPropertyChanged("shapes");
            }
        }

        public LogicBlock()
        {
            Shapes = new Shape[4];
            Shapes[Square.TopLeft] = new Shape(Color.Red, Figure.Square, this);
            Shapes[Square.TopRight] = new Shape(Color.Red, Figure.Square, this);
            Shapes[Square.BottomLeft] = new Shape(Color.Red, Figure.Square, this);
            Shapes[Square.BottomRight] = new Shape(Color.Red, Figure.Square, this);
            Target = Square.TopLeft;
            AnimationDuration = TimeSpan.FromMilliseconds(Constant.TLogicBlockAnimation);
            UpdateImage();
        }

        private void UpdateImage() //TODO
        {
            string NewImage = "LogicBlocks/LogicBlock-";
            /*
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
                */
            NewImage += ".png";
            Image = NewImage;
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
