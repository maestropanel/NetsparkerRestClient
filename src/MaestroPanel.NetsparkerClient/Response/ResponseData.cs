using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaestroPanel.NetsparkerClient.Response
{
    public class ResponseData<T> : Response
    {
        public T Data { get; set; }
    }

    public class ResponseData : Response
    {
        public object Data { get; set; }
    }

    public class Response
    {
        public string Content { get; set; }
    }
}
