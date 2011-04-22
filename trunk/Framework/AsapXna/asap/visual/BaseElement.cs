using asap.graphics;
using Microsoft.Xna.Framework;

namespace asap.visual
{
    public abstract class BaseElement
    {
        public const float ALIGN_MIN = 0.0f;
        public const float ALIGN_CENTER = 0.5f;
        public const float ALIGN_MAX = 1.0f;        
                
        public bool visible;
        public bool updateable;        

        public float x;
        public float y;
        public float drawX;
        public float drawY;

        public float width;
        public float height;

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

        public BaseElement(float width, float height)
            : this(0, 0, width, height)
        {
        }

        public BaseElement(float x, float y, float width, float height)
        {
            visible = true;
            updateable = true;            

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

        public virtual void Update(float delta)
        {         
        }

        public void RestoreTransformations()
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

        public virtual void PreDraw(Graphics g)
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
                    float rotationOffsetX = drawX + 0.5f * width + rotationCenterX;
                    float rotationOffsetY = drawY + 0.5f * height + rotationCenterY;

                    AppGraphics.Translate(rotationOffsetX, rotationOffsetY);

                    if (changeRotation)
                    {
                        AppGraphics.Rotate(rotation);
                    }

                    if (changeScale)
                    {
                        AppGraphics.Scale(scaleX, scaleY);
                    }
                    AppGraphics.Translate(-rotationOffsetX, -rotationOffsetY);
                }

                if (changeTranslate)
                {
                    AppGraphics.Translate(translateX, translateY);
                }
            }

            if (color != Color.White)
            {
                AppGraphics.SetColor(color);
            }
        }

        public virtual void PostDraw(Graphics g)
        {
            RestoreTransformations();
        }

        public virtual void Draw(Graphics g)
        {
            PreDraw(g);
            PostDraw(g);
        }

        public BaseElement GetParent()
        {
            return parent;
        }

        public void SetParent(BaseElement parent)
        {
            this.parent = parent;
        }        

        public void SetEnabled(bool e)
        {
            visible = e;
            updateable = e;
        }

        public bool IsEnabled()
        {
            return (visible && updateable);
        }

        public void SetAlign(float alignX, float alignY)
        {
            this.alignX = alignX;
            this.alignY = alignY;
        }

        public void SetParentAlign(float alignX, float alignY)
        {
            this.parentAlignX = alignX;
            this.parentAlignY = alignY;
        }                

        public float Width
        {
            get { return width; }
            set { width = value; }
        }

        public float Height
        {
            get { return height; }
            set { height = value; }
        }
    }
}
