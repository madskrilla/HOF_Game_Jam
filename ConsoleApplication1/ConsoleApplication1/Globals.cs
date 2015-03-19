using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace ConsoleApplication1
{

    class Globals
    {
        public static Session
        PlayerOne;
        public static int TileSize = 150;
        public static Random numberGenerator = new Random(DateTime.Now.Millisecond);
    }

    public enum DriverType
    {
        Human,
        AI
    }

    public enum Controls
    {
        Accelerate,
        SwapLaneLeft,
        SwapLaneRight,
        UseItem,
        KeyUP,
        KeyDown,
        Escape,
        Enter
    }

    public enum RaceState
    {
        Racing,
        RaceBegin,
        RaceEnd,
        Pause
    }

    public enum TrackType
    {
        straight4,
        straight2,
        sharpTurn4,
        sharpTurn2,
        wideTurn4,
        wideTurn2,
        merge2to4,
        merge4to2,
        startingLine,
        itemSpawn2,
        itemSpawn4
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public enum AIState
    {
        Aggressive,
        Relaxed,
        Balanced
    }

    
}
