using System;

using System.Collections.Generic;



namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * This class contains constants regarding SWF tags.
     */
    public class TagConstants
    {
        /** 
         *
         */
        public const int DEFINE_BITS = 6;
        
        /** 
         *
         */
        public const int DEFINE_BITS_JPEG_2 = 21;
        
        /** 
         *
         */
        public const int DEFINE_BITS_JPEG_3 = 35;
        
        /** 
         *
         */
        public const int DEFINE_BITS_LOSSLESS = 20;
        
        /** 
         *
         */
        public const int DEFINE_BITS_LOSSLESS_2 = 36;
        
        /** 
         *
         */
        public const int DEFINE_BUTTON = 7;
        
        /** 
         *
         */
        public const int DEFINE_BUTTON_2 = 34;
        
        /** 
         *
         */
        public const int DEFINE_BUTTON_C_XFORM = 23;
        
        /** 
         *
         */
        public const int DEFINE_BUTTON_SOUND = 17;
        
        /** 
         *
         */
        public const int DEFINE_EDIT_TEXT = 37;
        
        /** 
         *
         */
        public const int DEFINE_FONT = 10;
        
        /** 
         *
         */
        public const int DEFINE_FONT_2 = 48;
        
        /** 
         *
         */
        public const int DEFINE_FONT_3 = 75;
        
        /** 
         *
         */
        public const int DEFINE_FONT_INFO = 13;
        
        /** 
         *
         */
        public const int DEFINE_FONT_INFO_2 = 62;
        
        /** 
         *
         */
        public const short FLASHTYPE_SETTINGS = 74;
        
        /** 
         *
         */
        public const int DEFINE_FONT_ALIGNMENT = 73;
        
        /** 
         *
         */
        public const int DEFINE_MORPH_SHAPE = 46;
        
        /** 
         *
         */
        public const int DEFINE_MORPH_SHAPE_2 = 84;
        
        /** 
         *
         */
        public const int DEFINE_SHAPE = 2;
        
        /** 
         *
         */
        public const int DEFINE_SHAPE_2 = 22;
        
        /** 
         *
         */
        public const int DEFINE_SHAPE_3 = 32;
        
        /** 
         *
         */
        public const int DEFINE_SHAPE_4 = 83;
        
        /** 
         *
         */
        public const int DEFINE_SOUND = 14;
        
        /** 
         *
         */
        public const int DEFINE_SPRITE = 39;
        
        /** 
         *
         */
        public const int DEFINE_TEXT = 11;
        
        /** 
         *
         */
        public const int DEFINE_TEXT_2 = 33;
        
        /** 
         *
         */
        public const int DEFINE_VIDEO_STREAM = 60;
        
        /** 
         *
         */
        public const int DO_ACTION = 12;
        
        /** 
         *
         */
        public const int DO_INIT_ACTION = 59;
        
        /** 
         *
         */
        public const int ENABLE_DEBUGGER_2 = 64;
        
        /** 
         *
         */
        public const int ENABLE_DEBUGGER = 58;
        
        /** 
         *
         */
        public const int END = 0;
        
        /** 
         *
         */
        public const int EXPORT_ASSETS = 56;
        
        /** 
         *
         */
        public const int FILE_ATTRIBUTES = 69;
        
        /** 
         *
         */
        public const int FRAME_LABEL = 43;
        
        /** 
         *
         */
        public const int FREE_CHARACTER = 3;
        
        /** 
         *
         */
        public const int IMPORT_ASSETS = 57;
        
        /** 
         *
         */
        public const int IMPORT_ASSETS_2 = 71;
        
        /** 
         *
         */
        public const int JPEG_TABLES = 8;
        
        /** 
         *
         */
        public const int METADATA = 77;
        
        /** 
         *
         */
        public const int PLACE_OBJECT = 4;
        
        /** 
         *
         */
        public const int PLACE_OBJECT_2 = 26;
        
        /** 
         *
         */
        public const int PLACE_OBJECT_3 = 70;
        
        /** 
         *
         */
        public const int PROTECT = 24;
        
        /** 
         *
         */
        public const int REMOVE_OBJECT = 5;
        
        /** 
         *
         */
        public const int REMOVE_OBJECT_2 = 28;
        
        /** 
         *
         */
        public const int SCRIPT_LIMITS = 65;
        
        /** 
         *
         */
        public const int SET_BACKGROUND_COLOR = 9;
        
        /** 
         *
         */
        public const int SET_TAB_INDEX = 66;
        
        /** 
         *
         */
        public const int SHOW_FRAME = 1;
        
        /** 
         *
         */
        public const short SCALE_9_GRID = 78;
        
        /** 
         *
         */
        public const int SOUND_STREAM_BLOCK = 19;
        
        /** 
         *
         */
        public const int SOUND_STREAM_HEAD = 18;
        
        /** 
         *
         */
        public const int SOUND_STREAM_HEAD_2 = 45;
        
        /** 
         *
         */
        public const int START_SOUND = 15;
        
        /** 
         *
         */
        public const int VIDEO_FRAME = 61;
        
        /** 
         *
         */
        public const int MALFORMED = -1;
        
        /** 
         *
         */
        public const int SYMBOL_CLASS = 76;
        
        /** 
         *
         */
        public const int DO_ABC = 82;
        
        /** 
         *
         */
        public const int DEFINE_SCENE_AND_FRAME_LABEL_DATA = 86;
        
        /** 
         *
         */
        public const int DEFINE_PACKED_IMAGE = 100;
        
        private TagConstants() 
        {
        }
        
        public static String GetTagName(int code)
        {
            String result;
            switch (code)
            {
                case DEFINE_BITS:
                    result = "DefineBits";
                    break;
                case DEFINE_BITS_JPEG_2:
                    result = "DefineBitsJPEG2";
                    break;
                case DEFINE_BITS_JPEG_3:
                    result = "DefineBitsJPEG3";
                    break;
                case DEFINE_BITS_LOSSLESS:
                    result = "DefineBitsLossless";
                    break;
                case DEFINE_BITS_LOSSLESS_2:
                    result = "DefineBitsLossless2";
                    break;
                case DEFINE_BUTTON:
                    result = "DefineButton";
                    break;
                case DEFINE_BUTTON_2:
                    result = "DefineButton2";
                    break;
                case DEFINE_BUTTON_C_XFORM:
                    result = "DefineButtonCXform";
                    break;
                case DEFINE_BUTTON_SOUND:
                    result = "DefineButtonSound";
                    break;
                case DEFINE_EDIT_TEXT:
                    result = "DefineEditText";
                    break;
                case DEFINE_FONT:
                    result = "DefineFont";
                    break;
                case DEFINE_FONT_2:
                    result = "DefineFont2";
                    break;
                case DEFINE_FONT_3:
                    result = "DefineFont3";
                    break;
                case DEFINE_FONT_INFO:
                    result = "DefineFontInfo";
                    break;
                case DEFINE_FONT_INFO_2:
                    result = "DefineFontInfo2";
                    break;
                case FLASHTYPE_SETTINGS:
                    result = "FlashTypeSettings";
                    break;
                case DEFINE_FONT_ALIGNMENT:
                    result = "DefineFontInfo3";
                    break;
                case DEFINE_MORPH_SHAPE:
                    result = "DefineMorphShape";
                    break;
                case DEFINE_MORPH_SHAPE_2:
                    result = "DefineMorphShape2";
                    break;
                case DEFINE_SHAPE:
                    result = "DefineShape";
                    break;
                case DEFINE_SHAPE_2:
                    result = "DefineShape2";
                    break;
                case DEFINE_SHAPE_3:
                    result = "DefineShape3";
                    break;
                case DEFINE_SHAPE_4:
                    result = "DefineShape4";
                    break;
                case DEFINE_SOUND:
                    result = "DefineSound";
                    break;
                case DEFINE_SPRITE:
                    result = "DefineSprite";
                    break;
                case DEFINE_TEXT:
                    result = "DefineText";
                    break;
                case DEFINE_TEXT_2:
                    result = "DefineText2";
                    break;
                case DEFINE_VIDEO_STREAM:
                    result = "DefineVideoStream";
                    break;
                case DO_ACTION:
                    result = "DoAction";
                    break;
                case DO_INIT_ACTION:
                    result = "DoInitAction";
                    break;
                case ENABLE_DEBUGGER_2:
                    result = "EnableDebugger2";
                    break;
                case ENABLE_DEBUGGER:
                    result = "EnableDebugger";
                    break;
                case END:
                    result = "End";
                    break;
                case EXPORT_ASSETS:
                    result = "ExportAssets";
                    break;
                case FILE_ATTRIBUTES:
                    result = "FileAttributes";
                    break;
                case FRAME_LABEL:
                    result = "FrameLabel";
                    break;
                case FREE_CHARACTER:
                    result = "FreeCharacter";
                    break;
                case IMPORT_ASSETS:
                    result = "ImportAssets";
                    break;
                case IMPORT_ASSETS_2:
                    result = "ImportAssets2";
                    break;
                case JPEG_TABLES:
                    result = "JPEGTables";
                    break;
                case METADATA:
                    result = "Metadata";
                    break;
                case PLACE_OBJECT:
                    result = "PlaceObject";
                    break;
                case PLACE_OBJECT_2:
                    result = "PlaceObject2";
                    break;
                case PLACE_OBJECT_3:
                    result = "PlaceObject3";
                    break;
                case PROTECT:
                    result = "Protect";
                    break;
                case REMOVE_OBJECT:
                    result = "RemoveObject";
                    break;
                case REMOVE_OBJECT_2:
                    result = "RemoveObject2";
                    break;
                case SCRIPT_LIMITS:
                    result = "ScriptLimits";
                    break;
                case SET_BACKGROUND_COLOR:
                    result = "SetBackgroundColor";
                    break;
                case SET_TAB_INDEX:
                    result = "SetTabIndex";
                    break;
                case SHOW_FRAME:
                    result = "ShowFrame";
                    break;
                case SCALE_9_GRID:
                    result = "Scale9Grid";
                    break;
                case SOUND_STREAM_BLOCK:
                    result = "SoundStreamBlock";
                    break;
                case SOUND_STREAM_HEAD:
                    result = "SoundStreamHead";
                    break;
                case SOUND_STREAM_HEAD_2:
                    result = "SoundStreamHead2";
                    break;
                case START_SOUND:
                    result = "StartSound";
                    break;
                case VIDEO_FRAME:
                    result = "VideoFrame";
                    break;
                case MALFORMED:
                    result = "Malformed tag";
                    break;
                case SYMBOL_CLASS:
                    result = "SymbolClass";
                    break;
                case DO_ABC:
                    result = "DoABC";
                    break;
                case DEFINE_SCENE_AND_FRAME_LABEL_DATA:
                    result = "DefineSceneAndFrameLabelData";
                    break;
                case DEFINE_PACKED_IMAGE:
                    result = "DefinePackedImage";
                    break;
                default:
                    result = "Unknown tag";
                    break;
            }
            return result;
        }
    }
}