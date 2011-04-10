using System.Collections.Generic;
using asap.anim;
using swiff.com.jswiff.swfrecords.tags;
using asap.graphics;
using System;
using asap.core;
using System.Diagnostics;
using asap.anim.objects;
using Microsoft.Xna.Framework;
using swiff.com.jswiff.swfrecords;

namespace AsapXna.asap.anim
{
    public class SwfPlayer : TimerListener
    {
        private SwfMovie movie;
        private SwfDisplayList displayList;
                        
        private int currentFrame;
        private int tagPointer;
        
        private Timer timer;

        public SwfPlayer()
        {
            displayList = new SwfDisplayList();
        }

        public void SetMovie(SwfMovie movie)
        {
            this.movie = movie;                        
        }
        
        private void Reset()
        {
            currentFrame = -1;
            tagPointer = 0;
            displayList.Clear();

            if (timer != null)
            {
                timer.Cancel();
                timer = null;
            }
        }
   
        public void Start(TimerSource timerSource)
        {
            Reset();

            timer = new Timer(timerSource, this);
            float delay = 1.0f / FrameRate;            
            timer.Schedule(delay, true);
        }

        public void Draw(Graphics g)
        {
            g.PushTransform();

            int maxDepth = displayList.Size;
            for (int depth = 1; depth <= maxDepth; ++depth)
            {
                CharacterInstance inst = displayList[depth];
                SetMatrix(g, inst.Matrix);                
                inst.Draw(g);
            }
            g.PopTransform();
        }

        private void SetMatrix(Graphics g, SwfMatrix swfMatrix)
        {
            Matrix m = Matrix.Identity;            
            if (swfMatrix.HasScale())
            {
                m.M11 = (float)swfMatrix.GetScaleX();
                m.M22 = (float)swfMatrix.GetScaleY();
            }
            if (swfMatrix.HasRotateSkew())
            {
                m.M12 = (float)swfMatrix.GetRotateSkew0();
                m.M21 = (float)swfMatrix.GetRotateSkew1();
            }            
            m.M41 = swfMatrix.GetTranslateX() / 20.0f;
            m.M42 = swfMatrix.GetTranslateY() / 20.0f;
            g.SetTransform(ref m);
        }               

        public void OnTimer(Timer timer)
        {            
            ProcessFrame(++currentFrame);
        }

        private void ProcessFrame(int currentFrame)
        {
            List<Tag> tags = movie.Tags;
            int tagsCount = tags.Count;

            bool breakFlag = false;
            for (;tagPointer < tagsCount && !breakFlag; ++tagPointer)
            {
                Tag tag = tags[tagPointer];
                switch (tag.GetCode())
                {
                    case TagConstants.SHOW_FRAME:                        
                        breakFlag = true;
                        break;

                    case TagConstants.PLACE_OBJECT:
                    case TagConstants.PLACE_OBJECT_3:
                    {
                        throw new NotImplementedException();                        
                    }
                    case TagConstants.PLACE_OBJECT_2:                    
                    {
                        PlaceObject2 placeObject = (PlaceObject2)tag;
                        bool isMove = placeObject.IsMove();                        
                        int depth = placeObject.GetDepth();
                        if (isMove)
                        {
                            bool hasCharacter = placeObject.HasCharacter();  
                            if (hasCharacter)
                            {
                                throw new NotImplementedException(placeObject.ToString());
                            }
                            else
                            {
                                if (placeObject.HasMatrix())
                                {                                    
                                    CharacterInstance instance = displayList[depth];
                                    Debug.Assert(placeObject.GetMatrix() != null);
                                    instance.Matrix = placeObject.GetMatrix();
                                }
                            }
                        }
                        else
                        {
                            Debug.Assert(placeObject.HasCharacter());
                            int characterId = placeObject.GetCharacterId();
                            displayList[depth] = movie.CreateInstance(characterId);
                        }                        
                        
                        break;
                    }                        
                }
            }
            

            if (tagPointer == tagsCount)
            {                
                Debug.WriteLine("Animation ended");
                timer.Cancel();
            }
        }        
        
        private int FramesCount
        {
            get { return movie.FramesCount; }
        }

        private int FrameRate
        {
            get { return movie.FrameRate; }
        }        
    }
}
