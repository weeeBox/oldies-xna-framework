using asap.graphics;
using asap.util;
using Microsoft.Xna.Framework;
using asap.resources;
using asap.graphics.effects;

namespace asap.visual
{
    public abstract class BaseElement
    {
        public const float ALIGN_MIN = 0.0f;
        public const float ALIGN_CENTER = 0.5f;
        public const float ALIGN_MAX = 1.0f;        
                
#if DEBUG
        public Color borderColor = Color.White;
        public bool drawBorder;
#endif

        private bool visible;
        private bool updateable;        

        public float x;
        public float y;

        private float drawX;
        private float drawY;

        public float width;
        public float height;

        public float rotation;
        public float rotationCenterX;
        public float rotationCenterY;

        public float scaleX;
        public float scaleY;
                
        public ColorTransform ctForm;

        public GameEffect effect;

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
            ctForm = ColorTransform.NONE;
            translateX = 0;
            translateY = 0;

            parentAlignX = parentAlignY = alignX = alignY = ALIGN_MIN;
            parent = null;            
        }        

        public virtual void Update(float delta)
        {         
        }        

        public virtual void PreDraw(Graphics g)
        {
            // align to parent
            drawX = x - width * alignX;
            drawY = y - height * alignY;

            if (parent != null)
            {
                drawX += parent.width * parentAlignX;
                drawY += parent.height * parentAlignY;
            }            

            ApplyDrawState(g);
        }        
        
        public virtual void PostDraw(Graphics g)
        {
            RestoreDrawState(g);
        }
                
        public virtual void Draw(Graphics g)
        {
            PreDraw(g);
            PostDraw(g);
        }
                
        /////////////////////////////////////////////////////////////////////////////
        // Draw state

        protected void ApplyDrawState(Graphics g)
        {            
            ApplyTransformations();
            ApplyEffect();
            g.Translate(drawX, drawY);
        }

        protected virtual void ApplyTransformations()
        {
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
        }

        private void ApplyEffect()
        {            
            if (!ctForm.Equals(ColorTransform.NONE))
            {
                BaseElementEffect baseEffect = EmbededRes.baseElementEffect;
                baseEffect.SetCtForm(ref ctForm);
                effect = baseEffect;
            }

            if (effect != null)
            {
                AppGraphics.SetEffect(effect);
            }
        }

        protected void RestoreDrawState(Graphics g)
        {            
#if DEBUG
            if (drawBorder)
            {
                AppGraphics.DrawRect(0, 0, width, height, borderColor);
            }
#endif
            g.Translate(-drawX, -drawY);
            RestoreEffect();
            RestoreTransformations();            
        }

        protected virtual void RestoreTransformations()
        {
            // if any transformation
            if (rotation != 0.0 || scaleX != 1.0 || scaleY != 1.0 || translateX != 0.0 || translateY != 0.0)
            {
                AppGraphics.PopMatrix();
            }
        }

        private void RestoreEffect()
        {
            if (effect != null)
            {
                AppGraphics.SetEffect(null);
            }
        }

        public BaseElement GetParent()
        {
            return parent;
        }

        public void SetParent(BaseElement parent)
        {
            this.parent = parent;
        }

        public bool IsAncestorOf(BaseElement element)
        {
            BaseElement parent = this;

            while ((parent = parent.GetParent()) != null)
            {
                if (parent == element)
                    return true;
            }

            return false;
        }

        public virtual Color Tint
        {
            get { return new Color(ctForm.AddR, ctForm.AddG, ctForm.AddB, ctForm.AddA); }
            set { ctForm = ColorTransform.CreateTint(value); }
        }

        public virtual Color Color
        {
            get { return new Color(ctForm.MulR, ctForm.MulG, ctForm.MulB, ctForm.MulA); }
            set { ctForm = ColorTransform.CreateColorize(value); }
        }

        public void SetVisible(bool visible)
        {
            this.visible = visible;
        }

        public bool IsVisible()
        {
            return visible;
        }

        public void SetUpdatable(bool updateable)
        {
            this.updateable = updateable;
        }

        public bool IsUpdatable()
        {
            return updateable;
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
