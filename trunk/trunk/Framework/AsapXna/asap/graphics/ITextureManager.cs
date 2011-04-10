using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace asap.graphics
{
    public interface ITextureManager
    {
        void UnloadTexture(Texture2D tex);
    }
}
