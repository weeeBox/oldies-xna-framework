using System;

using System.Collections.Generic;


using flipstones;
using asap.ui;
using asap.core;
using asap.graphics;

namespace flipstones.menu
{
    abstract public class Button : View, KeyListener, PointerListener, Focusable
     {
        private bool focused = false;
        
        private bool isPressed;
        
        public ButtonType type;
        
        public int code;
        
        public MenuListener listener;
        
        public String text;
        
        public Image image;
        
        public int width;
        
        public int height;
        
        public Button(ButtonType type ,int code ,MenuListener listener) 
        {
            this.code = code;
            this.listener = listener;
            this.type = type;
        }
        
        public virtual void Init(String text, String imageRes, int width, int height)
        {
            this.text = text;
            this.width = width;
            this.height = height;
            if (imageRes != null)
                image = AppResManager.GetInstance().GetImage(imageRes);
            
        }
        
        public virtual void SetText(String text)
        {
            this.text = text;
        }
        
        public virtual void SetCode(int code)
        {
            this.code = code;
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
            if (IsEnabled()) 
            {
                isPressed = true;
                Sounds.Play(Sounds.CLICK);
            } 
        }
        
        public virtual void PointerReleased(int x, int y, int fingerId)
        {
            if (IsEnabled()) 
            {
                if (isPressed) 
                {
                    isPressed = false;
                    Click();
                } 
            } 
        }
        
        public virtual void PointerDragged(int x, int y, int fingerId)
        {
        }
        
        public virtual void PointerEntered(int x, int y, int fingerId)
        {
            if (IsEnabled())
                isPressed = true;
            
        }
        
        public virtual void PointerExited(int x, int y, int fingerId)
        {
            if (IsEnabled())
                isPressed = false;
            
        }
        
        public virtual bool IsPressedState()
        {
            return (focused) || (isPressed);
        }
        
        public virtual ButtonType _getType()
        {
            return type;
        }
        
        public virtual Image GetImage(String imageRes)
        {
            return AppResManager.GetInstance().GetImage(imageRes);
        }
        
        public virtual bool IsEnabled()
        {
            return true;
        }
        
        public virtual void Click()
        {
            listener.ButtonPressed(code);
        }
        
        public virtual bool CanAcceptFocus(FocusType focusType)
        {
            return (type) != (ButtonType.PAUSE);
        }
        
        public virtual void Focus(FocusType focusType)
        {
            this.focused = true;
        }
        
        public virtual void Blur()
        {
            this.focused = false;
        }
        
        public virtual bool KeyPressed(int keyCode, int keyAction)
        {
            if (keyCode == (KeyCode.OK)) 
            {
                Click();
                return true;
            } 
            return false;
        }
        
        public virtual bool KeyReleased(int keyCode, int keyAction)
        {
            return false;
        }
        
        public virtual bool KeyRepeated(int keyCode, int keyAction)
        {
            return false;
        }
        
    }
    
    
}