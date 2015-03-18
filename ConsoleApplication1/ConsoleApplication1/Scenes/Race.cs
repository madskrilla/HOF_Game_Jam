using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using ConsoleApplication1.Track_Elements;
using ConsoleApplication1.Vehicles;

namespace ConsoleApplication1.Scenes
{
    class Race : Scene
    {
        private Track theTrack;

        internal Track TheTrack
        {
            get { return theTrack; }
            set { theTrack = value; }
        }
        public int totalLaps;
        public List<Slot_Car> theCars;

        public RaceState currentState;

        public Race() : base()
        {
        }
    }
}
