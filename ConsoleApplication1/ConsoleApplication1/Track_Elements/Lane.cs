using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using ConsoleApplication1.Track_Elements;

namespace ConsoleApplication1.Track_Elements
{
    class Lane
    {
        public Track_Piece parentPiece;
        public List<Node> theNodes = new List<Node>();
        public float spinOutThreshold;

        public Lane(Track_Piece _parent, TrackType _type, Direction _dir, int _lane) // straight constructor
        {
            parentPiece = _parent;
            switch (_type)
            {
                case TrackType.straight4:
                    switch (_dir)
                    {
                        case Direction.Up:
			                    for (int node = 0; node < 5; node++)
                                {
                                    Node newNode = new Node();
                                    newNode.localSpace.X = _parent.X + (Globals.TileSize / 5) * _lane;
                                    newNode.localSpace.Y = _parent.Y + Globals.TileSize - ((Globals.TileSize / 5) * node);
                                    theNodes.Add(newNode);
                                }
                            break;
                        case Direction.Down:
			                    for (int node = 0; node < 5; node++)
                                {
                                    Node newNode = new Node();
                                    newNode.localSpace.X = _parent.X + Globals.TileSize - ((Globals.TileSize / 5) * _lane);
                                    newNode.localSpace.Y = _parent.Y + (Globals.TileSize / 5) * node;
                                    theNodes.Add(newNode);
                                }
                            break;
                        case Direction.Left:
			                    for (int node = 0; node < 5; node++)
                                {
                                    Node newNode = new Node();
                                    newNode.localSpace.X = _parent.X + Globals.TileSize - ((Globals.TileSize / 5) * node);
                                    newNode.localSpace.Y = _parent.Y + Globals.TileSize - ((Globals.TileSize / 5) * _lane);
                                    theNodes.Add(newNode);
                                }
                            break;
                        case Direction.Right:
			                    for (int node = 0; node < 5; node++)
                                {
                                    Node newNode = new Node();
                                    newNode.localSpace.X = _parent.X + ((Globals.TileSize / 5) * node);
                                    newNode.localSpace.Y = _parent.Y + ((Globals.TileSize / 5) * _lane);
                                    theNodes.Add(newNode);
                                }
                            break;
	                }
                    break;
                case TrackType.straight2:
                    switch (_dir)
	                {
                        case Direction.Up:
			                    for (int node = 0; node < 5; node++)
                                {
                                    Node newNode = new Node();
                                    newNode.localSpace.X = _parent.X + (Globals.TileSize / 3) * _lane;
                                    newNode.localSpace.Y = _parent.Y + Globals.TileSize - ((Globals.TileSize / 5) * node);
                                    theNodes.Add(newNode);
                                }
                            break;
                        case Direction.Down:
			                    for (int node = 0; node < 5; node++)
                                {
                                    Node newNode = new Node();
                                    newNode.localSpace.X = _parent.X + Globals.TileSize - ((Globals.TileSize / 3) * _lane);
                                    newNode.localSpace.Y = _parent.Y + (Globals.TileSize / 5) * node;
                                    theNodes.Add(newNode);
                                }
                            break;
                        case Direction.Left:
			                    for (int node = 0; node < 5; node++)
                                {
                                    Node newNode = new Node();
                                    newNode.localSpace.X = _parent.X + Globals.TileSize - ((Globals.TileSize / 5) * node);
                                    newNode.localSpace.Y = _parent.Y + Globals.TileSize - ((Globals.TileSize / 3) * _lane);
                                    theNodes.Add(newNode);
                                }
                            break;
                        case Direction.Right:
			                    for (int node = 0; node < 5; node++)
                                {
                                    Node newNode = new Node();
                                    newNode.localSpace.X = _parent.X + ((Globals.TileSize / 5) * node);
                                    newNode.localSpace.Y = _parent.Y + ((Globals.TileSize / 3) * _lane);
                                    theNodes.Add(newNode);
                                }
                            break;
	                }
                    break;
                case TrackType.merge2to4:
                    switch (_dir)
                    {
                        case Direction.Up:
                            for (int node = 0; node < 5; node++)
                            {
                                if (node < 2)
                                {
                                    Node newNode = new Node();
                                    newNode.localSpace.X = _parent.X + (Globals.TileSize / 3) * _lane;
                                    newNode.localSpace.Y = _parent.Y + Globals.TileSize - ((Globals.TileSize / 5) * node);
                                    theNodes.Add(newNode);
                                }
                                else
                                {
                                    Node newNode = new Node();
                                    newNode.localSpace.X = _parent.X + (Globals.TileSize / 5) * _lane;
                                    newNode.localSpace.Y = _parent.Y + Globals.TileSize - ((Globals.TileSize / 5) * node);
                                    theNodes.Add(newNode);
                                }
                            }
                            break;
                        case Direction.Down:
                            for (int node = 0; node < 5; node++)
                            {
                                if (node < 2)
                                {
                                    Node newNode = new Node();
                                    newNode.localSpace.X = _parent.X + Globals.TileSize - ((Globals.TileSize / 3) * _lane);
                                    newNode.localSpace.Y = _parent.Y + (Globals.TileSize / 5) * node;
                                    theNodes.Add(newNode);
                                }
                                else
                                {
                                    Node newNode = new Node();
                                    newNode.localSpace.X = _parent.X + Globals.TileSize - ((Globals.TileSize / 5) * _lane);
                                    newNode.localSpace.Y = _parent.Y + (Globals.TileSize / 5) * node;
                                    theNodes.Add(newNode);
                                }
                            }
                            break;
                        case Direction.Left:
                            for (int node = 0; node < 5; node++)
                            {
                                if (node < 2)
                                {
                                    Node newNode = new Node();
                                    newNode.localSpace.X = _parent.X + Globals.TileSize - ((Globals.TileSize / 5) * node);
                                    newNode.localSpace.Y = _parent.Y + Globals.TileSize - ((Globals.TileSize / 3) * _lane);
                                    theNodes.Add(newNode);
                                }
                                else
                                {
                                    Node newNode = new Node();
                                    newNode.localSpace.X = _parent.X + Globals.TileSize - ((Globals.TileSize / 5) * node);
                                    newNode.localSpace.Y = _parent.Y + Globals.TileSize - ((Globals.TileSize / 5) * _lane);
                                    theNodes.Add(newNode);
                                }
                            }
                            break;
                        case Direction.Right:
                            for (int node = 0; node < 5; node++)
                            {
                                if (node < 2)
                                {
                                    Node newNode = new Node();
                                    newNode.localSpace.X = _parent.X + ((Globals.TileSize / 5) * node);
                                    newNode.localSpace.Y = _parent.Y + ((Globals.TileSize / 3) * _lane);
                                    theNodes.Add(newNode);
                                }
                                else
                                {
                                    Node newNode = new Node();
                                    newNode.localSpace.X = _parent.X + ((Globals.TileSize / 5) * node);
                                    newNode.localSpace.Y = _parent.Y + ((Globals.TileSize / 5) * _lane);
                                    theNodes.Add(newNode);
                                }
                                
                            }
                            break;
                    }
                    break;
                case TrackType.merge4to2:
                    break;
                case TrackType.startingLine:
                    break;
                case TrackType.itemSpawn2:
                    break;
                case TrackType.itemSpawn4:
                    break;
                default:
                    break;
            }
        }
        public Lane(Track_Piece _parent, TrackType _type, Direction _startDir, Direction _endDir, int _lane) // turn constructor
        {
            switch (_type)
            {
                case TrackType.sharpTurn4:
                    switch (_startDir)
                    {
                        case Direction.Up:
                            if (_endDir == Direction.Left)
                            {
                                for (int node = 0; node < 10; node++)
                                {
                                    Node newNode = new Node();
                                    newNode.localSpace.X = _parent.X + (float)Math.Cos((double)(0 + ((Math.PI / 2) / 10) * node)) * ((Globals.TileSize / 5) * _lane);
                                    newNode.localSpace.Y = _parent.Y + Globals.TileSize - (float)Math.Sin((double)(0 + ((Math.PI / 2) / 10) * node)) * ((Globals.TileSize / 5) * _lane);
                                    theNodes.Add(newNode);
                                }
                            }
                            else
                            {
                                int realLane;
                                if (_lane == 1)
                                    realLane = 4;
                                else if (_lane == 2)
                                    realLane = 3;
                                else if (_lane == 3)
                                    realLane = 2;
                                else
                                    realLane = 1;
                                for (int node = 0; node < 10; node++)
                                {
                                    Node newNode = new Node();
                                    newNode.localSpace.X = _parent.X + Globals.TileSize + (float)Math.Cos((double)(Math.PI - ((Math.PI / 2) / 10) * node)) * ((Globals.TileSize / 5) * realLane);
                                    newNode.localSpace.Y = _parent.Y + Globals.TileSize - (float)Math.Sin((double)(Math.PI - ((Math.PI / 2) / 10) * node)) * ((Globals.TileSize / 5) * realLane);
                                    theNodes.Add(newNode);
                                }
                            }
                            break;
                        case Direction.Down:
                            if (_endDir == Direction.Left)
                            {
                                for (int node = 0; node < 10; node++)
                                {
                                    Node newNode = new Node();
                                    newNode.localSpace.X = _parent.X + Globals.TileSize + (float)Math.Cos((double)(Math.PI + ((Math.PI / 2) / 10) * node)) * ((Globals.TileSize / 5) * _lane);
                                    newNode.localSpace.Y = _parent.Y + (float)Math.Sin((double)(Math.PI + ((Math.PI / 2) / 10) * node)) * ((Globals.TileSize / 5) * _lane);
                                    theNodes.Add(newNode);
                                }
                            }
                            else
                            {
                                int realLane;
                                if (_lane == 1)
                                    realLane = 4;
                                else if (_lane == 2)
                                    realLane = 3;
                                else if (_lane == 3)
                                    realLane = 2;
                                else
                                    realLane = 1;
                                for (int node = 0; node < 10; node++)
                                {
                                    Node newNode = new Node();
                                    newNode.localSpace.X = _parent.X + (float)Math.Cos((double)(Math.PI * 2 - ((Math.PI / 2) / 10) * node)) * ((Globals.TileSize / 5) * realLane);
                                    newNode.localSpace.Y = _parent.Y - (float)Math.Sin((double)(Math.PI * 2 - ((Math.PI / 2) / 10) * node)) * ((Globals.TileSize / 5) * realLane);
                                    theNodes.Add(newNode);
                                }
                            }
                            break;
                        case Direction.Left:
                            if (_endDir == Direction.Left)
                            {
                                for (int node = 0; node < 10; node++)
                                {
                                    Node newNode = new Node();
                                    newNode.localSpace.X = _parent.X + Globals.TileSize + (float)Math.Cos((double)(Math.PI / 2 + ((Math.PI / 2) / 10) * node)) * ((Globals.TileSize / 5) * _lane);
                                    newNode.localSpace.Y = _parent.Y + Globals.TileSize - (float)Math.Sin((double)(Math.PI / 2 + ((Math.PI / 2) / 10) * node)) * ((Globals.TileSize / 5) * _lane);
                                    theNodes.Add(newNode);
                                }
                            }
                            else
                            {
                                int realLane;
                                if (_lane == 1)
                                    realLane = 4;
                                else if (_lane == 2)
                                    realLane = 3;
                                else if (_lane == 3)
                                    realLane = 2;
                                else
                                    realLane = 1;
                                for (int node = 0; node < 10; node++)
                                {
                                    Node newNode = new Node();
                                    newNode.localSpace.X = _parent.X + Globals.TileSize + (float)Math.Cos((double)(3 * Math.PI / 2 - ((Math.PI / 2) / 10) * node)) * ((Globals.TileSize / 5) * realLane);
                                    newNode.localSpace.Y = _parent.Y - (float)Math.Sin((double)(3 * Math.PI / 2 - ((Math.PI / 2) / 10) * node)) * ((Globals.TileSize / 5 * realLane));
                                    theNodes.Add(newNode);
                                }
                            }
                            break;
                        case Direction.Right:
                            if (_endDir == Direction.Left)
                            {
                                for (int node = 0; node < 10; node++)
                                {
                                    Node newNode = new Node();
                                    newNode.localSpace.X = _parent.X + (float)Math.Cos((double)(3 * Math.PI / 2 + ((Math.PI / 2) / 10) * node)) * ((Globals.TileSize / 5) * _lane);
                                    newNode.localSpace.Y = _parent.Y - (float)Math.Sin((double)(3 * Math.PI / 2 + ((Math.PI / 2) / 10) * node)) * ((Globals.TileSize / 5) * _lane);
                                    theNodes.Add(newNode);
                                }
                            }
                            else
                            {
                                int realLane;
                                if (_lane == 1)
                                    realLane = 4;
                                else if (_lane == 2)
                                    realLane = 3;
                                else if (_lane == 3)
                                    realLane = 2;
                                else
                                    realLane = 1;
                                for (int node = 0; node < 10; node++)
                                {
                                    Node newNode = new Node();
                                    newNode.localSpace.X = _parent.X + (float)Math.Cos((double)(Math.PI / 2 - ((Math.PI / 2) / 10) * node)) * ((Globals.TileSize / 5) * realLane);
                                    newNode.localSpace.Y = _parent.Y + Globals.TileSize - (float)Math.Sin((double)(Math.PI / 2 - ((Math.PI / 2) / 10) * node)) * ((Globals.TileSize / 5) * realLane);
                                    theNodes.Add(newNode);
                                }
                            }
                            break;
                    }
                    break;
                case TrackType.sharpTurn2:
                    switch (_startDir)
	                {
                        case Direction.Up:
                            if (_endDir == Direction.Left)
                            {
                                for (int node = 0; node < 10; node++)
                                {
                                    Node newNode = new Node();
                                    newNode.localSpace.X = _parent.X + (float)Math.Cos((double)(0 + ((Math.PI / 2) / 10) * node)) * ((Globals.TileSize / 3) * _lane);
                                    newNode.localSpace.Y = _parent.Y + Globals.TileSize - (float)Math.Sin((double)(0 + ((Math.PI / 2) / 10) * node)) * ((Globals.TileSize / 3) * _lane);
                                    theNodes.Add(newNode);
                                }
                            }
                            else
                            {
                                int realLane;
                                if (_lane == 1)
                                    realLane = 2;
                                else
                                    realLane = 1;
                                for (int node = 0; node < 10; node++)
                                {
                                    Node newNode = new Node();
                                    newNode.localSpace.X = _parent.X + Globals.TileSize + (float)Math.Cos((double)(Math.PI - ((Math.PI / 2) / 10) * node)) * ((Globals.TileSize / 3) * realLane);
                                    newNode.localSpace.Y = _parent.Y + Globals.TileSize - (float)Math.Sin((double)(Math.PI - ((Math.PI / 2) / 10) * node)) * ((Globals.TileSize / 3) * realLane);
                                    theNodes.Add(newNode);
                                }
                            }
                            break;
                        case Direction.Down:
                            if (_endDir == Direction.Left)
                            {
                                for (int node = 0; node < 10; node++)
                                {
                                    Node newNode = new Node();
                                    newNode.localSpace.X = _parent.X + Globals.TileSize + (float)Math.Cos((double)(Math.PI + ((Math.PI / 2) / 10) * node)) * ((Globals.TileSize / 3) * _lane);
                                    newNode.localSpace.Y = _parent.Y + (float)Math.Sin((double)(Math.PI + ((Math.PI / 2) / 10) * node)) * ((Globals.TileSize / 3) * _lane);
                                    theNodes.Add(newNode);
                                }
                            }
                            else
                            {
                                int realLane;
                                if (_lane == 1)
                                    realLane = 2;
                                else
                                    realLane = 1;
                                for (int node = 0; node < 10; node++)
                                {
                                    Node newNode = new Node();
                                    newNode.localSpace.X = _parent.X + (float)Math.Cos((double)(Math.PI * 2 - ((Math.PI / 2) / 10) * node)) * ((Globals.TileSize / 3) * realLane);
                                    newNode.localSpace.Y = _parent.Y - (float)Math.Sin((double)(Math.PI * 2 - ((Math.PI / 2) / 10) * node)) * ((Globals.TileSize / 3) * realLane);
                                    theNodes.Add(newNode);
                                }
                            }
                            break;
                        case Direction.Left:
                            if (_endDir == Direction.Left)
                            {
                                    for (int node = 0; node < 10; node++)
                                    {
                                        Node newNode = new Node();
                                        newNode.localSpace.X = _parent.X + Globals.TileSize + (float)Math.Cos((double)(Math.PI / 2 + ((Math.PI / 2) / 10) * node)) * ((Globals.TileSize / 3) * _lane);
                                        newNode.localSpace.Y = _parent.Y + Globals.TileSize - (float)Math.Sin((double)(Math.PI / 2 + ((Math.PI / 2) / 10) * node)) * ((Globals.TileSize / 3) * _lane);
                                        theNodes.Add(newNode);
                                    }
                            }
                            else
                            {
                                int realLane;
                                if (_lane == 1)
                                    realLane = 2;
                                else
                                    realLane = 1;
                                for (int node = 0; node < 10; node++)
                                {
                                    Node newNode = new Node();
                                    newNode.localSpace.X = _parent.X + Globals.TileSize + (float)Math.Cos((double)(3 * Math.PI / 2 - ((Math.PI / 2) / 10) * node)) * ((Globals.TileSize / 3) * realLane);
                                    newNode.localSpace.Y = _parent.Y - (float)Math.Sin((double)(3 * Math.PI / 2 - ((Math.PI / 2) / 10) * node)) * ((Globals.TileSize / 3) * realLane);
                                    theNodes.Add(newNode);
                                }
                            }
                            break;
                        case Direction.Right:
                            if (_endDir == Direction.Left)
                            {
                                    for (int node = 0; node < 10; node++)
                                    {
                                        Node newNode = new Node();
                                        newNode.localSpace.X = _parent.X + (float)Math.Cos((double)(3 * Math.PI / 2 + ((Math.PI / 2) / 10) * node)) * ((Globals.TileSize / 3) * _lane);
                                        newNode.localSpace.Y = _parent.Y - (float)Math.Sin((double)(3 * Math.PI / 2 + ((Math.PI / 2) / 10) * node)) * ((Globals.TileSize / 3) * _lane);
                                        theNodes.Add(newNode);
                                    }
                            }
                            else
                            {
                                int realLane;
                                if (_lane == 1)
                                    realLane = 2;
                                else
                                    realLane = 1;
                                for (int node = 0; node < 10; node++)
                                {
                                    Node newNode = new Node();
                                    newNode.localSpace.X = _parent.X + (float)Math.Cos((double)(Math.PI / 2 - ((Math.PI / 2) / 10) * node)) * ((Globals.TileSize / 3) * realLane);
                                    newNode.localSpace.Y = _parent.Y + Globals.TileSize - (float)Math.Sin((double)(Math.PI / 2 - ((Math.PI / 2) / 10) * node)) * ((Globals.TileSize / 3) * realLane);
                                    theNodes.Add(newNode);
                                }
                            }
                            break;
	                }
                    break;
                case TrackType.wideTurn4:
                    break;
                case TrackType.wideTurn2:
                    break;
            }
        }

    }
}
