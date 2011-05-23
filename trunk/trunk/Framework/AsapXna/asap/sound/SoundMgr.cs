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

        public SoundChannel PlaySound(int soundId)
        {
            return PlaySound(soundId, SoundTransform.NONE);
        }

        public SoundChannel PlaySound(int soundId, bool looped)
        {
            return PlaySound(soundId, SoundTransform.NONE, looped);
        }

        public SoundChannel PlaySound(int soundId, SoundTransform transform)
        {
            return PlaySound(soundId, transform, false);
        }

        public SoundChannel PlaySound(int soundId, SoundTransform transform, bool looped)
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
                    channel = new SoundChannel();
                    channels[i] = channel;
                    return channel;
                }

                if (channel.State == SoundChannelState.STOPPED)
                {                    
                    return channel;
                }
            }

            Debug.WriteLine("Can't play effect");
            return null;
        }
    }    
}