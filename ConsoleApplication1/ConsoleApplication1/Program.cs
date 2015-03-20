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
            Game game = new Game("Slotcar Game", 1920, 1080, 60, false);
            game.Color = new Color(0x5e5b5b);
            Globals.PlayerOne = game.AddSession("P1");
            Globals.PlayerTwo = game.AddSession("P2");

            Globals.PlayerOne.Controller = new ControllerXbox360(0);
            Globals.PlayerTwo.Controller = new ControllerXbox360(1);

            Globals.PlayerOne.Controller.AddButton(Controls.Accelerate);
            Globals.PlayerOne.Controller.AddButton(Controls.SwapLaneLeft);
            Globals.PlayerOne.Controller.AddButton(Controls.SwapLaneRight);
            Globals.PlayerOne.Controller.AddButton(Controls.UseItem);
            Globals.PlayerOne.Controller.AddButton(Controls.KeyUP);
            Globals.PlayerOne.Controller.AddButton(Controls.KeyDown);
            Globals.PlayerOne.Controller.AddButton(Controls.Escape);
            Globals.PlayerOne.Controller.AddButton(Controls.Enter);
            Globals.PlayerOne.Controller.AddButton(Controls.Back);
            Globals.PlayerOne.Controller.AddButton(Controls.Pause);

            Globals.PlayerTwo.Controller.AddButton(Controls.Accelerate);
            Globals.PlayerTwo.Controller.AddButton(Controls.SwapLaneLeft);
            Globals.PlayerTwo.Controller.AddButton(Controls.SwapLaneRight);
            Globals.PlayerTwo.Controller.AddButton(Controls.UseItem);
            Globals.PlayerTwo.Controller.AddButton(Controls.KeyUP);
            Globals.PlayerTwo.Controller.AddButton(Controls.KeyDown);
            Globals.PlayerTwo.Controller.AddButton(Controls.Escape);
            Globals.PlayerTwo.Controller.AddButton(Controls.Enter);
            Globals.PlayerTwo.Controller.AddButton(Controls.Pause);

            Globals.PlayerOne.Controller.Button(Controls.Accelerate).AddKey(Key.Space);
            Globals.PlayerOne.Controller.Button(Controls.SwapLaneLeft).AddKey(Key.Left);
            Globals.PlayerOne.Controller.Button(Controls.SwapLaneRight).AddKey(Key.Right);
            Globals.PlayerOne.Controller.Button(Controls.UseItem).AddKey(Key.LControl);
            Globals.PlayerOne.Controller.Button(Controls.KeyUP).AddKey(Key.Up);
            Globals.PlayerOne.Controller.Button(Controls.KeyDown).AddKey(Key.Down);
            Globals.PlayerOne.Controller.Button(Controls.Escape).AddKey(Key.Escape);
            Globals.PlayerOne.Controller.Button(Controls.Pause).AddKey(Key.Escape);
            Globals.PlayerOne.Controller.Button(Controls.Enter).AddKey(Key.Return);
            Globals.PlayerOne.Controller.Button(Controls.Back).AddKey(Key.Back);

            Globals.digestiveIntro.Volume = Globals.musicVolume;
            Globals.digestiveLoop.Volume = Globals.musicVolume;
            Globals.PlayerOne.Controller.Button(Controls.Accelerate).AddAxisButton(AxisButton.ZMinus,0);
            Globals.PlayerOne.Controller.Button(Controls.SwapLaneLeft).AddAxisButton(AxisButton.XMinus,0);
            Globals.PlayerOne.Controller.Button(Controls.SwapLaneRight).AddAxisButton(AxisButton.XPlus,0);
            Globals.PlayerOne.Controller.Button(Controls.UseItem).AddJoyButton(ControllerXbox360.JoyButtonX,0);
            Globals.PlayerOne.Controller.Button(Controls.KeyUP).AddAxisButton(AxisButton.YMinus,0);
            Globals.PlayerOne.Controller.Button(Controls.KeyDown).AddAxisButton(AxisButton.YPlus,0);
            Globals.PlayerOne.Controller.Button(Controls.Escape).AddJoyButton(ControllerXbox360.JoyButtonB, 0);
            Globals.PlayerOne.Controller.Button(Controls.Pause).AddJoyButton(ControllerXbox360.JoyButtonStart, 0);
            Globals.PlayerOne.Controller.Button(Controls.Enter).AddJoyButton(ControllerXbox360.JoyButtonA,0);

            Globals.PlayerTwo.Controller.Button(Controls.Accelerate).AddAxisButton(AxisButton.ZMinus,1);
            Globals.PlayerTwo.Controller.Button(Controls.SwapLaneLeft).AddAxisButton(AxisButton.XMinus,1);
            Globals.PlayerTwo.Controller.Button(Controls.SwapLaneRight).AddAxisButton(AxisButton.XPlus,1);
            Globals.PlayerTwo.Controller.Button(Controls.UseItem).AddJoyButton(ControllerXbox360.JoyButtonX,1);
            Globals.PlayerTwo.Controller.Button(Controls.KeyUP).AddAxisButton(AxisButton.YMinus,1);
            Globals.PlayerTwo.Controller.Button(Controls.KeyDown).AddAxisButton(AxisButton.YPlus,1);
            Globals.PlayerTwo.Controller.Button(Controls.Escape).AddJoyButton(ControllerXbox360.JoyButtonB, 1);
            Globals.PlayerTwo.Controller.Button(Controls.Pause).AddJoyButton(ControllerXbox360.JoyButtonStart, 1);
            Globals.PlayerTwo.Controller.Button(Controls.Enter).AddJoyButton(ControllerXbox360.JoyButtonA,1);

            game.FirstScene = new Menu();

            game.Start();
        }
    }
}
