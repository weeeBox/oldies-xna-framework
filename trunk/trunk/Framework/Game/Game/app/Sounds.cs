using System;

using System.Collections.Generic;


using asap.sound;
using asap.media;

namespace flipstones
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
        
        private static Hashtable idsToFilenames = Sounds.InitIdsToFilenames();
        
        private static Hashtable cached = new Hashtable();
        
        private static int[] ingameOnly = new int[]{ WINGS , START , READY_TRI , READY_SIX , MONEY , FREEZE , FALL , FAIL , EXPLOSION , DROP , DESTROY_TRI , DESTROY_SMALL , DESTROY_SIX , DELETE , COMPLETED , COLLISION , PUSH , BEGINNING };
        
        private static Player currentMusicPlayer;
        
        private static Hashtable InitIdsToFilenames()
        {
            Hashtable t = new Hashtable();
            if (Config.soundEnabled) 
            {
                t.Put(new Integer(WINGS), ("wings." + (Config.soundExtension)));
                t.Put(new Integer(START), ("start." + (Config.soundExtension)));
                t.Put(new Integer(READY_TRI), ("ready_tri." + (Config.soundExtension)));
                t.Put(new Integer(READY_SIX), ("ready_six." + (Config.soundExtension)));
                t.Put(new Integer(MONEY), ("money." + (Config.soundExtension)));
                t.Put(new Integer(FREEZE), ("freeze." + (Config.soundExtension)));
                t.Put(new Integer(FALL), ("fall." + (Config.soundExtension)));
                t.Put(new Integer(FAIL), ("fail." + (Config.soundExtension)));
                t.Put(new Integer(EXPLOSION), ("explosion." + (Config.soundExtension)));
                t.Put(new Integer(DROP), ("drop." + (Config.soundExtension)));
                t.Put(new Integer(DESTROY_TRI), ("destroy_tri." + (Config.soundExtension)));
                t.Put(new Integer(DESTROY_SMALL), ("destroy_small." + (Config.soundExtension)));
                t.Put(new Integer(DESTROY_SIX), ("destroy_six." + (Config.soundExtension)));
                t.Put(new Integer(DELETE), ("delete." + (Config.soundExtension)));
                t.Put(new Integer(COMPLETED), ("completed." + (Config.soundExtension)));
                t.Put(new Integer(COLLISION), ("collision." + (Config.soundExtension)));
                t.Put(new Integer(CLICK), ("click." + (Config.soundExtension)));
                t.Put(new Integer(PUSH), ("push." + (Config.soundExtension)));
                t.Put(new Integer(BEGINNING), ("beginning." + (Config.soundExtension)));
            } 
            return t;
        }
        
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
                if (Sounds.IsMusicEnabled()) 
                {
                    AudioSession.SilenceOther(true);
                    try 
                    {
                        Sounds.currentMusicPlayer = Manager.CreatePlayer(file);
                        if ((Sounds.currentMusicPlayer) != null) 
                        {
                            Sounds.currentMusicPlayer.Realize();
                            Sounds.currentMusicPlayer.Prefetch();
                            Sounds.currentMusicPlayer.Start();
                            Sounds.currentMusicPlayer.SetLoopCount(0);
                        } 
                    }
                    catch (Exception e) 
                    {
                        e.GetBaseException();
                    }
                } 
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
            if (Config.soundEnabled) 
            {
                for (int ident = FIRST_SOUND; ident <= (LAST_SOUND); ++ident) 
                {
                    if (!(Sounds.IsSoundLoaded(ident))) 
                    {
                        Sounds.LoadSound(new Integer(ident));
                    } 
                }
            } 
        }
        
        private static void LoadSoundEnvironment(bool ingame, int low, int range)
        {
            if (Config.soundEnabled) 
            {
                for (int ident = low; ident <= range; ++ident) 
                {
                    bool isIngame = false;
                    for (int i = 0; i < (Sounds.ingameOnly.Length); ++i) 
                    {
                        if ((Sounds.ingameOnly[i]) == ident) 
                        {
                            isIngame = true;
                            break;
                        } 
                    }
                    Integer key = new Integer(ident);
                    SoundPlayer p = ((SoundPlayer)(Sounds.cached.Get(key)));
                    if (p != null) 
                    {
                        if (isIngame && (!ingame)) 
                        {
                            Sounds.UnloadSound(key);
                        } 
                    } 
                    else if (ingame || (!isIngame)) 
                    {
                        Sounds.LoadSound(key);
                    } 
                }
            } 
        }
        
        private static bool IsSoundLoaded(int ident)
        {
            if (Config.soundEnabled) 
            {
                return (Sounds.cached.Get(new Integer(ident))) != null;
            } 
            else 
            {
                return false;
            }
        }
        
        private static void LoadSound(Integer key)
        {
            if (Config.soundEnabled) 
            {
                String filename = ((String)(Sounds.idsToFilenames.Get(key)));
                if (filename == null) 
                {
                    return ;
                } 
                SoundPlayer p = SoundManager.GetInstance().CreatePlayer(filename);
                if (p != null) 
                {
                    Sounds.cached.Put(key, p);
                } 
            } 
        }
        
        private static void UnloadSound(Integer key)
        {
            if (Config.soundEnabled) 
            {
                SoundPlayer p = ((SoundPlayer)(Sounds.cached.Get(key)));
                System.Diagnostics.Debug.Assert(p != null);
                SoundManager.GetInstance().DestroyPlayer(p);
                Sounds.cached.Remove(key);
            } 
        }
        
        public static void Play(int ident)
        {
            Sounds.Play(ident, false);
        }
        
        public static void Play(int ident, bool looped)
        {
            if ((Config.soundEnabled) && (Sounds.IsSoundEnabled())) 
            {
                SoundPlayer p = ((SoundPlayer)(Sounds.cached.Get(new Integer(ident))));
                if (p != null) 
                {
                    p.Play(looped);
                } 
            } 
        }
        
        private static bool IsMusicEnabled()
        {
            return Prefs.GetInstance().IsMusicEnabled();
        }
        
        private static bool IsSoundEnabled()
        {
            return Prefs.GetInstance().IsSoundEnabled();
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