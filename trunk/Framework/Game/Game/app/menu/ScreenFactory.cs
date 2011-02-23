using System;

using System.Collections.Generic;


using flipstones;
using asap.ui;
using asap.core;
using asap.graphics;
using flipstones.game;

namespace flipstones.menu
{
    /** 
     * Factory for screens and screen elements.
     */
    public class ScreenFactory
     {
        public static Screen CreateMainMenu(MenuListener listener)
        {
            Screen screen = new Screen(ScreenId.MAIN_MENU);
            ScreenFactory.CreateDefaultMenuScreen(screen, null);
            Image headerImage = ScreenFactory.GetImage((Config.freeVersion ? "logo_free.png" : "logo.png"));
            ImageView header = new ImageView(headerImage);
            screen.AddView(header);
            screen.AlignCenter(header);
            Container container = new Container();
            container.AddView(new MenuButton(ButtonType.PLAY_FRENZY , ButtonId.START_FRENZY , listener));
            container.AddView(new MenuButton(ButtonType.PLAY_CHALLENGE , ButtonId.START_CHALLENGE , listener));
            ScreenFactory.AddAndLayoutButtons(screen, container, 150);
            container = new Container();
            container.AddView(new MenuButton(ButtonType.MORE_GAMES_GREEN , "Leaderboard" , ButtonId.LEADERBOARD , listener));
            container.AddView(new MenuButton(ButtonType.MORE_GAMES_RED , ButtonId.MORE_GAMES , listener));
            container.AddView(new MenuButton(ButtonType.MISC , StrRes.Get(Strings.INFO) , ButtonId.INFO , listener));
            ScreenFactory.AddAndLayoutButtons(screen, container, 310);
            screen.SetBackListener(listener, ButtonId.APP_EXIT);
            return screen;
        }
        
        public static Screen CreateChooseLevelMenu(MenuListener listener, FlipstonesStoryData storyData)
        {
            return new LevelsScreen(listener , storyData);
        }
        
        public static Screen CreateStartMissionMenu(MenuListener listener, FlipstonesStory story)
        {
            Screen screen = new Screen(ScreenId.STORY_START_MISSION);
            Container cont = ScreenFactory.CreateDefaultIngameScreen(screen, ((StrRes.Get(Strings.START_MISSION_HEADER_1)) + ((story.GetLevelIndex()) + 1)), listener);
            int missionsViewIndex = cont.AddView(new MissionsView(story));
            cont.SetViewY(missionsViewIndex, (74 - 8));
            BitmapFont fnt = AppResManager.GetInstance().GetFont("font_menu_mini.fnt");
            int descrIndex = cont.AddView(new TextBox(story.GetMissionName() , fnt , ((cont.GetWidth()) - ((TEXT_INDENT) * 2))));
            cont.SetViewY(descrIndex, ((170 - 12) - 8));
            Container buttonsContainer = new Container();
            buttonsContainer.AddView(new MenuButton(ButtonType.PLAY_ORANGE , StrRes.Get(Strings.START_MISSION) , ButtonId.START_MISSION , listener));
            ScreenFactory.AddAndLayoutIngameButtons(cont, buttonsContainer);
            cont.AlignCenterAll();
            return screen;
        }
        
        public static Screen CreateResumeStory(MenuListener listener)
        {
            Screen screen = new Screen(ScreenId.RESUME_STORY);
            ScreenFactory.CreateDefaultMenuScreen(screen, StrRes.Get(Strings.RESUME_STORY_HEADER));
            int textIndex = screen.AddView(new TextBox(StrRes.Get(Strings.RESUME_STORY_QUESTION) , ((screen.GetWidth()) - ((TEXT_INDENT) * 2))));
            screen.SetViewY(textIndex, 150);
            Container buttons = new Container();
            buttons.AddView(new MenuButton(ButtonType.PLAY_ORANGE , StrRes.Get(Strings.RESUME_STORY_CONTINUE) , ButtonId.CONTINUE_STORY , listener));
            buttons.AddView(new MenuButton(ButtonType.PLAY_YELLOW , StrRes.Get(Strings.RESUME_STORY_START_NEW) , ButtonId.START_NEW_STORY , listener));
            MenuButton backButton = new MenuButton(ButtonType.BACK , ButtonId.BACK , listener);
            buttons.AddView(backButton);
            screen.SetBackButton(backButton);
            ScreenFactory.AddAndLayoutMenuBottomButtons(screen, buttons);
            screen.AlignCenterAll();
            return screen;
        }
        
