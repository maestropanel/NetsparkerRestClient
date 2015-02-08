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
        public void website_should_be_list_website()
        {
            var mockHttpRequest = new Mock<IHttpRequest>();

            var mockExecuter = new Mock<IExecuter>();
            mockExecuter.Setup(x => x.Get<PagedListApiResult<WebsiteApiModel>>())
                        .Returns(new ExecuteResult<PagedListApiResult<WebsiteApiModel>>
                        {
                            Status = HttpStatusCode.OK,
                            Data = new PagedListApiResult<WebsiteApiModel>
                            {
                                List = new List<WebsiteApiModel> 
                                {
                                     new WebsiteApiModel { Name="foo.com" },
                                     new WebsiteApiModel { Name="bar.com" },
                                }
                            }
                        });


            mockHttpRequest.Setup(x => x.CreateRequestWithQueryString(ApiResource.Website.LIST, It.IsAny<object>())).Returns(mockHttpRequest.Object);
            mockHttpRequest.Setup(x => x.Execute()).Returns(mockExecuter.Object);

            var netsparkerClient = new NetsparkerClient(mockHttpRequest.Object);

            var webSiteList = netsparkerClient.WebSite()
                                              .List();

            int expected = 2;
            int actual = webSiteList.Data.List.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void website_should_be_list_response_unauthorized()
        {
            var mockHttpRequest = new Mock<IHttpRequest>();

            var mockExecuter = new Mock<IExecuter>();
            mockExecuter.Setup(x => x.Get<PagedListApiResult<WebsiteApiModel>>())
                        .Returns(new ExecuteResult<PagedListApiResult<WebsiteApiModel>>
                        {
                            Status = HttpStatusCode.Unauthorized
                        });


            mockHttpRequest.Setup(x => x.CreateRequestWithQueryString(ApiResource.Website.LIST, It.IsAny<object>())).Returns(mockHttpRequest.Object);
            mockHttpRequest.Setup(x => x.Execute()).Returns(mockExecuter.Object);

            var netsparkerClient = new NetsparkerClient(mockHttpRequest.Object);

            var webSiteList = netsparkerClient.WebSite()
                                              .List();

            HttpStatusCode expected = HttpStatusCode.Unauthorized;
            HttpStatusCode actual = webSiteList.Status;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void website_should_be_add_new_website()
        {
            var testModel = new NewWebsiteApiModel
            {
                Name = "bar.com",
                RootUrl = "http://bar.com"
            };

            var mockHttpRequest = new Mock<IHttpRequest>();

            var mockExecuter = new Mock<IExecuter>();
            mockExecuter.Setup(x => x.Post<WebsiteApiModel>(testModel))
                        .Returns(new ExecuteResult<WebsiteApiModel>
                        {
                            Status = HttpStatusCode.OK,
                            Data = new WebsiteApiModel
                            {
                                Id = Guid.NewGuid(),
                                Name = testModel.Name,
                                RootUrl = testModel.RootUrl
                            }
                        });


            mockHttpRequest.Setup(x => x.CreateRequest(ApiResource.Website.NEW)).Returns(mockHttpRequest.Object);
            mockHttpRequest.Setup(x => x.Execute()).Returns(mockExecuter.Object);

            var netsparkerClient = new NetsparkerClient(mockHttpRequest.Object);

            var result = netsparkerClient.WebSite()
                                              .New(testModel);

            Guid notExpected = Guid.Empty;
            Guid actual = result.Data.Id;

            Assert.AreNotEqual(notExpected, actual);
        }

        [TestMethod]
        public void website_should_be_update_website()
        {
            var testModel = new UpdateWebsiteApiModel
            {
                Name = "bar.com",
                RootUrl = "http://bar.com"
            };

            var mockHttpRequest = new Mock<IHttpRequest>();

            var mockExecuter = new Mock<IExecuter>();
            mockExecuter.Setup(x => x.Post<WebsiteApiModel>(testModel))
                        .Returns(new ExecuteResult<WebsiteApiModel>
                        {
                            Status = HttpStatusCode.OK,
                            Data = new WebsiteApiModel
                            {
                                Id = new Guid("faf48dcc-93fd-4b44-93e9-a2ff7de497c0"),
                                Name = testModel.Name,
                                RootUrl = testModel.RootUrl
                            }
                        });


            mockHttpRequest.Setup(x => x.CreateRequest(ApiResource.Website.UPDATE)).Returns(mockHttpRequest.Object);
            mockHttpRequest.Setup(x => x.Execute()).Returns(mockExecuter.Object);

            var netsparkerClient = new NetsparkerClient(mockHttpRequest.Object);

            var result = netsparkerClient.WebSite()
                                         .Update(testModel);

            Guid expected = new Guid("faf48dcc-93fd-4b44-93e9-a2ff7de497c0");
            Guid actual = result.Data.Id;

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void website_should_be_delete_website()
        {
            var testModel = new DeleteWebsiteApiModel
            {
                RootUrl = "http://bar.com"
            };

            var mockHttpRequest = new Mock<IHttpRequest>();

            var mockExecuter = new Mock<IExecuter>();
            mockExecuter.Setup(x => x.Post<DeleteWebsiteResult>(testModel))
                        .Returns(new ExecuteResult<DeleteWebsiteResult>
                        {
                            Status = HttpStatusCode.OK,
                            Data = DeleteWebsiteResult.Ok
                        });


            mockHttpRequest.Setup(x => x.CreateRequest(ApiResource.Website.DELETE)).Returns(mockHttpRequest.Object);
            mockHttpRequest.Setup(x => x.Execute()).Returns(mockExecuter.Object);

            var netsparkerClient = new NetsparkerClient(mockHttpRequest.Object);

            var result = netsparkerClient.WebSite()
                                         .Delete(testModel);

            DeleteWebsiteResult expected = DeleteWebsiteResult.Ok;
            DeleteWebsiteResult actual = result.Data;

            Assert.AreEqual(expected, actual);
        }
    }
}