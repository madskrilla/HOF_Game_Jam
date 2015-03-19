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
        public Image carImage = Image.CreateRectangle(30, 50, Color.Cyan);
        public BoxCollider carCollider;
        //public Speed currentSpeed = new Speed(3,true);
        //public Vector2 acceleration = new Vector2(3,3);
        public Vector2 velocity;
        public Vector2 SteerVec;
        public Vector2 position;
        public Vector2 right = new Vector2(1, 0);
        public Vector2 up = new Vector2(0, -1);
        public Node targetNode = new Node();
        public int Lane;
        public int nodeIndex = 0;
        public int nextNode = 1;
        public int pieceIndex = 0;
        public int maxSpeed = 10;
        public PickUp currentPickup;
        public int nodesPassed = 0;
        private bool spinning = false;
        private int spinTicks = 90;

        public float currentSpeed;
        public float acceleration = 0.0f;

        public Slot_Car(Race _race, int _ln) : base()
        {
            SetGraphic(carImage);
            carImage.CenterOrigin();
            theRace = _race;
            Lane = _ln;
            SteerVec = new Vector2();
            velocity = new Vector2();
            position = new Vector2();

            targetNode = theRace.theTrack.thePieces[pieceIndex].theLanes[Lane].theNodes[nodeIndex];
            X = targetNode.localSpace.X;
            Y = targetNode.localSpace.Y;
        }

        public override void Update()
        {
            //currentSpeed.X += acceleration.X;
            //currentSpeed.Y += acceleration.Y;

            //if (Math.Abs(currentSpeed.X) < 0.05f) currentSpeed.X = 0;
           //if (Math.Abs(currentSpeed.Y) < 0.05f) currentSpeed.Y = 0;
            if(!spinning)
            Steer();
            //if (true)
            //    spinning = true;
            //if(spinning)
            //    SpinOut();
           
            X += velocity.X;
            Y += velocity.Y;



            base.Update();


        }

        public void Steer()
        {
            SteerVec = targetNode.localSpace;
            position.X = X;
            position.Y = Y;

            float dist = Vector2.Distance(SteerVec, position);

            if (dist < 45)
            {

                nodeIndex = nextNode;
               
                if (nodeIndex == theRace.theTrack.thePieces[pieceIndex].theLanes[Lane].theNodes.Count())
                {
                    nodeIndex = 0;
                    pieceIndex++;
                    if (pieceIndex == theRace.theTrack.thePieces.Count())
                    {
                        pieceIndex = 0;
                    }
                }
                nextNode = nodeIndex + 1;
                targetNode = theRace.theTrack.thePieces[pieceIndex].theLanes[Lane].theNodes[nodeIndex];
                SteerVec = targetNode.localSpace;
            }

            Vector2 toTarget = SteerVec - position;

            toTarget.Normalize();
            velocity += toTarget *acceleration;

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
        }

        private void SpinOut()
        {
            spinTicks--;
            if (spinTicks == 0)
            {
                spinning = false;
                spinTicks = 90;
                return;
            }

            velocity.X = 0;
            velocity.Y = 0;

            carImage.Angle += 15;
            Console.WriteLine(carImage.Angle.ToString());
        }
    }
}
