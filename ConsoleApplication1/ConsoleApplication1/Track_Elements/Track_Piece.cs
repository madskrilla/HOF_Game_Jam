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
        public Rectangle clippingRegion;
        public List<PickUp> trackPickups;

        public Track_Piece(Track _parent, TrackType _type, Direction _startDir, Direction _endDir, int _tilePosX, int _tilePosY) : base()
        {
            theTrack = _parent;
            type = _type;
            startDir = _startDir;
            endDir = _endDir;
            X = _tilePosX * Globals.TileSize;
            Y = _tilePosY * Globals.TileSize;
            if (type == TrackType.wideTurn2 ||
                type == TrackType.wideTurn4)
            {
                tileGraphic = new ImageSet("Assets/Images/TrackTiles.png", Globals.TileSize * 2, Globals.TileSize * 2);
            }
            else if (type == TrackType.startingLine2
                || type == TrackType.startingLine4)
            {
                if (startDir == Direction.Left
                    || startDir == Direction.Right)
                    tileGraphic = new ImageSet("Assets/Images/TrackTiles.png", Globals.TileSize * 2, Globals.TileSize);
                else
                    tileGraphic = new ImageSet("Assets/Images/TrackTiles.png", Globals.TileSize, Globals.TileSize * 2);
            }
            else
            {
                tileGraphic = new ImageSet("Assets/Images/TrackTiles.png", Globals.TileSize, Globals.TileSize);
            }
            Graphic = tileGraphic;
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
                        if (startDir == Direction.Left
                            || startDir == Direction.Right)
                        {
                            clippingRegion.X = Globals.TileSize * 1;
                            clippingRegion.Y = Globals.TileSize * 0;
                        }
                        else
                        {
                            clippingRegion.X = Globals.TileSize * 0;
                            clippingRegion.Y = Globals.TileSize * 0;
                        }
                        clippingRegion.Width = Globals.TileSize;
                        clippingRegion.Height = Globals.TileSize;
                        break;
                    }
                case TrackType.straight2:
                    {
                        Lane lane0 = new Lane(this, type, startDir, 1);
                        Lane lane1 = new Lane(this, type, startDir, 2);
                        theLanes.Add(lane0);
                        theLanes.Add(lane1);
                        if (startDir == Direction.Left
                            || startDir == Direction.Right)
                        {
                            clippingRegion.X = Globals.TileSize * 2;
                            clippingRegion.Y = Globals.TileSize * 0;
                        }
                        else
                        {
                            clippingRegion.X = Globals.TileSize * 3;
                            clippingRegion.Y = Globals.TileSize * 0;
                        }
                        clippingRegion.Width = Globals.TileSize;
                        clippingRegion.Height = Globals.TileSize;
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
                        if ((startDir == Direction.Left && endDir == Direction.Right)
                            || (startDir == Direction.Down && endDir == Direction.Left))
                        {
                            clippingRegion.X = Globals.TileSize * 0;
                            clippingRegion.Y = Globals.TileSize * 3;
                        }
                        else if ((startDir == Direction.Up && endDir == Direction.Right)
                            || (startDir == Direction.Left && endDir == Direction.Left))
                        {
                            clippingRegion.X = Globals.TileSize * 1;
                            clippingRegion.Y = Globals.TileSize * 3;
                        }
                        else if ((startDir == Direction.Right && endDir == Direction.Right)
                            || (startDir == Direction.Up && endDir == Direction.Left))
                        {
                            clippingRegion.X = Globals.TileSize * 2;
                            clippingRegion.Y = Globals.TileSize * 3;
                        }
                        else
                        {
                            clippingRegion.X = Globals.TileSize * 3;
                            clippingRegion.Y = Globals.TileSize * 3;
                        }
                        clippingRegion.Width = Globals.TileSize;
                        clippingRegion.Height = Globals.TileSize;
                        break;
                    }
                case TrackType.sharpTurn2:
                    {
                        Lane lane0 = new Lane(this, type, startDir, endDir, 1);
                        Lane lane1 = new Lane(this, type, startDir, endDir, 2);
                        theLanes.Add(lane0);
                        theLanes.Add(lane1);
                        if ((startDir == Direction.Left && endDir == Direction.Right)
                            || (startDir == Direction.Down && endDir == Direction.Left))
                        {
                            clippingRegion.X = Globals.TileSize * 0;
                            clippingRegion.Y = Globals.TileSize * 1;
                        }
                        else if ((startDir == Direction.Up && endDir == Direction.Right)
                            || (startDir == Direction.Left && endDir == Direction.Left))
                        {
                            clippingRegion.X = Globals.TileSize * 2;
                            clippingRegion.Y = Globals.TileSize * 1;
                        }
                        else if ((startDir == Direction.Right && endDir == Direction.Right)
                            || (startDir == Direction.Up && endDir == Direction.Left))
                        {
                            clippingRegion.X = Globals.TileSize * 3;
                            clippingRegion.Y = Globals.TileSize * 1;
                        }
                        else
                        {
                            clippingRegion.X = Globals.TileSize * 1;
                            clippingRegion.Y = Globals.TileSize * 1;
                        }
                        clippingRegion.Width = Globals.TileSize;
                        clippingRegion.Height = Globals.TileSize;
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
                        clippingRegion.Width = Globals.TileSize;
                        clippingRegion.Height = Globals.TileSize;
                        clippingRegion.Width *= 2;
                        clippingRegion.Height *= 2;
                        if ((startDir == Direction.Left && endDir == Direction.Right)
                            || (startDir == Direction.Down && endDir == Direction.Left))
                        {
                            clippingRegion.X = Globals.TileSize * 0;
                            clippingRegion.Y = Globals.TileSize * 10;
                        }
                        else if ((startDir == Direction.Up && endDir == Direction.Right)
                            || (startDir == Direction.Left && endDir == Direction.Left))
                        {
                            clippingRegion.X = Globals.TileSize * 0;
                            clippingRegion.Y = Globals.TileSize * 8;
                        }
                        else if ((startDir == Direction.Right && endDir == Direction.Right)
                            || (startDir == Direction.Up && endDir == Direction.Left))
                        {
                            clippingRegion.X = Globals.TileSize * 2;
                            clippingRegion.Y = Globals.TileSize * 8;
                        }
                        else
                        {
                            clippingRegion.X = Globals.TileSize * 2;
                            clippingRegion.Y = Globals.TileSize * 10;
                        }
                        break;
                    }
                case TrackType.wideTurn2:
                    {
                        Lane lane0 = new Lane(this, type, startDir, endDir, 1);
                        Lane lane1 = new Lane(this, type, startDir, endDir, 2);
                        theLanes.Add(lane0);
                        theLanes.Add(lane1);
                        clippingRegion.Width = Globals.TileSize;
                        clippingRegion.Height = Globals.TileSize;
                        clippingRegion.Width *= 2;
                        clippingRegion.Height *= 2;
                        if ((startDir == Direction.Left && endDir == Direction.Right)
                            || (startDir == Direction.Down && endDir == Direction.Left))
                        {
                            clippingRegion.X = Globals.TileSize * 0;
                            clippingRegion.Y = Globals.TileSize * 6;
                        }
                        else if ((startDir == Direction.Up && endDir == Direction.Right)
                            || (startDir == Direction.Left && endDir == Direction.Left))
                        {
                            clippingRegion.X = Globals.TileSize * 0;
                            clippingRegion.Y = Globals.TileSize * 4;
                        }
                        else if ((startDir == Direction.Right && endDir == Direction.Right)
                            || (startDir == Direction.Up && endDir == Direction.Left))
                        {
                            clippingRegion.X = Globals.TileSize * 2;
                            clippingRegion.Y = Globals.TileSize * 4;
                        }
                        else
                        {
                            clippingRegion.X = Globals.TileSize * 2;
                            clippingRegion.Y = Globals.TileSize * 6;
                        }
                        break;
                    }
                case TrackType.startingLine2:
                    {
                        Lane lane0 = new Lane(this, type, startDir, 1);
                        Lane lane1 = new Lane(this, type, startDir, 2);
                        theLanes.Add(lane0);
                        theLanes.Add(lane1);
                        if (startDir == Direction.Left
                            || startDir == Direction.Right)
                        {
                            clippingRegion.X = Globals.TileSize * 2;
                            clippingRegion.Y = Globals.TileSize * 2;
                        }
                        else
                        {
                            clippingRegion.X = Globals.TileSize * 2;
                            clippingRegion.Y = Globals.TileSize * 4;
                        }
                        clippingRegion.Width = Globals.TileSize;
                        clippingRegion.Height = Globals.TileSize;
                        break;
                    }
                case TrackType.startingLine4:
                    {
                        Lane lane0 = new Lane(this, type, startDir, 1);
                        Lane lane1 = new Lane(this, type, startDir, 2);
                        Lane lane2 = new Lane(this, type, startDir, 3);
                        Lane lane3 = new Lane(this, type, startDir, 4);
                        theLanes.Add(lane0);
                        theLanes.Add(lane1);
                        theLanes.Add(lane2);
                        theLanes.Add(lane3);
                        if (startDir == Direction.Left
                            || startDir == Direction.Right)
                        {
                            clippingRegion.X = Globals.TileSize * 0;
                            clippingRegion.Y = Globals.TileSize * 2;
                        }
                        else
                        {
                            clippingRegion.X = Globals.TileSize * 4;
                            clippingRegion.Y = Globals.TileSize * 0;
                        }
                        clippingRegion.Width = Globals.TileSize;
                        clippingRegion.Height = Globals.TileSize;
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
            }
            //tileGraphic.TextureRegion = clippingRegion;
            //tileGraphic.ClippingRegion = clippingRegion;
            tileGraphic.AtlasRegion = clippingRegion;

        }

    }
}
