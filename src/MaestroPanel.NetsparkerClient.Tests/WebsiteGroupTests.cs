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
        public void websitegroup_should_be_default()
        {
            var api = new NetsparkerRestApi();

            var mockApi = new Mock<INetsparkerRestApi>();
            mockApi.Setup(x => x.Get<PagedListApiResult<WebsiteGroupApiModel>>(It.IsAny<object>(), ApiResource.WebsiteGroup.LIST))
                   .Returns(new ExecuteResult<PagedListApiResult<WebsiteGroupApiModel>>
                   {
                       Data = new PagedListApiResult<WebsiteGroupApiModel>
                       {
                           List = new List<WebsiteGroupApiModel> 
                           {
                               new WebsiteGroupApiModel
                               {
                                    Name = "Default"
                               }
                           }
                       },
                       Status = HttpStatusCode.OK
                   });

            var _client = new NetsparkerClient(mockApi.Object);

            var result = _client.WebSiteGroup()
                                .List();

            var expected = result.Data.List.First().Name;
            var actual = "Default";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void websitegroup_should_be_add()
        {
            var _client = new NetsparkerClient(new NetsparkerRestApi("https://www.netsparkercloud.com/api/1.0", ""));

            var result = _client.WebSiteGroup()
                                .New(new NewWebsiteGroupApiModel
                                {
                                    Name = "Public"
                                });
        }
    }
}
