using System;
using asap.graphics;
using asap.util;
using asap.visual;
using Microsoft.Xna.Framework;
using swiff.com.jswiff.swfrecords;

namespace asap.anim.objects
{
    public class CharacterInstance : DisplayObjectContainer, IDisposable
    {
        public static CharacterInstance NULL = new CharacterInstance();

        private Matrix matrix;
        private bool hasTransform;
        private int characterId;
        private int code;

        private CharacterInstance()
        {
            SetUpdatable(false);
            SetVisible(false);
        }

        public CharacterInstance(int code, int characterId)
        {
            this.characterId = characterId;
            this.code = code;
            Reset();
        }

        private void Reset()
        {
            matrix = Matrix.Identity;
            hasTransform = false;
        }

        public void SetSwfColorTransform(CXformWithAlpha ct)
        {
            ctForm = ColorTransform.Create(ct.GetMulTerm(), ct.GetAddTerm());
        }        

        public void SetSwfMatrix(SwfMatrix swfMatrix)
        {
            matrix = Matrix.Identity;            
            matrix.M11 = (float)swfMatrix.GetScaleX();
            matrix.M22 = (float)swfMatrix.GetScaleY();            
            matrix.M12 = (float)swfMatrix.GetRotateSkew0();
            matrix.M21 = (float)swfMatrix.GetRotateSkew1();            
            matrix.M41 = swfMatrix.GetTranslateX() / 20.0f;
            matrix.M42 = swfMatrix.GetTranslateY() / 20.0f;
            hasTransform = matrix != Matrix.Identity;
        }

        protected override void ApplyTransformations()
        {            
            if (hasTransform)
            {
                AppGraphics.PushMatrix();
                AppGraphics.Transform(ref matrix);
            }
        }

        protected override void RestoreTransformations()
        {
            if (hasTransform)
            {
                AppGraphics.PopMatrix();
            }
        }

        public int GetCharacterId()
        {
            return characterId;
        }

        public void SetCharacterId(int characterId)
        {
            this.characterId = characterId;
        }

        public int GetCode()
        {
            return code;
        }

        public virtual void Dispose()
        {            
        }
    }
}
