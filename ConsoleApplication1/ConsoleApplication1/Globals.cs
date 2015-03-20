﻿using System;
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
        public static Text slotCarText = new Text("", "Assets/RACER___.TTF");
        public static Music digestiveIntro = new Music("Audio/DigestiveBiscuitIntro.wav", false);
        public static Music digestiveLoop = new Music("Audio/DigestiveBiscuitLoop.wav");
        public static long songStartTime;
        public static bool loopPlaying = false;
        public static float musicVolume = 0.3f;
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
        startingLine2,
        startingLine4,
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

    public enum ColliderType
    {
        Slot_Car,
        PickUp
    }

    
}
