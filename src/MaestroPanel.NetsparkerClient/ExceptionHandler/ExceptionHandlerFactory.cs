using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace MaestroPanel.NetsparkerClient.ExceptionHandler
{
    public interface IExceptionHandlerFactory
    {
        IExceptionHandler Create(Exception ex);
    }

    public class ExceptionHandlerFactory:IExceptionHandlerFactory
    {
        public IExceptionHandler Create(Exception ex)
        {
            if (ex is WebException)
            {
                return new WebExceptionHandler();
            }

            return new BaseExceptionHandler();
        }
    }
}
