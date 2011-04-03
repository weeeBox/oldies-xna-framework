using System;

using System.Collections.Generic;


using swiff.com.jswiff.io;

namespace swiff.com.jswiff.swfrecords.tags
{
    /** 
     * This class contains methods used for parsing tag headers and tags.
     */
    public class TagReader
    {
        private TagReader() 
        {
        }
        
        public static Tag ReadTag(TagHeader header, byte[] tagData, short swfVersion, bool japanese) /* throws IOException */
        {
            Tag tag;
            switch (header.GetCode())
            {
                case TagConstants.DEFINE_BITS:
                    tag = new DefineBits();
                    break;
                case TagConstants.DEFINE_BITS_JPEG_2:
                    tag = new DefineBitsJPEG2();
                    break;
                case TagConstants.DEFINE_BITS_JPEG_3:
                    tag = new DefineBitsJPEG3();
                    break;
                case TagConstants.DEFINE_BITS_LOSSLESS:
                    tag = new DefineBitsLossless();
                    break;
                case TagConstants.DEFINE_BITS_LOSSLESS_2:
                    tag = new DefineBitsLossless2();
                    break;
                case TagConstants.DEFINE_BUTTON:
                    tag = new DefineButton();
                    break;
                case TagConstants.DEFINE_BUTTON_2:
                    tag = new DefineButton2();
                    break;
                case TagConstants.DEFINE_BUTTON_C_XFORM:
                    tag = new DefineButtonCXform();
                    break;
                case TagConstants.DEFINE_BUTTON_SOUND:
                    tag = new DefineButtonSound();
                    break;
                case TagConstants.DEFINE_EDIT_TEXT:
                    tag = new DefineEditText();
                    break;
                case TagConstants.DEFINE_FONT:
                    tag = new DefineFont();
                    break;
                case TagConstants.DEFINE_FONT_2:
                    tag = new DefineFont2();
                    break;
                case TagConstants.DEFINE_FONT_3:
                    tag = new DefineFont3();
                    break;
                case TagConstants.DEFINE_FONT_INFO:
                    tag = new DefineFontInfo();
                    break;
                case TagConstants.DEFINE_FONT_INFO_2:
                    tag = new DefineFontInfo2();
                    break;
                case TagConstants.FLASHTYPE_SETTINGS:
                    tag = new FlashTypeSettings();
                    break;
                case TagConstants.DEFINE_FONT_ALIGNMENT:
                    tag = new DefineFontAlignment();
                    break;
                case TagConstants.DEFINE_MORPH_SHAPE:
                    tag = new DefineMorphShape();
                    break;
                case TagConstants.DEFINE_MORPH_SHAPE_2:
                    tag = new DefineMorphShape2();
                    break;
                case TagConstants.DEFINE_SHAPE:
                    tag = new DefineShape();
                    break;
                case TagConstants.DEFINE_SHAPE_2:
                    tag = new DefineShape2();
                    break;
                case TagConstants.DEFINE_SHAPE_3:
                    tag = new DefineShape3();
                    break;
                case TagConstants.DEFINE_SHAPE_4:
                    tag = new DefineShape4();
                    break;
                case TagConstants.DEFINE_SOUND:
                    tag = new DefineSound();
                    break;
                case TagConstants.DEFINE_SPRITE:
                    tag = new DefineSprite();
                    break;
                case TagConstants.DEFINE_TEXT:
                    tag = new DefineText();
                    break;
                case TagConstants.DEFINE_TEXT_2:
                    tag = new DefineText2();
                    break;
                case TagConstants.DEFINE_VIDEO_STREAM:
                    tag = new DefineVideoStream();
                    break;
                case TagConstants.DO_ACTION:
                    tag = new DoAction();
                    break;
                case TagConstants.DO_INIT_ACTION:
                    tag = new DoInitAction();
                    break;
                case TagConstants.ENABLE_DEBUGGER_2:
                    tag = new EnableDebugger2();
                    break;
                case TagConstants.ENABLE_DEBUGGER:
                    tag = new EnableDebugger();
                    break;
                case TagConstants.EXPORT_ASSETS:
                    tag = new ExportAssets();
                    break;
                case TagConstants.FILE_ATTRIBUTES:
                    tag = new FileAttributes();
                    break;
                case TagConstants.FRAME_LABEL:
                    tag = new FrameLabel();
                    break;
                case TagConstants.IMPORT_ASSETS:
                    tag = new ImportAssets();
                    break;
                case TagConstants.IMPORT_ASSETS_2:
                    tag = new ImportAssets2();
                    break;
                case TagConstants.JPEG_TABLES:
                    tag = new JPEGTables();
                    break;
                case TagConstants.METADATA:
                    tag = new Metadata();
                    break;
                case TagConstants.PLACE_OBJECT:
                    tag = new PlaceObject();
                    break;
                case TagConstants.PLACE_OBJECT_2:
                    tag = new PlaceObject2();
                    break;
                case TagConstants.PLACE_OBJECT_3:
                    tag = new PlaceObject3();
                    break;
                case TagConstants.PROTECT:
                    tag = new Protect();
                    break;
                case TagConstants.REMOVE_OBJECT:
                    tag = new RemoveObject();
                    break;
                case TagConstants.REMOVE_OBJECT_2:
                    tag = new RemoveObject2();
                    break;
                case TagConstants.SCRIPT_LIMITS:
                    tag = new ScriptLimits();
                    break;
                case TagConstants.SET_BACKGROUND_COLOR:
                    tag = new SetBackgroundColor();
                    break;
                case TagConstants.SET_TAB_INDEX:
                    tag = new SetTabIndexTag();
                    break;
                case TagConstants.SHOW_FRAME:
                    tag = new ShowFrame();
                    break;
                case TagConstants.SCALE_9_GRID:
                    tag = new Scale9Grid();
                    break;
                case TagConstants.SOUND_STREAM_BLOCK:
                    tag = new SoundStreamBlock();
                    break;
                case TagConstants.SOUND_STREAM_HEAD:
                    tag = new SoundStreamHead();
                    break;
                case TagConstants.SOUND_STREAM_HEAD_2:
                    tag = new SoundStreamHead2();
                    break;
                case TagConstants.START_SOUND:
                    tag = new StartSound();
                    break;
                case TagConstants.VIDEO_FRAME:
                    tag = new VideoFrame();
                    break;
                case TagConstants.SYMBOL_CLASS:
                    tag = new SymbolClass();
                    break;
                case TagConstants.DO_ABC:
                    tag = new DoABC();
                    break;
                case TagConstants.DEFINE_SCENE_AND_FRAME_LABEL_DATA:
                    tag = new DefineSceneAndFrameLabelData();
                    break;
                default:
                    tag = new UnknownTag();
                    break;
            }
            tag.SetCode(header.GetCode());
            tag.SetSWFVersion(swfVersion);
            tag.SetJapanese(japanese);
            tag.SetData(tagData);
            return tag;
        }
        
        public static byte[] ReadTagData(InputBitStream stream, TagHeader header) /* throws IOException */
        {
            return stream.ReadBytes(header.GetLength());
        }
        
        public static TagHeader ReadTagHeader(InputBitStream stream) /* throws IOException */
        {
            return new TagHeader(stream);
        }
        
        public static Tag ReadTag(InputBitStream stream, short swfVersion, bool shiftJIS) /* throws IOException */
        {
            TagHeader header = new TagHeader(stream);
            byte[] tagData = stream.ReadBytes(header.GetLength());
            return TagReader.ReadTag(header, tagData, swfVersion, shiftJIS);
        }
    }
}