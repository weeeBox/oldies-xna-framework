using asap.graphics;
using Microsoft.Xna.Framework.Graphics;

namespace asap.visual
{
    public abstract class Shape<T> : DisplayObject where T : struct, IVertexType
    {
        protected T[] vertexData;
        protected PrimitiveType type;
        protected int primitivesCount;        

        protected void SetData(T[] vertexData, PrimitiveType type, int primitivesCount)
        {
            this.vertexData = vertexData;
            this.type = type;
            this.primitivesCount = primitivesCount;
        }        

        public override void Draw(Graphics g)
        {
            PreDraw(g);
            AppGraphics.DrawGeometry(type, vertexData, primitivesCount);
            PostDraw(g);
        }
    }
}
