﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using ConsoleApplication1.Vehicles;


namespace ConsoleApplication1.Extra
{
    class HUD : Entity
    {
      public Text Player = new Text("", "Assets/RACER___.TTF");
      public Text Lap = new Text("", "Assets/RACER___.TTF");
      Scene theScene = new Scene();
      public Slot_Car owner;
      public int player;
      public int xPos;
      public int yPos;

        public HUD(Slot_Car _owner, Scene _scene) : base()
      {
          owner = _owner;
          Lap.String = "Lap: " + owner.completeLaps.ToString();
          player = owner.Lane + 1;
          Player.String = "player " + player.ToString();
          theScene = _scene;
          Player.FontSize = 50;
          Lap.FontSize = 50;
      }
        public override void Update()
        {
            base.Update();
            Lap.String = "Lap: " + owner.completeLaps.ToString();
        }
        public override void Render()
        {
            switch (player)
            {
                case 1:
                    xPos = 0;
                    yPos = 0;
                    Player.Color = Color.Green;
                    break;
                case 2:
                    xPos = theScene.Width - Player.Width;
                    yPos = 0;
                    Player.Color = Color.Blue;
                    break;
                case 3:
                    xPos = 0;
                    yPos = theScene.Height - 100;
                    Player.Color = Color.Yellow;
                    break;
                case 4:
                    xPos = theScene.Width - Player.Width;
                    yPos = theScene.Height - 100;
                    Player.Color = Color.Red;
                    break;
                default:
                    break;
            }
            Player.Render(xPos, yPos);
            Lap.Render(xPos, yPos + 50);
        }
    }
}
