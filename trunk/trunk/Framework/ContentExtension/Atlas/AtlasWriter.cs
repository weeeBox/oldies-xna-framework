using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace ContentExtension.Atlas
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to write the specified data type into binary .xnb format.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    /// </summary>
    [ContentTypeWriter]
    public class AtlasWriter : ContentTypeWriterEx<AtlasInfo>
    {
        public AtlasWriter()
            : base("asap.resources.readers.AtlasReader")
        {
        }

        protected override void Write(ContentWriter output, AtlasInfo value)
        {
            output.Write(value.Filename);
            output.Write(value.ImagesCount);

            List<AtlasImageInfo> images = value.Images;
            foreach (AtlasImageInfo e in images)
            {                
                output.Write(e.x);
                output.Write(e.y);
                output.Write(e.w);
                output.Write(e.h);
                output.Write(e.ox);
                output.Write(e.oy);
            }
        }       
    }
}
