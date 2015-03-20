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
        public List<int> finishOrder = new List<int>();
        public Text first = new Text("", "Assets/RACER___.TTF");
        public Text second = new Text("", "Assets/RACER___.TTF");
        public Text third = new Text("", "Assets/RACER___.TTF");
        public Text fourth = new Text("", "Assets/RACER___.TTF");
        public Sound raceStart = new Sound("Audio/startRace.wav");

        //public Race(int _laps, int numPlayers, int _track)
        public Race(int _laps, int numPlayers, Image player1Car, Image player2Car, int track_selection )
            : base()
        {
            Game.Instance.Color = new Color(0x018801);
            theTrack = new Track(this,track_selection);
            theTrack.BuildTrack();
            for (int i = 0; i < theTrack.thePieces.Count(); i++)
            {
                Add(theTrack.thePieces[i]);
            }
            for (int play = 0; play < numPlayers; play++)
            {
                if (play == 0)
                {
                    Slot_Car player = new Player(this, 0, Globals.PlayerOne, player1Car);
                    HUD hud = new HUD(player, this);
                    theCars.Add(player);
                    Add(player);
                    Add(hud);
                    //player.SetGraphic(player1Car);
                }
                else
                {
                    Slot_Car player2 = new Player(this, 1, Globals.PlayerTwo, player2Car);
                    HUD hud1 = new HUD(player2, this);
                    theCars.Add(player2);
                    Add(player2);
                    Add(hud1);
                    //player2.SetGraphic(player2Car);
                }
            }
            if (numPlayers == 1)
            {
                Slot_Car adam = new AIDriver(this, 1);
                HUD hud1 = new HUD(adam, this);
                theCars.Add(adam);
                Add(adam);
                Add(hud1);
                Slot_Car steve = new AIDriver(this, 2);
                theCars.Add(steve);
                Add(steve);
                HUD hud2 = new HUD(steve, this);
                Add(hud2);
                Slot_Car tom = new AIDriver(this, 3);
                HUD hud3 = new HUD(tom, this);
                theCars.Add(tom);
                Add(tom);
                Add(hud3);
            }
            else
            {
                Slot_Car steve = new AIDriver(this, 2);
                theCars.Add(steve);
                Add(steve);
                HUD hud2 = new HUD(steve, this);
                Add(hud2);
                Slot_Car tom = new AIDriver(this, 3);
                HUD hud3 = new HUD(tom, this);
                theCars.Add(tom);
                Add(tom);
                Add(hud3);
            }
            
            currNode = theTrack.thePieces[currPiece].theLanes[0].theNodes[currNodeIndex];
            currentState = RaceState.RaceBegin;
            countText.FontSize = 75;
            totalLaps = _laps;
            raceStart.Play();

        }
        public override void Update()
        {
            if (!Globals.loopPlaying
                && DateTime.Now.Ticks - Globals.songStartTime >= (204160000))
            {
                Globals.digestiveLoop.Play();
                Globals.loopPlaying = true;
            }
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
            {
                currentState = RaceState.RaceEnd;
                first.String = "1. Player " + finishOrder[0];
                first.FontSize = 50;
                first.Color = theCars[finishOrder[0]-1].playerCol;
                second.String = "2. Player " + finishOrder[1];
                second.FontSize = 50;
                second.Color = theCars[finishOrder[1]-1].playerCol;
                third.String = "3. Player " + finishOrder[2];
                third.FontSize = 50;
                third.Color = theCars[finishOrder[2]-1].playerCol;
                fourth.String = "Press Enter To Return to the Main Menu!";
            }
            if (currentState == RaceState.RaceEnd && Globals.PlayerOne.Controller.Button(Controls.Enter).Pressed)
            {
                for (int i = 0; i < theCars.Count(); i++)
                {
                    theCars[i].carRev.Stop();
                    theCars[i].carIdle.Stop();
                }
                Game.RemoveScene();
                Game.AddScene(new Menu());
                
            }
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
            else if (currentState == RaceState.Racing)
            {
                for (int player = 0; player < 4; player++)
                {
                    if (theCars[player].finished)
                    {
                        Globals.slotCarText.String = "Player " + theCars[player].playerNum.ToString() + "has finished!.";
                        Globals.slotCarText.FontSize = 50;
                        Globals.slotCarText.Color = theCars[player].playerCol;
                        Globals.slotCarText.Render((HalfWidth - Globals.slotCarText.Width) - 100, HalfHeight +  - Globals.slotCarText.Height + (50 * player) - 100);
                    }
                }
            }
            else if (currentState == RaceState.RaceEnd)
            {
                first.Render(HalfWidth - first.Width, HalfHeight - first.Height);
                second.Render(HalfWidth - first.Width, HalfHeight - first.Height + 50);
                third.Render(HalfWidth - first.Width, HalfHeight - first.Height + 100);
                fourth.Render(HalfWidth - first.Width, HalfHeight + 150);
            }
            base.Render();
        }
    }
}
