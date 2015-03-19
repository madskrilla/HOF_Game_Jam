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
            thePieces.Add(new Track_Piece(this, TrackType.startingLine4, Direction.Left, Direction.Left, 4, 3));
            thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Left, Direction.Left, 3, 3));
            thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Left, Direction.Left, 2, 3));
            thePieces.Add(new Track_Piece(this, TrackType.wideTurn4, Direction.Left, Direction.Right, 0, 2));
            thePieces.Add(new Track_Piece(this, TrackType.wideTurn4, Direction.Up, Direction.Right, 0, 0));
            thePieces.Add(new Track_Piece(this, TrackType.wideTurn4, Direction.Right, Direction.Right, 2, 0));
            thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Down, Direction.Left, 3, 2));
            thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Right, Direction.Right, 4, 2));
            thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Right, Direction.Right, 5, 2));
            thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Right, Direction.Right, 6, 2));
            thePieces.Add(new Track_Piece(this, TrackType.straight4, Direction.Right, Direction.Right, 7, 2));
            thePieces.Add(new Track_Piece(this, TrackType.wideTurn4, Direction.Right, Direction.Right, 8, 2));
            thePieces.Add(new Track_Piece(this, TrackType.wideTurn4, Direction.Down, Direction.Right, 8, 4));
            thePieces.Add(new Track_Piece(this, TrackType.wideTurn4, Direction.Left, Direction.Right, 6, 4));
            thePieces.Add(new Track_Piece(this, TrackType.sharpTurn4, Direction.Up, Direction.Left, 6, 3));
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
