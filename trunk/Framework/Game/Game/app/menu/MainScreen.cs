using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using app.menu;
using asap.graphics;
using AsapXna.asap.anim;
using app;
using asap.anim;
using asap.core;

namespace Game.app.menu
{
    public class MainScreen : Screen
    {
        private SwfPlayer player;               

        public MainScreen() : base(ScreenId.MAIN_MENU)
        {
            SwfMovie movie = (SwfMovie)AppResourceMgr.GetInstance().GetRes("test.swp");
            player = new SwfPlayer();
            player.SetMovie(movie);

            player.Start(this);
        }        

        public override void Draw(Graphics g)
        {
            base.Draw(g);

            player.Draw(g);
        }        
    }
}
