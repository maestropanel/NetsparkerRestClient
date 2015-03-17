using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MaestroPanel.NetsparkerClient.Response
{
    public class ByteArrayResponseHandler : IResponseHandler
    {
        public ResponseData<T> Handle<T>(Stream responseStream)
        {
            var memStream = new MemoryStream();

            responseStream.CopyTo(memStream);

            var obj = memStream.ToArray();

            GCHandle handle = GCHandle.Alloc(obj, GCHandleType.Pinned);

            T structure = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));

            handle.Free();

            var content = Encoding.UTF8.GetString(obj);

            return new ResponseData<T>
            {
                Content = content,
                Data = structure
            };
        }

        public ResponseData Handle(Stream responseStream)
        {
            var memStream = new MemoryStream();

            responseStream.CopyTo(memStream);

            var obj = memStream.ToArray();

            var content = Encoding.UTF8.GetString(obj);

            return new ResponseData
            {
                Content = content,
                Data = obj
            };
        }
    }
}
