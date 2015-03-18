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
        public Image carImage;
        public BoxCollider carCollider;
        public Speed currentSpeed;
        public Vector2 acceleration;

        public PickUp currentPickup;

        public int nodesPassed = 0;

        public Slot_Car(Race _race) : base()
        {
            theRace = _race;
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
