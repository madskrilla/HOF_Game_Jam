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
        public Speed currentSpeed = new Speed(10,true);
        public Vector2 acceleration;

        public PickUp currentPickup;

        public int nodesPassed = 0;

        public Slot_Car(Race _race) : base()
        {
            SetGraphic(carImage);
            carImage.CenterOrigin();
            theRace = _race;
        }

        public override void Update()
        {
            currentSpeed.X += acceleration.X;
            currentSpeed.Y += acceleration.Y;

            if (Math.Abs(currentSpeed.X) < 0.05f) currentSpeed.X = 0;
            if (Math.Abs(currentSpeed.Y) < 0.05f) currentSpeed.Y = 0;

            X += currentSpeed.X;
            Y += currentSpeed.Y;
            base.Update();
        }
    }
}
