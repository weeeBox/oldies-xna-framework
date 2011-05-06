using asap.graphics;
using asap.visual;
using Microsoft.Xna.Framework;
using swiff.com.jswiff.swfrecords;
using asap.util;

namespace asap.anim.objects
{
    public abstract class CharacterInstance : BaseElementContainer
    {
        private Matrix matrix = Matrix.Identity;
        private bool hasTransform = false;        

        public void SetSwfColorTransform(CXformWithAlpha ct)
        {
            ctForm = ColorTransform.Advance(ct.GetMulTerm(), ct.GetAddTerm());
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
    }
}
