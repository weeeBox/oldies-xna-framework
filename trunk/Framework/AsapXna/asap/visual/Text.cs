using System;
using System.Collections.Generic;
using System.Diagnostics;
using asap.graphics;

namespace asap.visual
{
    public enum TextAlign
    {
        LEFT, CENTER, RIGHT
    }

    public class Text : BaseElement
    {
        private const float UNDEFINED_WIDTH = float.PositiveInfinity;

        private String text;

        private List<FormattedString> lines;       

        private BaseFont font;

        private TextAlign textAlign = TextAlign.LEFT;

        private int lineHeight;

        public Text(BaseFont font, string text)
        {
            System.Diagnostics.Debug.Assert(text != null);
            SetFont(font);
            SetText(text, UNDEFINED_WIDTH, true);
        }

        public Text(BaseFont font, string text, int width, int height)
        {
            System.Diagnostics.Debug.Assert(text != null);
            this.width = width;
            this.height = height;
            SetFont(font);
            SetText(text, width, false);
        }

        public Text(BaseFont font, string text, int width)
        {
            System.Diagnostics.Debug.Assert(text != null);
            SetFont(font);
            SetText(text, width, true);            
        }        

        public String GetText()
        {
            return text;
        }        

        public void SetText(String text)
        {
            if (this.text != text)
            {
                SetText(text, width, false);
            }
        }

        public void SetText(String text, float width, bool reasize)
        {
            Debug.Assert(text != null);
            this.text = text;
            lines = font.WrapString(text, width);
            if (reasize)
            {
                if (width == UNDEFINED_WIDTH)
                {
                    width = 0;
                    foreach (FormattedString line in lines)
                    {
                        if (width < line.width)
                            width = line.width;
                    }
                }
                this.width = width;
                SetLineOffset(0);
            }
        }

        public void SetAlign(TextAlign align)
        {
            this.textAlign = align;
        }

        public void SetFont(BaseFont font)
        {
            this.font = font;
            lineHeight = font.GetHeight() + font.GetLineOffset();
        }

        public void SetLineOffset(int offset)
        {
            int lineOffset = font.GetLineOffset() + offset;
            lineHeight = font.GetHeight() + lineOffset;
            height = lines.Count * lineHeight - lineOffset;
        }

        public override void Draw(Graphics g)
        {
            PreDraw(g);

            float y = 0;
            foreach (FormattedString line in lines)            
            {                
                float x = 0;
                if (textAlign == TextAlign.CENTER)
                {
                    x = 0.5f * (width - line.width);
                }
                else if (textAlign == TextAlign.RIGHT)
                {
                    x = width - line.width;
                }

                font.DrawString(g, line.text, x, y);
                y += lineHeight;
            }

            PostDraw(g);
        }
    }
}
