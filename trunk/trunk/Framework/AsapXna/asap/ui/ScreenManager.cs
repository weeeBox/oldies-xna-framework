using asap.app;

namespace asap.ui
{
    public class ScreenManager : FocusListener
    {
        private BaseApp app;
        
        private View root;
        
        private ViewController viewController;
        
        public ScreenManager(BaseApp app) 
        {
            this.app = app;
            root = null;
            viewController = null;
        }
        
        public virtual void SetRoot(View root)
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
            } 
            else
                viewController = null;
            
            app.SetMainView(root);
            app.SetKeyListener(viewController);
            app.SetPointerListener(viewController);
        }
        
        public virtual void FocusChanged(FocusType focusType, View prev, View current)
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
    }    
}