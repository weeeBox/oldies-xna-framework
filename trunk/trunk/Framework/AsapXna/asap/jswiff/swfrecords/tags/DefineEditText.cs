using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;
using swiff.com.jswiff.swfrecords;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * <p>
     * This tag defines a dynamic text field. A text field can be associated with a
     * variable the contents of the text field are stored in and kept in sync
     * with.
     * </p>
     * 
     * <p>
     * Users may change the value of a text field interactively, unless the
     * <code>readOnly</code> tag is set.
     * </p>
     * 
     * <p>
     * Fonts used by this tag must be defined using <code>DefineFont2</code> (not
     * <code>DefineFont</code>). If the <code>useOutlines</code> flag is cleared, the
     * Flash Player will try to render text using device fonts rather than glyph
     * fonts.
     * </p>
     * 
     * <p>
     * When the <code>html</code> flag is set, the text may contain HTML tags.
     * Allowed tags are:
     * 
     * <ul>
     * <li>
     * <code>&lt;p&gt;...&lt;/p&gt;</code>: paragraph
     * </li>
     * <li>
     * <code>&lt;br&gt;</code>: line break
     * </li>
     * <li>
     * <code>&lt;a href="..."&gt;...&lt;/a&gt;</code>: hyperlink. Optional
     * attributes:
     * 
     * <ul>
     * <li>
     * <code>target</code>: a window name
     * </li>
     * </ul>
     * 
     * </li>
     * <li>
     * <code>&lt;font&gt;...&lt;/font&gt;</code>: font properties. Available
     * attributes:
     * 
     * <ul>
     * <li>
     * <code>face</code>: font name supplied in a <code>DefineFont2</code> tag.
     * </li>
     * <li>
     * <code>size</code> in twips (1/20 px). Leading <code>+</code> or
     * <code>-</code> indicates a relative size
     * </li>
     * <li>
     * <code>color</code> as a <code>#RRGGBB</code> hex value
     * </li>
     * </ul>
     * 
     * </li>
     * <li>
     * <code>&lt;b&gt;...&lt;/b&gt;</code>: bold text
     * </li>
     * <li>
     * <code>&lt;i&gt;...&lt;/i&gt;</code>: italic text
     * </li>
     * <li>
     * <code>&lt;u&gt;...&lt;/u&gt;</code>: underlined text
     * </li>
     * <li>
     * <code>&lt;li&gt;...&lt;/li&gt;</code>: bulleted list. Warning: &lt;ul&gt; is
     * not supported
     * </li>
     * <li>
     * <code>&lt;textformat&gt;...&lt;/textformat&gt;</code> lets you specify
     * formatting attributes:
     * 
     * <ul>
     * <li>
     * <code>leftmargin</code> in twips (1/20 px)
     * </li>
     * <li>
     * <code>rightmargin</code> in twips
     * </li>
     * <li>
     * <code>indent</code> in twips
     * </li>
     * <li>
     * <code>blockindent</code> in twips
     * </li>
     * <li>
     * <code>leading</code> in twips
     * </li>
     * <li>
     * <code>tabstops</code>: comma-separated list in twips
     * </li>
     * </ul>
     * 
     * </li>
     * <li>
     * <code>&lt;tab&gt;</code>: advances to next tab stop (defined with
     * <code>&lt;textformat&gt;</code>)
     * </li>
     * </ul>
     * </p>
     * 
     * <p>
     * Multiple other parameters can be set, e.g. text layout attributes like
     * margins and leading etc.
     * </p>
     *
     * @since SWF 4
     */
    public class DefineEditText : DefinitionTag
    {
        /** 
         *
         */
        public const short ALIGN_LEFT = 0;
        
        /** 
         *
         */
        public const short ALIGN_RIGHT = 1;
        
        /** 
         *
         */
        public const short ALIGN_CENTER = 2;
        
        /** 
         *
         */
        public const short ALIGN_JUSTIFY = 3;
        
        private Rect bounds;
        
        private bool wordWrap;
        
        private bool multiline;
        
        private bool password;
        
        private bool readOnly;
        
        private bool autoSize;
        
        private bool noSelect;
        
        private bool border;
        
        private bool html;
        
        private bool useOutlines;
        
        private int fontId = -1;
        
        private int fontHeight;
        
        private RGBA textColor;
        
        private int maxLength;
        
        private short align;
        
        private int leftMargin;
        
        private int rightMargin;
        
        private int indent;
        
        private int leading;
        
        private String variableName;
        
        private String initialText;
        
        private bool hasText;
        
        private bool hasTextColor;
        
        private bool hasMaxLength;
        
        private bool hasFont;
        
        private bool hasLayout;
        
        /** 
         * Creates a new DefineEditText tag. Specify the character ID of the text
         * field, its bounds and the name of the variable the contents of the text
         * field are stored in and kept in sync with.
         *
         * @param characterId character ID of the text field
         * @param bounds the size of the rectangle which completely encloses the
         * 		  text field.
         * @param variableName variable name (in dot or slash syntax)
         */
        public DefineEditText(int characterId ,Rect bounds ,String variableName) 
        {
            code = TagConstants.DEFINE_EDIT_TEXT;
            this.characterId = characterId;
            this.bounds = bounds;
            this.variableName = variableName;
        }
        
        public DefineEditText() 
        {
        }
        
        public virtual short GetAlign()
        {
            return align;
        }
        
        public virtual void SetAutoSize(bool autoSize)
        {
            this.autoSize = autoSize;
        }
        
        public virtual bool IsAutoSize()
        {
            return autoSize;
        }
        
        public virtual void SetBorder(bool border)
        {
            this.border = border;
        }
        
        public virtual bool IsBorder()
        {
            return border;
        }
        
        public virtual Rect GetBounds()
        {
            return bounds;
        }
        
        public virtual void SetFont(int fontId, int fontHeight)
        {
            this.fontId = fontId;
            this.fontHeight = fontHeight;
            hasFont = true;
        }
        
        public virtual int GetFontHeight()
        {
            return fontHeight;
        }
        
        public virtual int GetFontId()
        {
            return fontId;
        }
        
        public virtual void SetHtml(bool html)
        {
            this.html = html;
        }
        
        public virtual bool IsHtml()
        {
            return html;
        }
        
        public virtual int GetIndent()
        {
            return indent;
        }
        
        public virtual void SetInitialText(String initialText)
        {
            this.initialText = initialText;
            hasText = true;
        }
        
        public virtual String GetInitialText()
        {
            return initialText;
        }
        
        public virtual void SetLayout(short align, int leftMargin, int rightMargin, int indent, int leading)
        {
            this.align = align;
            this.leftMargin = leftMargin;
            this.rightMargin = rightMargin;
            this.indent = indent;
            this.leading = leading;
            hasLayout = true;
        }
        
        public virtual int GetLeading()
        {
            return leading;
        }
        
        public virtual int GetLeftMargin()
        {
            return leftMargin;
        }
        
        public virtual void SetMaxLength(int maxLength)
        {
            this.maxLength = maxLength;
            hasMaxLength = true;
        }
        
        public virtual int GetMaxLength()
        {
            return maxLength;
        }
        
        public virtual void SetMultiline(bool multiline)
        {
            this.multiline = multiline;
        }
        
        public virtual bool IsMultiline()
        {
            return multiline;
        }
        
        public virtual void SetNoSelect(bool noSelect)
        {
            this.noSelect = noSelect;
        }
        
        public virtual bool IsNoSelect()
        {
            return noSelect;
        }
        
        public virtual void SetPassword(bool password)
        {
            this.password = password;
        }
        
        public virtual bool IsPassword()
        {
            return password;
        }
        
        public virtual void SetReadOnly(bool readOnly)
        {
            this.readOnly = readOnly;
        }
        
        public virtual bool IsReadOnly()
        {
            return readOnly;
        }
        
        public virtual int GetRightMargin()
        {
            return rightMargin;
        }
        
        public virtual void SetTextColor(RGBA textColor)
        {
            this.textColor = textColor;
            hasTextColor = true;
        }
        
        public virtual RGBA GetTextColor()
        {
            return textColor;
        }
        
        public virtual void SetUseOutlines(bool useOutlines)
        {
            this.useOutlines = useOutlines;
        }
        
        public virtual bool IsUseOutlines()
        {
            return useOutlines;
        }
        
        public virtual void SetVariableName(String variableName)
        {
            this.variableName = variableName;
        }
        
        public virtual String GetVariableName()
        {
            return variableName;
        }
        
        public virtual void SetWordWrap(bool wordWrap)
        {
            this.wordWrap = wordWrap;
        }
        
        public virtual bool IsWordWrap()
        {
            return wordWrap;
        }
        
        public virtual bool HasFont()
        {
            return hasFont;
        }
        
        public virtual bool HasLayout()
        {
            return hasLayout;
        }
        
        public virtual bool HasMaxLength()
        {
            return hasMaxLength;
        }
        
        public virtual bool HasText()
        {
            return hasText;
        }
        
        public virtual bool HasTextColor()
        {
            return hasTextColor;
        }
        
        public override void SetData(byte[] data) /* throws IOException */
        {
            InputBitStream inStream = new InputBitStream(data);
            if ((GetSWFVersion()) < 6) 
            {
                if (IsJapanese()) 
                {
                    inStream.SetShiftJIS(true);
                } 
                else 
                {
                    inStream.SetANSI(true);
                }
            } 
            characterId = inStream.ReadUI16();
            bounds = new Rect(inStream);
            hasText = inStream.ReadBooleanBit();
            wordWrap = inStream.ReadBooleanBit();
            multiline = inStream.ReadBooleanBit();
            password = inStream.ReadBooleanBit();
            readOnly = inStream.ReadBooleanBit();
            hasTextColor = inStream.ReadBooleanBit();
            hasMaxLength = inStream.ReadBooleanBit();
            hasFont = inStream.ReadBooleanBit();
            inStream.ReadBooleanBit();
            autoSize = inStream.ReadBooleanBit();
            hasLayout = inStream.ReadBooleanBit();
            noSelect = inStream.ReadBooleanBit();
            border = inStream.ReadBooleanBit();
            inStream.ReadBooleanBit();
            html = inStream.ReadBooleanBit();
            useOutlines = inStream.ReadBooleanBit();
            if (hasFont) 
            {
                fontId = inStream.ReadUI16();
                fontHeight = inStream.ReadUI16();
            } 
            if (hasTextColor) 
            {
                textColor = new RGBA(inStream);
            } 
            if (hasMaxLength) 
            {
                maxLength = inStream.ReadUI16();
            } 
            if (hasLayout) 
            {
                align = inStream.ReadUI8();
                leftMargin = inStream.ReadUI16();
                rightMargin = inStream.ReadUI16();
                indent = inStream.ReadUI16();
                leading = inStream.ReadUI16();
            } 
            variableName = inStream.ReadString();
            if (hasText) 
            {
                initialText = inStream.ReadString();
            } 
        }
    }
}