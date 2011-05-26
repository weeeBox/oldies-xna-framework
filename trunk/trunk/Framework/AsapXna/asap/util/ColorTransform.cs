using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace asap.util
{
    public struct ColorTransform
    {
        public static ColorTransform NONE = new ColorTransform();

        public Color4 addTerm;
        public Color4 mulTerm;        

        public static ColorTransform CreateTint(Color color)
        {
            ColorTransform ct;
            ct.addTerm = new Color4(color.R, color.G, color.B, 0.0f);
            ct.mulTerm = new Color4(1.0f, 1.0f, 1.0f, 1.0f);
            return ct;
        }

        public static ColorTransform CreateColorize(float r, float g, float b, float a)
        {
            ColorTransform ct;
            ct.addTerm = new Color4(0.0f, 0.0f, 0.0f, 0.0f);
            ct.mulTerm = new Color4(r, g, b, a);
            return ct;
        }

        public static ColorTransform CreateColorize(Color color)
        {
            ColorTransform ct;
            ct.addTerm = new Color4(0.0f, 0.0f, 0.0f, 0.0f);
            ct.mulTerm = new Color4(color.R, color.G, color.B, color.A);
            return ct;
        }

        public static ColorTransform Create(Color4 mulTerm, Color4 addTerm)
        {
            ColorTransform ct;
            ct.addTerm = addTerm;
            ct.mulTerm = mulTerm;
            return ct;
        }        

        public float AddR
        {
            get { return addTerm.R; }
            set { addTerm.R = value; }
        }

        public float AddG
        {
            get { return addTerm.G; }
            set { addTerm.G = value; }
        }

        public float AddB
        {
            get { return addTerm.B; }
            set { addTerm.B = value; }
        }

        public float AddA
        {
            get { return addTerm.A; }
            set { addTerm.A = value; }
        }

        public float MulR
        {
            get { return mulTerm.R; }
            set { mulTerm.R = value; }
        }

        public float MulG
        {
            get { return mulTerm.G; }
            set { mulTerm.G = value; }
        }

        public float MulB
        {
            get { return mulTerm.B; }
            set { mulTerm.B = value; }
        }

        public float MulA
        {
            get { return mulTerm.A; }
            set { mulTerm.A = value; }
        }        
    }
}
