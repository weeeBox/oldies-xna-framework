using System;

using System.Collections.Generic;


using asap.sound;
using asap.media;

namespace app
{
    public class Sounds
     {
        public const int NO_SOUND = 0;
        
        public const int FIRST_SOUND = 1;
        
        public const String MENU_MUSIC = "menu_music." + (Config.musicExtension);
        
        public const String INGAME_MUSIC = "ingame_music." + (Config.musicExtension);
        
        public const int WINGS = 1;
        
        public const int START = 2;
        
        public const int READY_TRI = 3;
        
        public const int READY_SIX = 4;
        
        public const int MONEY = 5;
        
        public const int FREEZE = 6;
        
        public const int FALL = 7;
        
        public const int FAIL = 8;
        
        public const int EXPLOSION = 9;
        
        public const int DROP = 10;
        
        public const int DESTROY_TRI = 11;
        
        public const int DESTROY_SMALL = 12;
        
        public const int DESTROY_SIX = 13;
        
        public const int DELETE = 14;
        
        public const int COMPLETED = 15;
        
        public const int COLLISION = 16;
        
        public const int CLICK = 17;
        
        public const int PUSH = 18;
        
        public const int BEGINNING = 19;
        
        public const int LAST_SOUND = BEGINNING;        
        
        private static int[] ingameOnly = new int[]{ WINGS , START , READY_TRI , READY_SIX , MONEY , FREEZE , FALL , FAIL , EXPLOSION , DROP , DESTROY_TRI , DESTROY_SMALL , DESTROY_SIX , DELETE , COMPLETED , COLLISION , PUSH , BEGINNING };
        
        private static Player currentMusicPlayer;        
        
        public static void LoadIngameSoundEnvironment()
        {
            Sounds.LoadSoundEnvironment(true, FIRST_SOUND, LAST_SOUND);
        }
        
        public static void LoadMenuSoundEnvironment()
        {
            Sounds.LoadSoundEnvironment(false, FIRST_SOUND, LAST_SOUND);
        }
        
        public static void PlayIngameMusic()
        {
            Sounds.PlayMusic(INGAME_MUSIC);
        }
        
        public static void PlayMenuMusic()
        {
            Sounds.PlayMusic(MENU_MUSIC);
        }
        
        private static void PlayMusic(String file)
        {
            if (Config.soundEnabled) 
            {
                Sounds.StopMusic();
                //if (Sounds.IsMusicEnabled()) 
                //{
                //    AudioSession.SilenceOther(true);
                //    try 
                //    {
                //        Sounds.currentMusicPlayer = Manager.CreatePlayer(file);
                //        if ((Sounds.currentMusicPlayer) != null) 
                //        {
                //            Sounds.currentMusicPlayer.Realize();
                //            Sounds.currentMusicPlayer.Prefetch();
                //            Sounds.currentMusicPlayer.Start();
                //            Sounds.currentMusicPlayer.SetLoopCount(0);
                //        } 
                //    }
                //    catch (Exception e) 
                //    {
                //        e.GetBaseException();
                //    }
                //} 
                throw new NotImplementedException();
            } 
        }
        
        public static void StopMusic()
        {
            if ((Config.soundEnabled) && ((Sounds.currentMusicPlayer) != null)) 
            {
                try 
                {
                    Sounds.currentMusicPlayer.Stop();
                    Sounds.currentMusicPlayer.Deallocate();
                    Sounds.currentMusicPlayer = null;
                }
                catch (Exception e) 
                {
                    e.GetBaseException();
                }
            } 
        }
        
        public static void LoadFullEnvironment()
        {
            throw new NotImplementedException();
        }
        
        private static void LoadSoundEnvironment(bool ingame, int low, int range)
        {
            throw new NotImplementedException(); 
        }
        
        private static bool IsSoundLoaded(int ident)
        {
            throw new NotImplementedException(); 
        }
        
        private static void LoadSound(int key)
        {
            throw new NotImplementedException();
        }
        
        private static void UnloadSound(int key)
        {
            throw new NotImplementedException(); 
        }
        
        public static void Play(int ident)
        {
            Sounds.Play(ident, false);
        }
        
        public static void Play(int ident, bool looped)
        {
            throw new NotImplementedException(); 
        }        
        
        public static void Suspend()
        {
            if ((Config.soundEnabled) && ((Sounds.currentMusicPlayer) != null)) 
            {
                Sounds.currentMusicPlayer.Stop();
            } 
        }
        
        public static void Resume()
        {
            if ((Config.soundEnabled) && ((Sounds.currentMusicPlayer) != null)) 
            {
                Sounds.currentMusicPlayer.Start();
            } 
        }
        
    }
    
    
}