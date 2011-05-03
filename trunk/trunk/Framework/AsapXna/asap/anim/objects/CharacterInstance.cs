using asap.visual;
using swiff.com.jswiff.swfrecords;

namespace asap.anim.objects
{
    public abstract class CharacterInstance : BaseElementContainer
    {
        private SwfMatrix matrix;        

        public SwfMatrix Matrix
        {
            get { return matrix; }
            set { matrix = value; }
        }
    }
}
