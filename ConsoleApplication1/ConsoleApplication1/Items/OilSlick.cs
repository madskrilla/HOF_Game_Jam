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
    class OilSlick : PickUp
    {

        public int activeTime = 20;
        public bool active = false;
        public int frame = 0;
        public Vector2 spawnOffset;


        public OilSlick(Slot_Car _owner, Race race)
            : base(_owner, race)
        {
            owner = _owner;
            theRace = race;
            itemImage = new Image("Assets/Images/oilSlick.png");
            SetGraphic(itemImage);
            itemCollider = new BoxCollider(itemImage.Width - 5, itemImage.Height - 5, (int)ColliderType.PickUpUse);
            SetCollider(itemCollider);

            itemType = ItemType.OilSlick;

            this.itemCollider.Collidable = false;
            this.itemImage.Visible = false;
            owner.theRace.Add(this);
        }

        public override void Execute()
        {
            active = true;
            spawnOffset = owner.velocity;
            spawnOffset.Normalize();
            spawnOffset *= 10;
            if (Math.Abs(spawnOffset.X) > Math.Abs(spawnOffset.Y))
            {
                X = owner.X + spawnOffset.X;
                if (spawnOffset.X > 0)
                    Y = owner.Y - owner.Collider.Height * 0.5f;

                else
                    Y = owner.Y + owner.Collider.Height * 0.5f;



            }
            else
            {
                X = owner.X;
                if (spawnOffset.X > 0)
                    Y = owner.Y - owner.Collider.Height * 0.5f + (-spawnOffset.Y);

                else
                    Y = owner.Y + owner.Collider.Height * 0.5f + (-spawnOffset.Y);

                //  Y = owner.Y + (-spawnOffset.Y);
            }
            itemImage.Angle = owner.carImage.Angle;

            this.itemCollider.Collidable = true;
            this.itemImage.Visible = true;
           
        }

        public override void Update()
        {
            if (active == true)
            {
                itemCollider.Render();
                frame++;
                if (frame % 60 == 0)
                {
                    activeTime--;
                    if (activeTime == 0)
                    {
                        RemoveSelf();
                    }
                }
            }
          
        }
    
    }
}
