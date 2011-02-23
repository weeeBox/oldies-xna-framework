using System;

using System.Collections.Generic;


using flipstones;
using com.reaxion.adwhirl;
using asap.core;
using flipstones.game;

namespace flipstones.menu
{
    public class MenuController : TickListener, ShopListener, FlipstonesStoryListener, MenuListener, ToggleListener
     {
        private FlipstonesStoryData storyData;
        
        private FlipstonesStory story;
        
        private Mode mode;
        
        private int startLevel;
        
        private LoadingState loadingState;
        
        private int howToPlayPage;
        
        private Boost[] boosts;
        
        private bool[] boostButtons = new bool[Boost.BOOSTS_COUNT];
        
        private const int HOW_TO_PLAY_SCREENS_COUNT = 4;
        
        private bool flagToStartBeforeGame = false;
        
        private static readonly String[] STARTUP_RESOURCES = new String[]{ "poke_logo.png" , "logo_pp.png" };
        
        private static readonly String[] COMMON_RESOURCES = new String[]{ "font_menu.fnt" , "font_menu_mini.fnt" , "gems_info.png" , "button_regular_passive.png" , "button_regular_active.png" , "button_big_active.png" , "button_big_passive.png" , "gems_back.png" , "gems_other.png" , "gems_play.png" , "gems_more_games.png" , "gems_open_feint.png" , "gems_play_main.png" , "point_passive.png" , "point_active.png" , "lock.png" , "of_logo.png" , "menu_background.png" , "strings.str" };
        
        private static readonly String[] RESOURCES_FULL = new String[]{ "logo.png" };
        
        private static readonly String[] RESOURCES_FREE = new String[]{ "logo_free.png" };
        
        private static readonly String[] MENU_RESOURCES = new String[]{ "font_head.fnt" , "actions_done.png" , "actions_lock.png" , "actions_play.png" , "actions_select.png" , "frenzy.png" , "arrow_active.png" , "arrow_passive.png" , "full_ver_picture.png" , "full_ver_text.png" , "how_to_play_01.png" , "how_to_play_02.png" , "how_to_play_03.png" , "how_to_play_04.png" };
        