        public static Screen CreateLevelFailedMenu(MenuListener listener, FlipstonesStory story)
        {
            Screen screen = new Screen(ScreenId.STORY_LEVEL_FAILED);
            Container cont = ScreenFactory.CreateDefaultIngameScreen(screen, StrRes.Get(Strings.MISSION_FAILED), listener);
            String text = story.IsNextLevelAvailable() ? StrRes.Get(Strings.LEVEL_FAIL_CAN_START_NEXT) : StrRes.Get(Strings.LEVEL_FAIL);
            int textIndex = cont.AddView(new TextBox(text , ((cont.GetWidth()) - ((TEXT_INDENT) * 2))));
            cont.SetViewY(textIndex, 73);
            Container buttons = new Container();
            buttons.AddView(new MenuButton(ButtonType.PLAY_YELLOW , StrRes.Get(Strings.RESTART_MISSION) , ButtonId.LEVEL_FAILED_RESTART_MISSION , listener));
            buttons.AddView(new MenuButton(ButtonType.PLAY_YELLOW , StrRes.Get(Strings.RESTART_LEVEL) , ButtonId.LEVEL_FAILED_RESTART_LEVEL , listener));
            if ((story.IsNextLevelAvailable()) && (!(story.IsLastLevel())))
                buttons.AddView(new MenuButton(ButtonType.PLAY_ORANGE , StrRes.Get(Strings.START_NEXT_LEVEL) , ButtonId.LEVEL_FAILED_NEXT_LEVEL , listener));
            
            ScreenFactory.AddAndLayoutIngameButtons(cont, buttons);
            cont.AlignCenterAll();
            return screen;
        }
        
        public static Screen CreateLevelUpMenu(MenuListener listener, FlipstonesStory story)
        {
            Screen screen = new Screen(ScreenId.LEVEL_UP);
            String header = story.IsLastLevel() ? StrRes.Get(Strings.STORY_FINISH_HEADER) : StrRes.Get(Strings.LEVEL_UP_HEADER);
            Container cont = ScreenFactory.CreateDefaultIngameScreen(screen, header, listener);
            int textIndex = cont.AddView(new TextBox(story.GetLevelUpText() , ((cont.GetWidth()) - ((TEXT_INDENT) * 2))));
            cont.SetViewY(textIndex, 73);
            Container buttons = new Container();
            if (story.IsLastLevel()) 
            {
                buttons.AddView(new MenuButton(ButtonType.PLAY_YELLOW , StrRes.Get(Strings.FINIS_LEVEL) , ButtonId.FINISH_LEVEL , listener));
            } 
            else 
            {
                buttons.AddView(new MenuButton(ButtonType.PLAY_ORANGE , StrRes.Get(Strings.START_NEXT_LEVEL) , ButtonId.START_NEXT_LEVEL , listener));
            }
            ScreenFactory.AddAndLayoutIngameButtons(cont, buttons);
            cont.AlignCenterAll();
            return screen;
        }
        
        public static Screen CreateHintScreen(MenuListener listener, FlipstonesStory story)
        {
            Screen screen = new Screen(ScreenId.HINT);
            Container cont = ScreenFactory.CreateDefaultIngameScreen(screen, StrRes.Get(Strings.HINT_HEADER), listener);
            BitmapFont fnt = AppResManager.GetInstance().GetFont("font_menu_mini.fnt");
            int textIndex = cont.AddView(new TextBox(story.GetHintText() , fnt , ((cont.GetWidth()) - ((TEXT_INDENT) * 2))));
            cont.SetViewY(textIndex, 73);
            Container buttons = new Container();
            buttons.AddView(new MenuButton(ButtonType.PLAY_ORANGE , StrRes.Get(Strings.HINT_CONTINUE) , ButtonId.HINT_CONTINUE , listener));
            ScreenFactory.AddAndLayoutIngameButtons(cont, buttons);
            cont.AlignCenterAll();
            return screen;
        }
        
