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
        public enum TabedMenuButtons { play_NumPlayers = 0, play_NumRounds, play_Back, play_Play, options_Back, options_Volume, credits_Back }
        public int currentSelection, cursorX_offset, cursorY_offset, volumeRectLength, play_numPlayers, play_numRounds;
        public Image cursor_Image, backArrow_Image, MainMenuBg_Image, FullSail_Image;
        public Image PlayButton_Image, OptionsButton_Image, CreditsButton_Image, ExitButton_Image;
        bool Play, PlayTab_Close, playTab_NumPlayers_bool, playTab_NumRounds_bool, Options, OptionsTab_Close, volumeChange, Credits, CreditsTab_Close, Exit, SwitchScenes;

        public Image options_VolumeButton;
        public Image play_PlayButton, play_numPlayers_Button, play_numRounds_Button;
        public Image play_Background_Image, credits_Background_Image, options_Background_Image;

        public Menu() : base()
        {
            currentSelection = (int)MenuButtons.MB_Play;
            cursorX_offset = 0;
            cursorY_offset = 0;
            volumeRectLength = 400;
            play_numPlayers = 1;
            play_numRounds = 10;

            cursor_Image = new Image("Assets/Images/Menu_Cursor.png");
            backArrow_Image = new Image("Assets/Images/BackArrow.png");
            PlayButton_Image = new Image("Assets/Images/Menu_Play.png");
            OptionsButton_Image = new Image("Assets/Images/Menu_Options.png");
            CreditsButton_Image = new Image("Assets/Images/Menu_Credits.png");
            ExitButton_Image = new Image("Assets/Images/Menu_Exit.png");
            FullSail_Image = new Image("Assets/Images/FullSail.png");
            MainMenuBg_Image = new Image("Assets/Images/MainMenuBg.png");

            options_VolumeButton = new Image("Assets/Images/VolumeButton.png");
            play_PlayButton = new Image("Assets/Images/PlayButton.png");
            play_numPlayers_Button = new Image("Assets/Images/NumOfPlayersButton.png");
            play_numRounds_Button = new Image("Assets/Images/NumOfLapsButton.png");

            play_Background_Image = new Image("Assets/Images/BlueFlag.png");
            options_Background_Image = new Image("Assets/Images/GrayFlag.png");
            credits_Background_Image = new Image("Assets/Images/GreenFlag.png");

            backArrow_Image.SetPosition(192, 834);
            PlayButton_Image.SetPosition(160f, 128f);
            OptionsButton_Image.SetPosition(160f, 350f);
            CreditsButton_Image.SetPosition(160f, 572f);
            ExitButton_Image.SetPosition(160f, 794f);
            FullSail_Image.SetPosition(736, 288);
            MainMenuBg_Image.SetPosition(32, 32);

            options_VolumeButton.SetPosition(192, 650);
            play_PlayButton.SetPosition(1000, 834);
            play_numPlayers_Button.SetPosition(200, 225);
            play_numRounds_Button.SetPosition(800, 225);

            Play = false;
            PlayTab_Close = false;
            playTab_NumPlayers_bool = false;
            playTab_NumRounds_bool = false;
            Options = false;
            OptionsTab_Close = false;
            Credits = false;
            CreditsTab_Close = false;
            Exit = false;
            SwitchScenes = false;
            volumeChange = false;
        }

        public override void Update()
        {
            base.Update();

            #region Main Menu Tab

            //main menu input check
            if (!Options && !Play && !Credits)
            {
                if (Globals.PlayerOne.Controller.Button(Controls.KeyUP).Pressed)//move up
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
                    currentSelection = (int)TabedMenuButtons.options_Volume;
                }
                else if (Globals.PlayerOne.Controller.Button(Controls.Enter).Pressed && currentSelection == (int)MenuButtons.MB_Play)
                {
                    Play = true;
                    currentSelection = (int)TabedMenuButtons.play_NumPlayers;
                }
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
            if (PlayButton_Image.Left > 1920)//play car wrap
                PlayButton_Image.SetPosition(-352f, 128f);
            else if (PlayButton_Image.Left < 160)
            {
                PlayButton_Image.SetPosition(PlayButton_Image.Left + 25f, 128f);
                if (PlayButton_Image.Left > 160)
                    PlayButton_Image.SetPosition(160f, 128f);
            }

            if (OptionsButton_Image.Left > 1920)//options car wrap
                OptionsButton_Image.SetPosition(-352f, 350f);
            else if (OptionsButton_Image.Left < 160)
            {
                OptionsButton_Image.SetPosition(OptionsButton_Image.Left + 25f, 350f);
                if (OptionsButton_Image.Left > 160)
                    OptionsButton_Image.SetPosition(160f, 350f);
            }

            if (CreditsButton_Image.Left > 1920)//credits car wrap
                CreditsButton_Image.SetPosition(-352f, 572f);
            else if (CreditsButton_Image.Left < 160)
            {
                CreditsButton_Image.SetPosition(CreditsButton_Image.Left + 25f, 572f);
                if (CreditsButton_Image.Left > 160)
                    CreditsButton_Image.SetPosition(160f, 572f);
            }


            #endregion

            #region Play Tab

            if (Play && PlayButton_Image.Left >= 1496)
            {
                if (!playTab_NumPlayers_bool && !playTab_NumRounds_bool)
                {
                    //input check 
                    if (Globals.PlayerOne.Controller.Button(Controls.KeyUP).Pressed || Globals.PlayerOne.Controller.Button(Controls.SwapLaneRight).Pressed)
                    {
                        currentSelection--;
                        if (currentSelection < (int)TabedMenuButtons.play_NumPlayers)
                            currentSelection = (int)TabedMenuButtons.play_Play;
                    }
                    else if (Globals.PlayerOne.Controller.Button(Controls.KeyDown).Pressed || Globals.PlayerOne.Controller.Button(Controls.SwapLaneLeft).Pressed)
                    {
                        currentSelection++;
                        if (currentSelection > (int)TabedMenuButtons.play_Play)
                            currentSelection = (int)TabedMenuButtons.play_NumPlayers;
                    }

                    if (Globals.PlayerOne.Controller.Button(Controls.Enter).Pressed && currentSelection == (int)TabedMenuButtons.play_Back)
                        PlayTab_Close = true;
                    if (Globals.PlayerOne.Controller.Button(Controls.Enter).Pressed && currentSelection == (int)TabedMenuButtons.play_Play)
                        SwitchScenes = true;
                    if (Globals.PlayerOne.Controller.Button(Controls.Enter).Pressed && currentSelection == (int)TabedMenuButtons.play_NumPlayers)
                        playTab_NumPlayers_bool = true;
                    if (Globals.PlayerOne.Controller.Button(Controls.Enter).Pressed && currentSelection == (int)TabedMenuButtons.play_NumRounds)
                        playTab_NumRounds_bool = true;
                }
                if (playTab_NumPlayers_bool)
                {
                    //input for numPlayers button
                    if (Globals.PlayerOne.Controller.Button(Controls.KeyUP).Pressed)
                    {
                        play_numPlayers++;
                        if (play_numPlayers > 2)
                            play_numPlayers = 1;
                    }
                    else if (Globals.PlayerOne.Controller.Button(Controls.KeyDown).Pressed)
                    {
                        play_numPlayers--;
                        if (play_numPlayers < 1)
                            play_numPlayers = 2;
                    }
                    else if (Globals.PlayerOne.Controller.Button(Controls.Back).Pressed)
                        playTab_NumPlayers_bool = false;
                }
                if (playTab_NumRounds_bool)
                {
                    //input for numRounds button
                    if (Globals.PlayerOne.Controller.Button(Controls.KeyUP).Pressed)
                    {
                        play_numRounds++;
                        if (play_numRounds > 30)
                            play_numRounds = 1;
                    }
                    else if (Globals.PlayerOne.Controller.Button(Controls.KeyDown).Pressed)
                    {
                        play_numRounds--;
                        if (play_numRounds < 1)
                            play_numRounds = 30;
                    }
                    else if (Globals.PlayerOne.Controller.Button(Controls.Back).Pressed)
                        playTab_NumRounds_bool = false;
                }

                if (currentSelection == (int)TabedMenuButtons.play_Play)
                    cursor_Image.SetPosition(925, 875);
                else if (currentSelection == (int)TabedMenuButtons.play_Back)
                    cursor_Image.SetPosition(458, 875);
                else if (currentSelection == (int)TabedMenuButtons.play_NumPlayers)
                    cursor_Image.SetPosition(600, 282);
                else if (currentSelection == (int)TabedMenuButtons.play_NumRounds)
                    cursor_Image.SetPosition(1275, 282);


                if (PlayTab_Close && PlayButton_Image.Left < 1920)
                {
                    PlayButton_Image.SetPosition(PlayButton_Image.Left + 25f, PlayButton_Image.Y);
                    //backArrow_Image.SetPosition(backArrow_Image.Left + 25f, backArrow_Image.Y);
                    //cursor_Image.SetPosition(cursor_Image.Left + 25f, cursor_Image.Y);
                    //play_PlayButton.SetPosition(play_PlayButton.Left + 25f, play_PlayButton.Y);
                }
                if (PlayTab_Close && PlayButton_Image.Left > 1920)
                {
                    Play = false;
                    PlayTab_Close = false;
                    backArrow_Image.SetPosition(192, 834);
                    play_PlayButton.SetPosition(1000, 834);
                }

                if (SwitchScenes)
                {
                    PlayButton_Image.SetPosition(PlayButton_Image.Left + 25f, PlayButton_Image.Y);
                    //backArrow_Image.SetPosition(backArrow_Image.Left + 25f, backArrow_Image.Y);
                    //cursor_Image.SetPosition(cursor_Image.Left + 25f, cursor_Image.Y);
                    //play_PlayButton.SetPosition(play_PlayButton.Left + 25f, play_PlayButton.Y);

                    if (PlayButton_Image.Left > 1900)
                    {
                        Play = false;
                        PlayTab_Close = false;
                        SwitchScenes = false;
                        backArrow_Image.SetPosition(192, 834);
                        play_PlayButton.SetPosition(1000, 834);

                        //switch scenes
                        Game.RemoveScene();
                        Game.AddScene(new Race(3, play_numPlayers));
                    }
                }
            }

            #endregion

            #region Options Tab

            //Options pulled over tab update
            if (Options && OptionsButton_Image.Left >= 1496)
            {
                //input check for option tab cursor
                if (!volumeChange)
                {
                    if (Globals.PlayerOne.Controller.Button(Controls.KeyUP).Pressed)//move up
                    {
                        currentSelection--;
                        if (currentSelection < (int)TabedMenuButtons.options_Back)
                            currentSelection = (int)TabedMenuButtons.options_Volume;
                    }
                    else if (Globals.PlayerOne.Controller.Button(Controls.KeyDown).Pressed)//move down
                    {
                        currentSelection++;
                        if (currentSelection > (int)TabedMenuButtons.options_Volume)
                            currentSelection = (int)TabedMenuButtons.options_Back;
                    } 
                }

                if (Globals.PlayerOne.Controller.Button(Controls.Enter).Pressed && currentSelection == (int)TabedMenuButtons.options_Back)
                    OptionsTab_Close = true;
                if (Globals.PlayerOne.Controller.Button(Controls.Enter).Pressed && currentSelection == (int)TabedMenuButtons.options_Volume)
                    volumeChange = true;

                if (currentSelection == (int)TabedMenuButtons.options_Volume)
                    cursor_Image.SetPosition(458, 682);
                else if (currentSelection == (int)TabedMenuButtons.options_Back)
                    cursor_Image.SetPosition(458, 875);

                if (volumeChange)
                {
                    //input check for volume
                    if (Globals.PlayerOne.Controller.Button(Controls.SwapLaneRight).Pressed)//increase volume
                    {
                        volumeRectLength += 40;
                        if (volumeRectLength > 400)
                            volumeRectLength = 400;
                    }
                    else if (Globals.PlayerOne.Controller.Button(Controls.SwapLaneLeft).Pressed)//decrease volume
                    {
                        volumeRectLength -= 40;
                        if (volumeRectLength < 0)
                            volumeRectLength = 0;
                    }
                    else if (Globals.PlayerOne.Controller.Button(Controls.Back).Pressed)
                    {
                        volumeChange = false;
                        //make it save the volume

                    }
                }

                if (OptionsTab_Close && OptionsButton_Image.Left < 1920)
                {
                    OptionsButton_Image.SetPosition(OptionsButton_Image.Left + 25f, OptionsButton_Image.Y);
                    backArrow_Image.SetPosition(backArrow_Image.Left + 25f, backArrow_Image.Y);
                    cursor_Image.SetPosition(cursor_Image.Left + 25f, cursor_Image.Y);
                    options_VolumeButton.SetPosition(options_VolumeButton.Left + 25f, options_VolumeButton.Y);
                }

                if (OptionsTab_Close && OptionsButton_Image.Left > 1920)
                {
                    Options = false;
                    OptionsTab_Close = false;
                    backArrow_Image.SetPosition(192, 834);
                    options_VolumeButton.SetPosition(192, 650);
                    currentSelection = (int)MenuButtons.MB_Options;
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
                    currentSelection = (int)MenuButtons.MB_Credits;
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

            MainMenuBg_Image.Render();

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
                    FullSail_Image.Render();

                    cursor_Image.Render();
                    PlayButton_Image.Render();
                    OptionsButton_Image.Render();
                    CreditsButton_Image.Render();
                    ExitButton_Image.Render();
                }
                else if (Play)
                {
                    play_Background_Image.SetPosition(PlayButton_Image.Left - 1350, 32);
                    play_Background_Image.Render();
                    PlayButton_Image.Render();

                    if (PlayButton_Image.Left >= 1496 && !PlayTab_Close)
                    {
                        cursor_Image.Render();
                        backArrow_Image.Render();
                        play_PlayButton.Render();
                        play_numPlayers_Button.Render();
                        play_numRounds_Button.Render();

                        Globals.slotCarText.FontSize = 100;
                        Globals.slotCarText.Color = Color.White;
                        Globals.slotCarText.String = "Play";
                        Globals.slotCarText.SetPosition(600, 100);
                        Globals.slotCarText.Render();

                        //Number of players
                        Globals.slotCarText.FontSize = 100;
                        Globals.slotCarText.Color = Color.White;
                        Globals.slotCarText.String = play_numPlayers.ToString();
                        Globals.slotCarText.SetPosition(500, 250);
                        Globals.slotCarText.Render();

                        //number of laps
                        Globals.slotCarText.String = play_numRounds.ToString();
                        Globals.slotCarText.SetPosition(1100, 250);
                        Globals.slotCarText.Render();

                        //car selection

                        //track selection

                    }
                }
                else if (Options)
                {
                    options_Background_Image.SetPosition(OptionsButton_Image.Left - 1350, 32);
                    options_Background_Image.Render();
                    OptionsButton_Image.Render();

                    if (OptionsButton_Image.Left >= 1496 && !OptionsTab_Close)
                    {
                        cursor_Image.Render();
                        backArrow_Image.Render();
                        options_VolumeButton.Render();

                        Globals.slotCarText.String = "Options";
                        Globals.slotCarText.FontSize = 100;
                        Globals.slotCarText.Color = Color.Black;
                        Globals.slotCarText.SetPosition(500, 100);
                        Globals.slotCarText.Render();

                        //controls section
                        Globals.slotCarText.FontSize = 50;
                        Globals.slotCarText.Color = Color.White;

                        Globals.slotCarText.String = "Controls:";
                        Globals.slotCarText.SetPosition(200, 250);
                        Globals.slotCarText.Render();

                        Globals.slotCarText.FontSize = 30;
                        Globals.slotCarText.String = "Accelerate:               Space Bar";
                        Globals.slotCarText.SetPosition(250, 320);
                        Globals.slotCarText.Render();

                        Globals.slotCarText.String = "Swap Left Lane:       Left Arrow";
                        Globals.slotCarText.SetPosition(250, 360);
                        Globals.slotCarText.Render();

                        Globals.slotCarText.String = "Swap Right Lane:      Right Arrow";
                        Globals.slotCarText.SetPosition(250, 400);
                        Globals.slotCarText.Render();

                        Globals.slotCarText.String = "Use Item:                        Left Control";
                        Globals.slotCarText.SetPosition(250, 440);
                        Globals.slotCarText.Render();

                        //volume section
                        Draw.Rectangle(600, 675, volumeRectLength, 100, Color.Magenta, Color.Black, 5);

                        Globals.slotCarText.FontSize = 10;
                        Globals.slotCarText.String = "Use backspace to go back";
                        Globals.slotCarText.SetPosition(224, 630);
                        Globals.slotCarText.Render();
                    }
                }
                else if (Credits)
                {
                    credits_Background_Image.SetPosition(CreditsButton_Image.Left - 1350, 32);
                    credits_Background_Image.Render();
                    CreditsButton_Image.Render();

                    if (CreditsButton_Image.Left >= 1496 && !CreditsTab_Close)
                    {
                        cursor_Image.Render();
                        backArrow_Image.Render();

                        Globals.slotCarText.FontSize = 100;
                        Globals.slotCarText.Color = Color.Black;
                        Globals.slotCarText.String = "Credits";
                        Globals.slotCarText.SetPosition(500, 100);
                        Globals.slotCarText.Render();

                        //here is the actual credits
                        Globals.slotCarText.FontSize = 50;
                        Globals.slotCarText.Color = Color.White;
                        Globals.slotCarText.String = "Ben Ayers";
                        Globals.slotCarText.SetPosition(350, 300);
                        Globals.slotCarText.Render();

                        Globals.slotCarText.String = "Nick Garcia";
                        Globals.slotCarText.SetPosition(350, 375);
                        Globals.slotCarText.Render();

                        Globals.slotCarText.String = "Corey Herington";
                        Globals.slotCarText.SetPosition(350, 450);
                        Globals.slotCarText.Render();

                        Globals.slotCarText.String = "Nick Hryshko";
                        Globals.slotCarText.SetPosition(350, 525);
                        Globals.slotCarText.Render();

                        Globals.slotCarText.String = "Richard Lizana";
                        Globals.slotCarText.SetPosition(350, 600);
                        Globals.slotCarText.Render();

                        Globals.slotCarText.String = "Conner McClaine";
                        Globals.slotCarText.SetPosition(350, 675);
                        Globals.slotCarText.Render();

                        Globals.slotCarText.String = "Brett Vanderzanden";
                        Globals.slotCarText.SetPosition(350, 750);
                        Globals.slotCarText.Render();
                    }
                }
            }
        }


    }
}
