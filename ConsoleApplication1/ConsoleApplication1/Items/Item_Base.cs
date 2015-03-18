using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace ConsoleApplication1.Items
{
    class Item_Base : Entity
    {
        public Image itemImage;
        public BoxCollider itemCollider;
        public Vector2 tileSpacePos;
        public Item_Base() : base()
        {

        }
    }
}
