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
    class Bomb : PickUp
    {

        public Bomb(Slot_Car _owner, Race race)
            : base(_owner, race)
        {
            owner = _owner;
            theRace = race;
            itemType = ItemType.Bomb;
            itemImage = new Image("Assets/Images/bomb.png");
            SetGraphic(itemImage);
            itemCollider = new BoxCollider(itemImage.Width, itemImage.Height, (int)ColliderType.PickUpUse);
            SetCollider(itemCollider);

            this.itemCollider.Collidable = false;
            this.itemImage.Visible = false;
        }

        public override void Execute()
        {
            X = owner.X;
            Y = owner.Y;
            this.itemCollider.Collidable = true;
            this.itemImage.Visible = true;
           
        }

        public override void Update()
        {
          
        }
    
    }
}
