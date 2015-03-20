using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using ConsoleApplication1.Vehicles;
using ConsoleApplication1.Track_Elements;
using ConsoleApplication1.Scenes;

namespace ConsoleApplication1.Items
{

    class Missle : PickUp
    {
        public Vector2 SteerVec;
        public Node targetNode = new Node();
        public Vector2 position;
        public Race theRace;
        public Vector2 acceleration = new Vector2(3, 3);
        public Vector2 right = new Vector2(1, 0);
        public Vector2 up = new Vector2(0, -1);
        public Vector2 velocity;
        public float maxSpeed = 13;
        public int nodeIndex = 0;
        public int nextNode = 1;
        public int pieceIndex = 0;
        public int currLane;
        private bool active = false;

        public Missle(Slot_Car _owner, Race race)
            : base( race)
        {
            owner = _owner;
            theRace = race;
            itemImage = new Image("Assets/Images/missle.png");
            SetGraphic(itemImage);
            itemImage.CenterOrigin();
            itemType = ItemType.Missle;

            itemCollider = new BoxCollider(itemImage.Width, itemImage.Height, (int)ColliderType.PickUpUse);
            SetCollider(itemCollider);
            this.itemCollider.Collidable = false;
            this.itemImage.Visible = false;
            owner.theRace.Add(this);
            active = false;


        }


        private void Steer()
        {
            SteerVec = targetNode.localSpace;
            position.X = X;
            position.Y = Y;

            float dist = Vector2.Distance(SteerVec, position);

            if (dist < 45)
            {

                nodeIndex = nextNode;

                if (nodeIndex == theRace.theTrack.thePieces[pieceIndex].theLanes[currLane].theNodes.Count())
                {
                    nodeIndex = 0;
                    pieceIndex++;
                    if (pieceIndex == theRace.theTrack.thePieces.Count())
                    {
                        pieceIndex = 0;
                    }
                }
                nextNode = nodeIndex + 1;
                targetNode = theRace.theTrack.thePieces[pieceIndex].theLanes[currLane].theNodes[nodeIndex];
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
                itemImage.Angle = (float)Math.Acos(Vector2.Dot(up, toTarget)) * (180 / 3.14f);
            }
            else
            {
                itemImage.Angle = -(float)Math.Acos(Vector2.Dot(up, toTarget)) * (180 / 3.14f);
            }
        }

        public override void Update()
        {
            if (active)
            {
                Steer();

                X += velocity.X;
                Y += velocity.Y;
                base.Update();
            }

        }

        public override void Execute()
        {
            active = true;
            this.itemCollider.Collidable = true;
            this.itemImage.Visible = true;
            currLane = owner.Lane;
            velocity = owner.velocity;
            velocity.Normalize();
            velocity *= maxSpeed;
            itemImage.Angle = owner.carImage.Angle;
            nextNode = owner.nextNode;
            nodeIndex = owner.nodeIndex;
            pieceIndex = owner.pieceIndex;
            X = owner.X;
            Y = owner.Y;
            targetNode = theRace.theTrack.thePieces[pieceIndex].theLanes[currLane].theNodes[nodeIndex];
            
        }

    }
}
