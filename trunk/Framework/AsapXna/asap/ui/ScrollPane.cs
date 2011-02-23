using System;

using System.Collections.Generic;


using asap.core;
using asap.graphics;
using System.Diagnostics;

namespace asap.ui
{
    public class ScrollPane : View, KeyListener, PointerListener, TimerListener, FocusListener, Focusable
     {
        private View content;
        
        private ViewController controller;
        
        private int width;
        
        private int height;
        
        private int cameraX;
        
        private int cameraY;
        
        private bool horizontalScrolling;
        
        private bool verticalScrolling;
        
        private int lastDragX;
        
        private int lastDragY;
        
        private int lastDx;
        
        private int lastDy;
        
        private bool dragging;
        
        private bool isContentActive;
        
        public ScrollPane(View content ,bool horizontalScrolling ,bool verticalScrolling ,TimerSource timerSource) 
        {
            Debug.Assert(content != null);
            this.content = content;
            controller = new ViewController(content);
            controller.AddFocusListener(this);
            this.horizontalScrolling = horizontalScrolling;
            this.verticalScrolling = verticalScrolling;
            cameraX = 0;
            cameraY = 0;
            lastDx = 0;
            lastDy = 0;
            dragging = false;
            new Timer(timerSource , this , null).Schedule(50, true);
        }
        
        public virtual void OnTimer(Timer timer)
        {
            if (!(dragging)) 
            {
                if (verticalScrolling) 
                {
                    if ((lastDy) != 0) 
                    {
                        int delta = 1;
                        if (((cameraY) < 0) && ((lastDy) > 10))
                            lastDy = 10;
                        
                        else if (((cameraY) > ((content.GetHeight()) - (GetHeight()))) && ((lastDy) < (-10)))
                            lastDy = -10;
                        
                        if ((lastDy) > 0)
                            lastDy = Math.Max(0, ((lastDy) - delta));
                        
                        else
                            lastDy = Math.Min(0, ((lastDy) + delta));
                        
                        cameraY -= lastDy;
                    } 
                    else 
                    {
                        int minVal = 0;
                        int maxVal = ((content.GetHeight()) - (GetHeight())) + 1;
                        if ((cameraY) < minVal) 
                        {
                            cameraY = (minVal + (cameraY)) / 2;
                            if ((cameraY) > (minVal - 2))
                                cameraY = minVal;
                            
                        } 
                        else if ((cameraY) > maxVal) 
                        {
                            cameraY = (maxVal + (cameraY)) / 2;
                            if ((cameraY) < (maxVal + 2))
                                cameraY = maxVal;
                            
                        } 
                    }
                } 
            } 
        }
        
        public virtual void SetSize(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
        
        public override int GetWidth()
        {
            return width;
        }
        
        public override int GetHeight()
        {
            return height;
        }
        
        public virtual void PointerPressed(int x, int y, int fingerId)
        {
            if (((cameraY) >= 0) && ((cameraY) <= (((content.GetHeight()) - (GetHeight())) + 1)))
                dragging = true;
            
            else
                return ;
            
            isContentActive = true;
            controller.PointerEntered((x + (cameraX)), (y + (cameraY)), fingerId);
            controller.PointerPressed((x + (cameraX)), (y + (cameraY)), fingerId);
            lastDragX = x;
            lastDragY = y;
        }
        
        public virtual void PointerReleased(int x, int y, int fingerId)
        {
            if (dragging) 
            {
                dragging = false;
                controller.PointerReleased((x + (cameraX)), (y + (cameraY)), fingerId);
                if (isContentActive)
                    controller.PointerExited((x + (cameraX)), (y + (cameraY)), fingerId);
                
            } 
        }
        
        public virtual void PointerDragged(int x, int y, int fingerId)
        {
            if (!(dragging))
                return ;
            
            if (isContentActive) 
            {
                controller.PointerExited(x, y, fingerId);
                isContentActive = false;
            } 
            if ((horizontalScrolling) && ((GetWidth()) < (content.GetWidth()))) 
            {
                cameraX -= x - (lastDragX);
            } 
            if (verticalScrolling) 
            {
                cameraY -= y - (lastDragY);
            } 
            lastDx = x - (lastDragX);
            lastDy = y - (lastDragY);
            lastDragX = x;
            lastDragY = y;
        }
        
        public virtual void PointerEntered(int x, int y, int fingerId)
        {
        }
        
        public virtual void PointerExited(int x, int y, int fingerId)
        {
        }
        
        public virtual bool KeyPressed(int keyCode, int keyAction)
        {
            if ((content) is KeyListener) 
            {
                return controller.KeyPressed(keyCode, keyAction);
            } 
            else 
            {
                int minVal = 0;
                int maxVal = ((content.GetHeight()) - (GetHeight())) + 1;
                if (keyCode == (KeyCode.UP)) 
                {
                    lastDy = 10;
                    if ((cameraY) <= minVal) 
                    {
                        lastDy = 0;
                        return false;
                    } 
                    else if (((cameraY) + (lastDy)) <= minVal) 
                    {
                        lastDy = (cameraY) - minVal;
                    } 
                    return true;
                } 
                else if (keyCode == (KeyCode.DOWN)) 
                {
                    lastDy = -10;
                    if ((cameraY) >= maxVal) 
                    {
                        lastDy = 0;
                        return false;
                    } 
                    else if (((cameraY) - (lastDy)) >= maxVal) 
                    {
                        lastDy = (cameraY) - maxVal;
                    } 
                    return true;
                } 
                return false;
            }
        }
        
        public virtual bool KeyReleased(int keyCode, int keyAction)
        {
            return controller.KeyReleased(keyCode, keyAction);
        }
        
        public virtual bool KeyRepeated(int keyCode, int keyAction)
        {
            return controller.KeyRepeated(keyCode, keyAction);
        }
        
        public override void Draw(Graphics g)
        {
            int clipX = g.GetClipX();
            int clipY = g.GetClipY();
            int clipWidth = g.GetClipWidth();
            int clipHeight = g.GetClipHeight();
            g.ClipRect(0, 0, GetWidth(), GetHeight());
            g.Translate(-(cameraX), -(cameraY));
            content.Draw(g);
            g.Translate(cameraX, cameraY);
            g.SetClip(clipX, clipY, clipWidth, clipHeight);
            base.Draw(g);
        }
        
        private bool focused = false;
        
        public virtual void Focus(FocusType focusType)
        {
            focused = true;
            if (focusType == (FocusType.BACKWARD))
                controller.FocusLastView();
            
            else
                controller.FocusFirstView();
            
        }
        
        public virtual void Blur()
        {
            focused = false;
            controller.FocusView(null);
        }
        
        public virtual bool CanAcceptFocus(FocusType focusType)
        {
            return true;
        }
        
        public virtual void FocusChanged(FocusType focusType, View prev, View current)
        {
            switch (focusType)
            {
                case FocusType.BACKWARD:
                    break;
                case FocusType.DIRECT:
                    break;
                case FocusType.FORWARD:
                    break;
                default:
                    Debug.Assert(false);
                    break;                    
            }
            if (current == null) 
            {
            } 
        }
        
    }
    
    
}