﻿using Newtonsoft.Json;
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
    public class ExecuteResult<T>
    {
        public T Data { get; set; }
        public HttpStatusCode Status { get; set; }
        public string ErrorMessage { get; set; }
        public string Content { get; set; }
    }

    public interface INetsparkerRestApi
    {
        ExecuteResult<T> Post<T>(object requestModel, string url);
        ExecuteResult<T> Get<T>(object parameters, string url);
    }

    public enum HttpMethod
    {
        POST,
        GET
    }

    public interface IExecuter
    {
        ExecuteResult<T> Post<T>(object model);
        ExecuteResult<T> Get<T>();
    }

    public interface IHttpRequest
    {
        IExecuter CreateRequest(string requestUrl);
        IExecuter CreateRequestWithQueryString(string requestUrl, object parameters);
        IExecuter CreateRequestWithQueryString(string requestUrl, IDictionary<string, string> parameters);

        IExecuter Authenticate(string accessToken);
        HttpWebRequest GetHttpWebRequest();
    }

    public class Executer : IExecuter
    {
        private readonly IHttpRequest _req;

        public Executer(IHttpRequest req)
        {
            _req = req;
        }

        public ExecuteResult<T> Post<T>(object model)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                var request = _req.GetHttpWebRequest();

                request.Timeout = (int)TimeSpan.FromMinutes(15).TotalMilliseconds;
                request.ReadWriteTimeout = (int)TimeSpan.FromMinutes(15).TotalMilliseconds;
                request.Method = "POST";
                request.ContentType = "application/json";

                var jsonObject = JsonConvert.SerializeObject(model);
                byte[] requestData = Encoding.UTF8.GetBytes(jsonObject);

                using (Stream requesStream = request.GetRequestStream())
                {
                    requesStream.Write(requestData, 0, requestData.Length);
                }

                using (HttpWebResponse responseStream = (HttpWebResponse)request.GetResponse())
                {
                    var responseData = responseStream.GetResponseStream();

                    if (responseData == null)
                        return new ExecuteResult<T>
                        {
                            Status = HttpStatusCode.OK
                        };

                    string content = new StreamReader(responseData).ReadToEnd();

                    var result = JsonConvert.DeserializeObject<T>(content);

                    return new ExecuteResult<T>
                    {
                        Status = HttpStatusCode.OK,
                        Data = result,
                        Content = content
                    };
                }
            }
            catch (WebException ex)
            {
                return new ExecuteResult<T>
                {
                    ErrorMessage = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new ExecuteResult<T>
                {
                    ErrorMessage = ex.Message
                };
            }
        }

        public ExecuteResult<T> Get<T>()
        {
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            try
            {
                var request = _req.GetHttpWebRequest();

                request.Timeout = (int)TimeSpan.FromMinutes(15).TotalMilliseconds;
                request.ReadWriteTimeout = (int)TimeSpan.FromMinutes(15).TotalMilliseconds;
                request.Method = "GET";

                request.ContentType = "application/x-www-form-urlencoded";

                using (HttpWebResponse responseStream = (HttpWebResponse)request.GetResponse())
                {
                    var responseData = responseStream.GetResponseStream();

                    if (responseData == null)
                        return new ExecuteResult<T>
                        {
                            Status = HttpStatusCode.OK
                        };

                    string content = new StreamReader(responseData).ReadToEnd();

                    var result = JsonConvert.DeserializeObject<T>(content);

                    return new ExecuteResult<T>
                    {
                        Data = result,
                        Status = responseStream.StatusCode,
                        Content = content
                    };
                }
            }
            catch (WebException ex)
            {
                var res = (HttpWebResponse)ex.Response;

                return new ExecuteResult<T>
                {
                    ErrorMessage = ex.Message,
                    Status = res.StatusCode
                };
            }
            catch (Exception ex)
            {
                return new ExecuteResult<T>
                {
                    ErrorMessage = ex.Message
                };
            }
        }
    }

    public static class UrlTools
    {
        public static string ToQueryString(IDictionary<string, string> parameters, string separator = ",")
        {
            // Get names for all IEnumerable properties (excl. string)
            var propertyNames = parameters
                .Where(x => !(x.Value is string) && x.Value is IEnumerable)
                .Select(x => x.Key)
                .ToList();

            // Concat all IEnumerable properties into a comma separated string
            foreach (var key in propertyNames)
            {
                var valueType = parameters[key].GetType();
                var valueElemType = valueType.IsGenericType
                                        ? valueType.GetGenericArguments()[0]
                                        : valueType.GetElementType();
                if (valueElemType.IsPrimitive || valueElemType == typeof(string))
                {
                    var enumerable = parameters[key] as IEnumerable;
                    parameters[key] = string.Join(separator, enumerable.Cast<object>());
                }
            }

            // Concat all key/value pairs into a string separated by ampersand
            return string.Join("&", parameters
                .Select(x => string.Concat(
                    Uri.EscapeDataString(x.Key), "=",
                    Uri.EscapeDataString(x.Value.ToString()))));
        }

        public static string ToQueryString(object request, string separator = ",")
        {
            if (request == null)
                throw new ArgumentNullException("request");

            // Get all properties on the object
            var properties = request.GetType().GetProperties()
                .Where(x => x.CanRead)
                .Where(x => x.GetValue(request, null) != null)
                .ToDictionary(x => x.Name, x => x.GetValue(request, null));

            // Get names for all IEnumerable properties (excl. string)
            var propertyNames = properties
                .Where(x => !(x.Value is string) && x.Value is IEnumerable)
                .Select(x => x.Key)
                .ToList();

            // Concat all IEnumerable properties into a comma separated string
            foreach (var key in propertyNames)
            {
                var valueType = properties[key].GetType();
                var valueElemType = valueType.IsGenericType
                                        ? valueType.GetGenericArguments()[0]
                                        : valueType.GetElementType();
                if (valueElemType.IsPrimitive || valueElemType == typeof(string))
                {
                    var enumerable = properties[key] as IEnumerable;
                    properties[key] = string.Join(separator, enumerable.Cast<object>());
                }
            }

            // Concat all key/value pairs into a string separated by ampersand
            return string.Join("&", properties
                .Select(x => string.Concat(
                    Uri.EscapeDataString(x.Key), "=",
                    Uri.EscapeDataString(x.Value.ToString()))));
        }
    }

    public class HttpRequest : IHttpRequest
    {
        private HttpWebRequest _request;

        private string _baseUrl;

        public HttpRequest()
        {

        }

        public HttpRequest(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public IExecuter CreateRequest(string requestUrl)
        {
            _request = HttpWebRequest.CreateHttp(requestUrl);

            return new Executer(this);
        }

        public IExecuter CreateRequestWithQueryString(string requestUrl, object parameters)
        {
            _request = HttpWebRequest.CreateHttp(_baseUrl + requestUrl + "?" + UrlTools.ToQueryString(parameters));

            return new Executer(this);
        }

        public IExecuter CreateRequestWithQueryString(string requestUrl, IDictionary<string, string> parameters)
        {
            _request = HttpWebRequest.CreateHttp(_baseUrl + requestUrl + "?" + UrlTools.ToQueryString(parameters));

            return new Executer(this);
        }

        public IExecuter Authenticate(string accessToken)
        {
            if (_request == null)
            {
                throw new Exception("Before CreateRequest call");
            }

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                _request.Headers["Authorization"] = "Basic " + accessToken;
            }

            return new Executer(this);
        }

        public HttpWebRequest GetHttpWebRequest()
        {
            return _request;
        }
    }

    public class NetsparkerRestApi : INetsparkerRestApi
    {
        public string BaseURL { get; set; }
        public string AccessToken { get; set; }

        public NetsparkerRestApi()
        {

        }

        public NetsparkerRestApi(string baseUrl, string accessToken)
        {
            BaseURL = baseUrl;
            AccessToken = accessToken;
        }

        public ExecuteResult<T> Post<T>(object requestModel, string url)
        {
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(BaseURL + url);

                if (!string.IsNullOrWhiteSpace(AccessToken))
                {
                    request.Headers["Authorization"] = "Basic " + AccessToken;
                }

                request.Timeout = (int)TimeSpan.FromMinutes(15).TotalMilliseconds;
                request.ReadWriteTimeout = (int)TimeSpan.FromMinutes(15).TotalMilliseconds;
                request.Method = "POST";
                request.ContentType = "application/json";

                var jsonObject = JsonConvert.SerializeObject(requestModel);
                byte[] requestData = Encoding.UTF8.GetBytes(jsonObject);

                using (Stream requesStream = request.GetRequestStream())
                {
                    requesStream.Write(requestData, 0, requestData.Length);
                }

                using (HttpWebResponse responseStream = (HttpWebResponse)request.GetResponse())
                {
                    var responseData = responseStream.GetResponseStream();

                    if (responseData == null)
                        return new ExecuteResult<T>
                        {
                            Status = HttpStatusCode.OK
                        };

                    string content = new StreamReader(responseData).ReadToEnd();

                    var result = JsonConvert.DeserializeObject<T>(content);

                    return new ExecuteResult<T>
                    {
                        Status = HttpStatusCode.OK,
                        Data = result,
                        Content = content
                    };
                }
            }
            catch (WebException ex)
            {
                return new ExecuteResult<T>
                {
                    ErrorMessage = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new ExecuteResult<T>
                {
                    ErrorMessage = ex.Message
                };
            }
        }

        public ExecuteResult<T> Get<T>(object parameters, string url)
        {
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            try
            {
                string urlWithParameters = BaseURL + url + "?" + ToQueryString(parameters);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlWithParameters);

                if (!string.IsNullOrWhiteSpace(AccessToken))
                {
                    request.Headers["Authorization"] = "Basic " + AccessToken;
                }

                request.Timeout = (int)TimeSpan.FromMinutes(15).TotalMilliseconds;
                request.ReadWriteTimeout = (int)TimeSpan.FromMinutes(15).TotalMilliseconds;
                request.Method = "GET";

                request.ContentType = "application/x-www-form-urlencoded";

                using (HttpWebResponse responseStream = (HttpWebResponse)request.GetResponse())
                {
                    var responseData = responseStream.GetResponseStream();

                    if (responseData == null)
                        return new ExecuteResult<T>
                        {
                            Status = HttpStatusCode.OK
                        };

                    string content = new StreamReader(responseData).ReadToEnd();

                    var result = JsonConvert.DeserializeObject<T>(content);

                    return new ExecuteResult<T>
                    {
                        Data = result,
                        Status = responseStream.StatusCode,
                        Content = content
                    };
                }
            }
            catch (WebException ex)
            {
                var res = (HttpWebResponse)ex.Response;

                return new ExecuteResult<T>
                {
                    ErrorMessage = ex.Message,
                    Status = res.StatusCode
                };
            }
            catch (Exception ex)
            {
                return new ExecuteResult<T>
                {
                    ErrorMessage = ex.Message
                };
            }
        }

        private static string ToQueryString(object request, string separator = ",")
        {
            if (request == null)
                throw new ArgumentNullException("request");

            // Get all properties on the object
            var properties = request.GetType().GetProperties()
                .Where(x => x.CanRead)
                .Where(x => x.GetValue(request, null) != null)
                .ToDictionary(x => x.Name, x => x.GetValue(request, null));

            // Get names for all IEnumerable properties (excl. string)
            var propertyNames = properties
                .Where(x => !(x.Value is string) && x.Value is IEnumerable)
                .Select(x => x.Key)
                .ToList();

            // Concat all IEnumerable properties into a comma separated string
            foreach (var key in propertyNames)
            {
                var valueType = properties[key].GetType();
                var valueElemType = valueType.IsGenericType
                                        ? valueType.GetGenericArguments()[0]
                                        : valueType.GetElementType();
                if (valueElemType.IsPrimitive || valueElemType == typeof(string))
                {
                    var enumerable = properties[key] as IEnumerable;
                    properties[key] = string.Join(separator, enumerable.Cast<object>());
                }
            }

            // Concat all key/value pairs into a string separated by ampersand
            return string.Join("&", properties
                .Select(x => string.Concat(
                    Uri.EscapeDataString(x.Key), "=",
                    Uri.EscapeDataString(x.Value.ToString()))));
        }
    }
}
