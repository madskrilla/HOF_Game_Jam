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
    class Player : Slot_Car
    {
        Session player;
        public Player(Race _race, Session _player) : base(_race)
        {
            X = Game.Instance.HalfWidth;
            Y = Game.Instance.HalfHeight;
            player = _player;
        }

        public override void Update()
        {
            getInput();
           // carImage.Angle = 40;
            base.Update();
        }

        public void getInput()
        {
            if(player.Controller.Button(Controls.Accelerate).Down)
            {
                //acceleration.X = 0.1f;
                acceleration.Y = -0.1f * (float)Math.Cos((Math.PI / 180) * carImage.Angle);
                acceleration.X = -0.1f * (float)Math.Sin((Math.PI / 180) * carImage.Angle);
            }
            else if(currentSpeed.Length >= 0.01f)
            {
                if (Math.Abs(currentSpeed.Y) >= 0.01f)
                {
                    if (currentSpeed.Y * (float)Math.Cos((Math.PI / 180) * carImage.Angle) > 0) acceleration.Y = -0.05f * (float)Math.Sin((Math.PI / 180) * carImage.Angle);
                    if (currentSpeed.Y * (float)Math.Cos((Math.PI / 180) * carImage.Angle) < 0) acceleration.Y = +0.05f * (float)Math.Sin((Math.PI / 180) * carImage.Angle);
                    
                    Console.WriteLine("Y SPEED: " + currentSpeed.Y.ToString()); 
                }
                if (Math.Abs(currentSpeed.X )>= 0.01f)
                {
                    if (currentSpeed.X * (float)Math.Sin((Math.PI / 180) * carImage.Angle) > 0) acceleration.X = -0.05f * (float)Math.Sin((Math.PI / 180) * carImage.Angle);
                    if (currentSpeed.X * (float)Math.Sin((Math.PI / 180) * carImage.Angle) < 0) acceleration.X = +0.05f * (float)Math.Sin((Math.PI / 180) * carImage.Angle);
                    //if (Math.Abs(currentSpeed.Y) < 0.0f) acceleration.Y = 0;
                    //if (Math.Abs(currentSpeed.X) < 0.0f) acceleration.X = 0;
                    Console.WriteLine("X SPEED: " + currentSpeed.X.ToString());
                }
            }
        }
    }
}
