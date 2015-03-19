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
        public int shakeTime = 0;
        public Player(Race _race, int _ln, Session _player) : base(_race, _ln)
        {
            currentSpeed = 0;
            player = _player;
            Globals.slotCarText.String = "test";
            Globals.slotCarText.FontSize = 25;
            Globals.slotCarText.Color = Color.White;
            attacking = false;
        }

        public override void Update()
        {
            Globals.slotCarText.String = Game.Framerate.ToString();
            getInput();
            velocity *= currentSpeed;
              var collider = carCollider.Collide(X, Y, ColliderType.Slot_Car);
              if (collider != null)
              {
                  Slot_Car otherCah = (Slot_Car)collider.Entity;
                  if (otherCah.attacking && otherCah.Lane == Lane)
                  {
                      shakeTime = 90;
                  }
              }
            ScreenShake();
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

        public void ScreenShake()
        {
            shakeTime--;
            if (shakeTime <= 0)
                return;
            if (shakeTime % 2 == 0)
                theRace.CameraX += 2;
            else
                theRace.CameraX -= 2;
        }
    }
}