        public static Screen CreateChallengeResultScreen(MenuListener listener, FlipstonesStory story, int starsCount)
        {
            Screen screen = new Screen(ScreenId.CHALLENGE_RESULT);
            Container cont = ScreenFactory.CreateDefaultIngameScreen(screen, StrRes.Get(Strings.LEVEL_RESULT), listener);
            Container currentStars = new Container();
            BitmapFont fnt = AppResManager.GetInstance().GetFont("font_menu_mini.fnt");
            currentStars.AddView(new TextBox((((StrRes.Get(Strings.YOU_GOT)) + " ") + starsCount) , fnt));
            int index = currentStars.AddView(new ImageView(AppResManager.GetInstance().GetImage("star.png")));
            currentStars.SpreadHorizontally(2);
            currentStars.AlignMiddleAll();
            currentStars.SetViewY(index, ((currentStars.GetViewY(index)) - 4));
            currentStars.ResizeToFitViews();
            Container totalStars = new Container();
            int totalStarsCount = Prefs.GetInstance().GetStarsCount();
            totalStars.AddView(new TextBox((((StrRes.Get(Strings.TOTAL)) + " ") + totalStarsCount) , fnt));
            index = totalStars.AddView(new ImageView(AppResManager.GetInstance().GetImage("star.png")));
            totalStars.SpreadHorizontally(2);
            totalStars.AlignMiddleAll();
            totalStars.SetViewY(index, ((totalStars.GetViewY(index)) - 4));
            totalStars.ResizeToFitViews();
            Container stars = new Container();
            stars.AddView(currentStars);
            stars.AddView(totalStars);
            stars.SpreadVertically(0);
            stars.ResizeToFitViews();
            stars.AlignCenterAll();
            int starsIndex = cont.AddView(stars);
            cont.SetViewY(starsIndex, 60);
            String starsDescription = StrRes.Get(Strings.STARS_DESCRIPTION);
            TextBox description = new TextBox(starsDescription , fnt , ((cont.GetWidth()) - ((TEXT_INDENT) * 2)));
            int descIndex = cont.AddView(description);
            cont.SetViewY(descIndex, 143);
            Container buttons = new Container();
            buttons.AddView(new MenuButton(ButtonType.PLAY_ORANGE , StrRes.Get(Strings.PLAY_FRENZY) , ButtonId.RESULT_PLAY_FRENZY , listener));
            buttons.AddView(new MenuButton(ButtonType.PLAY_ORANGE , StrRes.Get(Strings.NEXT_LEVEL) , ButtonId.RESULT_CONTINUE , listener));
            ScreenFactory.AddAndLayoutIngameButtons(cont, buttons);
            cont.AlignCenterAll();
            return screen;
        }
        
        public static Screen CreateEndlessHintScreen(MenuListener listener)
        {
            Screen screen = new Screen(ScreenId.ENDLESS_HINT);
            Container cont = ScreenFactory.CreateDefaultIngameScreen(screen, StrRes.Get(Strings.HINT_HEADER), listener);
            BitmapFont fnt = AppResManager.GetInstance().GetFont("font_menu_mini.fnt");
            int textIndex = cont.AddView(new TextBox(StrRes.Get(Strings.HINT_ENDLESS) , fnt , ((cont.GetWidth()) - ((TEXT_INDENT) * 2))));
            cont.SetViewY(textIndex, 73);
            Container buttons = new Container();
            buttons.AddView(new MenuButton(ButtonType.PLAY_ORANGE , StrRes.Get(Strings.HINT_CONTINUE) , ButtonId.HINT_ENDLESS_CONTINUE , listener));
            ScreenFactory.AddAndLayoutIngameButtons(cont, buttons);
            cont.AlignCenterAll();
            return screen;
        }
        
        public static Screen CreateFrenzyHintScreen(MenuListener listener)
        {
            Screen screen = new Screen(ScreenId.FRENZY_HINT);
            BitmapFont fnt = ScreenFactory.GetFont("font_menu_mini.fnt");
            bool isStarsEnough = (Prefs.GetInstance().GetStarsCount()) >= (Boost.MIN_PRICE);
            Container cont = ScreenFactory.CreateDefaultIngameScreen(screen, null, listener);
            TextBox text = new TextBox(StrRes.Get((isStarsEnough ? Strings.BOOSTS_WERENT_CHOOSEN : Strings.BOOSTS_WERENT_CHOOSEN_AND_NO_STARS)) , ((cont.GetWidth()) - ((TEXT_INDENT) * 2)));
            cont.AddView(text);
            cont.AlignVertically(text, 40);
            Container buttons = new Container();
            buttons.AddView(new MenuButton(ButtonType.PLAY_ORANGE , StrRes.Get(Strings.HINT_CONTINUE) , ButtonId.HINT_FRENZY_CONTINUE , listener));
            if (isStarsEnough) 
            {
                buttons.AddView(new MenuButton(ButtonType.PLAY_YELLOW , StrRes.Get(Strings.SELECT_BOOSTS) , ButtonId.HINT_FRENZY_SELECT_BOOSTS , listener));
            } 
            buttons.SpreadVertically(0);
            buttons.ResizeToFitViews();
            buttons.AlignCenterAll();
            cont.AddView(buttons);
            cont.AlignVertically(buttons, 90);
            cont.AlignCenterAll();
            return screen;
        }
        
