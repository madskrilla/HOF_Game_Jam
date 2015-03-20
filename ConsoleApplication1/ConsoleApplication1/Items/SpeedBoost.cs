using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using ConsoleApplication1.Vehicles;
using ConsoleApplication1.Scenes;

namespace ConsoleApplication1.Items
{
    class SpeedBoost : PickUp
    {
        public int activeTimer = 240;
       // public bool active = false;

        public SpeedBoost(Slot_Car _owner, Race race) : base(race)
        {
            owner = _owner;
            theRace = race;
            itemType = ItemType.SpeedBoost;
            owner.theRace.Add(this);

        }
        public override void Execute()
        {
            active = true;
            owner.maxSpeed = 15;
         
        }

        public override void Update()
        {
            if (active)
            {
              
                activeTimer--;
                if (activeTimer == 0)
                {
                    owner.maxSpeed = 10;
                    RemoveSelf();
                }

            }
        }
    
    }
}
