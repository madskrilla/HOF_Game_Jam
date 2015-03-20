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
        public PolygonCollider carCollider;
        public float audioVolume;
        public Sound carRev = new Sound("Audio/EngineRev.wav");
        public bool revPlaying = false;
        public Sound carIdle = new Sound("Audio/EngineIdle.wav");
        public bool idlePlaying = false;
        public List<Sound> tireScreech = new List<Sound>();
        public bool tireScreechPlaying = false;
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
        public bool spinning = false;
        public bool attacking = false;
        private int spinTicks = 90;
        public List<int> tags = new List<int>();
        public float currentSpeed;
        public float acceleration = 0.0f;
        public int completeLaps = 0;
        public bool finished = false;
        public int playerNum;
        public Color playerCol;

        public int popTimer = 0;
        public int popDuration = 30;
        public int invulnTimer = 0;
        public int slowTimer = 0;
        public bool hasItem = false;

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
            playerNum = _ln + 1;

            targetNode = theRace.theTrack.thePieces[pieceIndex].theLanes[Lane].theNodes[nodeIndex];
            X = targetNode.localSpace.X;
            Y = targetNode.localSpace.Y;
            SetHitbox(50, 30, (int)ColliderType.Slot_Car);
            // Polygon box = new Polygon(new float[] { X, Y, X, Y + 50, X + 30, Y + 50, X + 30, Y});
            Polygon box = new Polygon(new float[] { 0, 0, 0, 50, 30, 50, 30, 0 });

            box.Scale(carImage.ScaleX, carImage.ScaleY, X, Y);
            box.Rotate(carImage.Angle, X, Y);
            carCollider = new PolygonCollider(box, ColliderType.Slot_Car);
            carCollider.SetOrigin(X, Y);
            SetCollider(carCollider);

            // SetHitbox(50, 30, (int)ColliderType.Slot_Car);
            carCollider.CenterOrigin();
            carCollider.Entity = this;
            switch (playerNum)
            {
                case 1:
                    playerCol = Color.Green;
                    break;
                case 2:
                    playerCol = Color.Blue;
                    break;
                case 3:
                    playerCol = Color.Yellow;
                    break;
                case 4:
                    playerCol = Color.Red;
                    break;
                default:
                    break;

            tireScreech.Add(new Sound("Audio/SpinOut1.wav"));
            tireScreech.Add(new Sound("Audio/SpinOut2.wav"));
            tireScreech.Add(new Sound("Audio/SpinOut3.wav"));

            audioVolume = 1.0f;
            }
        }
        public override void Update()
        {
            PopUp();
            if (!spinning)
                Steer();
            if (theRace.currentState == RaceState.RaceBegin)
            {

            }
           
            if (invulnTimer == 0)
            {
                

                var collider = carCollider.Collide(X, Y, ColliderType.Slot_Car);
                var itemHit = carCollider.Collide(X, Y, ColliderType.PickUpUse);
                var itemGet = carCollider.Collide(X, Y, ColliderType.PickUpBase);

                if (collider != null)
                {

                    Slot_Car otherCah = (Slot_Car)collider.Entity;
      
                    if (otherCah.attacking && otherCah.Lane == Lane && otherCah.invulnTimer == 0)
                    {
                        spinning = true;
                    if (!tireScreechPlaying)
                    {
                        tireScreech[Rand.Int(2)].Play();
                        tireScreechPlaying = true;
                    }
                    else if (!otherCah.attacking && otherCah.invulnTimer == 0)
                    {
                        velocity.X = velocity.X * 0.5f;
                        velocity.Y = velocity.Y * 0.5f;
                    }

                }
                if (itemGet != null)
                {
                    PickUp item = (PickUp)itemGet.Entity;
                    currentPickup = item.GenerateRandom(this);

                    hasItem = true;
                }
                if (itemHit != null)
                {
                    PickUp item = (PickUp)itemHit.Entity;
                    if (item != currentPickup)
                    {
                        if (item.itemType == ItemType.Bomb)
                        {
                            //Add Pop up
                            spinning = true;
                            popDuration = 30;
                            item.RemoveSelf();
                            currentPickup = null;

                        }
                        else if (item.itemType == ItemType.Rocket)
                        {
                            //Add Pop up
                            spinning = true;
                            popDuration = 30;
                            item.RemoveSelf();
                            currentPickup = null;


                        }
                        else if (item.itemType == ItemType.Missle)
                        {
                            //Add Pop up
                            spinning = true;
                            popDuration = 30;

                            item.RemoveSelf();
                            currentPickup = null;

                        }
                        else if (item.itemType == ItemType.OilSlick)
                        {


                            maxSpeed = 5;
                            item.RemoveSelf();
                            currentPickup = null;

                        }
                        invulnTimer += 120;
                    }
                } 
            }
            else
                invulnTimer--;

            if (slowTimer > 0)
            {
                slowTimer--;
                if (slowTimer <= 0)
                {
                    slowTimer = 0;
                    maxSpeed = 10;
                }
            }
            if (spinning)
                SpinOut();
            
            X += velocity.X;
            Y += velocity.Y;
            PlayAudio();
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
                            theRace.finishOrder.Add(playerNum);
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
                tireScreechPlaying = false;
                return;
            }
            acceleration = 0;
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

        public void PlayAudio()
        {
            carRev.Volume = audioVolume;
            carIdle.Volume = audioVolume;
            if (acceleration > 0.2)
            {
                carIdle.Stop();
                idlePlaying = false;
                if (!revPlaying)
                {
                    carRev.Play();
                    revPlaying = true;
    }
                carRev.Pitch = Util.Scale(acceleration, 0.0f, 10.0f, 0.2f, 1.0f);
            }
            else
            {
                carRev.Stop();
                revPlaying = false;
                if (!idlePlaying)
                {
                    carIdle.Play();
                    idlePlaying = true;
                }
            }
        }
    }
}
