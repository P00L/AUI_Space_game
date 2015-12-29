using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

}
