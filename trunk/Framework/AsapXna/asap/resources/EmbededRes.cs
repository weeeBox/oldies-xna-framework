using asap.graphics.effects;
using Microsoft.Xna.Framework.Content;
using asap.graphics;
using Microsoft.Xna.Framework.Graphics;

namespace asap.resources
{
    public class EmbededRes
    {
        public static BaseElementEffect baseElementEffect;

        public static void Load(ContentManager content)
        {
            baseElementEffect = new BaseElementEffect(content.Load<Effect>(@"Embeded\BaseEffect"));
        }

        public static void Dispose()
        {
            baseElementEffect.Dispose();
        }
    }
}
