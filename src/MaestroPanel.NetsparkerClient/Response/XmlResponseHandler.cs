using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MaestroPanel.NetsparkerClient.Response
{
    public class XmlResponseHandler : IResponseHandler
    {
        public ResponseData<T> Handle<T>(Stream responseStream)
        {
            var content = new StreamReader(responseStream).ReadToEnd();

            var deserializer = new XmlSerializer(typeof(T));

            var stringReader = new StringReader(content);

            var obj = (T)deserializer.Deserialize(stringReader);

            return new ResponseData<T>
            {
                Content = content,
                Data = obj
            };
        }

        public ResponseData Handle(Stream responseStream)
        {
            var content = new StreamReader(responseStream).ReadToEnd();

            return new ResponseData
            {
                Content = content,
                Data = content
            };
        }
    }
}
