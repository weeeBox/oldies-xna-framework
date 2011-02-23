using System;

using System.Collections.Generic;


using asap.ui;
using flipstones.game;

namespace flipstones.menu
{
    public class LevelsView : Container, MenuListener
     {
        private const int ROWS_COUNT = 2;
        
        private const int COLUMNS_COUNT = 3;
        
        private const int INDENT = 10;
        
        private const int LEVEL_BUTTONS_COUNT = (ROWS_COUNT) * (COLUMNS_COUNT);
        
        private FlipstonesStoryData storyData;
        
        private PageNavigator navigator;
        
        public LevelsView(MenuListener listener ,FlipstonesStoryData storyData) 
        {
            this.storyData = storyData;
            int horzSpace = INDENT;
            int vertSpace = INDENT;
            int buttonsCount = 0;
            for (int i = 0; i < (ROWS_COUNT); i++) 
            {
                int y = vertSpace + (((LevelButton.LEVEL_BUTTON_HEIGHT) + vertSpace) * i);
                for (int j = 0; j < (COLUMNS_COUNT); j++) 
                {
                    if (buttonsCount < (Settings.GetLevelsCount())) 
                    {
                        int x = horzSpace + (((LevelButton.LEVEL_BUTTON_WIDTH) + horzSpace) * j);
                        LevelButton button = new LevelButton(ButtonType.LEVEL_NORMAL , 0 , listener);
                        int buttonIndex = AddView(button);
                        SetViewX(buttonIndex, x);
                        SetViewY(buttonIndex, y);
                        buttonsCount++;
                    } 
                }
            }
            navigator = new PageNavigator(this , 0 , ((Settings.GetLevelsCount()) / (LEVEL_BUTTONS_COUNT)));
            
            {
                int navigIndex = AddView(navigator);
                SetViewY(navigIndex, ((GetHeight()) - (navigator.GetHeight())));
            }
            UpdatePage();
        }
        
        public virtual void UpdatePage()
        {
            for (int i = 0; i < (LEVEL_BUTTONS_COUNT); i++) 
            {
                int levelIndex = ((navigator.GetPage()) * (LEVEL_BUTTONS_COUNT)) + i;
                if (levelIndex < (Settings.GetLevelsCount())) 
                {
                    LevelButton button = ((LevelButton)(GetView(i)));
                    button.SetText(JUtils.ValueOf((levelIndex + 1)));
                    button.SetCode(((ButtonId.CHOOSE_LEVEL_BASE) + levelIndex));
                    if (levelIndex == (storyData.GetLevelsProgress()))
                        button.SetType(ButtonType.LEVEL_ACTIVE);
                    
                    else if (levelIndex < (storyData.GetLevelsProgress()))
                        button.SetType(ButtonType.LEVEL_NORMAL);
                    
                    else
                        button.SetType(ButtonType.LEVEL_LOCKED);
                    
                } 
            }
        }
        
        public override int GetHeight()
        {
            return 224;
        }
        
        public override int GetWidth()
        {
            return 258;
        }
        
        public virtual void ButtonPressed(int code)
        {
            if (navigator.ButtonPressed(code))
                UpdatePage();
            
        }
        
    }
    
    
}