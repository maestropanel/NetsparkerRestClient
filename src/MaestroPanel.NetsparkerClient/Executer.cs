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

        public Executer(HttpWebRequest request)
        {
            _request = request;
        }

        public ExecuteResult<T> Post<T>(object model)
        {
            if (model == null)
            {
                throw new Exception("model could not be null");
            }

            try
            {
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

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

                using (HttpWebResponse responseStream = (HttpWebResponse)_request.GetResponse())
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
                using (var errRes = (HttpWebResponse)ex.Response)
                {
                    using (var reader = new StreamReader(errRes.GetResponseStream()))
                    {
                        string error = reader.ReadToEnd();

                        return new ExecuteResult<T>
                        {
                            Status = errRes.StatusCode,
                            ErrorMessage = ex.Message,
                            Content = error
                        };
                    }
                }
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
                _request.Timeout = (int)TimeSpan.FromMinutes(15).TotalMilliseconds;
                _request.ReadWriteTimeout = (int)TimeSpan.FromMinutes(15).TotalMilliseconds;
                _request.Method = "GET";
                _request.ContentType = "application/x-www-form-urlencoded";

                using (HttpWebResponse responseStream = (HttpWebResponse)_request.GetResponse())
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
                using (var errRes = (HttpWebResponse)ex.Response)
                {
                    using (var reader = new StreamReader(errRes.GetResponseStream()))
                    {
                        string error = reader.ReadToEnd();

                        return new ExecuteResult<T>
                        {
                            Status = errRes.StatusCode,
                            ErrorMessage = ex.Message,
                            Content = error
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new ExecuteResult<T>
                {
                    ErrorMessage = ex.Message
                };
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
                ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

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

                using (HttpWebResponse responseStream = (HttpWebResponse)_request.GetResponse())
                {
                    responseStream.GetResponseStream();

                    return new ExecuteResult { Status = HttpStatusCode.OK };
                }
            }
            catch (WebException ex)
            {
                var res = (HttpWebResponse)ex.Response;

                return new ExecuteResult
                {
                    Status = res.StatusCode,
                    ErrorMessage = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new ExecuteResult
                {
                    ErrorMessage = ex.Message
                };
            }
        }

        public ExecuteResult Get()
        {
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            try
            {
                _request.Timeout = (int)TimeSpan.FromMinutes(15).TotalMilliseconds;
                _request.ReadWriteTimeout = (int)TimeSpan.FromMinutes(15).TotalMilliseconds;
                _request.Method = "GET";
                _request.ContentType = "application/x-www-form-urlencoded";

                using (HttpWebResponse responseStream = (HttpWebResponse)_request.GetResponse())
                {
                    var responseData = responseStream.GetResponseStream();

                    if (responseData == null)
                        return new ExecuteResult
                        {
                            Status = HttpStatusCode.OK
                        };

                    string content = new StreamReader(responseData).ReadToEnd();

                    return new ExecuteResult
                    {
                        Status = responseStream.StatusCode,
                        Content = content
                    };
                }
            }
            catch (WebException ex)
            {
                using (var errRes = (HttpWebResponse)ex.Response)
                {
                    using (var reader = new StreamReader(errRes.GetResponseStream()))
                    {
                        string error = reader.ReadToEnd();

                        return new ExecuteResult
                        {
                            Status = errRes.StatusCode,
                            ErrorMessage = ex.Message,
                            Content = error
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new ExecuteResult
                {
                    ErrorMessage = ex.Message
                };
            }
        }
    }

}
