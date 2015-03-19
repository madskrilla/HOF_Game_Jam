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

        public List<Lane> theLanes = new List<Lane>();
        public Track theTrack;
        public TrackType type;
        public Direction startDir;
        public Direction endDir; // This will always be left or right when used for turns

        public Image tileGraphic;
        public List<PickUp> trackPickups;

        public Track_Piece(Track _parent, TrackType _type, Direction _startDir, Direction _endDir, int _tilePosX, int _tilePosY) : base()
        {
            theTrack = _parent;
            type = _type;
            startDir = _startDir;
            endDir = _endDir;
            X = _tilePosX * Globals.TileSize;
            Y = _tilePosY * Globals.TileSize;
        }

        public void BuildLanes()
        {
            switch (type)
            {
                case TrackType.straight4:
                    {
                        Lane lane0 = new Lane(this, type, startDir, 1);
                        Lane lane1 = new Lane(this, type, startDir, 2);
                        Lane lane2 = new Lane(this, type, startDir, 3);
                        Lane lane3 = new Lane(this, type, startDir, 4);
                        theLanes.Add(lane0);
                        theLanes.Add(lane1);
                        theLanes.Add(lane2);
                        theLanes.Add(lane3);
                        break;
                    }
                case TrackType.straight2:
                    {
                        Lane lane0 = new Lane(this, type, startDir, 1);
                        Lane lane1 = new Lane(this, type, startDir, 2);
                        theLanes.Add(lane0);
                        theLanes.Add(lane1);
                        break;
                    }
                case TrackType.sharpTurn4:
                    {
                        Lane lane0 = new Lane(this, type, startDir, endDir, 1);
                        Lane lane1 = new Lane(this, type, startDir, endDir, 2);
                        Lane lane2 = new Lane(this, type, startDir, endDir, 3);
                        Lane lane3 = new Lane(this, type, startDir, endDir, 4);
                        theLanes.Add(lane0);
                        theLanes.Add(lane1);
                        theLanes.Add(lane2);
                        theLanes.Add(lane3);
                        break;
                    }
                case TrackType.sharpTurn2:
                    {
                        Lane lane0 = new Lane(this, type, startDir, endDir, 1);
                        Lane lane1 = new Lane(this, type, startDir, endDir, 2);
                        theLanes.Add(lane0);
                        theLanes.Add(lane1);
                        break;
                    }
                case TrackType.wideTurn4:
                    {
                        Lane lane0 = new Lane(this, type, startDir, endDir, 1);
                        Lane lane1 = new Lane(this, type, startDir, endDir, 2);
                        Lane lane2 = new Lane(this, type, startDir, endDir, 3);
                        Lane lane3 = new Lane(this, type, startDir, endDir, 4);
                        theLanes.Add(lane0);
                        theLanes.Add(lane1);
                        theLanes.Add(lane2);
                        theLanes.Add(lane3);
                        break;
                    }
                case TrackType.wideTurn2:
                    {
                        Lane lane0 = new Lane(this, type, startDir, endDir, 1);
                        Lane lane1 = new Lane(this, type, startDir, endDir, 2);
                        theLanes.Add(lane0);
                        theLanes.Add(lane1);
                        break;
                    }
                case TrackType.startingLine:
                    {
                        Lane lane0 = new Lane(this, type, startDir, 1);
                        Lane lane1 = new Lane(this, type, startDir, 2);
                        Lane lane2 = new Lane(this, type, startDir, 3);
                        Lane lane3 = new Lane(this, type, startDir, 4);
                        theLanes.Add(lane0);
                        theLanes.Add(lane1);
                        theLanes.Add(lane2);
                        theLanes.Add(lane3);
                        break;
                    }
                case TrackType.itemSpawn2:
                    {
                        Lane lane0 = new Lane(this, type, startDir, 1);
                        Lane lane1 = new Lane(this, type, startDir, 2);
                        theLanes.Add(lane0);
                        theLanes.Add(lane1);
                        break;
                    }
                case TrackType.itemSpawn4:
                    {
                        Lane lane0 = new Lane(this, type, startDir, 1);
                        Lane lane1 = new Lane(this, type, startDir, 2);
                        Lane lane2 = new Lane(this, type, startDir, 3);
                        Lane lane3 = new Lane(this, type, startDir, 4);
                        theLanes.Add(lane0);
                        theLanes.Add(lane1);
                        theLanes.Add(lane2);
                        theLanes.Add(lane3);
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
