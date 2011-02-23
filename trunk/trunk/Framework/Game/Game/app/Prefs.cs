using System;

using System.Collections.Generic;


using asap.rms;
using flipstones.game;

namespace flipstones
{
    public class Prefs
     {
        private static Prefs instance;
        
        private const String RMS_NAME = "FlipstonesPrefs";
        
        private const String BOOSTS_RMS_NAME = "FlipstonesBoostsPrefs";
        
        private bool soundEnabled = true;
        
        private bool musicEnabled = true;
        
        private bool flurryEnabled = true;
        
        private int starsCount = Settings.INITIAL_STARS_COUNT;
        
        private bool howToPlayOnStart = true;
        
        private int frenzyFinishesCount = 0;
        
        private Boost[] boosts = null;
        
        private Prefs() 
        {
        }
        
        public virtual void AddBoost(int boostId)
        {
            if ((boosts) == null) 
            {
                boosts = new Boost[1];
                boosts[0] = new Boost(boostId);
            } 
            else 
            {
                for (int i = 0; i < (boosts.Length); i++) 
                {
                    if ((boosts[i].GetId()) == boostId) 
                    {
                        return ;
                    } 
                }
                Boost[] newBoosts = new Boost[(boosts.Length) + 1];
                for (int i = 0; i < (boosts.Length); i++) 
                {
                    newBoosts[i] = boosts[i];
                }
                newBoosts[boosts.Length] = new Boost(boostId);
                boosts = newBoosts;
            }
            SaveBoosts();
        }
        
        public virtual void RemoveBoost(int boostId)
        {
            if ((boosts) != null) 
            {
                int count = 0;
                for (int i = 0; i < (boosts.Length); i++) 
                {
                    if (((boosts[i].GetId()) != boostId) || ((boosts[i].GetRemainedAmount()) < (Boost.FULL_AMOUNT))) 
                    {
                        count++;
                    } 
                }
                if (count == 0) 
                {
                    boosts = null;
                } 
                else if (count == (boosts.Length)) 
                {
                    System.Diagnostics.Debug.Assert(false);
                } 
                else 
                {
                    Boost[] newBoosts = new Boost[count];
                    int pos = 0;
                    for (int i = 0; i < (boosts.Length); i++) 
                    {
                        if (((boosts[i].GetId()) != boostId) || ((boosts[i].GetRemainedAmount()) < (Boost.FULL_AMOUNT))) 
                        {
                            newBoosts[pos++] = boosts[i];
                        } 
                    }
                    boosts = newBoosts;
                    System.Diagnostics.Debug.Assert(pos == count);
                }
                SaveBoosts();
            } 
        }
        
        public virtual bool CanRemoveBoost(int boostId)
        {
            if ((boosts) != null) 
            {
                for (int i = 0; i < (boosts.Length); i++) 
                {
                    if (((boosts[i].GetId()) == boostId) && ((boosts[i].GetRemainedAmount()) == (Boost.FULL_AMOUNT))) 
                    {
                        return true;
                    } 
                }
            } 
            return false;
        }
        
        public virtual int GetBoostsCount()
        {
            return (boosts) == null ? 0 : boosts.Length;
        }
        
        public virtual Boost GetBoostByNumber(int number)
        {
            return boosts[number];
        }
        
        public virtual Boost GetBoostById(int boostId)
        {
            if ((boosts) != null) 
            {
                for (int i = 0; i < (boosts.Length); i++) 
                {
                    if ((boosts[i].GetId()) == boostId) 
                    {
                        return boosts[i];
                    } 
                }
            } 
            return null;
        }
        
        public virtual void DecreaseBoostsAmount()
        {
            if ((boosts) != null) 
            {
                int count = 0;
                for (int i = 0; i < (boosts.Length); i++) 
                {
                    boosts[i].DecreaseAmount();
                    if ((boosts[i].GetRemainedAmount()) > 0) 
                    {
                        count++;
                    } 
                }
                if (count == 0) 
                {
                    boosts = null;
                } 
                else 
                {
                    Boost[] newBoosts = new Boost[count];
                    int pos = 0;
                    for (int i = 0; i < (boosts.Length); i++) 
                    {
                        if ((boosts[i].GetRemainedAmount()) > 0) 
                        {
                            newBoosts[pos++] = boosts[i];
                        } 
                    }
                    boosts = newBoosts;
                    System.Diagnostics.Debug.Assert(pos == count);
                }
                SaveBoosts();
            } 
        }
        
        public virtual bool HasBoost(int boostId)
        {
            return (GetBoostById(boostId)) != null;
        }
        
        public static Prefs GetInstance()
        {
            if ((Prefs.instance) == null)
                Prefs.instance = new Prefs();
            
            return Prefs.instance;
        }
        
