using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MaestroPanel.NetsparkerClient;

namespace MaestroPanel.HttpRequest.Tests
{
    [TestClass]
    public class UrlTests
    {
        const string BASE_URL = "http://foo.com/api/1.0/";

        [TestMethod]
        public void url_should_be_website_delete()
        {
            var req = new NetsparkerClient.HttpRequest(BASE_URL);

            req.CreateRequest(ApiResource.Website.DELETE);

            string expected = BASE_URL + "websites/delete";
            string actual = req.Url;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void url_should_be_website_new()
        {
            var req = new NetsparkerClient.HttpRequest(BASE_URL);

            req.CreateRequest(ApiResource.Website.NEW);

            string expected = BASE_URL + "websites/new";
            string actual = req.Url;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void url_should_be_website_list_only_page_parameter()
        {
            var req = new NetsparkerClient.HttpRequest(BASE_URL);

            req.CreateRequestWithQueryString(ApiResource.Website.LIST, new { page = 3, pageSize = 10 });

            string expected = BASE_URL + "websites/list?page=3&pageSize=10";
            string actual = req.Url;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void url_should_be_website_list_page3()
        {
            var req = new NetsparkerClient.HttpRequest(BASE_URL);

            req.CreateRequestWithQueryString(ApiResource.Website.LIST, new { page = 3 });

            string expected = BASE_URL + "websites/list?page=3";
            string actual = req.Url;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void url_should_be_website_update()
        {
            var req = new NetsparkerClient.HttpRequest(BASE_URL);

            req.CreateRequest(ApiResource.Website.UPDATE);

            string expected = BASE_URL + "websites/update";
            string actual = req.Url;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void url_should_be_website_verify()
        {
            var req = new NetsparkerClient.HttpRequest(BASE_URL);

            req.CreateRequest(ApiResource.Website.VERIFY);

            string expected = BASE_URL + "websites/verify";
            string actual = req.Url;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void url_should_be_website_verify_file()
        {
            var req = new NetsparkerClient.HttpRequest(BASE_URL);

            req.CreateRequest(ApiResource.Website.VERIFICATION_FILE);

            string expected = BASE_URL + "websites/verificationfile";
            string actual = req.Url;

            Assert.AreEqual(expected, actual);
        }
    }
}
