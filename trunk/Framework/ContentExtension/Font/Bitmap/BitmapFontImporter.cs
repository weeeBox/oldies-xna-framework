using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;

using System.IO;
using System.Xml;
using System.Globalization;

namespace ContentExtension.Font.Bitmap
{    
    [ContentImporter(".bitmapfont", DisplayName = "Bitmap Font Importer", DefaultProcessor = "BitmapFontProcessor")]
    public class BitmapFontImporter : ContentImporter<BitmapFontInfo>
    {
        public override BitmapFontInfo Import(string filename, ContentImporterContext context)
        {
            BitmapFontInfo fontInfo = null;            

            using (XmlTextReader reader = new XmlTextReader(File.Open(filename, FileMode.Open)))
            {
                while (reader.Read())
                {
                    string nodeName = reader.Name;
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            {
                                Dictionary<string, string> attributes = new Dictionary<string, string>();
                                while (reader.MoveToNextAttribute())
                                {
                                    attributes.Add(reader.Name, reader.Value);
                                }

                                if (nodeName == "font")
                                {
                                    NumberFormatInfo nf = new CultureInfo("en-US").NumberFormat;

                                    string sourceFilename = attributes["filename"];
                                    int index = sourceFilename.LastIndexOf('.');
                                    string sourceName = index == -1 ? sourceFilename : sourceFilename.Substring(0, index);
                                    fontInfo = new BitmapFontInfo(sourceName);
                                    fontInfo.InternalLeading = sbyte.Parse(attributes["internalLeading"]);
                                    fontInfo.Ascender = sbyte.Parse(attributes["ascender"]);
                                    fontInfo.Descender = sbyte.Parse(attributes["descender"]);
                                    fontInfo.ExternalLeading = sbyte.Parse(attributes["externalLeading"]);
                                    fontInfo.CharOffset = float.Parse(attributes["charOffset"], nf);                                    
                                    fontInfo.SpaceWidth = sbyte.Parse(attributes["spaceWidth"]);                                    
                                }
                                else if (nodeName == "char")
                                {
                                    char charValue = attributes["value"][0];
                                    short charX = short.Parse(attributes["x"]);
                                    short charY = short.Parse(attributes["y"]);
                                    sbyte charWidth = sbyte.Parse(attributes["w"]);
                                    sbyte charHeight = sbyte.Parse(attributes["h"]);
                                    sbyte charOx = sbyte.Parse(attributes["ox"]);
                                    sbyte charOy = sbyte.Parse(attributes["oy"]);

                                    CharInfo charInfo = new CharInfo(charValue, charX, charY, charWidth, charHeight, charOx, charOy);
                                    fontInfo.AddCharInfo(charInfo);
                                }
                            }
                            break;
                    }
                }
            }            

            return fontInfo;
        }
    }
}
