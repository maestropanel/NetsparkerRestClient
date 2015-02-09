using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MaestroPanel.NetsparkerClient
{
    public class ExecuteResult<T> : ExecuteResult
    {
        public T Data { get; set; }
        public string Content { get; set; }
    }

    public class ExecuteResult
    {
        public HttpStatusCode Status { get; set; }
        public string ErrorMessage { get; set; }
    }
}