        public virtual void LoadBoosts()
        {
            try 
            {
                sbyte[] data = RecordStorage.GetInstance().Load(BOOSTS_RMS_NAME);
                if (data != null) 
                {
                    DataInputStream stream = new DataInputStream(new ByteArrayInputStream(data));
                    int boostsCount = stream.ReadInt();
                    if (boostsCount == 0) 
                    {
                        boosts = null;
                    } 
                    else 
                    {
                        boosts = new Boost[boostsCount];
                        for (int i = 0; i < boostsCount; i++) 
                        {
                            int id = stream.ReadInt();
                            int amount = stream.ReadInt();
                            boosts[i] = new Boost(id , amount);
                        }
                    }
                    stream.Close();
                } 
                else 
                {
                    boosts = null;
                }
            }
            catch (Exception e) 
            {
                boosts = null;
                System.Diagnostics.Debug.Assert(false);
            }
        }
        
        public virtual void SaveBoosts()
        {
            try 
            {
                ByteArrayOutputStream outputStream = new ByteArrayOutputStream();
                DataOutputStream stream = new DataOutputStream(outputStream);
                if ((boosts) == null) 
                {
                    stream.WriteInt(0);
                } 
                else 
                {
                    stream.WriteInt(boosts.Length);
                    for (int i = 0; i < (boosts.Length); i++) 
                    {
                        stream.WriteInt(boosts[i].GetId());
                        stream.WriteInt(boosts[i].GetRemainedAmount());
                    }
                }
                RecordStorage.GetInstance().Save(BOOSTS_RMS_NAME, outputStream.ToByteArray());
            }
            catch (Exception e) 
            {
                System.Diagnostics.Debug.Assert(false);
            }
        }
        
        public virtual void Load()
        {
            try 
            {
                sbyte[] data = RecordStorage.GetInstance().Load(RMS_NAME);
                if (data != null) 
                {
                    DataInputStream stream = new DataInputStream(new ByteArrayInputStream(data));
                    soundEnabled = stream.ReadBoolean();
                    musicEnabled = stream.ReadBoolean();
                    flurryEnabled = stream.ReadBoolean();
                    howToPlayOnStart = stream.ReadBoolean();
                    starsCount = stream.ReadInt();
                    frenzyFinishesCount = stream.ReadInt();
                    if ((frenzyFinishesCount) < 0) 
                    {
                        frenzyFinishesCount = 0;
                    } 
                    stream.Close();
                } 
            }
            catch (Exception e) 
            {
                System.Diagnostics.Debug.Assert(false);
            }
            if (AudioSession.IsIpodMusicPlaying())
                musicEnabled = false;
            
            LoadBoosts();
        }
        
        public virtual void Save()
        {
            try 
            {
                ByteArrayOutputStream outputStream = new ByteArrayOutputStream();
                DataOutputStream stream = new DataOutputStream(outputStream);
                stream.WriteBoolean(soundEnabled);
                stream.WriteBoolean(musicEnabled);
                stream.WriteBoolean(flurryEnabled);
                stream.WriteBoolean(howToPlayOnStart);
                stream.WriteInt(starsCount);
                stream.WriteInt(frenzyFinishesCount);
                RecordStorage.GetInstance().Save(RMS_NAME, outputStream.ToByteArray());
            }
            catch (Exception e) 
            {
                System.Diagnostics.Debug.Assert(false);
            }
        }
        
        public virtual int GetFrenzyFinishesCount()
        {
            return frenzyFinishesCount;
        }
        
        public virtual void IncFrenzyFinishesCount()
        {
            (frenzyFinishesCount)++;
            Save();
        }
        
        public virtual int GetStarsCount()
        {
            return starsCount;
        }
        
        public virtual void SetStarsCount(int stars)
        {
            this.starsCount = stars;
            Save();
        }
        
        public virtual void AddStars(int stars)
        {
            this.starsCount += stars;
            Save();
        }
        
        public virtual void SubtractStars(int stars)
        {
            this.starsCount -= stars;
            if ((this.starsCount) < 0) 
            {
                System.Diagnostics.Debug.Assert(false);
                this.starsCount = 0;
            } 
            Save();
        }
        
        public virtual bool IsSoundEnabled()
        {
            return soundEnabled;
        }
        
        public virtual void SetSoundEnabled(bool enabled)
        {
            soundEnabled = enabled;
            Save();
        }
        
        public virtual bool IsMusicEnabled()
        {
            return musicEnabled;
        }
        
        public virtual void SetMusicEnabled(bool enabled)
        {
            musicEnabled = enabled;
            Save();
        }
        
        public virtual bool IsFlurryEnabled()
        {
            return flurryEnabled;
        }
        
        public virtual void SetFlurryEnabled(bool enabled)
        {
            flurryEnabled = enabled;
            Save();
        }
        
        public virtual bool IsHowToPlayOnStart()
        {
            return howToPlayOnStart;
        }
        
        public virtual void SetHowToPlayOnStart(bool enabled)
        {
            howToPlayOnStart = enabled;
            Save();
        }
        
    }
    
    
}