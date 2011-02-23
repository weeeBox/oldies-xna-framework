using System;

using System.Collections.Generic;

using app;
using asap.core;

namespace app.menu
{
    public class MenuController : TickListener, MenuListener
     {        
        private LoadingState loadingState;        
        
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
            Prefs.GetInstance().Load();            
            StartScreen(ScreenFactory.CreateStartLoading(this));
            loadingState = LoadingState.APP;
        }
        
        public virtual void Tick(long delta)
        {         
            if ((loadingState) != (LoadingState.NONE)) 
            {
                if (FlipstonesApp.GetScreensView().GetActiveScreen().WasDrawn())
                    ProcessLoadingState();                
            } 
            else 
            {                    
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
                    Sounds.LoadFullEnvironment();
                    Sounds.PlayMenuMusic();
                    UnloadPack(STARTUP_RESOURCES);
                    StartScreen(ScreenFactory.CreateMainMenu(this));                    
                }
                break;                
                default:                    
                {
                    System.Diagnostics.Debug.Assert(false);
                }
                break;
            }
            loadingState = LoadingState.NONE;
        }        
        
        private void LoadResourcesOnStart()
        {
            LoadPack((Config.freeVersion ? RESOURCES_FREE : RESOURCES_FULL));
            LoadPack(MENU_RESOURCES);            
        }
        
        private void LoadResourcesBeforeGame()
        {
            UnloadPack((Config.freeVersion ? RESOURCES_FREE : RESOURCES_FULL));
            UnloadPack(MENU_RESOURCES);
            LoadPack(GAME_RESOURCES);
            LoadPack((Config.freeVersion ? GAME_RESOURCES_FREE : GAME_RESOURCES_FULL));            
        }
        
        private void LoadResourcesAfterGame()
        {
            UnloadPack(GAME_RESOURCES);
            UnloadPack((Config.freeVersion ? GAME_RESOURCES_FREE : GAME_RESOURCES_FULL));
            LoadPack((Config.freeVersion ? RESOURCES_FREE : RESOURCES_FULL));
            LoadPack(MENU_RESOURCES);            
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
        
        public virtual void PrefetchFinished()
        {            
        } 
        
        public virtual void ButtonPressed(int code)
        {
            if (FlipstonesApp.ButtonPressed(code))
                return;            
        }        
        
        private void StartScreen(Screen screen)
        {
            FlipstonesApp.GetScreensView().StartScreen(screen);            
        }
        
        private void StartNextScreen(Screen screen)
        {
            FlipstonesApp.GetScreensView().StartNextScreen(screen);            
        }
        
        private void BackScreen()
        {
            FlipstonesApp.GetScreensView().BackScreen();            
        }
        
        public virtual void OnAppExit()
        {            
        }
        
        public virtual void Suspend()
        {     
        }        
    }        
}