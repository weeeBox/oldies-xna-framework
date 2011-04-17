// This file was generated. Do not modify.

using asap.resources;

namespace app
{
	public class ResPacks
	{
		public const int PACK_COMMON1 = 0;
		public const int PACK_COMMON2 = 1;
		public const int PACKS_COUNT = 2;
	}
	
	public class Res
	{
		// PACK_COMMON1
		public const int IMG_UI_BUTTON_A = 0;
		public const int IMG_UI_BUTTON_B = 1;
		// PACK_COMMON2
		public const int IMG_BUTTON_BASE1 = 2;
		public const int IMG_BUTTON_BASE2 = 3;
		public const int RES_COUNT = 4;
	}
	
	public class Resources
	{
		public static ResourceLoadInfo[][] PACKS =
		{
			// PACK_COMMON1
			new ResourceLoadInfo[]
			{
				new ResourceLoadInfo("buttonA", Res.IMG_UI_BUTTON_A, ResType.IMAGE),
				new ResourceLoadInfo("buttonB", Res.IMG_UI_BUTTON_B, ResType.IMAGE),
			},
			// PACK_COMMON2
			new ResourceLoadInfo[]
			{
				new ResourceLoadInfo("cloud_1", Res.IMG_BUTTON_BASE1, ResType.IMAGE),
				new ResourceLoadInfo("cloud_2", Res.IMG_BUTTON_BASE2, ResType.IMAGE),
			},
		};
	}
}