        private static readonly String[] GAME_RESOURCES = new String[]{ "popup_background.png" , "mission_done.png" , "mission_lock.png" , "mission_select.png" , "key.png" , "back.parts" , "blue.parts" , "bonus.parts" , "static.parts" , "default.parts" , "generator.parts" , "green.parts" , "purple.parts" , "red.parts" , "yellow.parts" , "black.parts" , "white.parts" , "orange.parts" , "colorless.parts" , "colorless1.parts" , "hud.parts" , "star_anim.ani" , "destroy_hex.ani" , "destroy_tri_1.ani" , "destroy_tri_2.ani" , "combine_hex.ani" , "combine_tri_1.ani" , "combine_tri_2.ani" , "combine_hex_colorless.ani" , "combine_tri_1_colorless.ani" , "combine_tri_2_colorless.ani" , "hex_colorless.ani" , "tri_1_colorless.ani" , "tri_2_colorless.ani" , "purple_anim_1.ani" , "purple_anim_2.ani" , "purple_anim_3.ani" , "red_anim_1.ani" , "red_anim_2.ani" , "red_anim_3.ani" , "yellow_anim_1.ani" , "yellow_anim_2.ani" , "yellow_anim_3.ani" , "blue_anim_1.ani" , "blue_anim_2.ani" , "blue_anim_3.ani" , "green_anim_1.ani" , "green_anim_2.ani" , "green_anim_3.ani" , "black_anim_1.ani" , "black_anim_2.ani" , "black_anim_3.ani" , "white_anim_1.ani" , "white_anim_2.ani" , "white_anim_3.ani" , "orange_anim_1.ani" , "orange_anim_2.ani" , "orange_anim_3.ani" , "generator.ani" , "drop_anim.ani" , "drop_destroy.ani" , "wings_anim.ani" , "wings_destroy.ani" , "main_destroy_1.ani" , "main_destroy_2.ani" , "hud.ani" , "hud_progress.ani" , "hud_score_bar.ani" , "hud_time_bar.ani" , "man_back.ani" , "man_run_right.ani" , "man_stand_left.ani" , "man_walk_left.ani" , "star.ani" , "star_destroy.ani" , "fire_anim.ani" , "main_turnover_1.ani" , "main_turnover_2.ani" , "obstacle_1.ani" , "obstacle_2.ani" , "main_rotate.ani" , "cell_marker_1.ani" , "cell_marker_2.ani" , "cell_way_1.ani" , "cell_way_2.ani" , "cell_selected_1.ani" , "cell_selected_2.ani" , "main_select_1.ani" , "main_select_2.ani" , "tap_here.ani" , "tap_here_tutorial.ani" , "win_effect.png" , "win_effect1.png" , "icicle.png" , "icicle_boost.png" , "pause_button_on.png" , "pause_button_off.png" , "star.png" , "star_ingame.png" , "level_counter_back.png" , "level_counter_progress.png" , "score_icon.png" , "time_up.png" , "font_score.fnt" , "font_score2.fnt" , "green_splash.png" , "pink_combo_wings.png" , "yellows_on.png" , "blue_freeze.png" , "black_exchange.png" , "time_bonus.png" , "caffeine.png" , "workaholic.png" , "arrow.png" , "hex_black_exchange.png" , "tri_1_black_exchange.png" , "tri_2_black_exchange.png" , "hex_blue_freeze.png" , "tri_1_blue_freeze.png" , "tri_2_blue_freeze.png" , "hex_green_splash.png" , "tri_1_green_splash.png" , "tri_2_green_splash.png" , "hex_pink_combo_wings.png" , "tri_1_pink_combo_wings.png" , "tri_2_pink_combo_wings.png" , "hex_yellows_on.png" , "tri_1_yellows_on.png" , "tri_2_yellows_on.png" , "hex_workaholic.png" , "tri_1_workaholics.png" , "tri_2_workaholic.png" };
        
        private static readonly String[] GAME_RESOURCES_FULL = new String[]{ "time_up_effect_1.png" , "time_up_effect_2.png" };
        
        private static readonly String[] GAME_RESOURCES_FREE = new String[]{ "time_up_effect_1_free.png" , "time_up_effect_2_free.png" };
        
        public MenuController() 
        {
            LoadPack(STARTUP_RESOURCES);
            LoadPack(COMMON_RESOURCES);
            AtlasRepacker.ScheduleRepack();
            Prefs.GetInstance().Load();
            FlurryAdapter.SetFlurryLoggingEnabled(Prefs.GetInstance().IsFlurryEnabled());
            StartScreen(ScreenFactory.CreateStartLoading(this));
            loadingState = LoadingState.APP;
        }
        
        public virtual void Tick(long delta)
        {
            if ((Config.freeVersion) && (Mobclix.IsAdShown())) 
            {
                return ;
            } 
            if (flagToStartBeforeGame) 
            {
                flagToStartBeforeGame = false;
                FlipstonesApp.GetInstance().PlacePlayStartBeforeGame(this);
                return ;
            } 
            if ((FlipstonesApp.GetInstance().GetPlacePlayListener()) != null) 
            {
                FlipstonesApp.GetInstance().GetPlacePlayListener().Tick();
            } 
            if ((loadingState) != (LoadingState.NONE)) 
            {
                if (FlipstonesApp.GetScreensView().GetActiveScreen().WasDrawn())
                    ProcessLoadingState();
                
            } 
            else 
            {
                if ((story) != null)
                    story.Tick(delta);
                
                else
                    FlipstonesApp.GetScreensView().Tick(delta);
                
            }
        }
        
