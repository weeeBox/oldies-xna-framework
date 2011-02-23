using System;

using System.Collections.Generic;


using flipstones;
using asap.ui;
using flipstones.game;

namespace flipstones.menu
{
    public class LevelsScreen : Screen
     {
        private LevelsView levelsView;
        
        public LevelsScreen(MenuListener listener ,FlipstonesStoryData story) 
         : base(ScreenId.STORY_CHOOSE_LEVEL)
        {
            String header = StrRes.Get(Strings.ADVENTURE_HEADER);
            ScreenFactory.CreateDefaultMenuScreen(this, header);
            int hintIndex = AddView(new TextBox(StrRes.Get(Strings.CHOOSE_LEVEL_HINT) , GetWidth() , GetHeight()));
            SetViewY(hintIndex, 108);
            levelsView = new LevelsView(listener , story);
            int levelsIndex = AddView(levelsView);
            SetViewY(levelsIndex, 178);
            AlignCenterAll();
            ScreenFactory.AddMenuBackBottomButton(this, listener);
        }
        
        public override void OnScreenBack()
        {
            levelsView.UpdatePage();
        }
        
    }
    
    
}