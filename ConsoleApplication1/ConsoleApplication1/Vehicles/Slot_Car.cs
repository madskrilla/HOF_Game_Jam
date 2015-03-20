using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using ConsoleApplication1.Scenes;
using ConsoleApplication1.Track_Elements;
using ConsoleApplication1.Items;
using ConsoleApplication1.Particles;

namespace ConsoleApplication1.Vehicles
{
    class Slot_Car : Entity
    {
        public DriverType driverType;
        public Race theRace;
        public Image carImage = Image.CreateRectangle(30, 50, Color.Cyan);
        public BoxCollider carCollider = new BoxCollider(50, 30, (int)ColliderType.Slot_Car);
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
        public bool attacking = false;
        private int spinTicks = 90;
        public List<int> tags = new List<int>();
        public float currentSpeed;
        public float acceleration = 0.0f;
        public int completeLaps = 0;
        public bool finished = false;
        public int playerNum;

        public int popTimer = 0;
        public int popDuration = 30;


        public Slot_Car(Race _race, int _ln)
            : base()
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
            tags.Add((int)ColliderType.PickUp);
            tags.Add((int)ColliderType.Slot_Car);
            SetHitbox(50, 30, (int)ColliderType.Slot_Car);
            carCollider.CenterOrigin();
            carCollider.Entity = this;
            
        }
       
        public override void Update()
        {
            PopUp();
            if (!spinning)
                Steer();
            if (theRace.currentState == RaceState.RaceBegin)
                return;


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
                if (otherCah.attacking && otherCah.Lane == Lane)
                {
                    spinning = true;
                }
                else if (!otherCah.attacking)
                {
                    velocity.X = velocity.X * 0.5f;
                    velocity.Y = velocity.Y * 0.5f;
                }

            }
            if (spinning)
                SpinOut();
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
                nodesPassed++;
                if (nodeIndex == theRace.theTrack.thePieces[pieceIndex].theLanes[Lane].theNodes.Count())
                {
                    nodeIndex = 0;
                    pieceIndex++;
                    if (pieceIndex == theRace.theTrack.thePieces.Count())
                    {
                        pieceIndex = 0;
                        completeLaps++;
                        if (completeLaps == theRace.totalLaps)
                        {
                            finished = true;
                            theRace.carsFin++;
                        }
                    }
                }
                nextNode = nodeIndex + 1;
                targetNode = theRace.theTrack.thePieces[pieceIndex].theLanes[Lane].theNodes[nodeIndex];
                SteerVec = targetNode.localSpace;
            }

            Vector2 toTarget = SteerVec - position;

            toTarget.Normalize();
            velocity += toTarget * acceleration;

            if (velocity.Length > maxSpeed)
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
        }

        public void PopUp()
        {
            if (popTimer > 0)
            {
                if (popTimer > popDuration / 2)
                {
                    this.carImage.Scale = this.carImage.ScaleX + 0.075f;
                }
                else this.carImage.Scale = this.carImage.ScaleX - 0.075f;
                popTimer--;
            }
            else this.carImage.Scale = 1;
        }
    }
}
