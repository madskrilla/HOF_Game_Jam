using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;


namespace ConsoleApplication1.Particles
{
    class Particle : Entity
    {

        public float life;
        public float orgLife;

        public Color color;

        public float rotation;
        public float rotationSpeed;
        public float radialAccel;
        public float tangentialAccel;

        public Vector2 radial;
        public Vector2 tangential;
        public Vector2 forces;

        public Vector2 size;

        public float scale;

        public Vector2 velocity;
        public Vector2 gravity;

        public Rectangle rect;

        public double mod;

        public bool alive;
    }
}
