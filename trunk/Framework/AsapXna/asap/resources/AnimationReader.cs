using System;
using asap.anim;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;
using swiff.com.jswiff;
using swiff.com.jswiff.listeners;
using System.Collections.Generic;
using swiff.com.jswiff.swfrecords.tags;
using System.IO;

namespace Game.core.res
{
    public enum ResType
    {
        BITMAP,
        MOVIE_CLIP,
        SHAPE
    }

    public class AnimationReader : ContentTypeReader<SwfMovie>
    {
        protected override SwfMovie Read(ContentReader input, SwfMovie existingInstance)
        {
            SWFDocument doc = readDocument(input.BaseStream);            
            List<Tag> tags = doc.GetTags();

            SwfMovie movie = new SwfMovie(doc.GetFrameCount(), doc.GetFrameRate());
            movie.SetTags(tags);

            foreach (Tag tag in tags)
            {
                
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
