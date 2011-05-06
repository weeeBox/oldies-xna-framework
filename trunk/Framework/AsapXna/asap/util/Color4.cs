using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace asap.util
{
    public struct Color4
    {
        public float R;
        public float G;
        public float B;
        public float A;

        public Color4(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public Color4(int r, int g, int b, int a)
        {
            R = r / 255f;
            G = g / 255f;
            B = b / 255f;
            A = a / 255f;
        }

        public Vector4 ToVector()
        {
            return new Vector4(R, G, B, A);
        }

        public override string ToString()
        {
            return String.Format("r:{0} g:{1} b:{2} a:{3}", R, G, B, A);
        }
    }
}
