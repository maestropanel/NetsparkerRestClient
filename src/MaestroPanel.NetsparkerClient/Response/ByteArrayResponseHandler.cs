using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace MaestroPanel.NetsparkerClient.Response
{
    public class ByteArrayResponseHandler : IResponseHandler
    {
        public ResponseData<T> Handle<T>(Stream responseStream)
        {
            return new ResponseData<T> 
            { 
                 
            };
        }

        public ResponseData Handle(Stream responseStream)
        {
            var memoryStream = new MemoryStream();

            responseStream.CopyTo(memoryStream);

            var obj = memoryStream.ToArray();

            var content = Encoding.UTF8.GetString(obj);

            return new ResponseData
            {
                Content = content,
                Data = obj
            };
        }
    }
}
