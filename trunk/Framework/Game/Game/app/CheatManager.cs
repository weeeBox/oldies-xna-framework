using System;

using System.Collections.Generic;


using asap.core;
using app.menu;
using asap.graphics;

namespace app
{
    public class CheatManager : MenuListener
     {
        private const int TAP_REGION = 40;
        
        private Object[] listeners;
        
        private bool engaged;
        
        private int digitNumber;
        
        private int cheatCode;
        
        private bool screenShowed;
        
        private bool active;
        
        private int activationStage = -1;
        
        private bool showActivation;
        
        public CheatManager() 
        {
            engaged = false;
            screenShowed = false;
            listeners = new Object[4];
            active = false;
            showActivation = false;
        }
        
        public virtual void SetPossibleActivating(bool possible)
        {
            activationStage = possible ? 0 : -1;
        }
        
        public virtual void AddCheatListener(Object listener)
        {
            for (int i = 0; i < (listeners.Length); i++) 
            {
                if ((listeners[i]) == null) 
                {
                    listeners[i] = listener;
                    return ;
                } 
            }
            System.Diagnostics.Debug.Assert(false);
        }
        
        public virtual void RemoveCheatListener(Object listener)
        {
            for (int i = 0; i < (listeners.Length); i++) 
            {
                if ((listeners[i]) == listener) 
                {
                    listeners[i] = null;
                    return ;
                } 
            }
            System.Diagnostics.Debug.Assert(false);
        }
        
        public virtual void Draw(Graphics g)
        {
            if (showActivation) 
            {
                showActivation = false;
                g.SetColor(65280);
                int screenWidth = FlipstonesApp.GetInstance().GetWidth();
                int screenHeight = FlipstonesApp.GetInstance().GetHeight();
                g.FillRect(0, 0, screenWidth, screenHeight);
            } 
        }
        
        public virtual bool PointerPressed(int x, int y, int fingerId)
        {
            int screenWidth = FlipstonesApp.GetInstance().GetWidth();
            int screenHeight = FlipstonesApp.GetInstance().GetHeight();
            if (!(active)) 
            {
                if ((((((x < (TAP_REGION)) && (y < (TAP_REGION))) && ((activationStage) == 0)) || (((x > (screenWidth - (TAP_REGION))) && (y < (TAP_REGION))) && ((activationStage) == 1))) || (((x < (TAP_REGION)) && (y > (screenHeight - (TAP_REGION)))) && ((activationStage) == 2))) || (((x > (screenWidth - (TAP_REGION))) && (y > (screenHeight - (TAP_REGION)))) && ((activationStage) == 3))) 
                {
                    (activationStage)++;
                } 
                else 
                {
                    activationStage = 0;
                }
                if ((activationStage) == 4) 
                {
                    System._out.Println("cheats activated");
                    showActivation = true;
                    active = true;
                } 
            } 
            else 
            {
                if (!(engaged)) 
                {
                    if ((x >= (screenWidth - (TAP_REGION))) && (y <= (TAP_REGION))) 
                    {
                        EngageCheats();
                        screenShowed = true;
                        FlipstonesApp.GetScreensView().StartNextScreen(ScreenFactory.CreateCheatsScreen(this));
                    } 
                } 
            }
            return false;
        }
        
        public virtual bool PointerReleased(int x, int y, int fingerId)
        {
            return false;
        }
        
        public virtual bool PointerDragged(int x, int y, int fingerId)
        {
            return false;
        }
        
        public virtual bool KeyPressed(int keyCode, int keyAction)
        {
            if (!(engaged)) 
            {
                if (keyCode == (KeyCode.CHEAT)) 
                {
                    EngageCheats();
                    return true;
                } 
                else 
                {
                    return false;
                }
            } 
            else 
            {
                if ((keyCode >= (KeyCode.NUM_0)) && (keyCode <= (KeyCode.NUM_9))) 
                {
                    ProcessCheatDigit((keyCode - (KeyCode.NUM_0)));
                    return true;
                } 
                else 
                {
                    engaged = false;
                    return true;
                }
            }
        }
        
        public virtual bool KeyReleased(int keyCode, int keyAction)
        {
            return engaged;
        }
        
        private void EngageCheats()
        {
            cheatCode = 0;
            engaged = true;
            digitNumber = 0;
        }
        
        private void ProcessCheatDigit(int num)
        {
            cheatCode = ((cheatCode) * 10) + num;
            (digitNumber)++;
            if ((digitNumber) == 2) 
            {
                engaged = false;
                if (screenShowed) 
                {
                    screenShowed = false;
                    FlipstonesApp.GetScreensView().BackScreen();
                } 
                for (int i = 0; i < (listeners.Length); i++) 
                {
                    if ((listeners[i]) != null)
                        ((CheatListener)(listeners[i])).CheatEntered(cheatCode);
                    
                }
            } 
        }
        
        public virtual void ButtonPressed(int code)
        {
            ProcessCheatDigit(code);
        }
        
    }
    
    
}