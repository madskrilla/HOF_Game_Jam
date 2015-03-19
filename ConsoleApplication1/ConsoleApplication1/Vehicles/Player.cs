using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using ConsoleApplication1.Scenes;
using ConsoleApplication1.Vehicles;

namespace ConsoleApplication1
{
    class Player : Slot_Car
    {
        Session player;
        public Player(Race _race, int _ln, Session _player) : base(_race, _ln)
        {
            currentSpeed = 0;
            player = _player;
            Globals.slotCarText.String = "test";
            Globals.slotCarText.FontSize = 25;
            Globals.slotCarText.Color = Color.White;
        }

        public override void Update()
        {
            Globals.slotCarText.String = Game.Framerate.ToString();
            getInput();
            velocity *= currentSpeed;
            
            base.Update();
        }

        public override void Render()
        {
            base.Render();
            Globals.slotCarText.Render();
        }

        public void getInput()
        {
            
            if(player.Controller.Button(Controls.Accelerate).Down)
            {
                acceleration += 0.1f;
                if (acceleration > maxSpeed) acceleration = maxSpeed;
            }
            else if (acceleration > 0)
            {
                acceleration -= 0.1f;
                //currentSpeed = 0;// acceleration;
                if (acceleration < 0) acceleration = 0;
            }

            if (player.Controller.Button(Controls.SwapLaneRight).Pressed)
            {
                if (Lane < 3)
                {
                    Lane++;
                    nodeIndex++;
                }
            }

            if (player.Controller.Button(Controls.SwapLaneLeft).Pressed)
            {
                if (Lane > 0)
                {
                    Lane--;
                    nodeIndex++;
                }
            }
        }
    }
}