        private void ProcessLoadingState()
        {
            switch (loadingState)
            {
                case LoadingState.APP:
                    
                    {
                        LoadResourcesOnStart();
                        storyData = new FlipstonesStoryData();
                        storyData.Load();
                        Sounds.LoadFullEnvironment();
                        Sounds.PlayMenuMusic();
                        UnloadPack(STARTUP_RESOURCES);
                        StartScreen(ScreenFactory.CreateMainMenu(this));
                        if (Config.freeVersion) 
                        {
                            FlipstonesApp.GetInstance().UpdateLastMobclixTime();
                            AdWhirlAdapter.ShowAd(AdWhirlAdapter.BOTTOM);
                        } 
                    }
                    break;
                case LoadingState.GAME:
                    
                    {
                        if ((mode) == (Mode.FRENZY)) 
                        {
                            FlipstonesApp.GetInstance().PrefetchPlacePlay(this);
                        } 
                        else 
                        {
                            BackScreen();
                        }
                        LoadResourcesBeforeGame();
                        story = new FlipstonesStory(this , storyData);
                        FlipstonesApp.GetInstance().GetCheatManager().AddCheatListener(story);
                        story.SetMode(mode);
                        if ((mode) == (Mode.FRENZY)) 
                        {
                            story.PauseState();
                        } 
                        else 
                        {
                            if ((startLevel) >= 0)
                                story.StartLevelWithScreen(startLevel);
                            
                            else
                                story.StartFromSavedState();
                            
                        }
                    }
                    break;
                case LoadingState.PREV_MENU:
                    
                    {
                        LoadMenuResources();
                        if (((mode) == (Mode.ENDLESS)) || ((mode) == (Mode.FRENZY))) 
                        {
                            FlipstonesApp.GetScreensView().BackToScreen(ScreenId.MAIN_MENU);
                        } 
                        else 
                        {
                            FlipstonesApp.GetScreensView().BackToScreen(ScreenId.STORY_CHOOSE_LEVEL);
                        }
                        break;
                    }
                case LoadingState.MAIN_MENU:
                    
                    {
                        LoadMenuResources();
                        FlipstonesApp.GetScreensView().ClearScreens();
                        StartScreen(ScreenFactory.CreateMainMenu(this));
                        if (Config.freeVersion)
                            AdWhirlAdapter.ShowAd(AdWhirlAdapter.BOTTOM);
                        
                        break;
                    }
                case LoadingState.PLAY_FRENZY:
                    
                    {
                        LoadMenuResources();
                        FlipstonesApp.GetScreensView().ClearScreens();
                        StartScreen(ScreenFactory.CreateMainMenu(this));
                        FlurryAdapter.LogFlurryEvent("FRENZY_STARTED_FROM_CHALLENGE_RESULT");
                        mode = Mode.FRENZY;
                        StartSelectedMode();
                        break;
                    }
                case LoadingState.FREE_FINISHED:
                    
                    {
                        LoadMenuResources();
                        FlipstonesApp.GetScreensView().ClearScreens();
                        StartScreen(ScreenFactory.CreateMainMenu(this));
                        StartNextScreen(ScreenFactory.CreateBuyFull(this));
                        break;
                    }
                default:
                    
                    {
                        System.Diagnostics.Debug.Assert(false);
                    }
                    break;
            }
            loadingState = LoadingState.NONE;
        }
        
        private void LoadMenuResources()
        {
            BackScreen();
            FlipstonesApp.GetInstance().GetCheatManager().RemoveCheatListener(story);
            story.UnloadLevelResources();
            story = null;
            LoadResourcesAfterGame();
            Sounds.PlayMenuMusic();
        }
        
        private void LoadResourcesOnStart()
        {
            LoadPack((Config.freeVersion ? RESOURCES_FREE : RESOURCES_FULL));
            LoadPack(MENU_RESOURCES);
            AtlasRepacker.ScheduleRepack();
        }
        
