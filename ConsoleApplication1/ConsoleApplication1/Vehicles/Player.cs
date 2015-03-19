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
        public Player(Race _race, int _ln, Session _player) : base(_race, _ln)
        {

            player = _player;
            acceleration.X = 0;
            acceleration.Y = 0;
        }

        public override void Update()
        {
            getInput();
            base.Update();
        }

        public void getInput()
        {
            if(player.Controller.Button(Controls.Accelerate).Down)
            {
                //acceleration.X = 0.1f;
                acceleration.Y = 0.1f * (float)Math.Cos((Math.PI / 180) * carImage.Angle);
                acceleration.X = 0.1f * (float)Math.Sin((Math.PI / 180) * carImage.Angle);
            }
            else if (velocity.Length >= 0.01f)
            {
                if (Math.Abs(velocity.Y) >= 0.01f)
                {
                    if (velocity.Y * (float)Math.Cos((Math.PI / 180) * carImage.Angle) > 0) acceleration.Y = -0.5f * (float)Math.Cos((Math.PI / 180) * carImage.Angle);
                    if (velocity.Y * (float)Math.Cos((Math.PI / 180) * carImage.Angle) < 0) acceleration.Y = +0.5f * (float)Math.Cos((Math.PI / 180) * carImage.Angle);

                    Console.WriteLine("Y SPEED: " + velocity.Y.ToString());
                }
                if (Math.Abs(velocity.X) >= 0.01f)
                {
                    if (velocity.X * (float)Math.Sin((Math.PI / 180) * carImage.Angle) > 0) acceleration.X = -0.5f * (float)Math.Sin((Math.PI / 180) * carImage.Angle);
                    if (velocity.X * (float)Math.Sin((Math.PI / 180) * carImage.Angle) < 0) acceleration.X = +0.5f * (float)Math.Sin((Math.PI / 180) * carImage.Angle);
                    //if (Math.Abs(velocity.Y) < 0.0f) acceleration.Y = 0;
                    //if (Math.Abs(velocity.X) < 0.0f) acceleration.X = 0;
                    Console.WriteLine("X SPEED: " + velocity.X.ToString());
                }
            }
        }
    }
}
