using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace ConsoleApplication1.Particles
{
    public class ParticleFlyweight
    {
        public float min_lifeTime = 1;
        public float max_lifeTime = 1;

        public float angleRads = 0;
        public float speed = 25;
        public float radius = 0;
        public int spreadAngle = 360;

        public float rotation = (float)Math.PI;
        public float rotationSpeed = 0;
        public float Emitter_Rotation = 0;

        public float radialAccel = 0;
        public float tangentialAccel = 0;

        public Vector2 forces = new Vector2(0, 0);
        public Vector2 radial = new Vector2(0, 0);
        public Vector2 gravity = new Vector2(0, 0);

        public Color start_Color = new Color(1.0f, 0, 0, 1.0f);
        public Color end_Color = new Color(0, 1.0f, 0, 0);

        public Vector2 size = new Vector2(20, 20);
        public float start_Scale = 1;
        public float end_Scale = 1;

        public int TotalParticles = 180;
        public float EmissionRate = 10;

        public bool looping = true;
        public bool center = true;

        public string imagePath;

        
    }
    class Emitter : Entity
    {
        public ParticleFlyweight flyweight;

        float time = 0;

        public int particleCount = 0;           //num of alive particles
        public int totalParticles = 0;          //max particles
        public int particleIndex = 0;           //which particle
        public float emissionRate = 0;          // particles emitted per second
        public float emitCounter = 0;           // time passed since last update
        public float EmitterRotationSpeed = 0;
        public float angle = 0;

        public Vector2 emitterPos = new Vector2(250, 250);
        public bool bursted = false;
        public bool LoadedImage = false;
        public Vector2 size = new Vector2(0, 0);
          
        //might not need
        public Rectangle rectShape;
        public Vector2 velocity = new Vector2(0, 0);

        public List<Particle> particleList = new List<Particle>();

        public Image particleImage;

        Entity entity;

        public Emitter(Entity Object)
        {
            flyweight = new ParticleFlyweight();
            if (Object != null)
            {
                entity = Object;
            }
        }

        public void LoadEmitter(Vector2 _size, Color _color, string ImagePath, int _TotalParticles = 100, float _EmissionRate = 10, float _minLife = 0, float _maxLife = 1, float _speed = 0,
             int _spreadAngle = 360, float _rotationSpeed = 0, bool _looping = true)
        {
            flyweight.size = size = _size;
            flyweight.start_Color = _color;
            flyweight.TotalParticles = totalParticles = _TotalParticles;
            flyweight.EmissionRate = emissionRate = _EmissionRate;
            flyweight.min_lifeTime = _minLife;
            flyweight.max_lifeTime = _maxLife;
            flyweight.speed = _speed;
            flyweight.spreadAngle = _spreadAngle;
            flyweight.rotationSpeed = _rotationSpeed;
            flyweight.looping = _looping;
            flyweight.imagePath = ImagePath;
            
            Load(_TotalParticles);
        }
        public bool IsFull()
        {
            return particleCount == totalParticles;
        }
        public bool AddParticle(int i)
        {
            if (IsFull())
                return false;

            Particle p = particleList[i];
            p.SetGraphic(p.particleImg);
            
            InitParticle(ref p);

            //particleCount++;
            return true;
        }
        public void InitParticle(ref Particle p)
        {
            if (entity != null)
            {
                emitterPos.X = entity.X - (size.X / 2);
                emitterPos.Y = entity.Y - (size.Y / 2);
            }
                p.orgLife = p.life = Globals.numberGenerator.Next((int)flyweight.min_lifeTime, (int)flyweight.max_lifeTime);
                p.size = flyweight.size;

                EmitterRotationSpeed = flyweight.Emitter_Rotation;

                if (!flyweight.center)
                {
                    p.X = Globals.numberGenerator.Next((int)emitterPos.X, (int)(emitterPos.X + size.X));
                    p.Y = Globals.numberGenerator.Next((int)emitterPos.Y, (int)(emitterPos.Y + size.Y));
                }
                else
                {
                    p.X = emitterPos.X + size.X / 2;
                    p.Y = emitterPos.Y + size.Y / 2;
                }

                p.mod = Globals.numberGenerator.NextDouble();
                if (Globals.numberGenerator.Next() % 2 == 0)
                {
                    p.mod = -p.mod;
                }

                flyweight.angleRads = angle + (float)(Globals.numberGenerator.Next(flyweight.spreadAngle) * Math.PI / 180);
                float speed = Globals.numberGenerator.Next((int)flyweight.speed);

                float emitAngle = angle + Globals.numberGenerator.Next(flyweight.spreadAngle) / (float)(180f * Math.PI);
                float radius = flyweight.radius;
                p.X += (float)(radius * Math.Cos(emitAngle));
                p.Y += (float)(radius * Math.Sin(emitAngle));

                p.velocity.X = (float)(Math.Cos(flyweight.angleRads) * speed);
                p.velocity.Y = (float)(-Math.Sin(flyweight.angleRads) * speed);

                p.color = flyweight.start_Color;

                p.radialAccel = flyweight.radialAccel;
                p.tangentialAccel = flyweight.tangentialAccel;
                p.forces = flyweight.forces;
                p.radial = flyweight.radial;
                p.gravity = flyweight.gravity;
               
            
        }
        public void Load(int _numberOfParticles)
        {
            flyweight.TotalParticles = totalParticles =_numberOfParticles;

            for (int iter = 0; iter < flyweight.TotalParticles; iter++)
            {
                Particle newP = new Particle();
                if (newP != null)
                {
                    particleList.Add(newP);
                    this.Scene.Add(newP);
                    //newP.particleImg = new Image(flyweight.imagePath);
                    newP.particleImg = Image.CreateRectangle((int)flyweight.size.X, (int)flyweight.size.Y, flyweight.start_Color);
                    newP.particleImg.CenterOrigin();
                    AddParticle(iter);
                }
            }

            if (flyweight.looping)
            {
                particleCount = 0;
            }
        }
        private void UpdateParticle(ref Particle p,  int i)
        {
            p.life -= (1.0f / 60.0f);
            if (p.life > 0)
            {
                p.Visible = true;
                if ((p.X != emitterPos.X || p.Y != emitterPos.Y) && (p.radialAccel != 0 || p.tangentialAccel != 0))
                {
                    p.radial.X = p.X - emitterPos.X; 
                    p.radial.Y = p.Y - emitterPos.Y;

                    float length = (float)(Math.Sqrt(p.radial.X * p.radial.X + p.radial.Y * p.radial.Y));

                    p.radial.X /= length;
                    p.radial.Y /= length;
                }
                p.rotation += flyweight.rotationSpeed * (float)p.mod;

                p.tangential = p.radial;
                p.radial *= p.radialAccel;

                float newY = p.tangential.X;
                p.tangential.X = -p.tangential.Y;
                p.tangential.Y = newY;

                p.tangential *= p.tangentialAccel;

                p.forces.X = p.radial.X + p.tangential.X + p.gravity.X;
                p.forces.Y = p.radial.Y + p.tangential.X + p.gravity.Y;
                p.forces *= (1.0f / 60.0f);

                p.X += p.velocity.X;
                p.Y += p.velocity.Y;

                float lifeRatio = p.life / p.orgLife;
                p.scale = LERP(flyweight.start_Scale, flyweight.end_Scale, (1.0f - lifeRatio));

                p.color = new Color(LERP(flyweight.start_Color.R, flyweight.end_Color.R, 1.0f - lifeRatio), 
                    LERP(flyweight.start_Color.G, flyweight.end_Color.G, 1.0f - lifeRatio),
                    LERP(flyweight.start_Color.B, flyweight.end_Color.B, 1.0f - lifeRatio),
                    LERP(flyweight.start_Color.A, flyweight.end_Color.A, 1.0f - lifeRatio));

                if (p.particleImg != null)
                {
                    p.particleImg.Alpha = p.color.A;
                }
            }
            else
            {
                Particle temp = particleList[i];
                temp.Visible = false;
                particleList[i] = particleList[particleCount - 1];
                particleList[particleCount - 1] = temp;
                --particleCount;
                --particleIndex;
            }
        }
        public override void Update()
        {
            base.Update();
            angle += EmitterRotationSpeed;

            Particle p;
            emitterPos += (velocity);

            if (flyweight.looping)
            {
                if (emissionRate > 0)
                {
                   float rate = 1.0f / emissionRate;
                   emitCounter += 1.0f;
                    while (!IsFull() && emitCounter > rate)
                    {
                        AddParticle(particleIndex);
                        particleCount++;
                        emitCounter -= rate;
                    }
                }

                particleIndex = 0;

                while (particleIndex < particleCount)
                {
                    p = particleList[particleIndex];
                    UpdateParticle(ref p, particleIndex);
                    particleIndex++;
                }
            }
            else
            {
                particleIndex = 0;
                time += (1.0f / 60.0f);
                while (particleIndex < particleCount)
                {
                    if (!bursted)
                    {
                        for (int iter = 0; iter < particleList.Count; iter++)
                        {
                            AddParticle(iter);
                        }
                        bursted = true;
                    }
                    p = particleList[particleIndex];
                    UpdateParticle(ref p, particleIndex);
                    particleIndex++;
                }

                if (time > 5)
                {
                    bursted = false;
                    time = 0;
                }

            }
        }
        public float LERP(float start, float end, float percent)
        {
            float returnValue;

            returnValue = (end - start) * percent + start;

            return returnValue;
        }
    }
   
}
