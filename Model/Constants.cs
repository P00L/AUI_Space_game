using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;

namespace AuiSpaceGame.Model
{
    public static class Constant
    {
        public const double ZCarpet = 2;

        /// <summary>
        /// Delay of the carpet (in milliseconds)
        /// </summary>
        public const double TPharos = 0;
        public const double ZLittleSpace = 0.07;
        public const double Square = 0.6;
        /// <summary>
        /// Precision to measure the position
        /// </summary>
        public const double Delta = 0.25;
        public const double TReinforcement = 22;
        public const double TLogicBlockAnimation = 10000;

        public const string ImageUriPrefix = "/AuiSpaceGame;component/Images/";
    }

    public static class Lane
    {
        public const double Left = -0.46;
        public const double Middle = 0;
        public const double Right = 0.46;
    }

    public static class Speed
    {
        public const double Low = 2;
        public const double High = 10;
    }

    public static class Square
    {
        public const int TopLeft = 0;
        public const int TopRight = 1;
        public const int BottomLeft = 2;
        public const int BottomRight = 3;
    }

    public static class SquareCoordinate
    {
        public const double Top = 0; //TODO
        public const double Bottom = 3;
        public const double Left = 2;
        public const double Right = 1;
    }

    public static class Figure
    {
        public const string Triangle = "Triangle";
        public const string Circle = "Circle";
        public const string Square = "Square";
    }

    public static class Color
    {
        public const string Red = "Red";
        public const string Blue = "Blue";
        public const string Yellow = "Yellow";
    }

}
