using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using ConsoleApplication1.Track_Elements;
using ConsoleApplication1.Items;

namespace ConsoleApplication1.Track_Elements
{
    
    class Track_Piece : Entity 
    {
        //private Track_Piece nextTrackPiece;
        //private Track_Piece prevTrackPiece;

        public List<Lane> theLanes;
        public TrackType type;

        public Image tileGraphic;
        public List<PickUp> trackPickups;

        public Track_Piece(TrackType _type) : base()
        {
            type = _type;
        }

        public void BuildLanes()
        {

        }
    }
}
