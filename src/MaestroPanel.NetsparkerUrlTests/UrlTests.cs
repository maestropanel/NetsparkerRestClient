using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MaestroPanel.NetsparkerClient;

namespace MaestroPanel.NetsparkerUrlTests
{
    [TestClass]
    public class UrlTests
    {
        public const string BASE_URL = "http://foo.com/api/1.0/";

        [TestMethod]
        public void TestMethod1()
        {
            var client = new NetsparkerRestClient(new HttpRequest(BASE_URL));
        }
    }
}
