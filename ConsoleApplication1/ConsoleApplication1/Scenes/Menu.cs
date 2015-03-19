using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace ConsoleApplication1.Scenes
{
    class Menu : Scene
    {
        public enum MenuButtons { MB_Play = 0, MB_Options = 1, MB_Credits = 2, MB_Exit = 3 }
        public int currentSelection;
        public Image cursor_Image;

        public Image PlayButton_Image;
        public Image OptionsButton_Image;
        public Image CreditsButton_Image;
        public Image ExitButton_Image;
        bool Options;
        bool Play;
        bool Credits;

        public Menu()
            : base()
        {
            currentSelection = (int)MenuButtons.MB_Play;
            cursor_Image = new Image("Assets/Images/Menu_Cursor.png");
            PlayButton_Image = new Image("Assets/Images/Menu_Play.png");
            OptionsButton_Image = new Image("Assets/Images/Menu_Options.png");
            CreditsButton_Image = new Image("Assets/Images/Menu_Credits.png");
            ExitButton_Image = new Image("Assets/Images/Menu_Exit.png");

            PlayButton_Image.SetPosition(160f, 128f);
            OptionsButton_Image.SetPosition(160f, 350f);
            CreditsButton_Image.SetPosition(160f, 572f);
            ExitButton_Image.SetPosition(160f, 794f);
            Options = false;
            Play = false;
            Credits = false;
        }

        public override void Update()
        {
            base.Update();

            //input check
            if (Globals.PlayerOne.Controller.Button(Controls.KeyUP).Pressed)//move up
            {
                currentSelection--;
                if (currentSelection < (int)MenuButtons.MB_Play)
                    currentSelection = (int)MenuButtons.MB_Exit;
            }
            if (Globals.PlayerOne.Controller.Button(Controls.KeyDown).Pressed)//move down
            {
                currentSelection++;
                if (currentSelection > (int)MenuButtons.MB_Exit)
                    currentSelection = (int)MenuButtons.MB_Play;

            }
            if (!Options && !Play && !Credits)
            {
                if (Globals.PlayerOne.Controller.Button(Controls.Enter).Pressed && currentSelection == (int)MenuButtons.MB_Exit)
                    Game.Close();
                else if (Globals.PlayerOne.Controller.Button(Controls.Enter).Pressed && currentSelection == (int)MenuButtons.MB_Options)
                    Options = true;
                else if (Globals.PlayerOne.Controller.Button(Controls.Enter).Pressed && currentSelection == (int)MenuButtons.MB_Play)
                    Play = true;
                else if (Globals.PlayerOne.Controller.Button(Controls.Enter).Pressed && currentSelection == (int)MenuButtons.MB_Credits)
                    Credits = true;
            }

            if (Options && OptionsButton_Image.Left < 1496)
            {
                OptionsButton_Image.SetPosition(OptionsButton_Image.Left + 25, 350f);
            }
            else if (Play && PlayButton_Image.Left < 1496)
            {
                PlayButton_Image.SetPosition(PlayButton_Image.Left + 25, 128f);
            }
            else if (Credits && CreditsButton_Image.Left < 1496)
            {
                CreditsButton_Image.SetPosition(CreditsButton_Image.Left + 25, 572f);
            }
            cursor_Image.SetPosition(525, 180 + (currentSelection * 222f));

        }

        public override void Render()
        {
            base.Render();
            if (!Options && !Play && !Credits)
            {
                Draw.Rectangle(800, 64, 928, 192, Color.Blue);
                Draw.Rectangle(736, 288, 1056, 664, Color.Yellow);
                cursor_Image.Render();
                PlayButton_Image.Render();
                OptionsButton_Image.Render();
                CreditsButton_Image.Render();
                ExitButton_Image.Render();
            }
            else if(Options)
            {
                Draw.Rectangle(OptionsButton_Image.Left - 1350, 32, 1248, 1016, Color.Gray);
                OptionsButton_Image.Render();

            }
            else if (Play)
            {
                PlayButton_Image.Render();
            }
            else
            {
                Draw.Rectangle(CreditsButton_Image.Left - 1350, 32, 1248, 1016, Color.Green);
                CreditsButton_Image.Render();
            }

        }
        
    }
}
