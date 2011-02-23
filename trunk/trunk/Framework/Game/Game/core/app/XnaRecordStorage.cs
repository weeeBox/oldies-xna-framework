using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using asap.rms;
using System.IO.IsolatedStorage;
using Microsoft.Xna.Framework;
using System.IO;
using System.Diagnostics;

namespace Flipstones2.app
{
    public class XnaRecordStorage : RecordStorage
    {
        public override bool Save(string name, sbyte[] data)
        {
            IsolatedStorageFile storageFile = getStorageFile();

            // open isolated storage, and write the savefile.
            IsolatedStorageFileStream fs = null;
            fs = storageFile.OpenFile(name, System.IO.FileMode.Create);
            if (fs != null)
            {
                using (BinaryWriter writer = new BinaryWriter(fs))
                {
                    byte[] buffer = new byte[data.Length];
                    for (int i = 0; i < data.Length; ++i)
                    {
                        buffer[i] = (byte)data[i];
                    }

                    writer.Write(buffer.Length);
                    writer.Write(buffer, 0, buffer.Length);
                }
                fs.Close();
                return true;
            }

            return false;
        }

        public override sbyte[] Load(string name)
        {
            IsolatedStorageFile storageFile = getStorageFile();            
            if (!storageFile.FileExists(name))
            {
                Debug.WriteLine("Storage '" + name + "' does not exist");
                return null;
            }
            IsolatedStorageFileStream fs = null;
            try
            {
                fs = storageFile.OpenFile(name, System.IO.FileMode.Open);                    
            }
            catch (IsolatedStorageException e)
            {
                Debug.WriteLine("Cannot open storage '" + name + "': " + e.Message);
                return null;
            }

            if (fs == null)
            {
                Debug.WriteLine("Cannot open storage '" + name + "'");
                return null;
            }

            sbyte[] data = null;
            using (BinaryReader reader = new BinaryReader(fs))
            {
                int size = reader.ReadInt32();
                byte[] buffer = new byte[size];
                reader.Read(buffer, 0, size);
                data = new sbyte[size];
                for (int i = 0; i < size; ++i)
                {
                    data[i] = (sbyte)buffer[i];
                }
            }

            fs.Close();

            return data;           
        }

        private IsolatedStorageFile getStorageFile()
        {
#if WINDOWS
            return IsolatedStorageFile.GetUserStoreForDomain();
#else
            return IsolatedStorageFile.GetUserStoreForApplication();
#endif
        }
    }    
}