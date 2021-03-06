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
                NotifyPropertyChanged("image");
            }
        }
        public Shape[] Shapes
        {
            get { return shapes; }
            set
            {
                shapes = value;
            }
        }

        public LogicBlock()
        {
            Shapes = new Shape[4];
            Shapes[Square.TopLeft] = new Shape(Colour.Red, FigureShape.Square, this, SquareCoordinate.Left, SquareCoordinate.Top);
            Shapes[Square.TopRight] = new Shape(Colour.Red, FigureShape.Square, this, SquareCoordinate.Right, SquareCoordinate.Top);
            Shapes[Square.BottomLeft] = new Shape(Colour.Red, FigureShape.Square, this, SquareCoordinate.Left, SquareCoordinate.Bottom);
            Shapes[Square.BottomRight] = new Shape(Colour.Red, FigureShape.Square, this, SquareCoordinate.Right, SquareCoordinate.Bottom);
            Target = Square.TopLeft;
            AnimationDuration = TimeSpan.FromMilliseconds(Constant.TLogicBlockAnimation);
            UpdateImage();
        }

        private void UpdateImage()
        {
            string NewImage = "LogicBlocks/LogicBlock-" + Shapes[target].Color + "-" + Shapes[target].Figure + ".png";
            Image = NewImage;
        }

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            string ShapesString = "";
            foreach (Shape shape in this.shapes)
            {
                string ZString = "";
                if (shape.Z == SquareCoordinate.Top)
                    ZString = "1";
                else if (shape.Z == SquareCoordinate.Bottom)
                    ZString = "2";

                string XString = "";
                if (shape.X == SquareCoordinate.Left)
                    XString = "1";
                else if (shape.X == SquareCoordinate.Right)
                    XString = "2";

                ShapesString += ZString + XString + "?" + shape.Figure.ToString() + ZString + XString + "," + shape.ColorToRGB() + ";";
            }
            ShapesString = ShapesString.Remove(ShapesString.Length - 1); // removes the last ","
            return ShapesString;
        }
    }
}
