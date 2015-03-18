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
        int currPiece = 0;
        int currNodeIndex = 0;
        Node currNode;
        int frame = 0;

        public RaceState currentState;

        public Race() : base()
        {
            theTrack = new Track();
            theTrack.BuildTrack();
            currNode = theTrack.thePieces[currPiece].theLanes[0].theNodes[currNodeIndex];
        }

        public override void Render()
        {
            frame++;
            for (int i = 0; i < theTrack.thePieces.Count(); i++)
			{
                for (int j = 0; j < theTrack.thePieces[i].theLanes.Count(); j++)
			    {
                    for (int k = 0; k < theTrack.thePieces[i].theLanes[j].theNodes.Count(); k++)
			        {
                        Image point = Image.CreateCircle(3);
                        point.Render(theTrack.thePieces[i].theLanes[j].theNodes[k].localSpace.X, theTrack.thePieces[i].theLanes[j].theNodes[k].localSpace.Y);
			        }
			    }
			}
            if (frame % 30 == 0)
                currNodeIndex++;
            if (currNodeIndex == theTrack.thePieces[currPiece].theLanes[0].theNodes.Count())
            {
                currNodeIndex = 0;
                currPiece++;
                if (currPiece == theTrack.thePieces.Count())
                {
                    currPiece = 0;
                }
            }

            Image racer = Image.CreateCircle(4, Color.Red);
            racer.Render(theTrack.thePieces[currPiece].theLanes[1].theNodes[currNodeIndex].localSpace.X, theTrack.thePieces[currPiece].theLanes[1].theNodes[currNodeIndex].localSpace.Y);
            base.Render();
        }
    }
}