        private void LoadResourcesBeforeGame()
        {
            UnloadPack((Config.freeVersion ? RESOURCES_FREE : RESOURCES_FULL));
            UnloadPack(MENU_RESOURCES);
            LoadPack(GAME_RESOURCES);
            LoadPack((Config.freeVersion ? GAME_RESOURCES_FREE : GAME_RESOURCES_FULL));
            AtlasRepacker.ScheduleRepack();
        }
        
        private void LoadResourcesAfterGame()
        {
            UnloadPack(GAME_RESOURCES);
            UnloadPack((Config.freeVersion ? GAME_RESOURCES_FREE : GAME_RESOURCES_FULL));
            LoadPack((Config.freeVersion ? RESOURCES_FREE : RESOURCES_FULL));
            LoadPack(MENU_RESOURCES);
            AtlasRepacker.ScheduleRepack();
        }
        
        private void LoadPack(String[] resources)
        {
            for (int i = 0; i < (resources.Length); i++)
                AppResManager.GetInstance().Load(resources[i]);
        }
        
        private void UnloadPack(String[] resources)
        {
            for (int i = 0; i < (resources.Length); i++)
                AppResManager.GetInstance().Unload(resources[i]);
        }
        
        public virtual void ButtonToggled(int code, bool _checked)
        {
            boostButtons[code] = _checked;
            int count = 0;
            for (int i = 0; i < (boostButtons.Length); i++) 
            {
                if (boostButtons[i]) 
                {
                    count++;
                } 
            }
            boosts = new Boost[count];
            int pos = 0;
            for (int i = 0; i < (boostButtons.Length); i++) 
            {
                if (boostButtons[i]) 
                {
                    boosts[pos++] = new Boost(i);
                } 
            }
        }
        
        public virtual void PrefetchFinished()
        {
            flagToStartBeforeGame = true;
        }
        
        public virtual void PlacePlayPlay()
        {
            BackScreen();
            if ((startLevel) >= 0)
                story.StartLevelWithScreen(startLevel);
            
            else
                story.StartFromSavedState();
            
            story.PlacePlayPlay();
        }
        
        public virtual void PlacePlayMenu()
        {
            BackScreen();
            story.PlacePlayMenu();
        }
        
        public virtual void StartSelectedMode()
        {
            if (((mode) == (Mode.ENDLESS)) || ((mode) == (Mode.FRENZY))) 
            {
                for (int i = 0; i < (boostButtons.Length); i++)
                    boostButtons[i] = false;
                if (Config.usePlacePlay) 
                {
                    if (Config.freeVersion)
                        AdWhirlAdapter.HideAd();
                    
                    StartGame(0);
                } 
                else 
                {
                    boosts = new Boost[0];
                    StartNextScreen(ScreenFactory.CreateBoostsSelector(this));
                }
            } 
            else if (!(storyData.HasUnfinishedLevel())) 
            {
                StartNextScreen(ScreenFactory.CreateChooseLevelMenu(this, storyData));
            } 
            else 
            {
                StartNextScreen(ScreenFactory.CreateResumeStory(this));
            }
        }
        
