using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuiSpaceGame.Model;
using System.Windows.Media;

namespace AuiSpaceGame.Model
{
    public class Shape : INotifyPropertyChanged
    {
        private string color;
        private string figure;
        private LogicBlock logicBlock;
        public event PropertyChangedEventHandler PropertyChanged;

        public string Color
        {
            get { return color; }
            set
            {
                color = value;
                UpdateImage();
                NotifyPropertyChanged("color");
            }
        }
        public string Figure
        {
            get { return figure; }
            set
            {
                figure = value;
                UpdateImage();
                NotifyPropertyChanged("figure");
            }
        }

        public Shape(string color, string figure, LogicBlock logicBlock)
        {
            Color = color;
            Figure = figure;
            this.logicBlock = logicBlock;
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
            logicBlock.Image = NewImage;
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }



    }
}
