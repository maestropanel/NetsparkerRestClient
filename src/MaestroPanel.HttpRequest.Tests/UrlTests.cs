using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MaestroPanel.NetsparkerClient;
using MaestroPanel.NetsparkerClient.Model;

namespace MaestroPanel.HttpRequest.Tests
{
    [TestClass]
    public class UrlTests
    {
        const string BASE_URL = "http://foo.com/api/1.0/";

        #region Websites
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

        [TestMethod]
        public void url_should_be_website_send_verification_email()
        {
            var req = new NetsparkerClient.HttpRequest(BASE_URL);

            req.CreateRequest(ApiResource.Website.SEND_VERIFICATION_EMAIL);

            string expected = BASE_URL + "websites/sendverificationemail";
            string actual = req.Url;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void url_should_be_website_start_verification()
        {
            var req = new NetsparkerClient.HttpRequest(BASE_URL);

            req.CreateRequest(ApiResource.Website.START_VERIFICATION);

            string expected = BASE_URL + "websites/startverification";
            string actual = req.Url;

            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region WebsiteGroup
        [TestMethod]
        public void url_should_be_website_group_delete()
        {
            var req = new NetsparkerClient.HttpRequest(BASE_URL);

            req.CreateRequest(ApiResource.WebsiteGroup.DELETE);

            string expected = BASE_URL + "websitegroups/delete";
            string actual = req.Url;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void url_should_be_website_group_new()
        {
            var req = new NetsparkerClient.HttpRequest(BASE_URL);

            req.CreateRequest(ApiResource.WebsiteGroup.NEW);

            string expected = BASE_URL + "websitegroups/new";
            string actual = req.Url;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void url_should_be_website_group_update()
        {
            var req = new NetsparkerClient.HttpRequest(BASE_URL);

            req.CreateRequest(ApiResource.WebsiteGroup.UPDATE);

            string expected = BASE_URL + "websitegroups/update";
            string actual = req.Url;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void url_should_be_website_group_list()
        {
            var req = new NetsparkerClient.HttpRequest(BASE_URL);

            req.CreateRequestWithQueryString(ApiResource.WebsiteGroup.LIST, new { page = 4, pageSize = 50 });

            string expected = BASE_URL + "websitegroups/list?page=4&pageSize=50";
            string actual = req.Url;

            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region ScanPolicy
        [TestMethod]
        public void url_should_be_scan_policy_list()
        {
            var req = new NetsparkerClient.HttpRequest(BASE_URL);

            req.CreateRequestWithQueryString(ApiResource.ScanPolicy.LIST, new { page = 4, pageSize = 50 });

            string expected = BASE_URL + "scanpolicies/list?page=4&pageSize=50";
            string actual = req.Url;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void url_should_be_scan_policy_get()
        {
            var req = new NetsparkerClient.HttpRequest(BASE_URL);

            req.CreateRequestWithQueryString(ApiResource.ScanPolicy.GET, new { name = "bar" });

            string expected = BASE_URL + "scanpolicies/get?name=bar";
            string actual = req.Url;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void url_should_be_scan_policy_new()
        {
            var req = new NetsparkerClient.HttpRequest(BASE_URL);

            req.CreateRequest(ApiResource.ScanPolicy.NEW);

            string expected = BASE_URL + "scanpolicies/new";
            string actual = req.Url;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void url_should_be_scan_policy_delete()
        {
            var req = new NetsparkerClient.HttpRequest(BASE_URL);

            req.CreateRequest(ApiResource.ScanPolicy.DELETE);

            string expected = BASE_URL + "scanpolicies/delete";
            string actual = req.Url;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void url_should_be_scan_policy_update()
        {
            var req = new NetsparkerClient.HttpRequest(BASE_URL);

            req.CreateRequest(ApiResource.ScanPolicy.UPDATE);

            string expected = BASE_URL + "scanpolicies/update";
            string actual = req.Url;

            Assert.AreEqual(expected, actual);
        }
        #endregion

        #region Scan
        [TestMethod]
        public void url_should_be_scan_new()
        {
            var req = new NetsparkerClient.HttpRequest(BASE_URL);

            req.CreateRequest(ApiResource.Scans.CANCEL);

            string expected = BASE_URL + "scans/cancel";
            string actual = req.Url;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void url_should_be_scan_delete()
        {
            var req = new NetsparkerClient.HttpRequest(BASE_URL);

            req.CreateRequest(ApiResource.Scans.DELETE);

            string expected = BASE_URL + "scans/delete";
            string actual = req.Url;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void url_should_be_scan_incremental()
        {
            var req = new NetsparkerClient.HttpRequest(BASE_URL);

            req.CreateRequest(ApiResource.Scans.INCREMENTAL);

            string expected = BASE_URL + "scans/incremental";
            string actual = req.Url;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void url_should_be_scan_list()
        {
            var req = new NetsparkerClient.HttpRequest(BASE_URL);

            req.CreateRequestWithQueryString(ApiResource.Scans.LIST, new { page = 1, pageSize = 10 });

            string expected = BASE_URL + "scans/list?page=1&pageSize=10";
            string actual = req.Url;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void url_should_be_scan_list_scheduled()
        {
            var req = new NetsparkerClient.HttpRequest(BASE_URL);

            req.CreateRequest(ApiResource.Scans.LIST_SCHEDULED);

            string expected = BASE_URL + "scans/list-scheduled";
            string actual = req.Url;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void url_should_be_scan_report()
        {
            //Guid id, ReportType type, ReportFormat format, ContentFormat contentFormat

            var req = new NetsparkerClient.HttpRequest(BASE_URL);

            req.CreateRequestWithQueryString(ApiResource.Scans.REPORT, new
            {
                id = "1",
                type = ReportType.Scanned,
                format = ReportFormat.Xml,
                contentFormat = ContentFormat.Html
            });

            string expected = BASE_URL + "scans/report?id=1&type=Scanned&format=Xml&contentFormat=Html";
            string actual = req.Url;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void url_should_be_scan_result()
        {
            var req = new NetsparkerClient.HttpRequest(BASE_URL);

            req.CreateRequestWithQueryString(ApiResource.Scans.RESULT, new
            {
                id = "1"
            });

            string expected = BASE_URL + "scans/result?id=1";
            string actual = req.Url;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void url_should_be_scan_retest()
        {
            var req = new NetsparkerClient.HttpRequest(BASE_URL);

            req.CreateRequest(ApiResource.Scans.RETEST);

            string expected = BASE_URL + "scans/retest";
            string actual = req.Url;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void url_should_be_scan_schedule()
        {
            var req = new NetsparkerClient.HttpRequest(BASE_URL);

            req.CreateRequest(ApiResource.Scans.SCHEDULE);

            string expected = BASE_URL + "scans/schedule";
            string actual = req.Url;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void url_should_be_scan_status()
        {
            var req = new NetsparkerClient.HttpRequest(BASE_URL);

            req.CreateRequestWithQueryString(ApiResource.Scans.STATUS, new { id = 1 });

            string expected = BASE_URL + "scans/status?id=1";
            string actual = req.Url;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void url_should_be_scan_unschedule()
        {
            var req = new NetsparkerClient.HttpRequest(BASE_URL);

            req.CreateRequest(ApiResource.Scans.UNSCHEDULE);

            string expected = BASE_URL + "scans/unschedule";
            string actual = req.Url;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void url_should_be_scan_update()
        {
            var req = new NetsparkerClient.HttpRequest(BASE_URL);

            req.CreateRequest(ApiResource.Scans.UPDATE);

            string expected = BASE_URL + "scans/update";
            string actual = req.Url;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void url_should_be_scan_update_scheduled()
        {
            var req = new NetsparkerClient.HttpRequest(BASE_URL);

            req.CreateRequest(ApiResource.Scans.UPDATE_SCHEDULED);

            string expected = BASE_URL + "scans/update-scheduled";
            string actual = req.Url;

            Assert.AreEqual(expected, actual);
        }
        #endregion
    }
}
