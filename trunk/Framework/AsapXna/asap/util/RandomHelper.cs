using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace asap.util
{
    public class RandomHelper
    {
        public static Random random = new Random();

        public static float rnd()
        {
            return (float)random.NextDouble();
        }

        public static bool rnd_bool()
        {
            return random.NextDouble() >= 0.5;
        }

        // [x1, x2)
        public static float rnd_float(float x1, float x2)
        {
            return (float)random.NextDouble() * (x2 - x1) + x1;
        }

        //[x1, x2]
        public static int rnd_int(int x1, int x2)
        {
            return (int)rnd_float(x1, x2 + 1);
        }

        // [0, max)
        public static int rnd_int(int max)
        {
            return random.Next(max);
        }
    }
}
