using System.Collections.Generic;
using System.IO;
using asap.anim;
using asap.graphics;
using Microsoft.Xna.Framework.Content;
using swiff.com.jswiff;
using swiff.com.jswiff.listeners;
using swiff.com.jswiff.swfrecords.tags;
using System.Diagnostics;
using System;

namespace asap.resources.readers
{
    public class AnimationReader : ContentTypeReader<SwfMovie>
    {
        protected override SwfMovie Read(ContentReader input, SwfMovie existingInstance)
        {
            SWFDocument doc = readDocument(input.BaseStream);
            List<Tag> tags = doc.GetTags();
            List<Tag> nextFrameTags = new List<Tag>();
            int frameIndex = 0;

            SwfMovie movie = new SwfMovie(doc);

            foreach (Tag tag in tags)
            {
                int code = tag.GetCode();
                if (code == TagConstants.DEFINE_ATLAS)
                {
                    DefineAtlas defineAtlas = (DefineAtlas)tag;
                    string textureName = defineAtlas.GetTextureName();
                    GameTexture texture = ResFactory.GetInstance().LoadImage(textureName);

                    PartsetInfo[] parts = defineAtlas.Parts;
                    SwfPartset partset = new SwfPartset(texture, parts.Length);
                    for (int partIndex = 0; partIndex < parts.Length; ++partIndex)
                    {
                        PartsetInfo info = parts[partIndex];
                        partset.SetPart(partIndex, info.x, info.y, info.width, info.height);
                    }

                    movie.AddPartset(partset);
                }
                else if (tag is DefinitionTag)
                {
                    switch (code)
                    {
                        case TagConstants.DEFINE_PACKED_IMAGE:
                        case TagConstants.DEFINE_SPRITE:
                        case TagConstants.DEFINE_SHAPE:
                        case TagConstants.DEFINE_SHAPE_2:
                        case TagConstants.DEFINE_SHAPE_3:
                        case TagConstants.DEFINE_SHAPE_4:
                            {
                                movie.AddDefinitionTag((DefinitionTag)tag);
                                break;
                            }
                        default:
                            throw new NotImplementedException(tag.ToString());
                    }

                }
                else if (code == TagConstants.SYMBOL_CLASS)
                {
                    SymbolClass symbolClass = (SymbolClass)tag;
                    Dictionary<string, int> symbols = symbolClass.GetSymbols();
                    foreach (KeyValuePair<string, int> symbol in symbols)
                    {
                        string name = symbol.Key;
                        int characterId = symbol.Value;
                        if (characterId == 0)
                        {
                            Debug.WriteLine("Ignore main timeline: " + name);
                            continue;
                        }
                        Debug.WriteLine("Add symbol: " + name + "=" + characterId);
                        movie.AddNamedSymbol(name, characterId);
                    }
                }
                else if (code == TagConstants.SHOW_FRAME || code == TagConstants.END)
                {
                    movie.SetFrameTags(nextFrameTags, frameIndex);
                    nextFrameTags.Clear();
                    frameIndex++;
                }
                else
                {
                    nextFrameTags.Add(tag);
                }
            }

            Debug.Assert(frameIndex == doc.GetFrameCount());

            return movie;
        }

        private SWFDocument readDocument(Stream stream)
        {
            SWFDocumentReader docReader = new SWFDocumentReader();
            SWFReader reader = new SWFReader(stream);
            reader.AddListener(docReader);
            reader.Read();
            return docReader.GetDocument();
        }
    }
}