        public static Screen CreateRestartLevelScreen(MenuListener listener, FlipstonesStory story)
        {
            Screen screen = new Screen(ScreenId.HINT);
            Container cont = ScreenFactory.CreateDefaultIngameScreen(screen, StrRes.Get(Strings.RESTART_LEVEL), listener);
            int textIndex = cont.AddView(new TextBox(StrRes.Get(Strings.RESTART_TEXT) , ((cont.GetWidth()) - ((TEXT_INDENT) * 2))));
            cont.SetViewY(textIndex, 100);
            Container buttons = new Container();
            buttons.AddView(new MenuButton(ButtonType.PLAY_ORANGE , StrRes.Get(Strings.TEXT_YES) , ButtonId.PAUSE_RESTART_YES , listener));
            buttons.AddView(new MenuButton(ButtonType.PLAY_YELLOW , StrRes.Get(Strings.TEXT_NO) , ButtonId.PAUSE_RESTART_NO , listener));
            ScreenFactory.AddAndLayoutIngameButtons(cont, buttons);
            cont.AlignCenterAll();
            return screen;
        }
        
        public static Screen CreatePauseMenu(MenuListener listener, int screenType)
        {
            return new InfoScreen(listener , screenType);
        }
        
        public static Screen CreateStartLoading(MenuListener listener)
        {
            Screen screen = new Screen(ScreenId.START_LOADING);
            screen.AddView(new ImageView(ScreenFactory.GetImage("menu_background.png")));
            Container content = new Container();
            content.SetSize(screen.GetWidth(), screen.GetHeight());
            content.AddView(new ImageView(ScreenFactory.GetImage("poke_logo.png")));
            Container spacer = new Container();
            content.AddView(spacer);
            content.AddView(ScreenFactory.CreatePlacePlayLogoContainer());
            content.AddView(new TextBox(StrRes.Get(Strings.LOADING_AD) , ScreenFactory.GetFont("font_menu_mini.fnt") , ((screen.GetWidth()) - 20)));
            spacer = new Container();
            spacer.SetSize(0, 10);
            content.AddView(spacer);
            content.AddView(new TextBox(StrRes.Get(Strings.LOADING) , ScreenFactory.GetFont("font_menu_mini.fnt") , ((screen.GetWidth()) - 20)));
            content.SpreadVertically();
            content.AlignCenterAll();
            screen.AddView(content);
            return screen;
        }
        
        private static Container CreatePlacePlayLogoContainer()
        {
            Container placePlayContainer = new Container();
            placePlayContainer.AddView(new TextBox(StrRes.Get(Strings.LOADING_PLACEPLAY_USING) , ScreenFactory.GetFont("font_menu_mini.fnt")));
            placePlayContainer.AddView(new ImageView(ScreenFactory.GetImage("logo_pp.png")));
            placePlayContainer.SpreadVertically(0);
            placePlayContainer.ResizeToFitViews();
            placePlayContainer.AlignCenterAll();
            return placePlayContainer;
        }
        
        private static BitmapFont GetFont(String fontName)
        {
            return AppResManager.GetInstance().GetFont(fontName);
        }
        
        public static Screen CreateBuyFull(MenuListener listener)
        {
            Screen screen = new Screen(ScreenId.BUY_FULL);
            ScreenFactory.CreateDefaultMenuScreen(screen, null);
            int picIndex = screen.AddView(new ImageView(AppResManager.GetInstance().GetImage("full_ver_picture.png")));
            screen.SetViewY(picIndex, 6);
            int headerIndex = screen.AddView(new TextBox(StrRes.Get(Strings.FULL_VERSION_HEADER)));
            screen.SetViewY(headerIndex, 204);
            int textIndex = screen.AddView(new ImageView(AppResManager.GetInstance().GetImage("full_ver_text.png")));
            screen.SetViewY(textIndex, (238 + 5));
            Container buttons = new Container();
            buttons.AddView(new MenuButton(ButtonType.GET_FULL , ButtonId.BUY_FULL_VIA_BROWSER , listener));
            buttons.AddView(new MenuButton(ButtonType.BACK , ButtonId.BACK , listener));
            ScreenFactory.AddAndLayoutMenuBottomButtons(screen, buttons);
            screen.AlignCenterAll();
            return screen;
        }
        
