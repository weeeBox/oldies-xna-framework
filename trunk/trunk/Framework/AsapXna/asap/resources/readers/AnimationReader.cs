using System.Collections.Generic;
using System.IO;
using asap.anim;
using asap.graphics;
using Microsoft.Xna.Framework.Content;
using swiff.com.jswiff;
using swiff.com.jswiff.listeners;
using swiff.com.jswiff.swfrecords.tags;

namespace asap.resources.readers
{
    public class AnimationReader : ContentTypeReader<SwfMovie>
    {
        protected override SwfMovie Read(ContentReader input, SwfMovie existingInstance)
        {
            SWFDocument doc = readDocument(input.BaseStream);            
            List<Tag> tags = doc.GetTags();

            SwfMovie movie = new SwfMovie(doc.GetFrameCount(), doc.GetFrameRate());            

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
                if (tag is DefinitionTag)
                {
                    movie.AddDefinitionTag((DefinitionTag)tag);
                }                
                else
                {                    
                    movie.AddControlTag(tag);
                }
            }   
        
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
