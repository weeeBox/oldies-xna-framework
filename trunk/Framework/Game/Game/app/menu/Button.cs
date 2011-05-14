using System;
using asap.core;
using asap.graphics;
using asap.ui;

namespace app.menu
{
    public abstract class Button : UiComponent, KeyListener, PointerListener, Focusable
    {
        private bool focused = false;
        
        private bool isPressed;
        
        public ButtonType type;
        
        public int code;
        
        public MenuListener listener;
        
        public String text;
        
        public GameTexture image;        
        
        public Button(ButtonType type ,int code ,MenuListener listener) 
        {
            this.code = code;
            this.listener = listener;
            this.type = type;
        }        
        
        public virtual void SetText(String text)
        {
            this.text = text;
        }
        
        public virtual void SetCode(int code)
        {
            this.code = code;
        }     
        
        public virtual void PointerPressed(int x, int y, int fingerId)
        {
            if (IsEnabled()) 
            {
                isPressed = true;
                // Application.sharedSoundMgr.PlaySound(Sounds.CLICK);
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

        public virtual bool KeyPressed(KeyEvent evt)
        {
            if (evt.action == KeyAction.OK) 
            {
                Click();
                return true;
            } 
            return false;
        }

        public virtual bool KeyReleased(KeyEvent evt)
        {
            return false;
        }        
    }    
}