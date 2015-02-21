using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MaestroPanel.NetsparkerClient.Model;
using Moq;
using System.Net;
using System.Collections.Generic;

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
            mockExecuter.Setup(x => x.Post<List<ScanTaskModel>>(testModel))
                        .Returns(new ExecuteResult<List<ScanTaskModel>>
                        {
                            Status = HttpStatusCode.OK,
                            Data = new List<ScanTaskModel> 
                            {
                                new ScanTaskModel
                                {
                                    Id = Guid.NewGuid(),
                                    TargetUrl = "http://foo.com"
                                }
                            }
                        });


            mockHttpRequest.Setup(x => x.CreateRequest(ApiResource.Scans.NEW))
                           .Returns(mockHttpRequest.Object);

            mockHttpRequest.Setup(x => x.Execute())
                           .Returns(mockExecuter.Object);

            var netsparkerClient = new NetsparkerRestClient(mockHttpRequest.Object);

            var result = netsparkerClient.Scan()
                                         .New(testModel);

            Guid notExpected = Guid.Empty;
            Guid? actual = result.Data[0].Id;

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

            var netsparkerClient = new NetsparkerRestClient(mockHttpRequest.Object);

            var result = netsparkerClient.Scan()
                                         .Cancel(id);

            HttpStatusCode expected = HttpStatusCode.OK;
            HttpStatusCode actual = result.Status;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void scan_delete_should_be_return_httpstatuscode_Ok()
        {
            var mockHttpRequest = new Mock<IHttpRequest>();

            List<Guid> ids = new List<Guid> 
            {
                Guid.NewGuid(),
                Guid.NewGuid()
            };

            var mockExecuter = new Mock<IExecuter>();
            mockExecuter.Setup(x => x.Post(ids))
                        .Returns(new ExecuteResult
                        {
                            Status = HttpStatusCode.OK
                        });


            mockHttpRequest.Setup(x => x.CreateRequest(ApiResource.Scans.DELETE))
                           .Returns(mockHttpRequest.Object);

            mockHttpRequest.Setup(x => x.Execute())
                           .Returns(mockExecuter.Object);

            var netsparkerClient = new NetsparkerRestClient(mockHttpRequest.Object);

            var result = netsparkerClient.Scan()
                                         .Delete(ids);

            HttpStatusCode expected = HttpStatusCode.OK;
            HttpStatusCode actual = result.Status;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void scan_incremental_should_be_return_scanapitaskmodel()
        {
            var mockHttpRequest = new Mock<IHttpRequest>();

            var testModel = new BaseScanApiModel
            {
                BaseScanId = new Guid("5a6f202c-4d88-454b-ad7a-9ac307859ce7")
            };

            Guid scanid = Guid.NewGuid();

            var mockExecuter = new Mock<IExecuter>();
            mockExecuter.Setup(x => x.Post<ScanTaskModel>(testModel))
                        .Returns(new ExecuteResult<ScanTaskModel>
                        {
                            Status = HttpStatusCode.OK,
                            Data = new ScanTaskModel
                            {
                                Id = scanid
                            }
                        });


            mockHttpRequest.Setup(x => x.CreateRequest(ApiResource.Scans.INCREMENTAL))
                           .Returns(mockHttpRequest.Object);

            mockHttpRequest.Setup(x => x.Execute())
                           .Returns(mockExecuter.Object);

            var netsparkerClient = new NetsparkerRestClient(mockHttpRequest.Object);

            var result = netsparkerClient.Scan()
                                         .Incremental(testModel);

            Guid expected = scanid;
            Guid? actual = result.Data.Id;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void scan_status_should_be_return_scanning()
        {
            var mockHttpRequest = new Mock<IHttpRequest>();

            var mockExecuter = new Mock<IExecuter>();
            mockExecuter.Setup(x => x.Get<ApiScanStatusModel>())
                        .Returns(new ExecuteResult<ApiScanStatusModel>
                        {
                            Status = HttpStatusCode.OK,
                            Data = new ApiScanStatusModel
                            {
                                State = ScanTaskState.Scanning
                            }
                        });


            mockHttpRequest.Setup(x => x.CreateRequestWithQueryString(ApiResource.Scans.STATUS, It.IsAny<object>()))
                           .Returns(mockHttpRequest.Object);

            mockHttpRequest.Setup(x => x.Execute())
                           .Returns(mockExecuter.Object);

            var netsparkerClient = new NetsparkerRestClient(mockHttpRequest.Object);

            var scan = netsparkerClient.Scan()
                                       .Status(Guid.NewGuid());

            ScanTaskState expected = ScanTaskState.Scanning;
            ScanTaskState actual = scan.Data.State;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void scan_retest_should_be_return_existing_scan()
        {
            var mockHttpRequest = new Mock<IHttpRequest>();

            var testModel = new BaseScanApiModel
            {
                BaseScanId = Guid.NewGuid()
            };

            Guid scanid = Guid.NewGuid();

            var mockExecuter = new Mock<IExecuter>();
            mockExecuter.Setup(x => x.Post<ScanTaskModel>(testModel))
                        .Returns(new ExecuteResult<ScanTaskModel>
                        {
                            Status = HttpStatusCode.OK,
                            Data = new ScanTaskModel
                            {
                                Id = scanid
                            }
                        });


            mockHttpRequest.Setup(x => x.CreateRequest(ApiResource.Scans.RETEST))
                           .Returns(mockHttpRequest.Object);

            mockHttpRequest.Setup(x => x.Execute())
                           .Returns(mockExecuter.Object);

            var netsparkerClient = new NetsparkerRestClient(mockHttpRequest.Object);

            var scan = netsparkerClient.Scan()
                                       .Retest(testModel);

            Guid? expected = scanid;
            Guid? actual = scan.Data.Id;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void scan_result_should_be_return_url_foocom()
        {
            var mockHttpRequest = new Mock<IHttpRequest>();

            var mockExecuter = new Mock<IExecuter>();
            mockExecuter.Setup(x => x.Get<List<VulnerabilityModel>>())
                        .Returns(new ExecuteResult<List<VulnerabilityModel>>
                        {
                            Status = HttpStatusCode.OK,
                            Data = new List<VulnerabilityModel>
                            {
                                new VulnerabilityModel(),
                                new VulnerabilityModel()
                            }
                        });


            mockHttpRequest.Setup(x => x.CreateRequestWithQueryString(ApiResource.Scans.RESULT, It.IsAny<object>()))
                           .Returns(mockHttpRequest.Object);

            mockHttpRequest.Setup(x => x.Execute())
                           .Returns(mockExecuter.Object);

            var netsparkerClient = new NetsparkerRestClient(mockHttpRequest.Object);

            var scan = netsparkerClient.Scan()
                                       .Result(Guid.NewGuid());

            int expected = 2;
            int actual = scan.Data.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void scan_list_should_be_return_two_scan()
        {
            var mockHttpRequest = new Mock<IHttpRequest>();

            var mockExecuter = new Mock<IExecuter>();
            mockExecuter.Setup(x => x.Get<PagedListApiResult<ScanTaskModel>>())
                        .Returns(new ExecuteResult<PagedListApiResult<ScanTaskModel>>
                        {
                            Status = HttpStatusCode.OK,
                            Data = new PagedListApiResult<ScanTaskModel>
                            {
                                List = new List<ScanTaskModel> 
                                {
                                    new ScanTaskModel(),
                                    new ScanTaskModel()
                                }
                            }
                        });


            mockHttpRequest.Setup(x => x.CreateRequestWithQueryString(ApiResource.Scans.LIST, It.IsAny<object>()))
                           .Returns(mockHttpRequest.Object);

            mockHttpRequest.Setup(x => x.Execute())
                           .Returns(mockExecuter.Object);

            var netsparkerClient = new NetsparkerRestClient(mockHttpRequest.Object);

            var scan = netsparkerClient.Scan()
                                       .List();

            int expected = 2;
            int actual = scan.Data.List.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void scan_schedule_should_be_return_update_schedule_model()
        {
            var mockHttpRequest = new Mock<IHttpRequest>();

            var testModel = new NewScheduledScanApiModel
            {
                TargetUri = "http://foo.com"
            };

            var mockExecuter = new Mock<IExecuter>();
            mockExecuter.Setup(x => x.Post<UpdateScheduledScanModel>(testModel))
                        .Returns(new ExecuteResult<UpdateScheduledScanModel>
                        {
                            Status = HttpStatusCode.OK,
                            Data = new UpdateScheduledScanModel
                            {
                                TargetUri = "http://foo.com"
                            }
                        });


            mockHttpRequest.Setup(x => x.CreateRequest(ApiResource.Scans.SCHEDULE))
                           .Returns(mockHttpRequest.Object);

            mockHttpRequest.Setup(x => x.Execute())
                           .Returns(mockExecuter.Object);

            var netsparkerClient = new NetsparkerRestClient(mockHttpRequest.Object);

            var scan = netsparkerClient.Scan()
                                       .Schedule(testModel);

            string expected = testModel.TargetUri;
            string actual = scan.Data.TargetUri;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void scan_update_schedule_should_be_return_update_schedule_model()
        {
            var mockHttpRequest = new Mock<IHttpRequest>();

            var testModel = new UpdateScheduledScanApiModel
            {
                TargetUri = "http://foo.com"
            };

            var mockExecuter = new Mock<IExecuter>();
            mockExecuter.Setup(x => x.Post<UpdateScheduledScanApiModel>(testModel))
                        .Returns(new ExecuteResult<UpdateScheduledScanApiModel>
                        {
                            Status = HttpStatusCode.OK,
                            Data = new UpdateScheduledScanApiModel
                            {
                                TargetUri = "http://foo.com"
                            }
                        });


            mockHttpRequest.Setup(x => x.CreateRequest(ApiResource.Scans.UPDATE_SCHEDULED))
                           .Returns(mockHttpRequest.Object);

            mockHttpRequest.Setup(x => x.Execute())
                           .Returns(mockExecuter.Object);

            var netsparkerClient = new NetsparkerRestClient(mockHttpRequest.Object);

            var scan = netsparkerClient.Scan()
                                       .UpdateSchedule(testModel);

            string expected = testModel.TargetUri;
            string actual = scan.Data.TargetUri;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void scan_unschedule_should_be_return_httpstatus_Ok()
        {
            var mockHttpRequest = new Mock<IHttpRequest>();

            Guid id = Guid.NewGuid();

            var mockExecuter = new Mock<IExecuter>();
            mockExecuter.Setup(x => x.Post(id))
                        .Returns(new ExecuteResult
                        {
                            Status = HttpStatusCode.OK,
                        });


            mockHttpRequest.Setup(x => x.CreateRequest(ApiResource.Scans.UNSCHEDULE))
                           .Returns(mockHttpRequest.Object);

            mockHttpRequest.Setup(x => x.Execute())
                           .Returns(mockExecuter.Object);

            var netsparkerClient = new NetsparkerRestClient(mockHttpRequest.Object);

            var scan = netsparkerClient.Scan()
                                       .Unschedule(id);

            HttpStatusCode expected = HttpStatusCode.OK;
            HttpStatusCode actual = scan.Status;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void scan_list_scheduled_should_be_return_two_scan()
        {
            var mockHttpRequest = new Mock<IHttpRequest>();

            var mockExecuter = new Mock<IExecuter>();
            mockExecuter.Setup(x => x.Get<PagedListApiResult<UpdateScheduledScanModel>>())
                        .Returns(new ExecuteResult<PagedListApiResult<UpdateScheduledScanModel>>
                        {
                            Status = HttpStatusCode.OK,
                            Data = new PagedListApiResult<UpdateScheduledScanModel>
                            {
                                List = new List<UpdateScheduledScanModel> 
                                {
                                    new UpdateScheduledScanModel(),
                                    new UpdateScheduledScanModel()
                                }
                            }
                        });


            mockHttpRequest.Setup(x => x.CreateRequestWithQueryString(ApiResource.Scans.LIST_SCHEDULED, It.IsAny<object>()))
                           .Returns(mockHttpRequest.Object);

            mockHttpRequest.Setup(x => x.Execute())
                           .Returns(mockExecuter.Object);

            var netsparkerClient = new NetsparkerRestClient(mockHttpRequest.Object);

            var scan = netsparkerClient.Scan()
                                       .ListScheduled();

            int expected = 2;
            int actual = scan.Data.List.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void scan_report_should_be_return_reportfile()
        {
            var mockHttpRequest = new Mock<IHttpRequest>();

            var mockExecuter = new Mock<IExecuter>();
            mockExecuter.Setup(x => x.Get<string>())
                        .Returns(new ExecuteResult<string>
                        {
                            Status = HttpStatusCode.OK,
                            Data = "xml"
                        });


            mockHttpRequest.Setup(x => x.CreateRequestWithQueryString(ApiResource.Scans.REPORT, It.IsAny<object>()))
                           .Returns(mockHttpRequest.Object);

            mockHttpRequest.Setup(x => x.Execute())
                           .Returns(mockExecuter.Object);

            var netsparkerClient = new NetsparkerRestClient(mockHttpRequest.Object);

            var scan = netsparkerClient.Scan()
                                       .Report(Guid.NewGuid(), ReportType.Vulnerabilities, ReportFormat.Xml);

            string expected = "xml";
            string actual = scan.Data;

            Assert.AreEqual(expected, actual);
        }
    }
}
