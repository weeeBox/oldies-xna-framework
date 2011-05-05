using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using System.Diagnostics;

namespace ContentExtension.Animation
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to write the specified data type into binary .xnb format.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    /// </summary>
    [ContentTypeWriter]
    public class AnimationWriter : ContentTypeWriterEx<AnimationBin>
    {
        public AnimationWriter()
            : base("asap.resources.readers.AnimationReader")
        {
        }

        protected override void Write(ContentWriter output, AnimationBin value)
        {
            byte[] data = value.Data;
            output.Write(data, 0, data.Length);
        }       
    }
}
