using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using ConsoleApplication1.Track_Elements;
using ConsoleApplication1.Scenes;

namespace ConsoleApplication1.Track_Elements
{
    class Track : Entity
    {
        public List<Track_Piece> thePieces = new List<Track_Piece>();
        public Race theRace;
        public Track(Race _race, int _track) : base()
        {
            theRace = _race;
            if (_track == 0)
            {
                thePieces.Add(new Track_Piece(this, TrackType.startingLine4, Direction.Left, Direction.Left, 4, 3));
                thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Left, Direction.Left, 3, 3));
                thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Left, Direction.Left, 2, 3));
                thePieces.Add(new Track_Piece(this, TrackType.wideTurn4, Direction.Left, Direction.Right, 0, 2));
                thePieces.Add(new Track_Piece(this, TrackType.wideTurn4, Direction.Up, Direction.Right, 0, 0));
                thePieces.Add(new Track_Piece(this, TrackType.wideTurn4, Direction.Right, Direction.Right, 2, 0));
                thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Down, Direction.Left, 3, 2));
                thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Right, Direction.Right, 4, 2));
                thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Right, Direction.Right, 5, 2));
                thePieces.Add(new Track_Piece(this, TrackType.itemSpawn4, Direction.Right, Direction.Right, 6, 2));
                thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Right, Direction.Right, 7, 2));
                thePieces.Add(new Track_Piece(this, TrackType.wideTurn4, Direction.Right, Direction.Right, 8, 2));
                thePieces.Add(new Track_Piece(this, TrackType.wideTurn4, Direction.Down, Direction.Right, 8, 4));
                thePieces.Add(new Track_Piece(this, TrackType.wideTurn4, Direction.Left, Direction.Right, 6, 4));
                thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Up, Direction.Left, 6, 3));
            }
            else if (_track == 1)
            {
                thePieces.Add(new Track_Piece(this, TrackType.startingLine4, Direction.Up, Direction.Up, 4, 2));
                thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Up, Direction.Left, 4, 1));
                thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Left, Direction.Right, 3, 1));
                thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Up, Direction.Right, 3, 0));
                thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Right, Direction.Right, 4, 0));
                thePieces.Add(new Track_Piece(this, TrackType.wideTurn4, Direction.Right, Direction.Right, 5, 0));
                thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Down, Direction.Right, 6, 2));
                thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Left, Direction.Left, 5, 2));
                thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Down, Direction.Left, 5, 3));
                thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Right, Direction.Right, 6, 3));
                thePieces.Add(new Track_Piece(this, TrackType.wideTurn4, Direction.Down, Direction.Right, 5, 4));
                thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Left, Direction.Left, 4, 5));
                thePieces.Add(new Track_Piece(this, TrackType.itemSpawn4, Direction.Left, Direction.Left, 3, 5));
                thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Left, Direction.Left, 2, 5));
                thePieces.Add(new Track_Piece(this, TrackType.wideTurn4, Direction.Left, Direction.Right, 0, 4));
                thePieces.Add(new Track_Piece(this, TrackType.wideTurn4, Direction.Up, Direction.Right, 0, 2));
                thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Right, Direction.Right, 2, 2));
                thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Down, Direction.Down, 2, 3));
                thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Down, Direction.Left, 2, 4));
                thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Right, Direction.Right, 3, 4));
                thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Right, Direction.Left, 4, 4));
            }
            else if (_track == 2)
            {
                thePieces.Add(new Track_Piece(this, TrackType.startingLine4, Direction.Left, Direction.Left, 4, 5));
                thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Left, Direction.Left, 3, 5));
                thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Left, Direction.Left, 2, 5));
                thePieces.Add(new Track_Piece(this, TrackType.itemSpawn4, Direction.Left, Direction.Left, 1, 5));
                thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Left, Direction.Right, 0, 5));
                thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Up, Direction.Up, 0, 4));
                thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Up, Direction.Up, 0, 3));
                thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Up, Direction.Up, 0, 2));
                thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Up, Direction.Up, 0, 1));
                thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Up, Direction.Right, 0, 0));
                thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Right, Direction.Right, 1, 0));
                thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Down, Direction.Down, 1, 1));
                thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Down, Direction.Down, 1, 2));
                thePieces.Add(new Track_Piece(this, TrackType.wideTurn4, Direction.Down, Direction.Left, 1, 3));
                thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Right, Direction.Left, 3, 4));
                thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Up, Direction.Up, 3, 3));
                thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Up, Direction.Left, 3, 2));
                thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Left, Direction.Right, 2, 2));
                thePieces.Add(new Track_Piece(this, TrackType.wideTurn4, Direction.Up, Direction.Right, 2, 0));
                thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Right, Direction.Right, 4, 0));
                thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Right, Direction.Right, 5, 0));
                thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Down, Direction.Right, 5, 1));
                thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Left, Direction.Left, 4, 1));
                thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Down, Direction.Down, 4, 2));
                thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Down, Direction.Down, 4, 3));
                thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Down, Direction.Left, 4, 4));
                thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Right, Direction.Left, 5, 4));
                thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Up, Direction.Up, 5, 3));
                thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Up, Direction.Right, 5, 2));
                thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Right, Direction.Left, 6, 2));
                thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Up, Direction.Up, 6, 1));
                thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Up, Direction.Right, 6, 0));
                thePieces.Add(new Track_Piece(this, TrackType.wideTurn4, Direction.Right, Direction.Right, 7, 0));
                thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Down, Direction.Down, 8, 2));
                thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Down, Direction.Right, 8, 3));
                thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Left, Direction.Left, 7, 3));
                thePieces.Add(new Track_Piece(this, TrackType.wideTurn4, Direction.Down, Direction.Right, 6, 4));
            }
        }

        public void BuildTrack()
        {
            for (int i = 0; i < thePieces.Count(); i++)
            {
                thePieces[i].BuildLanes();
            }
        }
    }
}
