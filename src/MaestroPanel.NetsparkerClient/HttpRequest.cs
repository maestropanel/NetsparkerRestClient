using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MaestroPanel.NetsparkerClient
{
    public interface IHttpRequest
    {
        string Url { get; }

        IHttpRequest CreateRequest(string path);
        IHttpRequest CreateRequestWithQueryString(string path, object parameters);
        IHttpRequest CreateRequestWithQueryString(string path, IDictionary<string, string> parameters);

        IHttpRequest Authenticate(string accessToken);

        IExecuter Execute();
    }

    public class HttpRequest : IHttpRequest
    {
        private HttpWebRequest _request;

        private string _accessToken;
        private string _url;

        public HttpRequest(string baseUrl)
        {
            _url = baseUrl;
        }

        public IHttpRequest CreateRequest(string path)
        {
            _url = UrlTools.Combine(_url, path);

            return this;
        }

        public IHttpRequest CreateRequestWithQueryString(string path, object parameters)
        {
            _url = string.Format("{0}?{1}", UrlTools.Combine(_url, path), UrlTools.ToQueryString(parameters));

            return this;
        }

        public IHttpRequest CreateRequestWithQueryString(string path, IDictionary<string, string> parameters)
        {
            _url = string.Format("{0}?{1}", UrlTools.Combine(_url, path), UrlTools.ToQueryString(parameters));

            return this;
        }

        public IHttpRequest Authenticate(string accessToken)
        {
            _accessToken = accessToken;

            return this;
        }

        public IExecuter Execute()
        {
            _request = _request = HttpWebRequest.CreateHttp(_url);

            if (!string.IsNullOrWhiteSpace(_accessToken))
            {
                _request.Headers["Authorization"] = "Basic " + _accessToken;
            }

            return new Executer(_request);
        }

        public string Url
        {
            get { return _url; }
        }
    }
}
