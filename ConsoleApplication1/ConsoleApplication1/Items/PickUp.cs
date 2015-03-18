using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Items
{
    class PickUp : Item_Base
    {
        public int respawnTimer;
        public PickUp() : base()
        {

        }

        public void Execute()
        {

        }

        // TO WHOEVER IS CODING THIS: dont actually return a base pickup
        // generate a random child pickup, this is just to make the strawman compile
        // also set the timer  and deactivate rendering/collision of this plz n thx
        public PickUp GenerateRandom()
        {
            PickUp temp = new PickUp();
            return temp;
        }

        public override void Update()
        {
            respawnTimer--;
            base.Update();
        }
    }
}