        public virtual void ButtonPressed(int code)
        {
            if (FlipstonesApp.ButtonPressed(code))
                return ;
            
            switch (code)
            {
                case ButtonId.LEADERBOARD:
                    FlipstonesApp.GetInstance().PlacePlayShowLeaderboard();
                    break;
                case ButtonId.START_FRENZY:
                case ButtonId.START_CHALLENGE:
                case ButtonId.START_ENDLESS:
                    mode = GetModeByButtonId(code);
                    switch (mode)
                    {
                        case Mode.FRENZY:
                            FlurryAdapter.LogFlurryEvent("FRENZY_STARTED_FROM_MAIN_MENU");
                            break;
                        case Mode.CHALLENGE:
                            FlurryAdapter.LogFlurryEvent("CHALLENGE_STARTED_FROM_MAIN_MENU");
                            break;
                        case Mode.ENDLESS:
                            FlurryAdapter.LogFlurryEvent("ENDLESS_STARTED_FROM_MAIN_MENU");
                            break;
                    }
                    if (Prefs.GetInstance().IsHowToPlayOnStart()) 
                    {
                        howToPlayPage = 0;
                        StartNextScreen(ScreenFactory.CreateHowToPlayScreen(this, 0, HOW_TO_PLAY_SCREENS_COUNT, false));
                    } 
                    else 
                    {
                        StartSelectedMode();
                    }
                    break;
                case ButtonId.PLAY:
                    StartGame(0);
                    break;
                case ButtonId.MORE_GAMES:
                    FlurryAdapter.LogFlurryEvent("MORE_GAMES_STARTED");
                    PlayHaven.LoadCharts();
                    break;
                case ButtonId.GET_FULL:
                    StartNextScreen(ScreenFactory.CreateBuyFull(this));
                    break;
                case ButtonId.BUY_FULL_VIA_BROWSER:
                    FlipstonesApp.GetInstance().BrowseFullVersion();
                    break;
                case ButtonId.INFO:
                    StartNextScreen(ScreenFactory.CreateInfoScreen(this));
                    break;
                case ButtonId.APP_EXIT:
                    FlipstonesApp.GetInstance().Stop();
                    break;
                case ButtonId.BACK:
                    FlipstonesApp.GetInstance().GetCheatManager().SetPossibleActivating(false);
                    BackScreen();
                    break;
                case ButtonId.HOW_TO_PLAY:
                    FlurryAdapter.LogFlurryEvent("HOW_TO_PLAY_STARTED");
                    howToPlayPage = 0;
                    StartNextScreen(ScreenFactory.CreateHowToPlayScreen(this, 0, HOW_TO_PLAY_SCREENS_COUNT, true));
                    break;
                case ButtonId.NAVIG_BUTTON_LEFT:
                    howToPlayPage = (((howToPlayPage) + (HOW_TO_PLAY_SCREENS_COUNT)) - 1) % (HOW_TO_PLAY_SCREENS_COUNT);
                    StartScreen(ScreenFactory.CreateHowToPlayScreen(this, howToPlayPage, HOW_TO_PLAY_SCREENS_COUNT, true));
                    break;
                case ButtonId.NAVIG_BUTTON_RIGHT:
                    howToPlayPage = ((howToPlayPage) + 1) % (HOW_TO_PLAY_SCREENS_COUNT);
                    StartScreen(ScreenFactory.CreateHowToPlayScreen(this, howToPlayPage, HOW_TO_PLAY_SCREENS_COUNT, true));
                    break;
                case ButtonId.CONTINUE_HOW_TO_PLAY:
                    if ((howToPlayPage) < ((HOW_TO_PLAY_SCREENS_COUNT) - 1)) 
                    {
                        (howToPlayPage)++;
                        StartScreen(ScreenFactory.CreateHowToPlayScreen(this, howToPlayPage, HOW_TO_PLAY_SCREENS_COUNT, false));
                    } 
                    else 
                    {
                        Prefs.GetInstance().SetHowToPlayOnStart(false);
                        BackScreen();
                        StartSelectedMode();
                    }
                    break;
                case ButtonId.SKIP_HOW_TO_PLAY:
                    Prefs.GetInstance().SetHowToPlayOnStart(false);
                    BackScreen();
                    StartSelectedMode();
                    break;
                case ButtonId.ABOUT:
                    FlipstonesApp.GetInstance().GetCheatManager().SetPossibleActivating(true);
                    StartNextScreen(ScreenFactory.CreateAboutScreen(this));
                    FlurryAdapter.LogFlurryEvent("ABOUT_SCREEN_STARTED");
                    break;
                case ButtonId.CONTINUE_STORY:
                    StartGame(-1);
                    break;
                case ButtonId.START_NEW_STORY:
                    StartScreen(ScreenFactory.CreateChooseLevelMenu(this, storyData));
                    break;
                default:
                    if ((code >= (ButtonId.CHOOSE_LEVEL_BASE)) && (code < ((ButtonId.CHOOSE_LEVEL_BASE) + (Settings.GetLevelsCount())))) 
                    {
                        StartGame((code - (ButtonId.CHOOSE_LEVEL_BASE)));
                    } 
                    else 
                    {
                        System.Diagnostics.Debug.Assert(false, "Unknown button in menu controller " + code);
                    }
                    break;
            }
        }
        
