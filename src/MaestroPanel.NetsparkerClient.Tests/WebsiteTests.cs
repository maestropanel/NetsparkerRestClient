using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MaestroPanel.NetsparkerClient.Model;
using System.Collections.Generic;
using Moq;
using System.Net;

namespace MaestroPanel.NetsparkerClient.Tests
{
    [TestClass]
    public class WebsiteTests
    {
        [TestMethod]
        public void website_should_be_one_website()
        {
            var moqApi = new Mock<INetsparkerRestApi>();
            moqApi.Setup(x => x.Get<Page<Website>>(It.IsAny<object>(), It.Is<string>(s => s == ApiResource.Website.LIST)))
                  .Returns(new ExecuteResult<Page<Website>>
                  {
                      Data = new Page<Website>
                      {
                          List = new List<Website> 
                          {
                               new Website
                               {
                                    RootUrl = "http://demo.maestro.com",
                                    Name = "maestro"
                               }
                          }
                      },
                      Status = HttpStatusCode.OK
                  });

            var _client = new NetsparkerClient(moqApi.Object);

            var response = _client.WebsiteList();

            int expected = 1;
            int actual = response.Data.List.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void website_should_be_response_null()
        {
            var moqApi = new Mock<INetsparkerRestApi>();
            moqApi.Setup(x => x.Get<Page<Website>>(It.IsAny<object>(), It.Is<string>(s => s == ApiResource.Website.LIST)))
                  .Returns(new ExecuteResult<Page<Website>>
                  {
                      ErrorMessage = "Unauthorized",
                      Status = HttpStatusCode.Unauthorized
                  });

            var _client = new NetsparkerClient(moqApi.Object);

            var result = _client.WebsiteList();

            var expected = HttpStatusCode.Unauthorized;
            var actual = result.Status;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void website_should_be_add()
        {
            var moqApi = new Mock<INetsparkerRestApi>();
            moqApi.Setup(x => x.Post<Website>(It.IsAny<object>(), It.Is<string>(s => s == ApiResource.Website.NEW)))
                  .Returns(new ExecuteResult<Website>
                  {
                      Data = new Website
                      {
                          Id = Guid.NewGuid(),
                          Name = "demo.maestrodemo.com",
                          RootUrl = "http://demo.maestrodemo.com/",
                          Groups = new List<string> { "Default" }
                      },
                      Status = HttpStatusCode.OK
                  });

            var _client = new NetsparkerClient(moqApi.Object);

            var result = _client.WebSiteAdd(new WebsiteNewOrUpdate
            {
                Name = "demo.maestrodemo.com",
                RootUrl = "http://demo.maestrodemo.com/",
                Groups = new List<string> { "Default" }
            });

            Assert.AreNotEqual(Guid.Empty, result.Data.Id);
        }

        [TestMethod]
        public void website_should_be_update()
        {
            var moqApi = new Mock<INetsparkerRestApi>();
            moqApi.Setup(x => x.Post<Website>(It.IsAny<object>(), It.Is<string>(s => s == ApiResource.Website.UPDATE)))
                  .Returns(new ExecuteResult<Website>
                  {
                      Data = new Website
                      {
                          Id = Guid.Parse("e0e0d0fa-20f5-4760-a3bc-e1adb9146af8"),
                          Name = "demo.maestrodemo.com",
                          RootUrl = "http://demo.maestrodemo.com/",
                          Groups = new List<string> { "Default" }
                      },
                      Status = HttpStatusCode.OK
                  });

            var _client = new NetsparkerClient(moqApi.Object);

            var result = _client.WebSiteUpdate(new WebsiteNewOrUpdate
            {
                Name = "demo.maestrodemo.com",
                RootUrl = "http://demo.maestrodemo.com/",
                Groups = new List<string> { "Default" }
            });

            Guid expected = Guid.Parse("e0e0d0fa-20f5-4760-a3bc-e1adb9146af8");
            Guid actual = result.Data.Id;

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void website_should_be_delete()
        {
            var moqApi = new Mock<INetsparkerRestApi>();
            moqApi.Setup(x => x.Get<string>(It.IsAny<object>(), It.Is<string>(s => s == ApiResource.Website.DELETE)))
                  .Returns(new ExecuteResult<string>
                  {
                      Data = "Ok",
                      Status = HttpStatusCode.OK
                  });

            var _client = new NetsparkerClient(moqApi.Object);

            var result = _client.WebSiteDelete("http://demo.maestrodemo.com/");

            string expected = "Ok";
            string actual = "Ok";

            Assert.AreEqual(expected, actual);
        }
    }
}