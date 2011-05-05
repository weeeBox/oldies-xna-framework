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
		// PACK_MENU
		public const int ANI_SWF_TEST = 3;
		public const int IMG_UI_BUTTON_A2 = 4;
		public const int IMG_UI_BUTTON_B2 = 5;
		public const int RES_COUNT = 6;
	}
	
	public class Resources
	{
		public static ResourceLoadInfo[][] PACKS =
		{
			// PACK_COMMON
			new ResourceLoadInfo[]
			{
				new ResourceLoadInfo("common", Res.ATLAS_COMMON, ResType.ATLAS),
			},
			// PACK_MENU
			new ResourceLoadInfo[]
			{
				new ResourceLoadInfo("test", Res.ANI_SWF_TEST, ResType.SWF),
				new ResourceLoadInfo("buttonA", Res.IMG_UI_BUTTON_A2, ResType.IMAGE),
				new ResourceLoadInfo("buttonB", Res.IMG_UI_BUTTON_B2, ResType.IMAGE),
			},
		};
	}
}
