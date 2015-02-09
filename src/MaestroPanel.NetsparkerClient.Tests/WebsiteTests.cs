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

        [TestMethod]
        public void website_should_be_verified_website()
        {
            var mockHttpRequest = new Mock<IHttpRequest>();

            var testModel = new VerifyApiModel { WebsiteUrl = "http://foo.com" };

            var mockExecuter = new Mock<IExecuter>();
            mockExecuter.Setup(x => x.Post<VerifyOwnershipResult>(testModel))
                        .Returns(new ExecuteResult<VerifyOwnershipResult>
                        {
                            Status = HttpStatusCode.OK,
                            Data = VerifyOwnershipResult.Verified
                        });


            mockHttpRequest.Setup(x => x.CreateRequest(ApiResource.Website.VERIFY)).Returns(mockHttpRequest.Object);
            mockHttpRequest.Setup(x => x.Execute()).Returns(mockExecuter.Object);

            var netsparkerClient = new NetsparkerClient(mockHttpRequest.Object);

            var result = netsparkerClient.WebSite()
                                         .Verify(testModel);

            VerifyOwnershipResult expected = VerifyOwnershipResult.Verified;
            VerifyOwnershipResult actual = result.Data;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void website_should_be_start_verification_verified_website()
        {
            var mockHttpRequest = new Mock<IHttpRequest>();

            var testModel = new StartVerificationApiModel { WebsiteUrl = "http://foo.com" };

            var mockExecuter = new Mock<IExecuter>();
            mockExecuter.Setup(x => x.Post<VerifyOwnershipResult>(testModel))
                        .Returns(new ExecuteResult<VerifyOwnershipResult>
                        {
                            Status = HttpStatusCode.OK,
                            Data = VerifyOwnershipResult.Verified
                        });


            mockHttpRequest.Setup(x => x.CreateRequest(ApiResource.Website.START_VERIFICATION)).Returns(mockHttpRequest.Object);
            mockHttpRequest.Setup(x => x.Execute()).Returns(mockExecuter.Object);

            var netsparkerClient = new NetsparkerClient(mockHttpRequest.Object);

            var result = netsparkerClient.WebSite()
                                         .StartVerification(testModel);

            VerifyOwnershipResult expected = VerifyOwnershipResult.Verified;
            VerifyOwnershipResult actual = result.Data;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void website_should_be_verification_file_verified_website()
        {
            var mockHttpRequest = new Mock<IHttpRequest>();

            var mockExecuter = new Mock<IExecuter>();
            mockExecuter.Setup(x => x.Get<VerifyOwnershipResult>())
                        .Returns(new ExecuteResult<VerifyOwnershipResult>
                        {
                            Status = HttpStatusCode.OK,
                            Data = VerifyOwnershipResult.Verified
                        });

            mockHttpRequest.Setup(x => x.CreateRequestWithQueryString(ApiResource.Website.VERIFICATION_FILE, It.IsAny<object>()))
                           .Returns(mockHttpRequest.Object);

            mockHttpRequest.Setup(x => x.Execute())
                           .Returns(mockExecuter.Object);

            var netsparkerClient = new NetsparkerClient(mockHttpRequest.Object);

            var result = netsparkerClient.WebSite()
                                         .VerificationFile("http://foo.com");

            VerifyOwnershipResult expected = VerifyOwnershipResult.Verified;
            VerifyOwnershipResult actual = result.Data;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void website_should_be_send_verification_email_verified_website()
        {
            var mockHttpRequest = new Mock<IHttpRequest>();

            var mockExecuter = new Mock<IExecuter>();
            mockExecuter.Setup(x => x.Post<VerifyOwnershipResult>(It.IsAny<string>()))
                        .Returns(new ExecuteResult<VerifyOwnershipResult>
                        {
                            Status = HttpStatusCode.OK,
                            Data = VerifyOwnershipResult.Verified
                        });


            mockHttpRequest.Setup(x => x.CreateRequest(ApiResource.Website.SEND_VERIFICATION_EMAIL))
                           .Returns(mockHttpRequest.Object);

            mockHttpRequest.Setup(x => x.Execute())
                           .Returns(mockExecuter.Object);

            var netsparkerClient = new NetsparkerClient(mockHttpRequest.Object);

            var result = netsparkerClient.WebSite()
                                         .WebSiteSendVerificationEmail("http://foo.com");

            VerifyOwnershipResult expected = VerifyOwnershipResult.Verified;
            VerifyOwnershipResult actual = result.Data;

            Assert.AreEqual(expected, actual);
        }
    }
}