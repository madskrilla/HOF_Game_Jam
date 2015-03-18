using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using ConsoleApplication1.Scenes;
using ConsoleApplication1.Vehicles;

namespace ConsoleApplication1
{
    class AIDriver : Slot_Car
    {
        public AIDriver(Race _race, int _ln) : base(_race, _ln)
        {
            driverType = DriverType.AI;


        }
    }
}
