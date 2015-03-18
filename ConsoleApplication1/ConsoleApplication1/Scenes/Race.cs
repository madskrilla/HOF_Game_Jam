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
        public List<Node> testNode;
        public RaceState currentState;

        public Race() : base()
        {
            Node test = new Node();
            test.localSpace.X = 0;
            test.localSpace.Y = 0;
            Node test2 = new Node();
            test2.localSpace.X = 50;
            test2.localSpace.Y = 50;
            test.nextNode = test2;
            testNode.Add(test);
            Node test3 = new Node();
            test3.localSpace.X = 50;
            test3.localSpace.Y = 0;
            test2.nextNode = test3;
            testNode.Add(test2);
            Node test4 = new Node();
            test4.localSpace.X = 0;
            test4.localSpace.Y = 50;
            test3.nextNode = test4;
            testNode.Add(test3);
            test4.nextNode = test;
            testNode.Add(test4);

            Slot_Car tester = new Slot_Car(this);
            tester.Target(test);
            Slot_Car player = new Player(this, Globals.PlayerOne);
            theCars.Add(player);
            Add(player);
        }
    }
}
