using System;

using System.Collections.Generic;


using flipstones;
using asap.ui;
using asap.graphics;
using flipstones.game;

namespace flipstones.menu
{
    public class MissionsView : View
     {
        private const int INDENT = 9;
        
        private const int ITEM_WIDTH = 28;
        
        private FlipstonesStory story;
        
        public MissionsView(FlipstonesStory story) 
        {
            this.story = story;
        }
        
        public override int GetHeight()
        {
            return 80;
        }
        
        public override int GetWidth()
        {
            return ((story.GetMissionsCount()) * (ITEM_WIDTH)) + ((INDENT) * 2);
        }
        
        public override void Draw(Graphics g)
        {
            AppResManager resManager = AppResManager.GetInstance();
            Image missionDone = resManager.GetImage("mission_done.png");
            Image missionLock = resManager.GetImage("mission_lock.png");
            Image missionSelect = resManager.GetImage("mission_select.png");
            Image missionKey = resManager.GetImage("key.png");
            int curValue = story.GetMissionIndex();
            int y = ((missionSelect.GetHeight()) - (missionDone.GetHeight())) / 2;
            int width = (curValue * (ITEM_WIDTH)) + ((INDENT) * 2);
            DrawTiledImage(g, (curValue == 0 ? missionLock : missionDone), 0, y, width, INDENT);
            width = ((GetWidth()) - width) + (INDENT);
            DrawTiledImage(g, missionLock, ((curValue * (ITEM_WIDTH)) + (INDENT)), y, width, INDENT);
            g.DrawImage(missionSelect, ((INDENT) + (curValue * (ITEM_WIDTH))), 0, ((Graphics.LEFT) | (Graphics.TOP)));
            BitmapFont font = AppResManager.GetDefaultFont();
            y = ((missionSelect.GetHeight()) - (font.GetLineHeight())) / 2;
            for (int i = 0; i < (story.GetMissionsCount()); i++) 
            {
                String s = JUtils.ValueOf((i + 1));
                int mx = ((INDENT) + (i * (ITEM_WIDTH))) + (((ITEM_WIDTH) - (font.GetStringWidth(s))) / 2);
                font.DrawString(g, s, mx, y);
            }
            int x = (((INDENT) + (INDENT)) + ((story.GetLevelUpMission()) * (ITEM_WIDTH))) - 4;
            y = (((missionSelect.GetHeight()) + (missionDone.GetHeight())) / 2) - 7;
            g.DrawImage(missionKey, x, y, ((Graphics.LEFT) | (Graphics.TOP)));
        }
        
        private void DrawTiledImage(Graphics g, Image image, int x, int y, int width, int edge)
        {
            g.DrawRegion(image, 0, 0, edge, image.GetHeight(), Graphics.TRANS_NONE, x, y, ((Graphics.LEFT) | (Graphics.TOP)));
            int fillW = width - (edge * 2);
            int fillX = x + edge;
            while (fillW > 0) 
            {
                int w = Math.Min(((image.GetWidth()) - (edge * 2)), fillW);
                g.DrawRegion(image, edge, 0, w, image.GetHeight(), Graphics.TRANS_NONE, fillX, y, ((Graphics.LEFT) | (Graphics.TOP)));
                fillW -= w;
                fillX += w;
            }
            g.DrawRegion(image, ((image.GetWidth()) - edge), 0, edge, image.GetHeight(), Graphics.TRANS_NONE, ((x + width) - edge), y, ((Graphics.LEFT) | (Graphics.TOP)));
        }
        
    }
    
    
}