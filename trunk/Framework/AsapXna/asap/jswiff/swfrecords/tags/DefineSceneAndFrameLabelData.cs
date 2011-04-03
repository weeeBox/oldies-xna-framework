using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    public class DefineSceneAndFrameLabelData : Tag
    {
        private List<DefineSceneAndFrameLabelData.Scene>  scenes;
        
        private List<DefineSceneAndFrameLabelData.Frame>  frames;
        
        private int size;
        
        public DefineSceneAndFrameLabelData() 
        {
            scenes = new List<DefineSceneAndFrameLabelData.Scene> ();
            frames = new List<DefineSceneAndFrameLabelData.Frame> ();
            code = TagConstants.DEFINE_SCENE_AND_FRAME_LABEL_DATA;
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            size = data.Length;
            InputBitStream inStream = new InputBitStream(data);
            int scenesCount = ((int)(inStream.ReadEncodedU32()));
            for (int sceneIndex = 0; sceneIndex < scenesCount; sceneIndex++) 
            {
                int offset = ((int)(inStream.ReadEncodedU32()));
                String name = inStream.ReadString();
                Scene scene = new Scene(offset , name);
                scenes.Add(scene);
            }
            int framesCount = ((int)(inStream.ReadEncodedU32()));
            for (int frameIndex = 0; frameIndex < framesCount; frameIndex++) 
            {
                int frameNum = ((int)(inStream.ReadEncodedU32()));
                String label = inStream.ReadString();
                Frame frame = new Frame(frameNum , label);
                frames.Add(frame);
            }
        }
        
        public virtual List<DefineSceneAndFrameLabelData.Scene>  GetScenes()
        {
            return scenes;
        }
        
        public virtual List<DefineSceneAndFrameLabelData.Frame>  GetFrames()
        {
            return frames;
        }
        
        public virtual int GetSize()
        {
            return size;
        }
        
        public class Scene
        {
            private int offset;
            
            private String name;
            
            public Scene(int offset ,String name) 
            {
                this.offset = offset;
                this.name = name;
            }
            
            public virtual int GetOffset()
            {
                return offset;
            }
            
            public virtual String GetName()
            {
                return name;
            }
        }
        
        public class Frame
        {
            private int frameNum;
            
            private String label;
            
            public Frame(int frameNum ,String label) 
            {
                this.frameNum = frameNum;
                this.label = label;
            }
            
            public virtual int GetFrameNum()
            {
                return frameNum;
            }
            
            public virtual String GetLabel()
            {
                return label;
            }
        }
    }
}