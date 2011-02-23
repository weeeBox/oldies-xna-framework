using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using asap.anim;

namespace Flipstones2.res
{
    public class AnimationReader : ContentTypeReader<Animation>
    {
        protected override Animation Read(ContentReader input, Animation existingInstance)
        {            
            Animation animation = new Animation();

            animation.boundX = input.ReadInt16();
            animation.boundY = input.ReadInt16();
            animation.boundWidth = input.ReadInt16();
            animation.boundHeight = input.ReadInt16();

            int framesCount = input.ReadInt32();
            int[] delays = new int[framesCount];
            sbyte[][] frameData = new sbyte[framesCount][];
            for (int frame = 0; frame < framesCount; frame++)
            {
                delays[frame] = input.ReadInt16();
                int frameSize = input.ReadInt32();
                frameData[frame] = new sbyte[frameSize];
                for (int i = 0; i < frameSize; ++i)
                {
                    frameData[frame][i] = input.ReadSByte();
                }                

            }
            int LAYER_INFO_SIZE = 10;
            int layersCount = input.ReadByte();
            int[] layerMasks = new int[layersCount];
            sbyte[] layerData = new sbyte[layersCount * LAYER_INFO_SIZE];
            int offset = 0;
            for (int i = 0; i < layersCount; i++)
            {
                layerMasks[i] = input.ReadInt32();
                for (int j = 0; j < LAYER_INFO_SIZE; j++)
                {
                    layerData[offset] = input.ReadSByte();
                    offset++;
                }
            }
            Frame[] frames = new Frame[framesCount];
            animation.frames = frames;
            for (int frameNum = 0; frameNum < framesCount; frameNum++)
            {
                Frame frame = new Frame(delays[frameNum]);
                frames[frameNum] = frame;
                sbyte[] data = frameData[frameNum];
                int pos = 0;
                while (pos < (data.Length))
                {
                    int ch1 = (data[pos++]) & 255;
                    int ch2 = (data[pos++]) & 255;
                    int layerId = ((short)((ch1 << 8) | ch2));
                    int layerX = 0;
                    int layerY = 0;
                    int layerWidth = 0;
                    int layerHeight = 0;
                    int layerAlpha = 100;
                    bool needPosRead = layerId < 0;
                    if (needPosRead)
                    {
                        layerId = (-layerId) - 1;
                        layerX = (data[pos++]) << 4;
                        layerY = (data[pos++]) << 4;
                        int b = (data[pos++]) & 255;
                        layerX |= (b >> 4) & 15;
                        layerY |= b & 15;
                        layerAlpha = (data[pos++]) & 255;
                        int partSetIdLocal = (layerData[(layerId * LAYER_INFO_SIZE)]) & 255;
                        if (partSetIdLocal == 255)
                        {
                            layerWidth = (data[pos++]) << 4;
                            layerHeight = (data[pos++]) << 4;
                            b = (data[pos++]) & 255;
                            layerWidth |= (b >> 4) & 15;
                            layerHeight |= b & 15;
                        }
                    }
                    int layerMask = layerMasks[layerId];
                    int layerPos = layerId * LAYER_INFO_SIZE;
                    int partSetId = (layerData[layerPos++]) & 255;
                    ch1 = (layerData[layerPos++]) & 255;
                    ch2 = (layerData[layerPos++]) & 255;
                    int partId = (ch1 << 8) | ch2;
                    if (!needPosRead)
                    {
                        layerX = (layerData[layerPos++]) << 4;
                        layerY = (layerData[layerPos++]) << 4;
                        int b = (layerData[layerPos++]) & 255;
                        layerX |= (b >> 4) & 15;
                        layerY |= b & 15;
                        layerAlpha = (layerData[layerPos++]) & 255;
                        layerWidth = (layerData[layerPos++]) << 4;
                        layerHeight = (layerData[layerPos++]) << 4;
                        b = (layerData[layerPos++]) & 255;
                        layerWidth |= (b >> 4) & 15;
                        layerHeight |= b & 15;
                    }
                    frame.AddLayer(new AnimLayer(layerX, layerY, layerWidth, layerHeight, layerAlpha, partSetId, partId, layerMask));
                }
            }

            return animation;
        }
    }
}
