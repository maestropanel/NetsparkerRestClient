using MaestroPanel.NetsparkerClient.Response;
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

        IHttpRequest XmlResponseHandler();
        IHttpRequest JsonResponseHandler();
        IHttpRequest ByteArrayResponseHandler();
        IHttpRequest EmptyResponseHandler();

        IHttpRequest Authenticate(string accessToken);

        IExecuter Execute();
    }

    public class HttpRequest : IHttpRequest
    {
        private HttpWebRequest _request;
        private IResponseHandler _responseHandler;

        private string _accessToken;
        private string _baseUrl;
        private string _url;

        public HttpRequest(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public IHttpRequest CreateRequest(string path)
        {
            _url = UrlTools.Combine(_baseUrl, path);

            return this;
        }

        public IHttpRequest CreateRequestWithQueryString(string path, object parameters)
        {
            _url = string.Format("{0}?{1}", UrlTools.Combine(_baseUrl, path), UrlTools.ToQueryString(parameters));

            return this;
        }

        public IHttpRequest CreateRequestWithQueryString(string path, IDictionary<string, string> parameters)
        {
            _url = string.Format("{0}?{1}", UrlTools.Combine(_baseUrl, path), UrlTools.ToQueryString(parameters));

            return this;
        }

        public IHttpRequest Authenticate(string accessToken)
        {
            _accessToken = accessToken;

            return this;
        }

        public IHttpRequest XmlResponseHandler()
        {
            _responseHandler = new XmlResponseHandler();

            return this;
        }

        public IHttpRequest JsonResponseHandler()
        {
            _responseHandler = new JsonResponseHandler();

            return this;
        }

        public IHttpRequest ByteArrayResponseHandler()
        {
            _responseHandler = new ByteArrayResponseHandler();

            return this;
        }

        public IHttpRequest EmptyResponseHandler()
        {
            _responseHandler = new EmptyResponseHandler();

            return this;
        }

        public IExecuter Execute()
        {
            _request = _request = (HttpWebRequest)HttpWebRequest.Create(_url);

            if (!string.IsNullOrWhiteSpace(_accessToken))
            {
                _request.Headers["Authorization"] = "Basic " + _accessToken;
            }

            return new Executer(_request, _responseHandler);
        }

        public string Url
        {
            get { return _url; }
        }
    }
}