        public static Screen CreateEndlessFinishMenu(MenuListener listener, FlipstonesStory story)
        {
            Screen screen = new Screen(ScreenId.ENDLESS_FINISH);
            Container cont = ScreenFactory.CreateDefaultIngameScreen(screen, StrRes.Get(Strings.ENDLESS_FINISH_HEADER), listener);
            int curScoreIndex = cont.AddView(new TextBox((((StrRes.Get(Strings.CURRENT_SCORE)) + " ") + (story.GetScore()))));
            cont.SetViewY(curScoreIndex, 73);
            int bestScoreIndex = cont.AddView(new TextBox((((StrRes.Get(Strings.BEST_SCORE)) + " ") + (story.GetBestEndlessScore()))));
            cont.SetViewY(bestScoreIndex, (73 + 30));
            Container buttons = new Container();
            buttons.AddView(new MenuButton(ButtonType.PLAY_YELLOW , StrRes.Get(Strings.POST_SCORES) , ButtonId.ENDLESS_POST_SCORES , listener));
            buttons.AddView(new MenuButton(ButtonType.PLAY_ORANGE , StrRes.Get(Strings.ENDLESS_TRY_AGAIN) , ButtonId.ENDLESS_TRY_AGAIN , listener));
            buttons.AddView(new MenuButton(ButtonType.MISC , StrRes.Get(Strings.RESULT_EXIT) , ButtonId.ENDLESS_EXIT , listener));
            ScreenFactory.AddAndLayoutIngameButtons(cont, buttons);
            cont.AlignCenterAll();
            return screen;
        }
        
        public static Screen CreateFrenzyFinishMenu(MenuListener listener, FlipstonesStory story)
        {
            Screen screen = new Screen(ScreenId.FRENZY_FINISH);
            Container cont = ScreenFactory.CreateDefaultIngameScreen(screen, StrRes.Get(Strings.FRENZY_FINISHED_TITLE), listener);
            Container resultContainer = new Container();
            BitmapFont fnt = AppResManager.GetInstance().GetFont("font_menu_mini.fnt");
            String scoreString = ScreenFactory.FormatString(StrRes.Get(Strings.EARNED_SCORE), new Object[]{ JUtils.ValueOf(story.GetScore()) });
            resultContainer.AddView(new TextBox(scoreString , fnt , ((cont.GetWidth()) - ((TEXT_INDENT) * 2))));
            resultContainer.SpreadVertically(0);
            resultContainer.ResizeToFitViews();
            resultContainer.AlignCenterAll();
            int resultIndex = cont.AddView(resultContainer);
            Container tryAgainContainer = new Container();
            String tryAgainDescription = StrRes.Get(Strings.TRY_AGAIN_DESCRIPTION);
            tryAgainContainer.AddView(new MenuButton(ButtonType.PLAY_ORANGE , StrRes.Get(Strings.ENDLESS_TRY_AGAIN) , ButtonId.FRENZY_TRY_AGAIN , listener));
            tryAgainContainer.AddView(new TextBox(tryAgainDescription , fnt , ((cont.GetWidth()) - (TEXT_INDENT))));
            tryAgainContainer.SpreadVertically(0);
            tryAgainContainer.ResizeToFitViews();
            tryAgainContainer.AlignCenterAll();
            int tryAgainContainerIndex = cont.AddView(tryAgainContainer);
            Container postScoreContainer = new Container();
            String postScoreDescription = StrRes.Get(Strings.POST_SCORE_DESCRIPTION);
            postScoreContainer.AddView(new MenuButton(ButtonType.POST_SCORE , ButtonId.FRENZY_POST_SCORES , listener));
            postScoreContainer.AddView(new TextBox(postScoreDescription , fnt , ((cont.GetWidth()) - (TEXT_INDENT))));
            postScoreContainer.SpreadVertically(0);
            postScoreContainer.ResizeToFitViews();
            postScoreContainer.AlignCenterAll();
            int postScoreContainerIndex = cont.AddView(postScoreContainer);
            cont.SetViewY(resultIndex, 50);
            cont.SetViewY(tryAgainContainerIndex, 110);
            cont.SetViewY(postScoreContainerIndex, 193);
            cont.AlignCenterAll();
            return screen;
        }
        
