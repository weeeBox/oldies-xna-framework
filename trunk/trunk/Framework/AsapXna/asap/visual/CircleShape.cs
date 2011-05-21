using asap.graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using asap.resources;

namespace asap.visual
{
    public class CircleShape : Shape<VertexPositionColorTexture>
    {        
        private Color color;

        public CircleShape(float radius, Color color)
        {            
            this.color = color;
            SetRadius(radius);
        }

        public void SetRadius(float radius)
        {     
            this.width = this.height = 2 * radius;

            VertexPositionColorTexture[] vertexData = new VertexPositionColorTexture[4];
            vertexData[0] = new VertexPositionColorTexture(new Vector3(0, height, 0), color, new Vector2(0, 1));
            vertexData[1] = new VertexPositionColorTexture(new Vector3(0, 0, 0), color, new Vector2(0, 0));
            vertexData[2] = new VertexPositionColorTexture(new Vector3(width, height, 0), color, new Vector2(1, 1));
            vertexData[3] = new VertexPositionColorTexture(new Vector3(width, 0, 0), color, new Vector2(1, 0));
            SetData(vertexData, PrimitiveType.TriangleStrip, 2);
        }

        private void SetColor(Color color)
        {
            this.color = color;            
            for (int i = 0; i < vertexData.Length; ++i)
            {
                vertexData[i].Color = color;
            }
        }

        public override void Draw(Graphics g)
        {
            PreDraw(g);
            Effect customEffect = EmbededRes.circleEffect;
            customEffect.Parameters["TextureSize"].SetValue(width);
            customEffect.Parameters["Radius"].SetValue(0.5f * width);            
            AppGraphics.DrawGeometry(type, vertexData, primitivesCount, customEffect);
            PostDraw(g);
        }

        public override Color Color
        {
            get
            {
                return color;
            }
            set
            {
                if (color != value)
                {
                    SetColor(value);
                }
            }
        }
    }
}
