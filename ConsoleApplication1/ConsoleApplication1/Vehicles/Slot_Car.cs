using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using ConsoleApplication1.Scenes;
using ConsoleApplication1.Track_Elements;
using ConsoleApplication1.Items;

namespace ConsoleApplication1.Vehicles
{
    class Slot_Car : Entity
    {
        public DriverType driverType;
        public Race theRace;
        public Image carImage = Image.CreateRectangle(40, 100, Color.Cyan);
        public BoxCollider carCollider;
        Vector2 forward = new Vector2();
        public Speed currentSpeed = new Speed(3,true);
        public Vector2 steerVec;
        public Vector2 acceleration;
        private Node target;

        public Vector2 velocity;
        public Vector2 SteerVec;
        public Vector2 position;
        public Vector2 right = new Vector2(1, 0);
        public Vector2 up = new Vector2(0, -1);
        public Node targetNode;
        public int maxSpeed = 5;



        internal Node Target
        {
            get { return target; }
            set { target = value; }
        }

        public PickUp currentPickup;

        public int nodesPassed = 0;

        public Slot_Car(Race _race) : base()
        {
            SetGraphic(carImage);
            carImage.CenterOrigin();
            theRace = _race;

            SteerVec = new Vector2();
            velocity = new Vector2();
            position = new Vector2();
        }

        public override void Render()
        {
           // Draw.Line(X, Y, steerVec.X, steerVec.Y, Color.Cyan);         
        }
        public override void Update()
        {
            currentSpeed.X += acceleration.X;
            currentSpeed.Y += acceleration.Y;

            if (Math.Abs(currentSpeed.X) < 0.05f) currentSpeed.X = 0;
            if (Math.Abs(currentSpeed.Y) < 0.05f) currentSpeed.Y = 0;
            Steer();
           
            X += velocity.X;
            Y += velocity.Y; 
            
            base.Update();


        }

        private void Steer()
        {
            SteerVec = target.localSpace;
            position.X = X;
            position.Y = Y;

            float dist = Vector2.Distance(SteerVec, position);

            if (dist < 45)
            {
                target = target.nextNode;
                SteerVec = target.localSpace;
            }

            Vector2 toTarget = SteerVec - position;

            toTarget.Normalize();
            velocity += toTarget;

            if(velocity.Length > maxSpeed)
            {
                velocity.Normalize();
                velocity *= maxSpeed;
            }

            float dpR = Vector2.Dot(toTarget, right);
            if (dpR < 0)
            {
                carImage.Angle = (float)Math.Acos(Vector2.Dot(up, toTarget)) * (180 / 3.14f);
            }
            else
            {
                carImage.Angle = -(float)Math.Acos(Vector2.Dot(up, toTarget)) * (180 / 3.14f);
            }



           //float dist ;
           //steerVec = target.localSpace;
           //position.X = X;
           //position.Y = Y;
           //dist = Vector2.Distance(steerVec, position);
           //
           //if (dist < 45)
           //{
           //    target = target.nextNode;
           //    steerVec = target.localSpace;
           //}
           //steerVec = steerVec - position;
           //steerVec.Normalize();
           //Vector2 up = new Vector2(0, -1);
           //
           //urrentSpeed.X += steerVec.X;
           //urrentSpeed.Y += steerVec.Y;
           //
           //forward.X = currentSpeed.X;
           //forward.Y = currentSpeed.Y;
           //forward.Normalize();
           //
           //carImage.Angle = (float)Math.Acos(Vector2.Dot(up, steerVec)) * (180/3.14f);

        }

       
    }
}