        public static Screen CreateCheatsScreen(MenuListener listener)
        {
            Screen screen = new Screen(ScreenId.CHEATS);
            ColorRect rect = new ColorRect(160 , 200 , 0);
            screen.AddView(rect);
            Container container = new Container();
            for (int row = 0; row < 4; row++) 
            {
                Container c = new Container();
                for (int col = 0; col < 3; col++) 
                {
                    int num = ((row * 3) + col) + 1;
                    if (num == 10)
                        num = 0;
                    
                    if (num > 10)
                        break;
                    
                    CheatButton button = new CheatButton(num , listener);
                    c.AddView(button);
                }
                c.SpreadHorizontally(5);
                c.ResizeToFitViews();
                container.AddView(c);
            }
            container.SpreadVertically(5);
            container.ResizeToFitViews();
            container.AlignCenterAll();
            screen.AddView(container);
            screen.AlignCenterAll();
            screen.AlignMiddleAll();
            return screen;
        }
        
        public static InfoScreen CreateInfoScreen(MenuListener listener)
        {
            return new InfoScreen(listener , InfoScreen.SCREEN_MENU);
        }
        
        public static Screen CreateHowToPlayScreen(MenuListener listener, int page, int count, bool fromMenu)
        {
            System.Diagnostics.Debug.Assert((page >= 0) && (page < count));
            Screen screen = new Screen(ScreenId.HOW_TO_PLAY);
            ScreenFactory.CreateDefaultMenuScreen(screen, null);
            Container cont = new Container();
            cont.SetSize(screen.GetWidth(), screen.GetHeight());
            screen.AddView(cont);
            int imageIndex = cont.AddView(new ImageView(AppResManager.GetInstance().GetImage((("how_to_play_0" + (page + 1)) + ".png"))));
            cont.SetViewY(imageIndex, 10);
            int headerIndex = -1;
            int textIndex = -1;
            int HOW_TO_PLAY_TEXT_HEIGHT = 100;
            switch (page)
            {
                case 0:
                    headerIndex = cont.AddView(new TextBox(StrRes.Get(Strings.HOW_TO_PLAY_HEAD_0)));
                    textIndex = cont.AddView(new TextBox(StrRes.Get(Strings.HOW_TO_PLAY_0) , cont.GetWidth() , HOW_TO_PLAY_TEXT_HEIGHT));
                    break;
                case 1:
                    headerIndex = cont.AddView(new TextBox(StrRes.Get(Strings.HOW_TO_PLAY_HEAD_1)));
                    textIndex = cont.AddView(new TextBox(StrRes.Get(Strings.HOW_TO_PLAY_1) , cont.GetWidth() , HOW_TO_PLAY_TEXT_HEIGHT));
                    break;
                case 2:
                    headerIndex = cont.AddView(new TextBox(StrRes.Get(Strings.HOW_TO_PLAY_HEAD_2)));
                    textIndex = cont.AddView(new TextBox(StrRes.Get(Strings.HOW_TO_PLAY_2) , cont.GetWidth() , HOW_TO_PLAY_TEXT_HEIGHT));
                    break;
                case 3:
                    headerIndex = cont.AddView(new TextBox(StrRes.Get(Strings.HOW_TO_PLAY_HEAD_3)));
                    textIndex = cont.AddView(new TextBox(StrRes.Get(Strings.HOW_TO_PLAY_3) , cont.GetWidth() , HOW_TO_PLAY_TEXT_HEIGHT));
                    break;
                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }
            cont.SetViewY(headerIndex, 216);
            cont.SetViewY(textIndex, 252);
            if (fromMenu) 
            {
                int pageNavigIndex = cont.AddView(new PageNavigator(listener , page , count));
                cont.SetViewY(pageNavigIndex, 374);
            } 
            cont.AlignCenterAll();
            if (fromMenu) 
            {
                ScreenFactory.AddMenuBackBottomButton(screen, listener);
            } 
            else 
            {
                Container buttonsContainer = new Container();
                buttonsContainer.AddView(new MenuButton(ButtonType.MISC , StrRes.Get(Strings.CONTINUE_HOW_TO_PLAY) , ButtonId.CONTINUE_HOW_TO_PLAY , listener));
                MenuButton skipButton = new MenuButton(ButtonType.MISC , StrRes.Get(Strings.SKIP_HOW_TO_PLAY) , ButtonId.SKIP_HOW_TO_PLAY , listener);
                buttonsContainer.AddView(skipButton);
                screen.SetBackButton(skipButton);
                ScreenFactory.AddAndLayoutMenuBottomButtons(screen, buttonsContainer);
            }
            return screen;
        }
        
