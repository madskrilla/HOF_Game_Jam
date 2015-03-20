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
    class AIDriver : Slot_Car
    {
        const int laneSwapChance = 1;
        public AIDriver(Race _race, int _ln) : base(_race, _ln)
        {
            carImage = new Image("Assets/Images/Car7_Gray.png");
            SetGraphic(carImage);
            carImage.CenterOrigin();

            driverType = DriverType.AI;
            attacking = true;
            acceleration = maxSpeed;
        }

        public override void Update()
        {
            if (theRace.currentState == RaceState.RaceBegin)
                return;
            base.Update();

            if (Globals.numberGenerator.Next() % 100 < laneSwapChance && popTimer <= 0)
            {
                if (Globals.numberGenerator.Next() % 2 == 0)
                {
                    if (Lane < 3)
                    {
                        popTimer = popDuration;
                        Lane++;
                        nodeIndex++; 
                    }
                }
                else
                {
                    if (Lane > 0)
                    {
                        popTimer = popDuration;
                        Lane--;
                        nodeIndex++;
                    }
                }
            }

            var collider = carCollider.Collide(X, Y, ColliderType.Slot_Car);
            if (collider != null)
            {
                //if (collider.Tags[0] == (int)ColliderType.PickUp)
                //{
                //    PickUp item = (PickUp)collider.Entity;
                //    item.Collidable = false;
                //    item.itemImage.Visible = false;
                //    spinning = true;
                //}

                Slot_Car otherCah = (Slot_Car)collider.Entity;
                 if (!otherCah.attacking)
                {
                    if (Lane == 3)
                        Lane--;
                    else if (Lane == 0)
                        Lane++;
                    else if (Rand.Chance(50))
                        Lane++;
                    else
                        Lane--;
                }

            }
        }
    }
}
