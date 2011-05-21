using asap.core;
using asap.app;
using System.Collections.Generic;
using System.Diagnostics;

namespace asap.ui
{
    public class InputManager : KeyListener
    {
        private static FocusTraversalPolicy defaultFocusTraversalPolicy = new DefaultFocusTraversalPolicy();

        private static List<KeyCode> defaultNextFocusKeyCodes = new List<KeyCode>();

        private static List<KeyCode> defaultPrevFocusKeyCodes = new List<KeyCode>();

        private BaseApp app;
        
        private UiComponent root;
        
        private ViewController viewController;        
        
        public InputManager(BaseApp app) 
        {
            this.app = app;
            root = null;
            viewController = null;

            defaultNextFocusKeyCodes.Add(KeyCode.VK_Down);
            defaultNextFocusKeyCodes.Add(KeyCode.DPadDown);
            defaultNextFocusKeyCodes.Add(KeyCode.LeftThumbstickDown);

            defaultPrevFocusKeyCodes.Add(KeyCode.VK_Up);
            defaultPrevFocusKeyCodes.Add(KeyCode.DPadUp);
            defaultPrevFocusKeyCodes.Add(KeyCode.LeftThumbstickUp);
        }
        
        public virtual void SetRoot(UiComponent root)
        {
            if ((viewController) != null) 
            {
                viewController.RemoveFocus();
            } 
            this.root = root;
            if (root != null) 
            {
                viewController = new ViewController(root);                
                viewController.FocusDefaultComponent();
            } 
            else
                viewController = null;
            
            //app.SetMainView(root);
            //app.SetKeyListener(viewController);
            //app.SetPointerListener(viewController);
            app.SetMainView(root);
            app.SetKeyListener(this);
        }
     
        public bool KeyPressed(KeyEvent evt)
        {            
            return viewController.KeyPressed(evt);
        }

        public bool KeyReleased(KeyEvent evt)
        {
            return viewController.KeyReleased(evt);
        }        

        public static FocusTraversalPolicy GetDefaultFocusTraversalPolicy()
        {
            return defaultFocusTraversalPolicy;
        }

        public static List<KeyCode> GetDefaultNextFocusKeyCodes()
        {
            return defaultNextFocusKeyCodes;
        }

        public static List<KeyCode> GetDefaultPrevFocusKeyCodes()
        {
            return defaultPrevFocusKeyCodes;
        }
    }    
}