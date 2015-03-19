using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using ConsoleApplication1.Scenes;



namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game("Slotcar Game", 1500, 900, 60, false);

            Globals.PlayerOne = game.AddSession("P1");

            Globals.PlayerOne.Controller = new ControllerXbox360();
            Globals.PlayerOne.Controller.AddButton(Controls.Accelerate);
            Globals.PlayerOne.Controller.AddButton(Controls.SwapLaneLeft);
            Globals.PlayerOne.Controller.AddButton(Controls.SwapLaneRight);
            Globals.PlayerOne.Controller.AddButton(Controls.UseItem);
            Globals.PlayerOne.Controller.AddButton(Controls.KeyUP);
            Globals.PlayerOne.Controller.AddButton(Controls.KeyDown);
            Globals.PlayerOne.Controller.AddButton(Controls.Escape);
            Globals.PlayerOne.Controller.AddButton(Controls.Enter);

            Globals.PlayerOne.Controller.Button(Controls.Accelerate).AddKey(Key.Space);
            Globals.PlayerOne.Controller.Button(Controls.SwapLaneLeft).AddKey(Key.Left);
            Globals.PlayerOne.Controller.Button(Controls.SwapLaneRight).AddKey(Key.Right);
            Globals.PlayerOne.Controller.Button(Controls.UseItem).AddKey(Key.LControl);
            Globals.PlayerOne.Controller.Button(Controls.KeyUP).AddKey(Key.Up);
            Globals.PlayerOne.Controller.Button(Controls.KeyDown).AddKey(Key.Down);
            Globals.PlayerOne.Controller.Button(Controls.Escape).AddKey(Key.Escape);
            Globals.PlayerOne.Controller.Button(Controls.Enter).AddKey(Key.Return);

            //game.FirstScene = new Race();
            game.FirstScene = new Menu();

            game.Start();
        }
    }
}
