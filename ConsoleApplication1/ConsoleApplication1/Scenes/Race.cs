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
        public int totalLaps;
        public List<Slot_Car> theCars = new List<Slot_Car>();

        public RaceState currentState;

        public Race() : base()
        {
            Slot_Car player = new Player(this, Globals.PlayerOne);
            theCars.Add(player);
            Add(player);
        }
    }
}
