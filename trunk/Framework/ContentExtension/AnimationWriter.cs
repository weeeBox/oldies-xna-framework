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
    public class AnimationWriter : ContentTypeWriter<AnimationInfo>
    {
        protected override void Write(ContentWriter output, AnimationInfo value)
        {
            Debug.WriteLine("Export animation");

            output.Write(value.FramesCount);
		    output.Write(value.FrameRate);
		    output.Write(value.Width);
		    output.Write(value.Height);
		    foreach (Transform m in value.Transforms)
		    {
			    output.Write(m.scaleX);
                output.Write(m.skewY);
                output.Write(m.skewX);
			    output.Write(m.scaleY);			    
			    output.Write(m.transX);
			    output.Write(m.transY);
		    }
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return typeof(AnimationInfo).AssemblyQualifiedName;
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "Game.core.res.AniReader, Game," + " Version=1.0.0.0, Culture=neutral";
        }
    }
}
