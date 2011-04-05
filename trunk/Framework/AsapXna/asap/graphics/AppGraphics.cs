﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace asap.graphics
{
    public enum AppBlendMode
    {
        AlphaBlend,
        NonPremultiplied,
        Additive,
        Opaque,
    }

    public class AppGraphics
    {
        enum BatchMode
        {
            None,
            Sprite,
            Geometry,
        }

        private static GraphicsDevice graphicsDevice;
        private static SpriteBatch spriteBatch;
        private static BasicEffect basicEffect;
        private static RasterizerState rasterizerState;

        private static BatchMode batchMode = BatchMode.None;
        private static Matrix matrix;
        public static Color drawColor;
        private static AppBlendMode blendMode = AppBlendMode.AlphaBlend;

        private static Stack<Matrix> matrixStack = new Stack<Matrix>();

        private static Color transpColor = new Color(0, 0, 0, 0);
        private static Vector2 zeroVector = new Vector2(0, 0);
        private static Matrix worldMatrix;
        private static Matrix viewMatrix;
        private static Matrix projection;

        private static Rectangle scissorRect;

        private static void BeginSpriteBatch(SpriteBatch sb, AppBlendMode blendMode, Matrix m, BatchMode mode)
        {
            Debug.Assert(mode != BatchMode.None);

            if (mode == BatchMode.Sprite)
            {
                BlendState blendState = toBlendState(blendMode);

                sb.GraphicsDevice.ScissorRectangle = scissorRect;
                sb.Begin(SpriteSortMode.Immediate, blendState, null, null, rasterizerState, null, m);
            }
            else if (mode == BatchMode.Geometry)
            {
                basicEffect.GraphicsDevice.ScissorRectangle = scissorRect;
                basicEffect.World = Matrix.Multiply(worldMatrix, m);
                basicEffect.CurrentTechnique.Passes[0].Apply();
            }
            batchMode = mode;
        }

        private static BlendState toBlendState(AppBlendMode mode)
        {
            switch (mode)
            {
                case AppBlendMode.AlphaBlend:
                    return BlendState.AlphaBlend;
                case AppBlendMode.Additive:
                    return BlendState.Additive;
                case AppBlendMode.Opaque:
                    return BlendState.Opaque;
                case AppBlendMode.NonPremultiplied:
                    return BlendState.NonPremultiplied;
                default:
                    throw new NotImplementedException();
            }
        }

        private static SpriteBatch GetSpriteBatch(BatchMode mode)
        {
            if (batchMode != mode)
            {
                EndBatch();
                BeginSpriteBatch(spriteBatch, blendMode, matrix, mode);
            }
            return spriteBatch;
        }

        private static void EndBatch()
        {
            if (batchMode == BatchMode.Geometry)
            {
                basicEffect.World = Matrix.Identity;
                basicEffect.CurrentTechnique.Passes[0].Apply();
            }
            else if (batchMode == BatchMode.Sprite)
            {
                spriteBatch.End();
            }

            batchMode = BatchMode.None;
        }

        public static AppBlendMode BlendMode
        {
            get { return blendMode; }
            set
            {
                if (blendMode != value)
                {
                    EndBatch();
                    blendMode = value;
                }
            }
        }       

        public static void SetColor(Color color)
        {
            drawColor = color;
        }

        public static Color GetColor()
        {
            return drawColor;
        }        

        public static void SetBlendMode(AppBlendMode mode)
        {
            if (blendMode != mode)
            {
                blendMode = mode;
                EndBatch();
            }
        }

        public static AppBlendMode GetBlendMode()
        {
            return blendMode;
        }

        public static void SetMatrix(Matrix _matrix)
        {
            matrix = _matrix;
            EndBatch();
        }

        public static void Begin(GraphicsDevice gd, int width, int height)
        {
            Debug.Assert(batchMode == BatchMode.None, "Bad batch mode: " + batchMode);

            matrixStack.Clear();
            matrix = Matrix.Identity;

            drawColor = Color.White;
            blendMode = AppBlendMode.AlphaBlend;
            scissorRect = new Rectangle(0, 0, width, height);

            if (graphicsDevice != gd)
            {
                graphicsDevice = gd;
                spriteBatch = new SpriteBatch(graphicsDevice);
                basicEffect = new BasicEffect(graphicsDevice);

                worldMatrix = Matrix.Identity;
                viewMatrix = Matrix.CreateLookAt(new Vector3(0.0f, 0.0f, 1.0f), Vector3.Zero, Vector3.Up);
                projection = Matrix.CreateOrthographicOffCenter(0.0f, width, height, 0, 1.0f, 1000.0f);

                basicEffect.World = worldMatrix;
                basicEffect.View = viewMatrix;
                basicEffect.Projection = projection;
                basicEffect.VertexColorEnabled = true;

                rasterizerState = new RasterizerState();
                rasterizerState.CullMode = CullMode.CullCounterClockwiseFace;
                rasterizerState.ScissorTestEnable = true;
            }
        }

        public static void End()
        {
            EndBatch();            
        }

        public static void PushMatrix()
        {
            matrixStack.Push(matrix);
        }

        public static void PopMatrix()
        {
            EndBatch();
            matrix = matrixStack.Pop();
        }

        public static void SetIdentity()
        {
            EndBatch();
            matrix = Matrix.Identity;
        }

        private static void AddTransform(Matrix t)
        {
            EndBatch();
            matrix = Matrix.Multiply(t, matrix);
        }

        public static void Translate(float tx, float ty, float tz)
        {
            AddTransform(Matrix.CreateTranslation(tx, ty, tz));
        }

        public static void Rotate(float grad, float ax, float ay, float az)
        {
            Matrix r;
            float rad = (float)(Math.PI * grad / 180);
            if (ax == 1 && ay == 0 && az == 0)
                r = Matrix.CreateRotationX(rad);
            else if (ax == 0 && ay == 1 && az == 0)
                r = Matrix.CreateRotationY(rad);
            else if (ax == 0 && ay == 0 && az == 1)
                r = Matrix.CreateRotationZ(rad);
            else
                throw new NotImplementedException();

            AddTransform(r);
        }

        public static void Scale(float sx, float sy, float sz)
        {
            Matrix r = Matrix.CreateScale(sx, sy, sz);
            AddTransform(r);
        }

        public static void Transform(ref Matrix m)
        {
            matrix = Matrix.Identity;
            AddTransform(m);
        }

        public static void ClearTransform()
        {
            EndBatch();
            matrix = Matrix.Identity;
        }

        public static void ClipRect(int x, int y, int width, int height)
        {
            if (!(x == scissorRect.X && y == scissorRect.Y && width == scissorRect.Width && height == scissorRect.Height))
            {
                EndBatch();
                scissorRect = Rectangle.Intersect(scissorRect, new Rectangle(x, y, width, height));
            }
        }

        public static void SetClip(int x, int y, int width, int height)
        {
            if (!(x == scissorRect.X && y == scissorRect.Y && width == scissorRect.Width && height == scissorRect.Height))
            {
                EndBatch();
                scissorRect.X = x;
                scissorRect.Y = y;
                scissorRect.Width = width;
                scissorRect.Height = height;
            }
        }

        public static int GetClipX()
        {
            return scissorRect.X;
        }

        public static int GetClipY()
        {
            return scissorRect.Y;
        }

        public static int GetClipWidth()
        {
            return scissorRect.Width;
        }

        public static int GetClipHeight()
        {
            return scissorRect.Height;
        }

        public static void DrawString(SpriteFont font, float x, float y, String text)
        {
            GetSpriteBatch(BatchMode.Sprite).DrawString(font, text, new Vector2((float)x, (float)y), drawColor);
        }

        public static void DrawImage(Texture2D tex, float x, float y)
        {
            GetSpriteBatch(BatchMode.Sprite).Draw(tex, new Vector2(x, y), drawColor);
        }

        public static void DrawImage(Texture2D tex, float x, float y, float opacity)
        {
            GetSpriteBatch(BatchMode.Sprite).Draw(tex, new Vector2(x, y), new Color(1.0f, 1.0f, 1.0f, opacity));
        }

        public static void DrawImage(Texture2D tex, float x, float y, Color color)
        {
            GetSpriteBatch(BatchMode.Sprite).Draw(tex, new Vector2(x, y), color);
        }

        public static void DrawImagePart(Texture2D tex, ref Rectangle src, float x, float y)
        {
            GetSpriteBatch(BatchMode.Sprite).Draw(tex, new Vector2(x, y), src, drawColor);
        }

        public static void DrawImagePart(Texture2D tex, Rectangle src, float x, float y, Color dc, float size)
        {
            GetSpriteBatch(BatchMode.Sprite).Draw(tex, new Vector2(x, y), src, drawColor);
        }

        public static void DrawImage(Texture2D tex, float x, float y, SpriteEffects flip)
        {
            GetSpriteBatch(BatchMode.Sprite).Draw(tex, new Vector2(x, y), null, drawColor, 0.0f, Vector2.Zero, 1.0f, flip, 0.0f);
        }

        public static void DrawImage(Texture2D tex, ref Vector2 position, ref Color color, float rotation, ref Vector2 origin, ref Vector2 scale, ref Vector2 flip)
        {
            SpriteEffects flipEffects = flip.X == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            if (flip.Y == 1)
                flipEffects |= SpriteEffects.FlipVertically;

            GetSpriteBatch(BatchMode.Sprite).Draw(tex, position, null, color, rotation, origin, scale, flipEffects, 0.0f);
        }

        public static void DrawImage(Texture2D tex, ref Rectangle src, ref Vector2 position, ref Color color, float rotation, ref Vector2 origin, ref Vector2 scale, ref Vector2 flip)
        {
            SpriteEffects flipEffects = flip.X == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            if (flip.Y == 1)
                flipEffects |= SpriteEffects.FlipVertically;

            GetSpriteBatch(BatchMode.Sprite).Draw(tex, position, src, color, rotation, origin, scale, flipEffects, 0.0f);
        }

        public static void DrawScaledImage(Texture2D tex, float x, float y, float scaleX, float scaleY)
        {
            Vector2 origin = new Vector2(0.5f * tex.Width, 0.5f * tex.Height);
            Vector2 scale = new Vector2(scaleX, scaleY);
            GetSpriteBatch(BatchMode.Sprite).Draw(tex, new Vector2(x, y), null, drawColor, 0.0f, origin, scale, SpriteEffects.None, 0.0f);
        }

        public static void DrawScaledImage(Texture2D tex, float x, float y, float scale)
        {
            Vector2 origin = new Vector2(0.5f * tex.Width, 0.5f * tex.Height);
            GetSpriteBatch(BatchMode.Sprite).Draw(tex, new Vector2(x, y), null, drawColor, 0.0f, origin, scale, SpriteEffects.None, 0.0f);
        }

        public static void DrawImageRotated(Texture2D tex, float x, float y, Vector2 origin, float rotation)
        {
            GetSpriteBatch(BatchMode.Sprite).Draw(tex, new Vector2(x, y), null, drawColor, rotation, origin, 1.0f, SpriteEffects.None, 0.0f);
        }

        public static void DrawImageTiled(Texture2D tex, ref Rectangle src, ref Rectangle dest)
        {
            // TODO: implement with texture repeat
            int destWidth = dest.Width;
            int destHeight = dest.Height;
            int srcWidth = src.Width;
            int srcHeight = src.Height;
            int numTilesX = destWidth / srcWidth + (destWidth % srcWidth != 0 ? 1 : 0);
            int numTilesY = destHeight / srcHeight + (destHeight % srcHeight != 0 ? 1 : 0);
            int x = dest.X;
            int y = dest.Y;
            for (int tileY = 0; tileY < numTilesY; ++tileY)
            {
                for (int tileX = 0; tileX < numTilesX; ++tileX)
                {
                    DrawImagePart(tex, ref src, x, y);
                    x += srcWidth;
                }
                y += srcHeight;
            }
        }

        public static void DrawCircle(float x, float y, float r, Color color)
        {
            GetSpriteBatch(BatchMode.Geometry);

            int numVertex = 200;
            VertexPositionColor[] vertexData = new VertexPositionColor[numVertex];
            float da = MathHelper.TwoPi / (numVertex - 1);
            float angle = 0;
            for (int i = 0; i < numVertex - 1; ++i)
            {
                float vx = (float)(x + r * Math.Cos(angle));
                float vy = (float)(y + r * Math.Sin(angle));
                vertexData[i] = new VertexPositionColor(new Vector3(vx, vy, 0), color);
                angle += da;
            }
            vertexData[numVertex - 1] = vertexData[0];
            graphicsDevice.DrawUserPrimitives(PrimitiveType.LineStrip, vertexData, 0, numVertex - 1);
        }

        public static void DrawRect(float x, float y, float width, float height)
        {
            DrawRect(x, y, width, height, drawColor);
        }

        public static void DrawRect(float x, float y, float width, float height, Color color)
        {
            GetSpriteBatch(BatchMode.Geometry);

            VertexPositionColor[] vertexData = new VertexPositionColor[4];
            vertexData[0] = new VertexPositionColor(new Vector3(x, y, 0), color);
            vertexData[1] = new VertexPositionColor(new Vector3(x + width, y, 0), color);
            vertexData[2] = new VertexPositionColor(new Vector3(x + width, y + height, 0), color);
            vertexData[3] = new VertexPositionColor(new Vector3(x, y + height, 0), color);
            short[] indexData = new short[] { 0, 1, 2, 3, 0 };

            graphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.LineStrip, vertexData, 0, 4, indexData, 0, 4);
        }

        public static void DrawLine(float x1, float y1, float x2, float y2, Color color)
        {
            GetSpriteBatch(BatchMode.Geometry);

            VertexPositionColor[] vertexData = new VertexPositionColor[2];
            vertexData[0] = new VertexPositionColor(new Vector3(x1, y1, 0), color);
            vertexData[1] = new VertexPositionColor(new Vector3(x2, y2, 0), color);

            graphicsDevice.DrawUserPrimitives(PrimitiveType.LineStrip, vertexData, 0, 1);
        }

        public static void FillRect(float x, float y, float width, float height, Color color)
        {
            GetSpriteBatch(BatchMode.Geometry);

            VertexPositionColor[] vertexData = new VertexPositionColor[4];
            vertexData[0] = new VertexPositionColor(new Vector3(x, y, 0), color);
            vertexData[1] = new VertexPositionColor(new Vector3(x + width, y, 0), color);
            vertexData[2] = new VertexPositionColor(new Vector3(x, y + height, 0), color);
            vertexData[3] = new VertexPositionColor(new Vector3(x + width, y + height, 0), color);

            graphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleStrip, vertexData, 0, 2);
        }

        public static void Clear(Color color)
        {
            EndBatch();
            graphicsDevice.Clear(color);
        }

        public static GraphicsDevice GraphicsDevice
        {
            get { return graphicsDevice; }
        }
    }
}
