using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using ConsoleApplication1.Track_Elements;
using ConsoleApplication1.Vehicles;
using ConsoleApplication1.Extra;

namespace ConsoleApplication1.Scenes
{
    class Race : Scene
    {
        public Track theTrack;
        public int totalLaps;
        public List<Slot_Car> theCars = new List<Slot_Car>();
        int currPiece = 0;
        int currNodeIndex = 0;
        Node currNode;
        int frame = 0;
        public Text countText = new Text("", "Assets/RACER___.TTF");
        public RaceState currentState;
        public int countDown = 3;
        public int carsFin = 0;
        public Race(int _laps)
            : base()
        {

            theTrack = new Track();
            theTrack.BuildTrack();
            for (int i = 0; i < theTrack.thePieces.Count(); i++)
            {
                Add(theTrack.thePieces[i]);
            }
            Slot_Car player = new Player(this, 0, Globals.PlayerOne);
            HUD hud = new HUD(player, this);
            theCars.Add(player);

            Slot_Car player2 = new Player(this, 3, Globals.PlayerTwo);
            HUD hud3 = new HUD(player2, this);
            theCars.Add(player2);

            Slot_Car adam = new AIDriver(this, 1);
            HUD hud1 = new HUD(adam, this);
            theCars.Add(adam);

            Slot_Car steve = new AIDriver(this, 2);
            theCars.Add(steve);
            HUD hud2 = new HUD(steve, this);

            Add(hud);
            Add(hud1);
            Add(hud2);
            Add(player);
            Add(player2);
            Add(adam);
            Add(steve);
            currNode = theTrack.thePieces[currPiece].theLanes[0].theNodes[currNodeIndex];
            currentState = RaceState.RaceBegin;
            countText.FontSize = 75;
            totalLaps = _laps;

        }
        public override void Update()
        {
            if (countDown < 0)
            {
                currentState = RaceState.Racing;
            }
            if (currentState == RaceState.RaceBegin)
            {
                countText.String = countDown.ToString();
                switch (countDown)
                {
                    case 3:
                        countText.Color = Color.Red;
                        break;
                    case 2:
                        countText.Color = Color.Yellow;
                        break;
                    case 1:
                        countText.Color = Color.Yellow;
                        break;
                    case 0:
                        countText.String = "GoGoGo!!";
                        countText.Color = Color.Green;
                        countDown--;
                        break;
                    default:
                        break;
                }
                frame++;
                if (frame % 60 == 0)
                    countDown--;
            }
            if (carsFin == theCars.Count)
                currentState = RaceState.RaceEnd;
            base.Update();
        }
        public override void Render()
        {
            /*  frame++;
            for (int i = 0; i < theTrack.thePieces.Count(); i++)
            {
                for (int j = 0; j < theTrack.thePieces[i].theLanes.Count(); j++)
                {
                    for (int k = 0; k < theTrack.thePieces[i].theLanes[j].theNodes.Count(); k++)
                    {
                        Image point = Image.CreateCircle(3);
                        //point.Render(theTrack.thePieces[i].theLanes[j].theNodes[k].localSpace.X, theTrack.thePieces[i].theLanes[j].theNodes[k].localSpace.Y);
                        theTrack.thePieces[i].Render();
                    }
                }
            }
            if (frame % 10 == 0)
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
             racer.Render(theTrack.thePieces[currPiece].theLanes[1].theNodes[currNodeIndex].localSpace.X, theTrack.thePieces[currPiece].theLanes[1].theNodes[currNodeIndex].localSpace.Y);*/


            if (currentState == RaceState.RaceBegin)
            {
                countText.Render(HalfWidth - countText.Width/2, HalfHeight);
            }

            base.Render();
        }
    }
}