        public virtual void StartGame(int levelIndex)
        {
            if (Config.freeVersion)
                AdWhirlAdapter.HideAd();
            
            Sounds.StopMusic();
            startLevel = levelIndex;
            if ((mode) == (Mode.FRENZY)) 
            {
                StartNextScreen(ScreenFactory.CreateLoadingScreen(-2));
            } 
            else 
            {
                StartNextScreen(ScreenFactory.CreateLoadingScreen(((startLevel) + 1)));
            }
            loadingState = LoadingState.GAME;
        }
        
        private Mode GetModeByButtonId(int buttonId)
        {
            switch (buttonId)
            {
                case ButtonId.START_FRENZY:
                    return Mode.FRENZY;
                case ButtonId.START_CHALLENGE:
                    return Mode.CHALLENGE;
                case ButtonId.START_ENDLESS:
                    return Mode.ENDLESS;
            }
            System.Diagnostics.Debug.Assert(false);
            return Mode.ENDLESS;
        }
        
        public virtual void GameExited(ExitGameType exitGameType)
        {
            if (Config.freeVersion)
                AdWhirlAdapter.HideAd();
            
            Sounds.StopMusic();
            StartNextScreen(ScreenFactory.CreateLoadingScreen(-1));
            switch (exitGameType)
            {
                case ExitGameType.PREV_MENU:
                    loadingState = LoadingState.PREV_MENU;
                    break;
                case ExitGameType.MAIN_MENU:
                    loadingState = LoadingState.MAIN_MENU;
                    break;
                case ExitGameType.FREE_FINISHED:
                    loadingState = LoadingState.FREE_FINISHED;
                    break;
                case ExitGameType.PLAY_FRENZY:
                    loadingState = LoadingState.PLAY_FRENZY;
                    break;
            }
        }
        
        private void StartScreen(Screen screen)
        {
            FlipstonesApp.GetScreensView().StartScreen(screen);
            if (Config.freeVersion)
                AdWhirlAdapter.HideAd();
            
        }
        
        private void StartNextScreen(Screen screen)
        {
            FlipstonesApp.GetScreensView().StartNextScreen(screen);
            if (Config.freeVersion)
                AdWhirlAdapter.HideAd();
            
        }
        
        private void BackScreen()
        {
            FlipstonesApp.GetScreensView().BackScreen();
            if ((FlipstonesApp.GetScreensView().GetActiveScreen().GetId()) == (ScreenId.MAIN_MENU)) 
            {
                if (Config.freeVersion)
                    AdWhirlAdapter.ShowAd(AdWhirlAdapter.BOTTOM);
                
            } 
        }
        
        public virtual void OnAppExit()
        {
            if ((story) != null)
                story.SaveState();
            
        }
        
        public virtual void Suspend()
        {
            if (((story) != null) && (!(story.IsPaused()))) 
            {
                story.PauseGame();
            } 
        }
        
        public virtual void DebugSetStoryAsPassed()
        {
            for (int i = 0; i < (Settings.GetLevelsCount()); i++)
                storyData.SetLevelPassed(i);
            storyData.SetLevelsProgress(Settings.GetLevelsCount());
        }
        
    }
    
    
}