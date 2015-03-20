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
    
    class Rocket : PickUp
    {
       
        public Vector2 velocity;
        public int speed = 15;
        public Sound rocketFire = new Sound("Audio/rocketFire.wav");

        public Rocket(Slot_Car _owner, Race race) : base( race)
        {
            owner = _owner;
            theRace = race;
            itemImage = new Image("Assets/Images/rocket.png");
            SetGraphic(itemImage);
            itemImage.CenterOrigin();
            itemCollider = new BoxCollider(itemImage.Width, itemImage.Height, (int)ColliderType.PickUpUse);
            SetCollider(itemCollider);
            this.itemCollider.Collidable = false;
            this.itemImage.Visible = false;
            itemType = ItemType.Rocket;
            owner.theRace.Add(this);
            active = false;
        }

        public override void Execute()
        {
            active = true;
            itemCollider.Collidable = true;
            itemImage.Visible = true;
            velocity = owner.velocity;
            velocity.Normalize();
            velocity *= speed;
            itemImage.Angle = owner.carImage.Angle;
            X = owner.X;
            Y = owner.Y;
            rocketFire.Play();
        }

        public override void Update()
        {
            X += velocity.X;
            Y += velocity.Y;
            if ((X < 0 || X > 1920) || (Y < 0 || Y > 1080))
            {
                owner.currentPickup = null;
                RemoveSelf();
            }
        }
    


    }




}

