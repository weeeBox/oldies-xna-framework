using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * This tag is used to set the tab order index of character instances. This
     * index determines the order in which character instances receive input focus
     * when repeatedly pressing the TAB key (aka 'tab order'). It also affects the
     * access order (aka 'reading order') when using screen readers.
     *
     * @since SWF 7
     */
    public class SetTabIndexTag : Tag
    {
        private int depth;
        
        private int tabIndex;
        
        /** 
         * Creates a new SetTabIndex tag. Provide the depth of the character
         * instance and its tab order index.
         *
         * @param depth depth the character instance is placed at
         * @param tabIndex tab order index (up to 65535)
         */
        public SetTabIndexTag(int depth ,int tabIndex) 
        {
            code = TagConstants.SET_TAB_INDEX;
            this.depth = depth;
            this.tabIndex = tabIndex;
        }
        
        public SetTabIndexTag() 
        {
        }
        
        public virtual void SetDepth(int depth)
        {
            this.depth = depth;
        }
        
        public virtual int GetDepth()
        {
            return depth;
        }
        
        public virtual void SetTabIndex(int tabIndex)
        {
            this.tabIndex = tabIndex;
        }
        
        public virtual int GetTabIndex()
        {
            return tabIndex;
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            depth = inStream.ReadUI16();
            tabIndex = inStream.ReadUI16();
        }
    }
}