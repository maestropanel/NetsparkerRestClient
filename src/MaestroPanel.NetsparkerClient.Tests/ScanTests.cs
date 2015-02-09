using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MaestroPanel.NetsparkerClient.Model;
using Moq;
using System.Net;

namespace MaestroPanel.NetsparkerClient.Tests
{
    [TestClass]
    public class ScanTests
    {
        [TestMethod]
        public void scan_should_be_return_new_scan()
        {
            var testModel = new NewScanTaskApiModel
            {
                TargetUri = "http://foo.com"
            };

            var mockHttpRequest = new Mock<IHttpRequest>();

            var mockExecuter = new Mock<IExecuter>();
            mockExecuter.Setup(x => x.Post<ScanTaskModel>(testModel))
                        .Returns(new ExecuteResult<ScanTaskModel>
                        {
                            Status = HttpStatusCode.OK,
                            Data = new ScanTaskModel
                            {
                                Id = Guid.NewGuid(),
                                TargetUrl = "http://foo.com"
                            }
                        });


            mockHttpRequest.Setup(x => x.CreateRequest(ApiResource.Scans.NEW))
                           .Returns(mockHttpRequest.Object);

            mockHttpRequest.Setup(x => x.Execute())
                           .Returns(mockExecuter.Object);

            var netsparkerClient = new NetsparkerClient(mockHttpRequest.Object);

            var result = netsparkerClient.Scan()
                                         .New(testModel);

            Guid notExpected = Guid.Empty;
            Guid actual = result.Data.Id;

            Assert.AreNotEqual(notExpected, actual);
        }

        [TestMethod]
        public void scan_cancel_should_be_return_httpstatuscode_Ok()
        {
            var mockHttpRequest = new Mock<IHttpRequest>();

            Guid id = Guid.NewGuid();

            var mockExecuter = new Mock<IExecuter>();
            mockExecuter.Setup(x => x.Post(id))
                        .Returns(new ExecuteResult
                        {
                            Status = HttpStatusCode.OK
                        });


            mockHttpRequest.Setup(x => x.CreateRequest(ApiResource.Scans.CANCEL))
                           .Returns(mockHttpRequest.Object);

            mockHttpRequest.Setup(x => x.Execute())
                           .Returns(mockExecuter.Object);

            var netsparkerClient = new NetsparkerClient(mockHttpRequest.Object);

            var result = netsparkerClient.Scan()
                                         .Cancel(id);

            HttpStatusCode expected = HttpStatusCode.OK;
            HttpStatusCode actual = result.Status;

            Assert.AreEqual(expected, actual);
        }
    }
}
