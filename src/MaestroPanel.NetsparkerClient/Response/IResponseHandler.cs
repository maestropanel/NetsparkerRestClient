using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace MaestroPanel.NetsparkerClient.Response
{
    public interface IResponseHandler
    {
        ResponseData<T> Handle<T>(Stream responseStream);
        ResponseData Handle(Stream responseStream);
    }
}