        public static Screen CreateAboutScreen(MenuListener listener)
        {
            Screen screen = new Screen(ScreenId.ABOUT);
            ScreenFactory.CreateDefaultMenuScreen(screen, StrRes.Get(Strings.ABOUT_HEADER));
            String name = StrRes.Get((Config.freeVersion ? Strings.APP_NAME_FREE : Strings.APP_NAME_FULL));
            Object[] _params = new Object[]{ name , Config.appVersion , Config.appRevision };
            String aboutText = ScreenFactory.FormatString(StrRes.Get(Strings.ABOUT_TEXT), _params);
            TextBox textBox = new TextBox(aboutText , ((screen.GetWidth()) - ((TEXT_INDENT) * 2)));
            ScrollPane scroll = new ScrollPane(textBox , false , true , screen);
            scroll.SetSize(textBox.GetWidth(), ((screen.GetHeight()) - 160));
            screen.AddView(scroll);
            screen.AlignMiddle(scroll);
            screen.AlignCenterAll();
            ScreenFactory.AddMenuBackBottomButton(screen, listener);
            return screen;
        }
        
        public static Screen CreateLoadingScreen(int levelNumber)
        {
            Screen screen = new Screen(ScreenId.LOADING);
            ImageView image = new ImageView(ScreenFactory.GetImage("menu_background.png"));
            screen.AddView(image);
            TextBox centerText;
            if (levelNumber == (-2)) 
            {
                centerText = new TextBox(StrRes.Get(Strings.FRENZY));
            } 
            else if (levelNumber > 0) 
            {
                centerText = new TextBox(((StrRes.Get(Strings.START_MISSION_HEADER_1)) + levelNumber));
            } 
            else 
            {
                centerText = new TextBox(StrRes.Get(Strings.WAIT));
            }
            screen.AddView(centerText);
            TextBox loadingText = new TextBox(StrRes.Get(Strings.LOADING));
            screen.AddView(loadingText);
            screen.AlignCenterAll().AlignMiddleAll();
            screen.AlignVertically(loadingText, 90);
            return screen;
        }
        
        private const int TEXT_INDENT = 10;
        
        private const int SPACE_BETWEEN_BUTTONS = 5;
        
        private static Image GetImage(String imageRes)
        {
            return AppResManager.GetInstance().GetImage(imageRes);
        }
        
        public static Container CreateDefaultIngameScreen(Screen screen, String header, MenuListener pauseButtonListener)
        {
            return ScreenFactory.CreateDefaultIngameScreen(screen, header, pauseButtonListener, true);
        }
        
        public static Container CreateDefaultIngameScreen(Screen screen, String header, MenuListener pauseButtonListener, bool showPauseButton)
        {
            ColorRect semiBack = new ColorRect(screen.GetWidth() , screen.GetHeight() , 0);
            semiBack.SetAlpha(128);
            screen.AddView(semiBack);
            ImageView back = new ImageView(ScreenFactory.GetImage("popup_background.png"));
            int backIndex = screen.AddView(back);
            screen.AlignCenter(back);
            screen.AlignMiddle(back);
            if (header != null) 
            {
                TextBox headerBox = new TextBox(header , AppResManager.GetDefaultFont());
                int headerIndex = screen.AddView(headerBox);
                screen.AlignCenter(headerBox);
                screen.SetViewY(headerIndex, ((screen.GetViewY(backIndex)) + 12));
            } 
            if (showPauseButton) 
            {
                ScreenFactory.AddPauseButton(screen, pauseButtonListener);
            } 
            Container cont = new Container();
            int contIndex = screen.AddView(cont);
            cont.SetSize(back.GetWidth(), back.GetHeight());
            screen.SetViewX(contIndex, screen.GetViewX(backIndex));
            screen.SetViewY(contIndex, screen.GetViewY(backIndex));
            return cont;
        }
        
        public static void CreateDefaultMenuScreen(Screen screen, String header)
        {
            ImageView back = new ImageView(ScreenFactory.GetImage("menu_background.png"));
            screen.AddView(back);
            if (header != null) 
            {
                TextBox headerBox = new TextBox(header , AppResManager.GetInstance().GetFont("font_head.fnt"));
                int headerIndex = screen.AddView(headerBox);
                screen.AlignCenter(headerBox);
                screen.SetViewY(headerIndex, 14);
            } 
        }
        
