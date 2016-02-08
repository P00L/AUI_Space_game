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
        public const string URLServer = "http://localhost:5050";
        public const double ZCarpet = 0.9;
        /// <summary>
        /// Delay of the carpet (in milliseconds)
        /// </summary>
        public const double TPharos = 0;
        public const double TIntroVideo = 30000;
        public const double ZLittleSpace = 0.2;
        public const double Square = 0.62;
        /// <summary>
        /// Precision to measure the position
        /// </summary>
        public const double Delta = 0.2;
        public const double DeltaLogicBlock = 0.30;
        public const int NumberOfCarpetSquares = 4;
        public const double TReinforcement = 16000;
        public const double TLogicBlockAnimation = 30000;

        public const string ImageUriPrefix = "/AuiSpaceGame;component/Images/";
    }

    public static class Lane
    {
        public const double Left = -0.3;
        public const double Middle = 0;
        public const double Right = 0.3;
    }

    public static class Speed
    {
        public const double Low = 0.27;
        public const double High = 0.47;
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
        public const double Top = Constant.ZCarpet + Constant.Square/2;
        public const double Bottom = Constant.ZCarpet + 3 * Constant.Square + Constant.Square/2;
        public const double Left = (-3 / 2) * Constant.Square;
        public const double Right = (3 / 2) * Constant.Square;
    }

    public static class FigureShape
    {
        public const string Triangle = "1";
        public const string Circle = "2";
        public const string Square = "3";
    }

    public static class Colour
    {
        public const string Red = "Red";
        public const string Blue = "Blue";
        public const string Yellow = "Yellow";
    }

}
