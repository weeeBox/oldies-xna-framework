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
        public override bool Save(string name, byte[] data)
        {
            IsolatedStorageFile storageFile = getStorageFile();

            // open isolated storage, and write the savefile.
            IsolatedStorageFileStream fs = null;
            fs = storageFile.OpenFile(name, System.IO.FileMode.Create);
            if (fs != null)
            {
                using (BinaryWriter writer = new BinaryWriter(fs))
                {
                    writer.Write(data.Length);
                    writer.Write(data, 0, data.Length);
                }
                fs.Close();
                return true;
            }

            return false;
        }

        public override byte[] Load(string name)
        {
            IsolatedStorageFile storageFile = getStorageFile();            
            if (!storageFile.FileExists(name))
            {
                Debug.WriteLine("Storage '" + name + "' does not exist");
                return null;
            }

            try
            {
                using (IsolatedStorageFileStream fs = storageFile.OpenFile(name, System.IO.FileMode.Open))
                {
                    if (fs == null)
                    {
                        Debug.WriteLine("Cannot open storage '" + name + "'");
                        return null;
                    }
                                        
                    using (BinaryReader reader = new BinaryReader(fs))
                    {
                        int size = reader.ReadInt32();
                        byte[] buffer = new byte[size];
                        reader.Read(buffer, 0, size);
                        return buffer;
                    }
                }
            }
            catch (IsolatedStorageException e)
            {
                Debug.WriteLine("Cannot open storage '" + name + "': " + e.Message);
                return null;
            }
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