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
            moqApi.Setup(x => x.Get<PagedListApiResult<WebsiteApiModel>>(It.IsAny<object>(), It.Is<string>(s => s == ApiResource.Website.LIST)))
                  .Returns(new ExecuteResult<PagedListApiResult<WebsiteApiModel>>
                  {
                      Data = new PagedListApiResult<WebsiteApiModel>
                      {
                          List = new List<WebsiteApiModel> 
                          {
                               new WebsiteApiModel
                               {
                                    RootUrl = "http://demo.maestro.com",
                                    Name = "maestro"
                               }
                          }
                      },
                      Status = HttpStatusCode.OK
                  });

            var _client = new NetsparkerClient(moqApi.Object);

            var response = _client.WebSite()
                                  .List();

            int expected = 1;
            int actual = response.Data.List.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void website_should_be_response_null()
        {
            var moqApi = new Mock<INetsparkerRestApi>();
            moqApi.Setup(x => x.Get<PagedListApiResult<WebsiteApiModel>>(It.IsAny<object>(), It.Is<string>(s => s == ApiResource.Website.LIST)))
                  .Returns(new ExecuteResult<PagedListApiResult<WebsiteApiModel>>
                  {
                      ErrorMessage = "Unauthorized",
                      Status = HttpStatusCode.Unauthorized
                  });

            var _client = new NetsparkerClient(moqApi.Object);

            var result = _client.WebSite()
                                .List();

            var expected = HttpStatusCode.Unauthorized;
            var actual = result.Status;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void website_should_be_add()
        {
            var moqApi = new Mock<INetsparkerRestApi>();
            moqApi.Setup(x => x.Post<WebsiteApiModel>(It.IsAny<object>(), It.Is<string>(s => s == ApiResource.Website.NEW)))
                  .Returns(new ExecuteResult<WebsiteApiModel>
                  {
                      Data = new WebsiteApiModel
                      {
                          Id = Guid.NewGuid(),
                          Name = "demo.maestrodemo.com",
                          RootUrl = "http://demo.maestrodemo.com/",
                          Groups = new List<string> { "Default" }
                      },
                      Status = HttpStatusCode.OK
                  });

            var _client = new NetsparkerClient(moqApi.Object);

            var result = _client.WebSite()
                                .New(new NewWebsiteApiModel
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
            moqApi.Setup(x => x.Post<WebsiteApiModel>(It.IsAny<object>(), It.Is<string>(s => s == ApiResource.Website.UPDATE)))
                  .Returns(new ExecuteResult<WebsiteApiModel>
                  {
                      Data = new WebsiteApiModel
                      {
                          Id = Guid.Parse("e0e0d0fa-20f5-4760-a3bc-e1adb9146af8"),
                          Name = "demo.maestrodemo.com",
                          RootUrl = "http://demo.maestrodemo.com/",
                          Groups = new List<string> { "Default" }
                      },
                      Status = HttpStatusCode.OK
                  });

            var _client = new NetsparkerClient(moqApi.Object);

            var result = _client.WebSite()
                                .Update(new UpdateWebsiteApiModel
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

            var result = _client.WebSite()
                                .Delete(new DeleteWebsiteApiModel { RootUrl = "http://demo.maestrodemo.com/" });

            string expected = "Ok";
            string actual = "Ok";

            Assert.AreEqual(expected, actual);
        }
    }
}