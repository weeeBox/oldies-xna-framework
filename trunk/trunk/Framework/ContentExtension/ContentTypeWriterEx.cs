using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace ContentExtension
{
    public abstract class ContentTypeWriterEx<T> : ContentTypeWriter<T>
    {
        private string readerName;

        public ContentTypeWriterEx(string readerName)
        {
            this.readerName = readerName;
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return typeof(T).AssemblyQualifiedName;
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return readerName + ", AsapXna," + " Version=1.0.0.0, Culture=neutral";
        }
    }
}
