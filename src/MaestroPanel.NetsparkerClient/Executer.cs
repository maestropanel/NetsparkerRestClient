using MaestroPanel.NetsparkerClient.ApiModels;
using MaestroPanel.NetsparkerClient.ExceptionHandler;
using MaestroPanel.NetsparkerClient.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MaestroPanel.NetsparkerClient
{

    public interface IExecuter
    {
        ExecuteResult Post(object model);
        ExecuteResult<T> Post<T>(object model);
        ExecuteResult<T> Get<T>();
        ExecuteResult Get();
    }

    public class Executer : IExecuter
    {
        private readonly HttpWebRequest _request;
        private readonly IResponseHandler _responseHandler;

        public Executer(HttpWebRequest request, IResponseHandler responseHandler)
        {
            _request = request;
            _responseHandler = responseHandler;
        }

        public ExecuteResult<T> Post<T>(object model)
        {
            if (model == null)
            {
                throw new Exception("model could not be null");
            }

            try
            {
                _request.Timeout = (int)TimeSpan.FromMinutes(15).TotalMilliseconds;
                _request.ReadWriteTimeout = (int)TimeSpan.FromMinutes(15).TotalMilliseconds;
                _request.Method = "POST";
                _request.ContentType = "application/json";

                var jsonObject = JsonConvert.SerializeObject(model);
                byte[] requestData = Encoding.UTF8.GetBytes(jsonObject);

                using (Stream requesStream = _request.GetRequestStream())
                {
                    requesStream.Write(requestData, 0, requestData.Length);
                }

                using (HttpWebResponse response = (HttpWebResponse)_request.GetResponse())
                {
                    var responseStream = response.GetResponseStream();

                    if (responseStream == null)
                        return new ExecuteResult<T>
                        {
                            Status = response.StatusCode
                        };

                    var responseData = _responseHandler.Handle<T>(responseStream);

                    return new ExecuteResult<T>
                    {
                        Status = HttpStatusCode.OK,
                        Data = responseData.Data,
                        Content = responseData.Content
                    };
                }
            }
            catch (Exception ex)
            {
                var exceptionHandlerFactory = new ExceptionHandlerFactory();

                var exceptionHandler = exceptionHandlerFactory.Create(ex);

                return exceptionHandler.Handle<T>(ex);
            }
        }

        public ExecuteResult<T> Get<T>()
        {
            try
            {
                _request.Timeout = (int)TimeSpan.FromMinutes(15).TotalMilliseconds;
                _request.ReadWriteTimeout = (int)TimeSpan.FromMinutes(15).TotalMilliseconds;
                _request.Method = "GET";
                _request.ContentType = "application/x-www-form-urlencoded";

                using (HttpWebResponse response = (HttpWebResponse)_request.GetResponse())
                {
                    var responseStream = response.GetResponseStream();

                    if (responseStream == null)
                        return new ExecuteResult<T>
                        {
                            Status = response.StatusCode
                        };


                    var responseData = _responseHandler.Handle<T>(responseStream);

                    return new ExecuteResult<T>
                    {
                        Data = responseData.Data,
                        Status = response.StatusCode,
                        Content = responseData.Content
                    };
                }
            }
            catch (Exception ex)
            {
                var exceptionHandlerFactory = new ExceptionHandlerFactory();

                var exceptionHandler = exceptionHandlerFactory.Create(ex);

                return exceptionHandler.Handle<T>(ex);
            }
        }

        public ExecuteResult Post(object model)
        {
            if (model == null)
            {
                throw new Exception("model could not be null");
            }

            try
            {
                _request.Timeout = (int)TimeSpan.FromMinutes(15).TotalMilliseconds;
                _request.ReadWriteTimeout = (int)TimeSpan.FromMinutes(15).TotalMilliseconds;
                _request.Method = "POST";
                _request.ContentType = "application/json";

                string requestContent = JsonConvert.SerializeObject(model);
                byte[] requestData = Encoding.UTF8.GetBytes(requestContent);

                using (Stream requesStream = _request.GetRequestStream())
                {
                    requesStream.Write(requestData, 0, requestData.Length);
                }

                using (HttpWebResponse response = (HttpWebResponse)_request.GetResponse())
                {
                    var responseStream = response.GetResponseStream();

                    if (responseStream == null)
                        return new ExecuteResult
                        {
                            Status = response.StatusCode
                        };

                    var responseData = _responseHandler.Handle(responseStream);

                    return new ExecuteResult
                    {
                        Status = response.StatusCode,
                        Data = responseData.Data,
                        Content = responseData.Content
                    };
                }
            }
            catch (Exception ex)
            {
                var exceptionHandlerFactory = new ExceptionHandlerFactory();

                var exceptionHandler = exceptionHandlerFactory.Create(ex);

                return exceptionHandler.Handle(ex);
            }
        }

        public ExecuteResult Get()
        {
            try
            {
                _request.Timeout = (int)TimeSpan.FromMinutes(15).TotalMilliseconds;
                _request.ReadWriteTimeout = (int)TimeSpan.FromMinutes(15).TotalMilliseconds;
                _request.Method = "GET";
                _request.ContentType = "application/x-www-form-urlencoded";

                using (HttpWebResponse response = (HttpWebResponse)_request.GetResponse())
                {
                    var responseStream = response.GetResponseStream();

                    if (responseStream == null)
                        return new ExecuteResult
                        {
                            Status = response.StatusCode
                        };

                    var responseData = _responseHandler.Handle(responseStream);

                    return new ExecuteResult
                    {
                        Status = response.StatusCode,
                        Content = responseData.Content,
                        Data = responseData.Data
                    };
                }
            }
            catch (Exception ex)
            {
                var exceptionHandlerFactory = new ExceptionHandlerFactory();

                var exceptionHandler = exceptionHandlerFactory.Create(ex);

                return exceptionHandler.Handle(ex);
            }
        }
    }
}
