using MaestroPanel.NetsparkerClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaestroPanel.HttpRequest.Tests
{
    [TestClass]
    public class HttpRequestTests
    {
        [TestMethod]
        public void create_request_test()
        {
            IHttpRequest req = new MaestroPanel.NetsparkerClient.HttpRequest("http://foo.com");

            req.CreateRequest("/api");

            Assert.AreEqual(req.Url, "http://foo.com/api");
        }

        [TestMethod]
        public void create_request_with_querystring_anonymus_object_test()
        {
            IHttpRequest req = new MaestroPanel.NetsparkerClient.HttpRequest("http://foo.com/");

            req.CreateRequestWithQueryString("/api/", new { id = 1 });

            Assert.AreEqual(req.Url, "http://foo.com/api?id=1");
        }

        [TestMethod]
        public void create_request_with_querystring_dic_test()
        {
            IHttpRequest req = new MaestroPanel.NetsparkerClient.HttpRequest("http://foo.com/");

            req.CreateRequestWithQueryString("/api/", new Dictionary<string, string> 
            {
                 { "id", "1" }
            });

            Assert.AreEqual(req.Url, "http://foo.com/api?id=1");
        }
    }
}
