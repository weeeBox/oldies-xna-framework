using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace asap.graphics
{
    public abstract class BaseElement
    {
        public const float ALIGN_MIN = 0.0f;
        public const float ALIGN_CENTER = 0.5f;
        public const float ALIGN_MAX = 1.0f;               
                
        public bool visible;
        public bool updateable;
        private bool focusable;
        private bool focused;

        public float x;
        public float y;
        public float drawX;
        public float drawY;

        public int width;
        public int height;

        public float rotation;
        public float rotationCenterX;
        public float rotationCenterY;

        public float scaleX;
        public float scaleY;

        public Color color;

        public float translateX;
        public float translateY;

        public float alignX;
        public float alignY;

        public float parentAlignX;
        public float parentAlignY;

        private BaseElement parent;               

        public BaseElement()
            : this(0, 0)
        {
        }

        public BaseElement(int width, int height)
            : this(0, 0, width, height)
        {
        }

        public BaseElement(float x, float y, int width, int height)
        {
            visible = true;
            updateable = true;
            focusable = false;
            focused = false;

            this.x = x;
            this.y = y;

            this.width = width;
            this.height = height;

            rotation = 0;
            rotationCenterX = 0;
            rotationCenterY = 0;
            scaleX = 1.0f;
            scaleY = 1.0f;
            color = Color.White; //solidOpaqueRGBA;
            translateX = 0;
            translateY = 0;

            parentAlignX = parentAlignY = alignX = alignY = ALIGN_MIN;
            parent = null;
        }        

        public virtual void update(float delta)
        {          
        }

        public void restoreTransformations()
        {
            if (color != Color.White)
            {
                AppGraphics.SetColor(Color.White);
            }

            // if any transformation
            if (rotation != 0.0 || scaleX != 1.0 || scaleY != 1.0 || translateX != 0.0 || translateY != 0.0)
            {
                AppGraphics.PopMatrix();
            }
        }

        public virtual void preDraw()
        {
            // align to parent
            drawX = x - width * alignX;
            drawY = y - height * alignY;

            if (parent != null)
            {
                drawX += parent.drawX + parent.width * parentAlignX;
                drawY += parent.drawY + parent.height * parentAlignY;
            }

            bool changeScale = (scaleX != 1.0 || scaleY != 1.0);
            bool changeRotation = (rotation != 0.0);
            bool changeTranslate = (translateX != 0.0 || translateY != 0.0);

            // apply transformations
            if (changeScale || changeRotation || changeTranslate)
            {
                AppGraphics.PushMatrix();

                if (changeScale || changeRotation)
                {
                    float rotationOffsetX = drawX + (width >> 1) + rotationCenterX;
                    float rotationOffsetY = drawY + (height >> 1) + rotationCenterY;

                    AppGraphics.Translate(rotationOffsetX, rotationOffsetY, 0);

                    if (changeRotation)
                    {
                        AppGraphics.Rotate(rotation, 0, 0, 1);
                    }

                    if (changeScale)
                    {
                        AppGraphics.Scale(scaleX, scaleY, 1);
                    }
                    AppGraphics.Translate(-rotationOffsetX, -rotationOffsetY, 0);
                }

                if (changeTranslate)
                {
                    AppGraphics.Translate(translateX, translateY, 0);
                }
            }

            if (color != Color.White)
            {
                AppGraphics.SetColor(color);
            }
        }

        public virtual void postDraw()
        {
            restoreTransformations();
        }

        public virtual void draw()
        {
            preDraw();
            postDraw();
        }

        public BaseElement getParent()
        {
            return parent;
        }

        public void setParent(BaseElement parent)
        {
            this.parent = parent;
        }        

        public void setEnabled(bool e)
        {
            visible = e;
            updateable = e;
        }

        public bool isEnabled()
        {
            return (visible && updateable);
        }

        public void setFocusable(bool f)
        {
            focusable = true;
        }

        public bool isFocusable()
        {
            return focusable;
        }

        public virtual bool isAcceptingInput()
        {
            return isFocusable() && isEnabled();
        }       

        public void toParentCenter()
        {
            setAlign(ALIGN_CENTER, ALIGN_CENTER);
            setParentAlign(ALIGN_CENTER, ALIGN_CENTER);
        }

        public void setAlign(float alignX, float alignY)
        {
            this.alignX = alignX;
            this.alignY = alignY;
        }

        public void setParentAlign(float alignX, float alignY)
        {
            this.parentAlignX = alignX;
            this.parentAlignY = alignY;
        }
    }
}
