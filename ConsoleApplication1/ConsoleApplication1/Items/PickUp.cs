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
    class PickUp : Item_Base
    {

        public int respawnTimer;
        public int frame;
        public Slot_Car owner;
        public Race theRace;
        public ItemType itemType;
        public bool active = false;
        
        
        public PickUp( Race race) : base()
        {
            theRace = race;
            itemImage = new Image("Assets/Images/pickup.png");
            itemCollider = new BoxCollider(itemImage.Width, itemImage.Height, (int)ColliderType.PickUpBase);
            SetGraphic(itemImage);
            SetCollider(itemCollider);
            itemCollider.Collidable = true;
            itemImage.Visible = true;
            itemImage.CenterOrigin();
         

            respawnTimer = 0;
            frame = 0;
            Layer = -5;

            theRace.Add(this);
        }

        public PickUp()
        {
            // TODO: Complete member initialization
        }

        public virtual void Execute()
        {

        }

        // TO WHOEVER IS CODING THIS: dont actually return a base pickup
        // generate a random child pickup, this is just to make the strawman compile
        // also set the timer  and deactivate rendering/collision of this plz n thx
        public PickUp GenerateRandom(Slot_Car owner)
        {
            PickUp p;
            Random rnd = new Random();
            int pickupType = rnd.Next(0, 5);
            switch (pickupType)
            {
                case 0:
                    p = new Rocket(owner, theRace);
                    break;
                case 1:
                    p = new Missle(owner, theRace);
                    break;
                case 2:
                    p = new OilSlick(owner, theRace);
                    break;
                case 3:
                    p = new Bomb(owner, theRace);
                    break;
                case 4:
                    p = new SpeedBoost(owner, theRace);
                    break;
                default:
                    p = new PickUp( theRace);
                    break;
            }
            itemCollider.Collidable = false;
            itemImage.Visible = false;
            respawnTimer = 5;
            return p;

 
        }

        public override void Update()
        {
            if (respawnTimer > 0)
            {
                frame++;
                if (frame % 60 == 0)
                    respawnTimer--;
                if (respawnTimer == 0)
                {
                    itemCollider.Collidable = true;
                    itemImage.Visible = true;
                }
            }
          
            base.Update();
        }
    }
}
