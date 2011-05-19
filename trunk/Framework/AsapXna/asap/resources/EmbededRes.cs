using asap.graphics.effects;
using Microsoft.Xna.Framework.Content;
using asap.graphics;
using Microsoft.Xna.Framework.Graphics;

namespace asap.resources
{
    public class EmbededRes
    {
        public static BaseElementEffect baseElementEffect;
        public static Effect circleEffect;

        public static void Load(ContentManager content)
        {
            baseElementEffect = new BaseElementEffect(content.Load<Effect>(@"Embeded\BaseEffect"));
            circleEffect = content.Load<Effect>(@"Embeded\Circle");
        }

        public static void Dispose()
        {
            baseElementEffect.Dispose();
            circleEffect.Dispose();
        }
    }
}
