using asap.app;
using asap.resources;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;
namespace asap.sound
{
    public class SoundMgr
    {
        private MusicChannel musicChannel;
        private SoundChannel[] channels;        

        public SoundMgr(int maxSounds)
        {
            channels = new SoundChannel[maxSounds];            
            musicChannel = new MusicChannel();
        }        

        public MusicChannel PlayMusic(int musicId)
        {
            return PlayMusic(musicId, false);
        }

        public MusicChannel PlayMusic(int musicId, bool looped)
        {
            GameMusic music = BaseApp.sharedResourceMgr.GetMusic(musicId);
            musicChannel.Play(music, looped);
            return musicChannel;
        }

        public SoundChannel Play(int soundId)
        {
            return Play(soundId, SoundTransform.NONE);
        }

        public SoundChannel Play(int soundId, bool looped)
        {
            return Play(soundId, SoundTransform.NONE, looped);
        }

        public SoundChannel Play(int soundId, SoundTransform transform)
        {
            return Play(soundId, transform, false);
        }

        public SoundChannel Play(int soundId, SoundTransform transform, bool looped)
        {
            GameSound sound = BaseApp.sharedResourceMgr.GetSound(soundId);
            SoundChannel channel = FindDead();
            if (channel != null)
            {
                channel.Play(sound, transform, looped);
            }
            return channel;
        }   

        public void StopAll()
        {
            musicChannel.Stop();
            foreach (SoundChannel channel in channels)
            {
                if (channel != null)
                {
                    channel.Stop();
                }
            }
        }

        public void ResumeAll()
        {
            musicChannel.Resume();
            foreach (SoundChannel channel in channels)
            {
                if (channel != null)
                {
                    channel.Resume();
                }
            }
        }

        public void PauseAll()
        {
            musicChannel.Pause();
            PauseAllButMusic();
        }

        private void PauseAllButMusic()
        {
            foreach (SoundChannel channel in channels)
            {
                if (channel != null)
                {
                    channel.Resume();
                }
            }
        }
     
        private SoundChannel FindDead()
        {
            for (int i = 0; i < channels.Length; ++i)
            {
                SoundChannel channel = channels[i];
                if (channel == null)
                {
                    Debug.WriteLine("Create instance at: " + i);
                    channel = new SoundChannel();
                    channels[i] = channel;
                    return channel;
                }

                if (channel.State == SoundChannelState.STOPPED)
                {
                    Debug.WriteLine("Find dead at: " + i);
                    return channel;
                }
            }

            Debug.WriteLine("Found noting");
            return null;
        }
    }    
}