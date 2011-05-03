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
		public const int IMG_UI_BUTTON_A = 0;
		public const int IMG_UI_BUTTON_B = 1;
		// PACK_MENU
        public const int ANI_TEST = 2;
		public const int RES_COUNT = 3;
	}
	
	public class Resources
	{
		public static ResourceLoadInfo[][] PACKS =
		{
			// PACK_COMMON
			new ResourceLoadInfo[]
			{
				new ResourceLoadInfo("buttonA", Res.IMG_UI_BUTTON_A, ResType.IMAGE),
				new ResourceLoadInfo("buttonB", Res.IMG_UI_BUTTON_B, ResType.IMAGE),
			},
			// PACK_MENU
			new ResourceLoadInfo[]
			{
                new ResourceLoadInfo("test", Res.ANI_TEST, ResType.SWF),
			},
		};
	}
}
