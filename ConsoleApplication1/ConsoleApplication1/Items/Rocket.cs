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
        public int speed = 10;
       

        public Rocket(Slot_Car _owner, Race race) : base(_owner, race)
        {
            owner = _owner;
            theRace = race;
            this.itemCollider.Collidable = false;
            this.itemImage.Visible = false;
        }

        public override void Execute()
        {
            this.itemCollider.Collidable = true;
            this.itemImage.Visible = true;
            velocity = owner.velocity;
            velocity.Normalize();
            velocity *= speed;
        }

        public override void Update()
        {
            X += velocity.X;
            Y += velocity.Y; 
        }
    


    }




}

