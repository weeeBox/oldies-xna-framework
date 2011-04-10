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

namespace ContentExtension
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to write the specified data type into binary .xnb format.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    /// </summary>
    [ContentTypeWriter]
    public class AnimationWriter : ContentTypeWriter<AnimationBin>
    {
        protected override void Write(ContentWriter output, AnimationBin value)
        {
            byte[] data = value.Data;
            output.Write(data, 0, data.Length);
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return typeof(AnimationBin).AssemblyQualifiedName;
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "asap.resources.AnimationReader, AsapXna," + " Version=1.0.0.0, Culture=neutral";
        }
    }
}
