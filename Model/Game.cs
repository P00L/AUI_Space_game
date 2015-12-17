using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuiSpaceGame.Model
{
    class Game
    {
        public String Name { get; set; }
        public ObservableCollection<Animation> AnimationsSequence { get; set; }
        public Child Child { get; set; }
        public TimeSpan GameDuration { get; set; }

        public Game()
        {
            AnimationsSequence = new ObservableCollection<Animation>();
        }
    }
}
