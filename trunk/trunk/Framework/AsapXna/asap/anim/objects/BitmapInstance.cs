using asap.graphics;
using swiff.com.jswiff.swfrecords.tags;

namespace asap.anim.objects
{
    public class BitmapInstance : CharacterInstance
    {
        private GameTexture image;

        public BitmapInstance(int characterId, GameTexture image) : base(CharacterConstansts.BITMAP, characterId)
        {
            this.image = image;
            this.width = image.GetWidth();
            this.height = image.GetHeight();
        }

        public override void Draw(Graphics g)
        {
            PreDraw(g);
            g.DrawImage(image, drawX, drawY);
            PostDraw(g);
        }
    }
}
