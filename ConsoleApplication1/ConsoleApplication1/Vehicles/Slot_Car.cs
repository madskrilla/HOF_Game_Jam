﻿using System;
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
        public Vector2 position;
        public Race theRace;
        public Image carImage;
        public BoxCollider carCollider;
        public Speed currentSpeed;
        public Vector2 steerVec;
        public Vector2 acceleration;
        private Node target;

        public PickUp currentPickup;

        public int nodesPassed = 0;

        public Slot_Car(Race _race) : base()
        {
            theRace = _race;
            steerVec = new Vector2();
            position = new Vector2();
        }

        public override void Render()
        {
            base.Render();
            
        }
        public override void Update()
        {
            base.Update();
        }

        private void Steer()
        {
            float dist = new float();
            steerVec = convertToWorld(target.localSpace);
            position.X = X;
            position.Y = Y;
            dist = Vector2.Distance(steerVec, position);

            if(dist < 10)
                target = target.nextNode;

            steerVec = steerVec -acceleration;
           
        }

        private Vector2 convertToWorld(Vector2 _local)
        {
            return Vector2.Zero;
        }

       
    }
}
