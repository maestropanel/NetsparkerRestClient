using MaestroPanel.NetsparkerClient.ApiModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace MaestroPanel.NetsparkerClient.ExceptionHandler
{
    public interface IExceptionHandler
    {
        ExecuteResult Handle(Exception ex);
        ExecuteResult<T> Handle<T>(Exception ex);
    }

    public class WebExceptionHandler : IExceptionHandler
    {
        public ExecuteResult Handle(Exception ex)
        {
            var webEx = (WebException)ex;

            using (var errRes = (HttpWebResponse)webEx.Response)
            {
                using (var reader = new StreamReader(errRes.GetResponseStream()))
                {
                    string error = reader.ReadToEnd();

                    try
                    {
                        return new ExecuteResult
                        {
                            Status = errRes.StatusCode,
                            ErrorMessage = JsonConvert.DeserializeObject<ErrorModel>(ex.Message).Message,
                            Content = error
                        };
                    }
                    catch
                    {
                        return new ExecuteResult
                        {
                            Status = errRes.StatusCode,
                            ErrorMessage = error,
                            Content = error
                        };
                    }
                }
            }
        }


        public ExecuteResult<T> Handle<T>(Exception ex)
        {
            var webEx = (WebException)ex;

            using (var errRes = (HttpWebResponse)webEx.Response)
            {
                using (var reader = new StreamReader(errRes.GetResponseStream()))
                {
                    string error = reader.ReadToEnd();

                    try
                    {
                        return new ExecuteResult<T>
                        {
                            Status = errRes.StatusCode,
                            ErrorMessage = JsonConvert.DeserializeObject<ErrorModel>(error).Message,
                            Content = error
                        };
                    }
                    catch
                    {
                        return new ExecuteResult<T>
                        {
                            Status = errRes.StatusCode,
                            ErrorMessage = error,
                            Content = error
                        };
                    }
                }
            }
        }
    }

    public class BaseExceptionHandler : IExceptionHandler
    {
        public ExecuteResult Handle(Exception ex)
        {
            return new ExecuteResult
            {
                ErrorMessage = ex.Message
            };
        }


        public ExecuteResult<T> Handle<T>(Exception ex)
        {
            return new ExecuteResult<T>
            {
                ErrorMessage = ex.Message
            };
        }
    }
}
