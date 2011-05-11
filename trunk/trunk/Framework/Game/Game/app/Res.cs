// This file was generated. Do not modify.

using asap.resources;

namespace app
{
	public class ResPacks
	{
		public const int PACK_COMMON = 0;
		public const int PACK_MENU = 1;
		public const int PACKS_COUNT = 2;
	}
	
	public class Res
	{
		// PACK_COMMON
		public const int ATLAS_COMMON = 0;
		public const int IMG_UI_BUTTON_A = 1;
		public const int IMG_UI_BUTTON_B = 2;
		public const int FNT_FONT_TEST = 3;
		// PACK_MENU
		public const int ANI_ANIM = 4;
		public const int IMG_UI_BUTTON_A2 = 5;
		public const int IMG_UI_BUTTON_B2 = 6;
		public const int IMG_DUCK_DEAD = 7;
		public const int MUSIC_MUSIC = 8;
		public const int SND_SOUND = 9;
		public const int RES_COUNT = 10;
	}
	
	public class Resources
	{
		public static ResourceLoadInfo[][] PACKS =
		{
			// PACK_COMMON
			new ResourceLoadInfo[]
			{
				new ResourceLoadInfo("common", Res.ATLAS_COMMON, ResType.ATLAS),
				new ResourceLoadInfo("font_test", Res.FNT_FONT_TEST, ResType.BITMAP_FONT),
			},
			// PACK_MENU
			new ResourceLoadInfo[]
			{
				new ResourceLoadInfo("test", Res.ANI_ANIM, ResType.SWF),
				new ResourceLoadInfo("buttonA", Res.IMG_UI_BUTTON_A2, ResType.IMAGE),
				new ResourceLoadInfo("buttonB", Res.IMG_UI_BUTTON_B2, ResType.IMAGE),
				new ResourceLoadInfo("duck_dead", Res.IMG_DUCK_DEAD, ResType.IMAGE),
				new ResourceLoadInfo("music", Res.MUSIC_MUSIC, ResType.MUSIC),
				new ResourceLoadInfo("sound", Res.SND_SOUND, ResType.SOUND),
			},
		};
	}
}
