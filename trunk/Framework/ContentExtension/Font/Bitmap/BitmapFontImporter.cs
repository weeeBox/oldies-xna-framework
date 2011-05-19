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
                                    fontInfo.InternalLeading = byte.Parse(attributes["internalLeading"]);
                                    fontInfo.Ascender = byte.Parse(attributes["ascender"]);
                                    fontInfo.Descender = byte.Parse(attributes["descender"]);
                                    fontInfo.ExternalLeading = byte.Parse(attributes["externalLeading"]);
                                    fontInfo.CharOffset = float.Parse(attributes["charOffset"], nf);                                    
                                    fontInfo.SpaceWidth = byte.Parse(attributes["spaceWidth"]);                                    
                                }
                                else if (nodeName == "char")
                                {
                                    char charValue = attributes["value"][0];
                                    short charX = short.Parse(attributes["x"]);
                                    short charY = short.Parse(attributes["y"]);
                                    byte charWidth = byte.Parse(attributes["w"]);
                                    byte charHeight = byte.Parse(attributes["h"]);
                                    byte charOx = byte.Parse(attributes["ox"]);
                                    byte charOy = byte.Parse(attributes["oy"]);

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
