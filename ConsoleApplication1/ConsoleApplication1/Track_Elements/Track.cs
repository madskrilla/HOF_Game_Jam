using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using ConsoleApplication1.Track_Elements;

namespace ConsoleApplication1.Track_Elements
{
    class Track : Entity
    {
        public List<Track_Piece> thePieces = new List<Track_Piece>();
        public Track()
            : base()
        {
            // test track
            thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Up, Direction.Up, 0, 2));
            thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Up, Direction.Right, 0, 1));
            thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Right, Direction.Left, 1, 1));
            thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Up, Direction.Right, 1, 0));
            thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Right, Direction.Right, 2, 0));
            thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Down, Direction.Down, 2, 1));
            thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Down, Direction.Down, 2, 2));
            thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Down, Direction.Down, 2, 3));
            thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Down, Direction.Right, 2, 4));
            thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Left, Direction.Left, 1, 4));
            thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Left, Direction.Right, 0, 4));
            thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Up, Direction.Up, 0, 3));
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
