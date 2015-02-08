using MaestroPanel.NetsparkerClient.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var _client = new NetsparkerClient(new NetsparkerRestApi("https://www.netsparkercloud.com/api/1.0", "2ePd24tUZFgX2pladg7ZshHc0jNdM8jd4XOY70d8GnM="));

            var result = _client.WebSiteGroup()
                                .List();
        }

        [TestMethod]
        public void websitegroup_should_be_add()
        {
            var _client = new NetsparkerClient(new NetsparkerRestApi("https://www.netsparkercloud.com/api/1.0", "2ePd24tUZFgX2pladg7ZshHc0jNdM8jd4XOY70d8GnM="));

            var result = _client.WebSiteGroup()
                                .New(new NewWebsiteGroupApiModel
            {
                Name = "Public"
            });
        }
    }
}
