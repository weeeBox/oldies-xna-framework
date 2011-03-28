using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using System.IO;

namespace ContentExtension
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to import a file from disk into the specified type, TImport.
    /// 
    /// This should be part of a Content Pipeline Extension Library project.
    /// 
    /// TODO: change the ContentImporter attribute to specify the correct file
    /// extension, display name, and default processor for this importer.
    /// </summary>
    [ContentImporter(".an", DisplayName = "AnimationImporter", DefaultProcessor = "AnimationProcessor")]
    public class AnimationImporter : ContentImporter<AnimationInfo>
    {
        public override AnimationInfo Import(string filename, ContentImporterContext context)
        {
            using (BinaryReader reader = new BinaryReader(File.OpenRead(filename)))
            {
                int framesCount = reader.ReadInt32();
                int frameRate = reader.ReadInt32();
                float width = reader.ReadSingle();
                float height = reader.ReadSingle();
                AnimationInfo info = new AnimationInfo(width, height, framesCount, frameRate);
                for (int i = 0; i < framesCount; ++i)
                {
                    Transform m;
                    m.scaleX = reader.ReadSingle();
                    m.scaleY = reader.ReadSingle();
                    m.skewX = reader.ReadSingle();
                    m.skewY = reader.ReadSingle();
                    m.transX = reader.ReadSingle();
                    m.transY = reader.ReadSingle();

                    info.AddTransform(m);
                }
                return info;
            }
        }
    }
}
