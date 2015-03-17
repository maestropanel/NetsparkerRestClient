using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MaestroPanel.NetsparkerClient.Response
{
    public class JsonResponseHandler : IResponseHandler
    {
        public ResponseData<T> Handle<T>(Stream responseStream)
        {
            var content = new StreamReader(responseStream).ReadToEnd();

            var obj = JsonConvert.DeserializeObject<T>(content);

            return new ResponseData<T>
            {
                Content = content,
                Data = obj
            };
        }

        public ResponseData Handle(Stream responseStream)
        {
            var content = new StreamReader(responseStream).ReadToEnd();

            var obj = JsonConvert.DeserializeObject(content);

            return new ResponseData
            {
                Content = content,
                Data = obj
            };
        }
    }
}
