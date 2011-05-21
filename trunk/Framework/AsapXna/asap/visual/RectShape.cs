using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AsapXna.asap.visual;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace asap.visual
{
    public class RectShape : Shape<VertexPositionColor>
    {
        public RectShape(float width, float height, Color color) : this(width, height, color, color)
        {
        }

        public RectShape(float width, float height, Color c1, Color c2)
        {
            VertexPositionColor[] vertexData = new VertexPositionColor[4];
            vertexData[0] = new VertexPositionColor(new Vector3(0, height, 0), c2);
            vertexData[1] = new VertexPositionColor(new Vector3(0, 0, 0), c1);
            vertexData[2] = new VertexPositionColor(new Vector3(width, height, 0), c2);
            vertexData[3] = new VertexPositionColor(new Vector3(width, 0, 0), c1);
            SetData(vertexData, PrimitiveType.TriangleStrip, 2);
        }        
    }
}
