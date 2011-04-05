using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using app.menu;
using asap.graphics;
using AsapXna.asap.anim;
using app;
using asap.anim;

namespace Game.app.menu
{
    public class MainScreen : Screen
    {
        private SwfPlayer player;

        public MainScreen() : base(ScreenId.MAIN_MENU)
        {
            SwfMovie movie = (SwfMovie)AppResManager.GetInstance().GetRes("test.swp");
            SwfPlayer player = new SwfPlayer();
            player.SetMovie(movie);
        }

        public override void Draw(Graphics g)
        {
            base.Draw(g);

            player.Draw(g);
        }
    }
}
