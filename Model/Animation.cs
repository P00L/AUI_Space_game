using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuiSpaceGame.Model
{
    public abstract class Animation
    {
        public DateTime StartingAnimationTime { get; set; }
        public TimeSpan AnimationDuration { get; set; }
        private string image;

        public string Image
        {
            get { return image; }
            set
            {
                image = Constant.ImageUriPrefix + value;
            }
        }
    }
}
