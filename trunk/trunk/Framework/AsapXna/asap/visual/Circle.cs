using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using asap.visual;
using asap.graphics;
using Microsoft.Xna.Framework;

namespace asap.visual
{
    public class Circle : BaseElementContainer
    {
        private float radius;
        private Color color;

        public Circle(float radius, Color color)
        {
            this.radius = radius;
            this.width = this.height = 2 * radius;
            this.color = color;
        }

        public override void Draw(Graphics g)
        {
            base.PreDraw(g);
            g.DrawCircle(0, 0, radius, color);
            base.PostDraw(g);
        }      
  
        public override Color Color
        {
            get { return color; }
            set { this.color = value; }            
        }
    }
}
