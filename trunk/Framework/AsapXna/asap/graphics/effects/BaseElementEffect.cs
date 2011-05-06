using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using asap.util;

namespace asap.graphics.effects
{
    public class BaseElementEffect : GameEffect
    {
        private static readonly string VAL_ADD = "colorAdd";
        private static readonly string VAL_MUL = "colorMul";               

        public BaseElementEffect(Effect effect) : base(effect)
        {            
        }        

        public void SetMulTerm(Color4 colorMult)
        {
            SetValue(VAL_MUL, colorMult);
        }

        public void SetAddTerm(Color4 colorAdd)
        {
            SetValue(VAL_ADD, colorAdd);
        }

        public void SetCtForm(ref ColorTransform ctForm)
        {
            SetMulTerm(ctForm.mulTerm);
            SetAddTerm(ctForm.addTerm);
        }
    }
}
