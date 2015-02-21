using MaestroPanel.NetsparkerClient.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MaestroPanel.NetsparkerClient.Tests
{
    [TestClass]
    public class WebsiteGroupTests
    {
        [TestMethod]
        public void websitegroup_should_be_list()
        {
            var mockHttpRequest = new Mock<IHttpRequest>();

            var mockExecuter = new Mock<IExecuter>();
            mockExecuter.Setup(x => x.Get<PagedListApiResult<WebsiteGroupApiModel>>())
                        .Returns(new ExecuteResult<PagedListApiResult<WebsiteGroupApiModel>>
                        {
                            Status = HttpStatusCode.OK,
                            Data = new PagedListApiResult<WebsiteGroupApiModel>
                            {
                                List = new List<WebsiteGroupApiModel> 
                                {
                                     new WebsiteGroupApiModel { Name="Default" },
                                     new WebsiteGroupApiModel { Name="Public" },
                                }
                            }
                        });


            mockHttpRequest.Setup(x => x.CreateRequestWithQueryString(ApiResource.WebsiteGroup.LIST, It.IsAny<object>())).Returns(mockHttpRequest.Object);
            mockHttpRequest.Setup(x => x.Execute()).Returns(mockExecuter.Object);

            var netsparkerClient = new NetsparkerRestClient(mockHttpRequest.Object);

            var webSiteList = netsparkerClient.WebSiteGroup()
                                              .List();

            int expected = 2;
            int actual = webSiteList.Data.List.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void websitegroup_should_be_add()
        {
            var testModel = new NewWebsiteGroupApiModel
            {
                Name = "Private"
            };

            var mockHttpRequest = new Mock<IHttpRequest>();

            var mockExecuter = new Mock<IExecuter>();
            mockExecuter.Setup(x => x.Post<WebsiteGroupApiModel>(testModel))
                        .Returns(new ExecuteResult<WebsiteGroupApiModel>
                        {
                            Status = HttpStatusCode.OK,
                            Data = new WebsiteGroupApiModel
                            {
                                Id = Guid.NewGuid(),
                                Name = testModel.Name
                            }
                        });


            mockHttpRequest.Setup(x => x.CreateRequest(ApiResource.WebsiteGroup.NEW)).Returns(mockHttpRequest.Object);
            mockHttpRequest.Setup(x => x.Execute()).Returns(mockExecuter.Object);

            var netsparkerClient = new NetsparkerRestClient(mockHttpRequest.Object);

            var result = netsparkerClient.WebSiteGroup()
                                         .New(testModel);

            bool isTrue = result.Data.Id != Guid.Empty && result.Data.Name == testModel.Name;

            Assert.IsTrue(isTrue);
        }
    }
}
