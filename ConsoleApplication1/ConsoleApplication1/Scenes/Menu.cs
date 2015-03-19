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
        public enum TabedMenuButtons { options_Back = 0, options_otherButton, credits_Back }
        public int currentSelection, cursorX_offset, cursorY_offset;
        public Image cursor_Image, backArrow_Image;
        public Image PlayButton_Image, OptionsButton_Image, CreditsButton_Image, ExitButton_Image;
        bool Options, OptionsTab_Close, Play, Credits, CreditsTab_Close, Exit;

        public Image options_otherButton;

        public Menu() : base()
        {
            currentSelection = (int)MenuButtons.MB_Play;
            cursorX_offset = 0;
            cursorY_offset = 0;

            cursor_Image = new Image("Assets/Images/Menu_Cursor.png");
            backArrow_Image = new Image("Assets/Images/BackArrow.png");
            PlayButton_Image = new Image("Assets/Images/Menu_Play.png");
            OptionsButton_Image = new Image("Assets/Images/Menu_Options.png");
            CreditsButton_Image = new Image("Assets/Images/Menu_Credits.png");
            ExitButton_Image = new Image("Assets/Images/Menu_Exit.png");

            options_otherButton = new Image("Assets/Images/otherButton.png");

            backArrow_Image.SetPosition(192, 834);
            PlayButton_Image.SetPosition(160f, 128f);
            OptionsButton_Image.SetPosition(160f, 350f);
            CreditsButton_Image.SetPosition(160f, 572f);
            ExitButton_Image.SetPosition(160f, 794f);

            options_otherButton.SetPosition(192, 128);

            Options = false;
            OptionsTab_Close = false;
            Play = false;
            Credits = false;
            CreditsTab_Close = false;
            Exit = false;
        }

        public override void Update()
        {
            base.Update();

#region Main Menu Tab

            //main menu input check
            if (!Options && !Play && !Credits)
	        {
		        if(Globals.PlayerOne.Controller.Button(Controls.KeyUP).Pressed)//move up
                {
                    currentSelection--;
                    if (currentSelection < (int)MenuButtons.MB_Play)
                        currentSelection = (int)MenuButtons.MB_Exit;
                }
                else if (Globals.PlayerOne.Controller.Button(Controls.KeyDown).Pressed)//move down
                {
                   currentSelection++;
                    if (currentSelection > (int)MenuButtons.MB_Exit)
                        currentSelection = (int)MenuButtons.MB_Play;
                } 

                //enter checks
                if (Globals.PlayerOne.Controller.Button(Controls.Enter).Pressed && currentSelection == (int)MenuButtons.MB_Exit)
                    Exit = true;
                else if (Globals.PlayerOne.Controller.Button(Controls.Enter).Pressed && currentSelection == (int)MenuButtons.MB_Options)
                {
                    Options = true;
                    currentSelection = (int)TabedMenuButtons.options_otherButton;
                }
                else if (Globals.PlayerOne.Controller.Button(Controls.Enter).Pressed && currentSelection == (int)MenuButtons.MB_Play)
                    Play = true;
                else if (Globals.PlayerOne.Controller.Button(Controls.Enter).Pressed && currentSelection == (int)MenuButtons.MB_Credits)
                    Credits = true;
            }

            //move image based on selecting
            if (Options && OptionsButton_Image.Left < 1496)
                OptionsButton_Image.SetPosition(OptionsButton_Image.Left + 25f, 350f);
            else if (Play && PlayButton_Image.Left < 1496)
                PlayButton_Image.SetPosition(PlayButton_Image.Left + 25f, 128f);
            else if (Credits && CreditsButton_Image.Left < 1496)
                CreditsButton_Image.SetPosition(CreditsButton_Image.Left + 25f, 572f);

            cursor_Image.SetPosition(525, 180 + (currentSelection * 222f));

            //button image wrapping 
            if (OptionsButton_Image.Left > 1920)//options car wrap
                OptionsButton_Image.SetPosition(-352f, 350f);
            else if (OptionsButton_Image.Left < 160)
                OptionsButton_Image.SetPosition(OptionsButton_Image.Left + 25f, 350f);

            if (CreditsButton_Image.Left > 1920)//credits car wrap
                CreditsButton_Image.SetPosition(-352f, 572f);
            else if (CreditsButton_Image.Left < 160)
                CreditsButton_Image.SetPosition(CreditsButton_Image.Left + 25f, 572f);

#endregion

#region Option Tab

            //Options pulled over tab update
            if (Options && OptionsButton_Image.Left >= 1496)
            {
                //input check for option tab cursor
                if (Globals.PlayerOne.Controller.Button(Controls.KeyUP).Pressed)//move up
                {
                    currentSelection--;
                    if (currentSelection < (int)TabedMenuButtons.options_Back)
                        currentSelection = (int)TabedMenuButtons.options_otherButton;
                }
                else if (Globals.PlayerOne.Controller.Button(Controls.KeyDown).Pressed)//move down
                {
                    currentSelection++;
                    if (currentSelection > (int)TabedMenuButtons.options_otherButton)
                        currentSelection = (int)TabedMenuButtons.options_Back;
                }

                //set cursor position based on what its on
                if (currentSelection == (int)TabedMenuButtons.options_otherButton)
                    cursor_Image.SetPosition(458, 160);
                else if (currentSelection == (int)TabedMenuButtons.options_Back)
                    cursor_Image.SetPosition(458, 875);

                if (Globals.PlayerOne.Controller.Button(Controls.Enter).Pressed && currentSelection == (int)TabedMenuButtons.options_Back)
                    OptionsTab_Close = true;


                if (OptionsTab_Close && OptionsButton_Image.Left < 1920)
                {
                    //move everything on this tab to the right
                    OptionsButton_Image.SetPosition(OptionsButton_Image.Left + 25f, OptionsButton_Image.Y);
                    backArrow_Image.SetPosition(backArrow_Image.Left + 25f, backArrow_Image.Y);
                    cursor_Image.SetPosition(cursor_Image.Left + 25f, cursor_Image.Y);
                    options_otherButton.SetPosition(options_otherButton.Left + 25f, options_otherButton.Y);
                }

                if (OptionsTab_Close && OptionsButton_Image.Left > 1920)
                {
                     Options = false;
                     OptionsTab_Close = false;
                     backArrow_Image.SetPosition(192, 834);
                }
            }

#endregion

#region Credits Tab

            if (Credits && CreditsButton_Image.Left >= 1496)
            {
                currentSelection = (int)TabedMenuButtons.credits_Back;
                cursor_Image.SetPosition(458, 875);

                if (Globals.PlayerOne.Controller.Button(Controls.Enter).Pressed && currentSelection == (int)TabedMenuButtons.credits_Back)
                    CreditsTab_Close = true;

                if (CreditsTab_Close && CreditsButton_Image.Left < 1920)
                {
                    CreditsButton_Image.SetPosition(CreditsButton_Image.Left + 25f, CreditsButton_Image.Y);
                    backArrow_Image.SetPosition(backArrow_Image.Left + 25f, backArrow_Image.Y);
                    cursor_Image.SetPosition(cursor_Image.Left + 25f, cursor_Image.Y);
                }

                if (CreditsTab_Close && CreditsButton_Image.Left > 1920)
                {
                    Credits = false;
                    CreditsTab_Close = false;
                    backArrow_Image.SetPosition(192, 834);
                }
            }

#endregion

#region Exit Tab

            if (Exit && ExitButton_Image.Left < 1920)
                ExitButton_Image.SetPosition(ExitButton_Image.Left + 25f, ExitButton_Image.Y);

            if (Exit && ExitButton_Image.Left > 1920)
                Game.Close();

#endregion

        }



        public override void Render()
        {
            base.Render();

            Draw.Rectangle(32, 32, 1856, 1016, Color.Mix(Color.Black, Color.Gray));

            if (Exit)
            {
                Draw.Rectangle(ExitButton_Image.Left - 2050, 32, 1948, 1016, Color.Black);
                ExitButton_Image.Render();
            }
            else
            {
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
                else if (Options)
                {
                    Draw.Rectangle(OptionsButton_Image.Left - 1350, 32, 1248, 1016, Color.Gray);
                    OptionsButton_Image.Render();

                    if (OptionsButton_Image.Left >= 1496)
                    {
                        cursor_Image.Render();
                        backArrow_Image.Render();
                        options_otherButton.Render();
                    }

                }
                else if (Play)
                {
                    PlayButton_Image.Render();
                }
                else if (Credits)
                {
                    Draw.Rectangle(CreditsButton_Image.Left - 1350, 32, 1248, 1016, Color.Green);
                    CreditsButton_Image.Render();

                    if (CreditsButton_Image.Left >= 1496)
                    {
                        cursor_Image.Render();
                        backArrow_Image.Render();
                    }
                }
            }
        }


    }
}
