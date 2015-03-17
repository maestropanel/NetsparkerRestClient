using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MaestroPanel.NetsparkerClient;
using Moq;
using System.Net;
using System.IO;
using System.Text;
using MaestroPanel.NetsparkerClient.Response;

namespace MaestroPanel.HttpRequest.Tests
{
    [TestClass]
    public class ExecuterTests
    {
        [TestMethod]
        public void executer_get_should_be_response_issuccess_true()
        {
            byte[] buffer = Encoding.UTF8.GetBytes("{ IsSuccess : true }");

            var mockHttpWebRequest = new Mock<HttpWebRequest>();
            var mockHttpWebResponse = new Mock<HttpWebResponse>();
            mockHttpWebResponse.Setup(x => x.GetResponseStream()).Returns(new MemoryStream(buffer));

            mockHttpWebRequest.Setup(x => x.GetResponse()).Returns(mockHttpWebResponse.Object);

            IExecuter executer = new Executer(mockHttpWebRequest.Object, new JsonResponseHandler());

            var result = executer.Get<TestObject>();

            Assert.IsTrue(result.Data.IsSuccess);
        }

        [TestMethod]
        public void executer_get_should_be_throw_webexception()
        {
            byte[] buffer = Encoding.UTF8.GetBytes("{ IsSuccess : true }");

            byte[] exBuffer = Encoding.UTF8.GetBytes("{ Message : \"Test Error\" }");
            var mockHttpWebRequest = new Mock<HttpWebRequest>();
            var mockHttpWebResponse = new Mock<HttpWebResponse>();
            mockHttpWebResponse.Setup(x => x.StatusCode).Returns(HttpStatusCode.Unauthorized);
            mockHttpWebResponse.Setup(x => x.GetResponseStream()).Returns(new MemoryStream(exBuffer));

            mockHttpWebRequest.Setup(x => x.GetResponse())
                              .Throws(new WebException("foo", null, WebExceptionStatus.ConnectFailure, mockHttpWebResponse.Object));

            IExecuter executer = new Executer(mockHttpWebRequest.Object, new JsonResponseHandler());

            var result = executer.Get<TestObject>();

            bool actual = result.Status == HttpStatusCode.Unauthorized && result.ErrorMessage == "Test Error";

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void executer_get_should_be_throw_exception()
        {
            byte[] buffer = Encoding.UTF8.GetBytes("{ IsSuccess : true }");

            var mockHttpWebRequest = new Mock<HttpWebRequest>();

            mockHttpWebRequest.Setup(x => x.GetResponse())
                              .Throws(new Exception("bar"));

            IExecuter executer = new Executer(mockHttpWebRequest.Object, new JsonResponseHandler());

            var result = executer.Get<TestObject>();

            bool actual = result.ErrorMessage == "bar";

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void executer_post_should_be_response_ok_enum()
        {
            byte[] buffer = Encoding.UTF8.GetBytes(TestEnum.Ok.ToString());

            var mockHttpWebRequest = new Mock<HttpWebRequest>();
            var mockHttpWebResponse = new Mock<HttpWebResponse>();
            mockHttpWebResponse.Setup(x => x.GetResponseStream()).Returns(new MemoryStream(buffer));

            mockHttpWebRequest.Setup(x => x.GetResponse()).Returns(mockHttpWebResponse.Object);

            IExecuter executer = new Executer(mockHttpWebRequest.Object, new JsonResponseHandler());

            var result = executer.Post<TestEnum>(new { foo = "bar" });

            TestEnum expected = TestEnum.Ok;
            TestEnum actual = result.Data;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void executer_post_should_be_throw_exception_model_could_not_be_null()
        {
            IExecuter executer = new Executer(null, new JsonResponseHandler());

            var result = executer.Post<TestObject>(null);
        }

        [TestMethod]
        public void executer_post_should_be_throw_exception()
        {
            byte[] buffer = Encoding.UTF8.GetBytes("{ IsSuccess : true }");

            var mockHttpWebRequest = new Mock<HttpWebRequest>();
            mockHttpWebRequest.Setup(x => x.GetRequestStream()).Returns(new MemoryStream(buffer));

            var mockHttpWebResponse = new Mock<HttpWebResponse>();
            mockHttpWebResponse.Setup(x => x.GetResponseStream()).Returns(new MemoryStream(buffer));

            mockHttpWebRequest.Setup(x => x.GetResponse()).Throws(new Exception("bar"));

            IExecuter executer = new Executer(mockHttpWebRequest.Object, new JsonResponseHandler());

            var result = executer.Post<TestObject>(new { foo = "bar" });

            Assert.IsTrue(result.ErrorMessage == "bar");
        }
    }

    public class TestObject
    {
        public bool IsSuccess { get; set; }
    }

    public enum TestEnum
    {
        Ok,
        Fail
    }
}