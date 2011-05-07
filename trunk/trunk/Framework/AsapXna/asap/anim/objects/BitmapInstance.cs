using asap.graphics;
using swiff.com.jswiff.swfrecords.tags;

namespace asap.anim.objects
{
    public class BitmapInstance : CharacterInstance
    {
        private GameTexture image;

        public BitmapInstance(int characterId, GameTexture image) : base(characterId)
        {
            this.image = image;
        }

        public override void Draw(Graphics g)
        {
            PreDraw(g);
            g.DrawImage(image, drawX, drawY);
            PostDraw(g);
        }
    }
}