        public static int AddAndLayoutButtons(Container parentCont, Container buttonsCont, int contY)
        {
            return ScreenFactory.AddAndLayoutButtons(parentCont, buttonsCont, contY, SPACE_BETWEEN_BUTTONS);
        }
        
        public static int AddAndLayoutButtons(Container parentCont, Container buttonsCont, int contY, int spaceBetweenButtons)
        {
            int contIndex = parentCont.AddView(buttonsCont);
            buttonsCont.SpreadVertically(spaceBetweenButtons);
            buttonsCont.ResizeToFitViews();
            buttonsCont.AlignCenterAll();
            parentCont.AlignCenter(buttonsCont);
            parentCont.SetViewY(contIndex, contY);
            return contIndex;
        }
        
        public static int AddAndLayoutMenuBottomButtons(Container parentCont, Container buttonsCont)
        {
            int contIndex = ScreenFactory.AddAndLayoutButtons(parentCont, buttonsCont, 0);
            parentCont.SetViewY(contIndex, (((parentCont.GetHeight()) - (buttonsCont.GetHeight())) - 12));
            return contIndex;
        }
        
        public static int AddAndLayoutIngameButtons(Container parentCont, Container buttonsCont)
        {
            int contIndex = ScreenFactory.AddAndLayoutButtons(parentCont, buttonsCont, 0);
            parentCont.SetViewY(contIndex, (((parentCont.GetHeight()) - (buttonsCont.GetHeight())) - 15));
            return contIndex;
        }
        
        public static void AddMenuBackBottomButton(Screen screen, MenuListener listener)
        {
            Container buttonsContainer = new Container();
            MenuButton backButton = new MenuButton(ButtonType.BACK , ButtonId.BACK , listener);
            buttonsContainer.AddView(backButton);
            screen.SetBackButton(backButton);
            ScreenFactory.AddAndLayoutMenuBottomButtons(screen, buttonsContainer);
        }
        
        public static PauseButton AddPauseButton(Screen screen, MenuListener listener)
        {
            PauseButton pauseButton = new PauseButton(ButtonId.PAUSE , listener);
            int pauseIndex = screen.AddView(pauseButton);
            screen.SetBackButton(pauseButton);
            screen.SetViewX(pauseIndex, 3);
            return pauseButton;
        }
        
        public static Screen CreateBoostsSelector(MenuController listener)
        {
            Screen screen = new Screen(ScreenId.MAIN_MENU);
            ScreenFactory.CreateDefaultMenuScreen(screen, "BOOSTS");
            Container buttons = new Container();
            for (int i = 0; i < (Boost.BOOSTS_COUNT); i++) 
            {
                buttons.AddView(new ToggleButton(Boost.GetNameById(i) , i , listener));
            }
            buttons.SpreadVertically(0);
            buttons.ResizeToFitViews();
            buttons.SetWidth(screen.GetWidth());
            buttons.AlignCenterAll();
            Container container = new Container();
            ScrollPane scroll = new ScrollPane(buttons , false , true , screen);
            scroll.SetSize(screen.GetWidth(), 250);
            container.AddView(scroll);
            container.AddView(new MenuButton(ButtonType.PLAY_ORANGE , StrRes.Get(Strings.PLAY) , ButtonId.PLAY , listener));
            MenuButton backButton = new MenuButton(ButtonType.MISC , StrRes.Get(Strings.BACK) , ButtonId.BACK , listener);
            container.AddView(backButton);
            screen.SetBackButton(backButton);
            ScreenFactory.AddAndLayoutButtons(screen, container, 70, 15);
            return screen;
        }
        
        private static String FormatString(String str, Object[] _params)
        {
            String _out = "";
            int len = str.Length;
            int pos = 0;
            int paramNum = 0;
            for (int i = 0; i < len; i++) 
            {
                if ((str[i]) == '%') 
                {
                    i++;
                    _out += JUtils.Substring(str, pos, (i - 1));
                    pos = i;
                    if ((i >= len) || ((str[i]) != '%')) 
                    {
                        System.Diagnostics.Debug.Assert(paramNum < (_params.Length));
                        System.Diagnostics.Debug.Assert((_params[paramNum]) != null, "Null param in formatString()");
                        _out += _params[paramNum].ToString();
                        paramNum++;
                    } 
                } 
            }
            _out += JUtils.Substring(str, pos);
            return _out;
        }
        
    }
    
    
}