using System;

using System.Collections.Generic;


using asap.resources;

namespace asap.anim
{
    public class Animation
    {
        public int boundX;

        public int boundY;

        public int boundWidth;

        public int boundHeight;

        public PartSet[] partsets;

        public Frame[] frames;

        public Animation() /* throws Exception */
        {
            //partsets = new PartSet[partsetPaths.Length];
            //for (int i = 0; i < (partsetPaths.Length); i++)
            //    partsets[i] = ((PartSet)(ResManager.GetBaseInstance().GetRes(partsetPaths[i])));
            //DataInputStream stream = new DataInputStream(_is);
            //boundX = stream.ReadShort();
            //boundY = stream.ReadShort();
            //boundWidth = stream.ReadShort();
            //boundHeight = stream.ReadShort();
            //int framesCount = stream.ReadShort();
            //int[] delays = new int[framesCount];
            //sbyte[][] frameData = new sbyte[framesCount][];
            //for (int frame = 0; frame < framesCount; frame++) 
            //{
            //    delays[frame] = stream.ReadShort();
            //    int frameSize = stream.ReadShort();
            //    frameData[frame] = new sbyte[frameSize];
            //    if (frameSize > 0)
            //        stream.ReadFully(frameData[frame]);

            //}
            //int LAYER_INFO_SIZE = 10;
            //int layersCount = (stream.ReadByte()) & 255;
            //int[] layerMasks = new int[layersCount];
            //sbyte[] layerData = new sbyte[layersCount * LAYER_INFO_SIZE];
            //int offset = 0;
            //for (int i = 0; i < layersCount; i++) 
            //{
            //    layerMasks[i] = stream.ReadInt();
            //    for (int j = 0; j < LAYER_INFO_SIZE; j++) 
            //    {
            //        layerData[offset] = stream.ReadByte();
            //        offset++;
            //    }
            //}
            //frames = new Frame[framesCount];
            //for (int frameNum = 0; frameNum < framesCount; frameNum++) 
            //{
            //    Frame frame = new Frame(delays[frameNum]);
            //    frames[frameNum] = frame;
            //    sbyte[] data = frameData[frameNum];
            //    int pos = 0;
            //    while (pos < (data.Length)) 
            //    {
            //        int ch1 = (data[pos++]) & 255;
            //        int ch2 = (data[pos++]) & 255;
            //        int layerId = ((short)((ch1 << 8) | ch2));
            //        int layerX = 0;
            //        int layerY = 0;
            //        int layerWidth = 0;
            //        int layerHeight = 0;
            //        int layerAlpha = 100;
            //        bool needPosRead = layerId < 0;
            //        if (needPosRead) 
            //        {
            //            layerId = (-layerId) - 1;
            //            layerX = (data[pos++]) << 4;
            //            layerY = (data[pos++]) << 4;
            //            int b = (data[pos++]) & 255;
            //            layerX |= (b >> 4) & 15;
            //            layerY |= b & 15;
            //            layerAlpha = (data[pos++]) & 255;
            //            int partSetId = (layerData[(layerId * LAYER_INFO_SIZE)]) & 255;
            //            if (partSetId == 255) 
            //            {
            //                layerWidth = (data[pos++]) << 4;
            //                layerHeight = (data[pos++]) << 4;
            //                b = (data[pos++]) & 255;
            //                layerWidth |= (b >> 4) & 15;
            //                layerHeight |= b & 15;
            //            } 
            //        } 
            //        int layerMask = layerMasks[layerId];
            //        int layerPos = layerId * LAYER_INFO_SIZE;
            //        int partSetId = (layerData[layerPos++]) & 255;
            //        ch1 = (layerData[layerPos++]) & 255;
            //        ch2 = (layerData[layerPos++]) & 255;
            //        int partId = (ch1 << 8) | ch2;
            //        if (!needPosRead) 
            //        {
            //            layerX = (layerData[layerPos++]) << 4;
            //            layerY = (layerData[layerPos++]) << 4;
            //            int b = (layerData[layerPos++]) & 255;
            //            layerX |= (b >> 4) & 15;
            //            layerY |= b & 15;
            //            layerAlpha = (layerData[layerPos++]) & 255;
            //            layerWidth = (layerData[layerPos++]) << 4;
            //            layerHeight = (layerData[layerPos++]) << 4;
            //            b = (layerData[layerPos++]) & 255;
            //            layerWidth |= (b >> 4) & 15;
            //            layerHeight |= b & 15;
            //        } 
            //        frame.AddLayer(new AnimLayer(layerX , layerY , layerWidth , layerHeight , layerAlpha , partSetId , partId , layerMask));
            //    }
            //}
            throw new NotImplementedException();
        }

        public virtual PartSet GetPartset(int partsetIndex)
        {
            return partsets[partsetIndex];
        }

        public virtual int GetFramesCount()
        {
            return frames.Length;
        }

        public virtual Frame GetFrame(int frameNum)
        {
            return frames[frameNum];
        }

        public virtual int GetBoundX()
        {
            return boundX;
        }

        public virtual int GetBoundY()
        {
            return boundY;
        }

        public virtual int GetBoundWidth()
        {
            return boundWidth;
        }

        public virtual int GetBoundHeight()
        {
            return boundHeight;
        }

    }


}