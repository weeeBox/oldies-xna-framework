using asap.core;
using asap.app;

namespace asap.ui
{
    public class InputManager : FocusListener, KeyListener
    {
        private static FocusTraversalPolicy defaultFocusTraversalPolicy = new DefaultFocusTraversalPolicy();

        private BaseApp app;
        
        private UiComponent root;
        
        private ViewController viewController;        
        
        public InputManager(BaseApp app) 
        {
            this.app = app;
            root = null;
            viewController = null;
        }
        
        public virtual void SetRoot(UiComponent root)
        {
            if ((viewController) != null) 
            {
                viewController.BlurFocusedView();
            } 
            this.root = root;
            if (root != null) 
            {
                viewController = new ViewController(root);
                viewController.AddFocusListener(this);
                FocusTraversalPolicy focusTraversal = root.GetFocusTraversalPolicy();
                if (focusTraversal != null)
                {
                    viewController.FocusView(focusTraversal.GetDefaultComponent(root));
                }
            } 
            else
                viewController = null;
            
            //app.SetMainView(root);
            //app.SetKeyListener(viewController);
            //app.SetPointerListener(viewController);
            app.SetMainView(root);
            app.SetKeyListener(this);
        }
        
        public virtual void FocusChanged(FocusType focusType, UiComponent prev, UiComponent current)
        {
            if ((viewController) != null) 
            {
                if (current == null) 
                {
                    if (focusType == (FocusType.FORWARD)) 
                    {
                        viewController.FocusFirstView();
                    } 
                    else if (focusType == (FocusType.BACKWARD)) 
                    {
                        viewController.FocusLastView();
                    } 
                } 
            } 
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
    }    
}