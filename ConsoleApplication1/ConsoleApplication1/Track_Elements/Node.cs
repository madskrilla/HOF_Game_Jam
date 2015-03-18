using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace ConsoleApplication1.Track_Elements
{
    class Node 
    {
        public Vector2 localSpace;
        public Node nextNode;
        public Image temp = Image.CreateRectangle(5);
        
        public Node()
        {
            temp.X = localSpace.X;
            temp.Y = localSpace.Y;
        }
    }
}
