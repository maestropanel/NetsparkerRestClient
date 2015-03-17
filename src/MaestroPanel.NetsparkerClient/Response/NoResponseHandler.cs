using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MaestroPanel.NetsparkerClient.Response
{
    public class EmptyResponseHandler : IResponseHandler
    {
        public ResponseData<T> Handle<T>(Stream responseStream)
        {
            var content = new StreamReader(responseStream).ReadToEnd();

            return new ResponseData<T>
            {
                Content = content,
                Data = default(T)
            };
        }

        public ResponseData Handle(Stream responseStream)
        {
            var content = new StreamReader(responseStream).ReadToEnd();

            return new ResponseData
            {
                Content = content,
                Data = null
            };
        }
    }
}
