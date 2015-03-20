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
        #region Public Variables

        //enumerations
        public enum MenuButtons { MB_Play = 0, MB_Options = 1, MB_Credits = 2, MB_Exit = 3 }
        public enum TabedMenuButtons { play_Back = 0, play_NumPlayers, play_NumRounds, play_carSelect, play_trackSelect, play_Play, options_Back, options_Volume, credits_Back }
        public enum TrackSelection { track1_select = 0, track2_select = 1, track3_select = 2 }

        //main menu variables
        public Image MainMenuBg_Image, FullSail_Image, PlayButton_Image, OptionsButton_Image, CreditsButton_Image, ExitButton_Image;
        public bool Play, Options, Credits, Exit;

        //play menu variables
        public int play_numPlayers, play_numRounds, play_currentTack_select, carSelct_player1, carSelect_player2, carSelect_Counter;
        public Image play_Background_Image, play_PlayButton, play_numPlayers_Button, play_numRounds_Button, play_carSelection_Button, play_trackSelection_Button, carCursor_Image_P1, carCursor_Image_P2, player1CarSelection, player2CarSelection;
        public Image car1_Image, car2_Image, car3_Image, car4_Image, car5_Image, car6_Image, car7_Image;
        public bool PlayTab_Close, playTab_NumPlayers_bool, playTab_NumRounds_bool, playTab_CarSelect_bool, playTab_TrackSelect_bool, SwitchScenes;

        //options menu variables
        public int volumeRectLength;
        public Image options_Background_Image, options_VolumeButton;
        public bool OptionsTab_Close, volumeChange;

        //credits menu variables
        public Image credits_Background_Image;
        public bool CreditsTab_Close;

        //shared/other variables
        public int currentSelection;
        public Image cursor_Image, backArrow_Image;

        #endregion

        public Menu() : base()
        {
            //int intialization
            currentSelection = (int)MenuButtons.MB_Play;
            volumeRectLength = 400;
            play_numPlayers = 1;
            play_numRounds = 10;
            play_currentTack_select = 0;
            carSelct_player1 = 0;
            carSelect_player2 = 1;
            carSelect_Counter = 0;

            #region Image Loading

            //button images
            backArrow_Image = new Image("Assets/Images/BackArrow.png");
            PlayButton_Image = new Image("Assets/Images/Menu_Play.png");
            OptionsButton_Image = new Image("Assets/Images/Menu_Options.png");
            CreditsButton_Image = new Image("Assets/Images/Menu_Credits.png");
            ExitButton_Image = new Image("Assets/Images/Menu_Exit.png");
            options_VolumeButton = new Image("Assets/Images/VolumeButton.png");
            play_PlayButton = new Image("Assets/Images/PlayButton.png");
            play_numPlayers_Button = new Image("Assets/Images/NumOfPlayersButton.png");
            play_numRounds_Button = new Image("Assets/Images/NumOfLapsButton.png");
            play_carSelection_Button = new Image("Assets/Images/CarSelectionButton.png");
            play_trackSelection_Button = new Image("Assets/Images/TrackSelectionButton.png");

            //car images
            car1_Image = new Image("Assets/Images/Car1_Orange.png");
            car2_Image = new Image("Assets/Images/Car2_Blue.png");
            car3_Image = new Image("Assets/Images/Car3_Red.png");
            car4_Image = new Image("Assets/Images/Car4_Green.png");
            car5_Image = new Image("Assets/Images/Car5_Black.png");
            car6_Image = new Image("Assets/Images/Car6_Yellow.png");
            car7_Image = new Image("Assets/Images/Car7_Gray.png");

            //cursor images
            cursor_Image = new Image("Assets/Images/Menu_Cursor.png");
            carCursor_Image_P1 = new Image("Assets/Images/CarSelect_P1.png");
            carCursor_Image_P2 = new Image("Assets/Images/CarSelect_P2.png");

            //default player cars
            player1CarSelection = new Image("Assets/Images/Car1_Orange.png");
            player2CarSelection = new Image("Assets/Images/Car2_Blue.png");

            //background images
            play_Background_Image = new Image("Assets/Images/BlueFlag.png");
            options_Background_Image = new Image("Assets/Images/GrayFlag.png");
            credits_Background_Image = new Image("Assets/Images/GreenFlag.png");
            MainMenuBg_Image = new Image("Assets/Images/MainMenuBg.png");
            FullSail_Image = new Image("Assets/Images/FullSail.png");

            #endregion

            #region Image Initial Position

            //button images 
            backArrow_Image.SetPosition(192, 834);
            PlayButton_Image.SetPosition(160f, 128f);
            OptionsButton_Image.SetPosition(160f, 350f);
            CreditsButton_Image.SetPosition(160f, 572f);
            ExitButton_Image.SetPosition(160f, 794f);
            options_VolumeButton.SetPosition(192, 675);
            play_PlayButton.SetPosition(1000, 834);
            play_numPlayers_Button.SetPosition(200, 225);
            play_numRounds_Button.SetPosition(800, 225);
            play_carSelection_Button.SetPosition(200, 425);
            play_trackSelection_Button.SetPosition(800, 425);

            //car images for car select 
            car1_Image.SetPosition(425, 700);
            car2_Image.SetPosition(525, 700);
            car3_Image.SetPosition(625, 700);
            car4_Image.SetPosition(725, 700);
            car5_Image.SetPosition(825, 700);
            car6_Image.SetPosition(925, 700);
            car7_Image.SetPosition(1025, 700);
            carCursor_Image_P1.SetPosition(390, 675);
            carCursor_Image_P2.SetPosition(390, 675);

            //other images 
            FullSail_Image.SetPosition(736, 288);
            MainMenuBg_Image.SetPosition(32, 32);

            #endregion

            //bool initalization!!!!!
            Play = false;
            PlayTab_Close = false;
            playTab_NumPlayers_bool = false;
            playTab_NumRounds_bool = false;
            playTab_CarSelect_bool = false;
            playTab_TrackSelect_bool = false;
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
                if (!playTab_CarSelect_bool && !playTab_TrackSelect_bool)
                {
                    //input check 
                    if (Globals.PlayerOne.Controller.Button(Controls.SwapLaneLeft).Pressed)
                    {
                        currentSelection--;
                        if (currentSelection < (int)TabedMenuButtons.play_Back)
                            currentSelection = (int)TabedMenuButtons.play_Play;
                    }
                    else if (Globals.PlayerOne.Controller.Button(Controls.SwapLaneRight).Pressed)
                    {
                        currentSelection++;
                        if (currentSelection > (int)TabedMenuButtons.play_Play)
                            currentSelection = (int)TabedMenuButtons.play_Back;
                    }

                    if ((Globals.PlayerOne.Controller.Button(Controls.Enter).Pressed && currentSelection == (int)TabedMenuButtons.play_Back) || Globals.PlayerOne.Controller.Button(Controls.Back).Pressed)
                        PlayTab_Close = true;
                    if (Globals.PlayerOne.Controller.Button(Controls.Enter).Pressed && currentSelection == (int)TabedMenuButtons.play_Play)
                        SwitchScenes = true;
                    if (Globals.PlayerOne.Controller.Button(Controls.Enter).Pressed && currentSelection == (int)TabedMenuButtons.play_carSelect)
                        playTab_CarSelect_bool = true;
                    if (Globals.PlayerOne.Controller.Button(Controls.Enter).Pressed && currentSelection == (int)TabedMenuButtons.play_trackSelect)
                        playTab_TrackSelect_bool = true;


                    if (currentSelection == (int)TabedMenuButtons.play_NumPlayers)
                    {
                        playTab_NumPlayers_bool = true;
                        playTab_NumRounds_bool = false;
                    }
                    if (currentSelection == (int)TabedMenuButtons.play_NumRounds)
                    {
                        playTab_NumRounds_bool = true;
                        playTab_NumPlayers_bool = false;
                    }
                    if (currentSelection == (int)TabedMenuButtons.play_carSelect)
                    {
                        playTab_NumRounds_bool = false;
                        playTab_NumPlayers_bool = false;
                    }
                    if (currentSelection == (int)TabedMenuButtons.play_trackSelect)
                    {
                        playTab_NumRounds_bool = false;
                        playTab_NumPlayers_bool = false;
                    }
                    if (currentSelection == (int)TabedMenuButtons.play_Back)
                    {
                        playTab_NumRounds_bool = false;
                        playTab_NumPlayers_bool = false;
                    }
                    if (currentSelection == (int)TabedMenuButtons.play_Play)
                    {
                        playTab_NumRounds_bool = false;
                        playTab_NumPlayers_bool = false;
                    }


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
                    //else if (Globals.PlayerOne.Controller.Button(Controls.Back).Pressed)
                    //playTab_NumPlayers_bool = false;
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
                    //else if (Globals.PlayerOne.Controller.Button(Controls.Back).Pressed)
                    //playTab_NumRounds_bool = false;
                }
                if (playTab_CarSelect_bool)
                {
                    carSelect_Counter++;

                    if (play_numPlayers == 1)
                    {
                        //input for selecting a car. player 1
                        if (Globals.PlayerOne.Controller.Button(Controls.SwapLaneRight).Pressed)
                        {
                            carSelct_player1++;
                            if (carSelct_player1 > 6)
                                carSelct_player1 = 0;

                            carCursor_Image_P1.SetPosition(carCursor_Image_P1.Left + 100, 675);
                            if (carCursor_Image_P1.Left > 990)
                                carCursor_Image_P1.SetPosition(390, 675);
                        }
                        else if (Globals.PlayerOne.Controller.Button(Controls.SwapLaneLeft).Pressed)
                        {
                            carSelct_player1--;
                            if (carSelct_player1 < 0)
                                carSelct_player1 = 6;

                            carCursor_Image_P1.SetPosition(carCursor_Image_P1.Left - 100, 675);
                            if (carCursor_Image_P1.Left < 390)
                                carCursor_Image_P1.SetPosition(990, 675);
                        }
                        else if (Globals.PlayerOne.Controller.Button(Controls.Enter).Pressed && carSelect_Counter > 10)//player 1
                        {
                            //select current car for player 1
                            if (carSelct_player1 == 0)
                                player1CarSelection = new Image("Assets/Images/Car1_Orange.png");
                            else if (carSelct_player1 == 1)
                                player1CarSelection = new Image("Assets/Images/Car2_Blue.png");
                            else if (carSelct_player1 == 2)
                                player1CarSelection = new Image("Assets/Images/Car3_Red.png");
                            else if (carSelct_player1 == 3)
                                player1CarSelection = new Image("Assets/Images/Car4_Green.png");
                            else if (carSelct_player1 == 4)
                                player1CarSelection = new Image("Assets/Images/Car5_Black.png");
                            else if (carSelct_player1 == 5)
                                player1CarSelection = new Image("Assets/Images/Car6_Yellow.png");
                            else if (carSelct_player1 == 6)
                                player1CarSelection = new Image("Assets/Images/Car7_Gray.png");

                            playTab_CarSelect_bool = false;
                            carSelect_Counter = 0;
                        }
                        else if (Globals.PlayerOne.Controller.Button(Controls.Back).Pressed)
                            playTab_CarSelect_bool = false;
                    }
                    else if (play_numPlayers == 2)
                    {
                        //input for selecting a car. player 1
                        if (Globals.PlayerOne.Controller.Button(Controls.SwapLaneRight).Pressed)
                        {
                            carSelct_player1++;
                            if (carSelct_player1 > 6)
                                carSelct_player1 = 0;

                            carCursor_Image_P1.SetPosition(carCursor_Image_P1.Left + 100, 675);
                            if (carCursor_Image_P1.Left > 990)
                                carCursor_Image_P1.SetPosition(390, 675);
                        }
                        else if (Globals.PlayerOne.Controller.Button(Controls.SwapLaneLeft).Pressed)
                        {
                            carSelct_player1--;
                            if (carSelct_player1 < 0)
                                carSelct_player1 = 6;

                            carCursor_Image_P1.SetPosition(carCursor_Image_P1.Left - 100, 675);
                            if (carCursor_Image_P1.Left < 390)
                                carCursor_Image_P1.SetPosition(990, 675);
                        }
                        else if (Globals.PlayerOne.Controller.Button(Controls.Enter).Pressed && carSelect_Counter > 10)//player 1
                        {
                            //select current car for player 1
                            if (carSelct_player1 == 0)
                                player1CarSelection = new Image("Assets/Images/Car1_Orange.png");
                            else if (carSelct_player1 == 1)
                                player1CarSelection = new Image("Assets/Images/Car2_Blue.png");
                            else if (carSelct_player1 == 2)
                                player1CarSelection = new Image("Assets/Images/Car3_Red.png");
                            else if (carSelct_player1 == 3)
                                player1CarSelection = new Image("Assets/Images/Car4_Green.png");
                            else if (carSelct_player1 == 4)
                                player1CarSelection = new Image("Assets/Images/Car5_Black.png");
                            else if (carSelct_player1 == 5)
                                player1CarSelection = new Image("Assets/Images/Car6_Yellow.png");
                            else if (carSelct_player1 == 6)
                                player1CarSelection = new Image("Assets/Images/Car7_Gray.png");

                            playTab_CarSelect_bool = false;
                            carSelect_Counter = 0;
                        }
                        else if (Globals.PlayerOne.Controller.Button(Controls.Back).Pressed)
                            playTab_CarSelect_bool = false;


                        //input for selecting a car. player 2
                        if (Globals.PlayerTwo.Controller.Button(Controls.SwapLaneRight).Pressed)
                        {
                            carSelect_player2++;
                            if (carSelect_player2 > 6)
                                carSelect_player2 = 0;

                            carCursor_Image_P2.SetPosition(carCursor_Image_P2.Left + 100, 675);
                            if (carCursor_Image_P2.Left > 990)
                                carCursor_Image_P2.SetPosition(390, 675);
                        }
                        else if (Globals.PlayerTwo.Controller.Button(Controls.SwapLaneLeft).Pressed)
                        {
                            carSelect_player2--;
                            if (carSelect_player2 < 0)
                                carSelect_player2 = 6;

                            carCursor_Image_P2.SetPosition(carCursor_Image_P2.Left - 100, 675);
                            if (carCursor_Image_P2.Left < 390)
                                carCursor_Image_P2.SetPosition(990, 675);
                        }
                        else if (Globals.PlayerTwo.Controller.Button(Controls.Enter).Pressed && carSelect_Counter > 10)
                        {
                            //select current car for player 2
                            if (carSelect_player2 == 0)
                                player2CarSelection = new Image("Assets/Images/Car1_Orange.png");
                            else if (carSelect_player2 == 1)
                                player2CarSelection = new Image("Assets/Images/Car2_Blue.png");
                            else if (carSelect_player2 == 2)
                                player2CarSelection = new Image("Assets/Images/Car3_Red.png");
                            else if (carSelect_player2 == 3)
                                player2CarSelection = new Image("Assets/Images/Car4_Green.png");
                            else if (carSelect_player2 == 4)
                                player2CarSelection = new Image("Assets/Images/Car5_Black.png");
                            else if (carSelect_player2 == 5)
                                player2CarSelection = new Image("Assets/Images/Car6_Yellow.png");
                            else if (carSelect_player2 == 6)
                                player2CarSelection = new Image("Assets/Images/Car7_Gray.png");

                            carSelect_Counter = 0;
                        }
                    }

                }
                if (playTab_TrackSelect_bool)
                {
                    //input for selecting a track
                    if (Globals.PlayerOne.Controller.Button(Controls.SwapLaneRight).Pressed)
                    {
                        play_currentTack_select++;
                        if (play_currentTack_select > (int)TrackSelection.track3_select)
                            play_currentTack_select = (int)TrackSelection.track1_select;
                    }
                    else if (Globals.PlayerOne.Controller.Button(Controls.SwapLaneLeft).Pressed)
                    {
                        play_currentTack_select--;
                        if (play_currentTack_select < (int)TrackSelection.track1_select)
                            play_currentTack_select = (int)TrackSelection.track3_select;
                    }
                    else if (Globals.PlayerOne.Controller.Button(Controls.Enter).Pressed)
                    {
                        //set the track to be the one played


                    }
                    else if (Globals.PlayerOne.Controller.Button(Controls.Back).Pressed)
                        playTab_TrackSelect_bool = false;
                }

                //set playTab cursor
                if (currentSelection == (int)TabedMenuButtons.play_Play)
                    cursor_Image.SetPosition(925, 875);
                else if (currentSelection == (int)TabedMenuButtons.play_Back)
                    cursor_Image.SetPosition(458, 875);
                else if (currentSelection == (int)TabedMenuButtons.play_NumPlayers)
                    cursor_Image.SetPosition(600, 282);
                else if (currentSelection == (int)TabedMenuButtons.play_NumRounds)
                    cursor_Image.SetPosition(1275, 282);
                else if (currentSelection == (int)TabedMenuButtons.play_carSelect)
                    cursor_Image.SetPosition(500, 482);
                else if (currentSelection == (int)TabedMenuButtons.play_trackSelect)
                    cursor_Image.SetPosition(1100, 482);


                if (PlayTab_Close && PlayButton_Image.Left < 1920)
                    PlayButton_Image.SetPosition(PlayButton_Image.Left + 25f, PlayButton_Image.Y);
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

                    if (PlayButton_Image.Left > 1900)
                    {
                        Play = false;
                        PlayTab_Close = false;
                        SwitchScenes = false;
                        backArrow_Image.SetPosition(192, 834);
                        play_PlayButton.SetPosition(1000, 834);

                        //switch scenes
                        Game.RemoveScene();
                        //Game.AddScene(new Race(10));
                        Game.AddScene(new Race(play_numRounds, play_numPlayers, player1CarSelection, player2CarSelection));
                    }
                }
            }

            #endregion

            #region Options Tab

            //Options pulled over tab update
            if (Options && OptionsButton_Image.Left >= 1496)
            {
                //input check for option tab cursor
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

                if ((Globals.PlayerOne.Controller.Button(Controls.Enter).Pressed && currentSelection == (int)TabedMenuButtons.options_Back) || Globals.PlayerOne.Controller.Button(Controls.Back).Pressed)
                    OptionsTab_Close = true;

                if (currentSelection == (int)TabedMenuButtons.options_Volume)
                    volumeChange = true;
                if (currentSelection == (int)TabedMenuButtons.options_Back)
                    volumeChange = false;

                if (currentSelection == (int)TabedMenuButtons.options_Volume)
                    cursor_Image.SetPosition(458, 708);
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
                    OptionsButton_Image.SetPosition(OptionsButton_Image.Left + 25f, OptionsButton_Image.Y);

                if (OptionsTab_Close && OptionsButton_Image.Left > 1920)
                {
                    Options = false;
                    OptionsTab_Close = false;
                    volumeChange = false;
                    currentSelection = (int)MenuButtons.MB_Options;
                }
            }

            #endregion

            #region Credits Tab

            if (Credits && CreditsButton_Image.Left >= 1496)
            {
                currentSelection = (int)TabedMenuButtons.credits_Back;
                cursor_Image.SetPosition(458, 875);

                if ((Globals.PlayerOne.Controller.Button(Controls.Enter).Pressed && currentSelection == (int)TabedMenuButtons.credits_Back) || Globals.PlayerOne.Controller.Button(Controls.Back).Pressed)
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

                    if (PlayButton_Image.Left >= 1496 && !PlayTab_Close && !SwitchScenes)
                    {
                        cursor_Image.Render();
                        backArrow_Image.Render();
                        play_PlayButton.Render();
                        play_numPlayers_Button.Render();
                        play_numRounds_Button.Render();
                        play_carSelection_Button.Render();
                        play_trackSelection_Button.Render();

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
                        if (playTab_CarSelect_bool)
                        {
                            Draw.Rectangle(350, 625, 750, 200, Color.White, Color.Black, 5);

                            car1_Image.Render();
                            car2_Image.Render();
                            car3_Image.Render();
                            car4_Image.Render();
                            car5_Image.Render();
                            car6_Image.Render();
                            car7_Image.Render();

                            carCursor_Image_P2.Render();
                            carCursor_Image_P1.Render();

                            Globals.slotCarText.FontSize = 20;
                            Globals.slotCarText.Color = Color.Black;
                            Globals.slotCarText.String = "Backspace to go back";
                            Globals.slotCarText.SetPosition(750, 800);
                            Globals.slotCarText.Render();
                        }

                        //track selection
                        if (playTab_TrackSelect_bool)
                        {
                            if (play_currentTack_select == (int)TrackSelection.track1_select)
                                Draw.Rectangle(330, 605, 240, 240, Color.Gold);
                            else if (play_currentTack_select == (int)TrackSelection.track2_select)
                                Draw.Rectangle(605, 605, 240, 240, Color.Gold);
                            else if (play_currentTack_select == (int)TrackSelection.track3_select)
                                Draw.Rectangle(880, 605, 240, 240, Color.Gold);

                            Draw.Rectangle(350, 625, 200, 200, Color.White, Color.Black, 5);
                            Draw.Rectangle(625, 625, 200, 200, Color.White, Color.Black, 5);
                            Draw.Rectangle(900, 625, 200, 200, Color.White, Color.Black, 5);

                            Globals.slotCarText.FontSize = 20;
                            Globals.slotCarText.Color = Color.Grey;
                            Globals.slotCarText.String = "Backspace to go back";
                            Globals.slotCarText.SetPosition(565, 850);
                            Globals.slotCarText.Render();
                        }
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

                        //controls section
                        Globals.slotCarText.FontSize = 75;
                        Globals.slotCarText.Color = Color.White;

                        Globals.slotCarText.String = "Controls";
                        Globals.slotCarText.SetPosition(500, 100);
                        Globals.slotCarText.Render();

                        Globals.slotCarText.Color = Color.Green;
                        Globals.slotCarText.FontSize = 30;
                        Globals.slotCarText.String = "Keyboard";
                        Globals.slotCarText.SetPosition(200, 220);
                        Globals.slotCarText.Render();

                        Globals.slotCarText.Color = Color.White;
                        Globals.slotCarText.FontSize = 20;
                        Globals.slotCarText.String = "Accelerate               Space Bar";
                        Globals.slotCarText.SetPosition(250, 260);
                        Globals.slotCarText.Render();

                        Globals.slotCarText.String = "Swap Left Lane       Left Arrow";
                        Globals.slotCarText.SetPosition(250, 290);
                        Globals.slotCarText.Render();

                        Globals.slotCarText.String = "Swap Right Lane      Right Arrow";
                        Globals.slotCarText.SetPosition(250, 320);
                        Globals.slotCarText.Render();

                        Globals.slotCarText.String = "Use Item                       Left Control";
                        Globals.slotCarText.SetPosition(250, 350);
                        Globals.slotCarText.Render();


                        Globals.slotCarText.Color = Color.Green;
                        Globals.slotCarText.FontSize = 30;
                        Globals.slotCarText.String = "Controller";
                        Globals.slotCarText.SetPosition(200, 420);
                        Globals.slotCarText.Render();

                        Globals.slotCarText.Color = Color.White;
                        Globals.slotCarText.FontSize = 20;
                        Globals.slotCarText.String = "Accelerate               Space Bar";
                        Globals.slotCarText.SetPosition(250, 450);
                        Globals.slotCarText.Render();

                        Globals.slotCarText.String = "Swap Left Lane       Left Arrow";
                        Globals.slotCarText.SetPosition(250, 480);
                        Globals.slotCarText.Render();

                        Globals.slotCarText.String = "Swap Right Lane      Right Arrow";
                        Globals.slotCarText.SetPosition(250, 510);
                        Globals.slotCarText.Render();

                        Globals.slotCarText.String = "Use Item                       Left Control";
                        Globals.slotCarText.SetPosition(250, 540);
                        Globals.slotCarText.Render();

                        //volume section
                        Draw.Rectangle(600, 700, volumeRectLength, 100, Color.Magenta, Color.Black, 5);

                        Globals.slotCarText.FontSize = 30;
                        Globals.slotCarText.String = "Backspace to go back";
                        Globals.slotCarText.SetPosition(600, 900);
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
