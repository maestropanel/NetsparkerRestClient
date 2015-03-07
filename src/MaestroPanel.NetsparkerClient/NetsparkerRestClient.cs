using MaestroPanel.NetsparkerClient.Model;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MaestroPanel.NetsparkerClient
{
    public class NetsparkerRestClient
    {
        IHttpRequest _webRequest;

        public NetsparkerRestClient(IHttpRequest webRequest)
        {
            _webRequest = webRequest;
        }

        public void Authenticate(string accessToken)
        {
            _webRequest.Authenticate(accessToken);
        }

        public Website WebSite()
        {
            return new Website(_webRequest);
        }

        public WebsiteGroup WebSiteGroup()
        {
            return new WebsiteGroup(_webRequest);
        }

        public Scan Scan()
        {
            return new Scan(_webRequest);
        }

        public ScanPolicy ScanPolicy()
        {
            return new ScanPolicy(_webRequest);
        }
    }

    public class Website
    {
        IHttpRequest _webRequest;

        public Website(IHttpRequest webRequest)
        {
            _webRequest = webRequest;
        }

        public ExecuteResult<PagedListApiResult<WebsiteApiModel>> List(int page = 1, int pageSize = 20)
        {
            return _webRequest.CreateRequestWithQueryString(ApiResource.Website.LIST, new { page = page, pageSize = pageSize })
                              .Execute()
                              .Get<PagedListApiResult<WebsiteApiModel>>();
        }

        public ExecuteResult<WebsiteApiModel> New(NewWebsiteApiModel webSite)
        {
            return _webRequest.CreateRequest(ApiResource.Website.NEW)
                              .Execute()
                              .Post<WebsiteApiModel>(webSite);
        }

        public ExecuteResult<WebsiteApiModel> Update(UpdateWebsiteApiModel webSite)
        {
            return _webRequest.CreateRequest(ApiResource.Website.UPDATE)
                              .Execute()
                              .Post<WebsiteApiModel>(webSite);
        }

        public ExecuteResult<DeleteWebsiteResult> Delete(DeleteWebsiteApiModel model)
        {
            return _webRequest.CreateRequest(ApiResource.Website.DELETE)
                              .Execute()
                              .Post<DeleteWebsiteResult>(model);
        }

        public ExecuteResult<VerifyOwnershipResult> Verify(VerifyApiModel model)
        {
            return _webRequest.CreateRequest(ApiResource.Website.VERIFY)
                              .Execute()
                              .Post<VerifyOwnershipResult>(model);
        }

        public ExecuteResult<StartVerificationResponse> StartVerification(StartVerificationApiModel model)
        {
            return _webRequest.CreateRequest(ApiResource.Website.START_VERIFICATION)
                              .Execute()
                              .Post<StartVerificationResponse>(model);
        }

        public ExecuteResult VerificationFile(string websiteUrl)
        {
            return _webRequest.CreateRequestWithQueryString(ApiResource.Website.VERIFICATION_FILE, new { webSiteUrl = websiteUrl })
                              .Execute()
                              .Get();
        }

        public ExecuteResult<VerifyOwnershipResult> WebSiteSendVerificationEmail(string websiteUrl)
        {
            return _webRequest.CreateRequest(ApiResource.Website.SEND_VERIFICATION_EMAIL)
                              .Execute()
                              .Post<VerifyOwnershipResult>(websiteUrl);
        }
    }

    public class WebsiteGroup
    {
        IHttpRequest _webRequest;

        public WebsiteGroup(IHttpRequest webRequest)
        {
            _webRequest = webRequest;
        }

        public ExecuteResult<PagedListApiResult<WebsiteGroupApiModel>> List(int page = 1, int pageSize = 20)
        {
            return _webRequest.CreateRequestWithQueryString(ApiResource.WebsiteGroup.LIST, new { page = page, pageSize = pageSize })
                              .Execute()
                              .Get<PagedListApiResult<WebsiteGroupApiModel>>();
        }

        public ExecuteResult<WebsiteGroupApiModel> New(NewWebsiteGroupApiModel model)
        {
            return _webRequest.CreateRequest(ApiResource.WebsiteGroup.NEW)
                              .Execute()
                              .Post<WebsiteGroupApiModel>(model);
        }

        public ExecuteResult<WebsiteApiModel> Update(WebsiteGroupApiModel model)
        {
            return _webRequest.CreateRequest(ApiResource.WebsiteGroup.UPDATE)
                              .Execute()
                              .Post<WebsiteApiModel>(model);
        }

        public ExecuteResult Delete(DeleteWebsiteGroupApiModel model)
        {
            return _webRequest.CreateRequest(ApiResource.WebsiteGroup.DELETE)
                              .Execute()
                              .Post(model);
        }
    }

    public class Scan
    {
        IHttpRequest _webRequest;

        public Scan(IHttpRequest webRequest)
        {
            _webRequest = webRequest;
        }

        public ExecuteResult<List<ScanTaskModel>> New(NewScanTaskApiModel model)
        {
            return _webRequest.CreateRequest(ApiResource.Scans.NEW)
                              .Execute()
                              .Post<List<ScanTaskModel>>(model);
        }

        public ExecuteResult Cancel(Guid scanid)
        {
            return _webRequest.CreateRequest(ApiResource.Scans.CANCEL)
                              .Execute()
                              .Post(scanid);
        }

        public ExecuteResult<ScanTaskModel> Retest(BaseScanApiModel model)
        {
            return _webRequest.CreateRequest(ApiResource.Scans.RETEST)
                              .Execute()
                              .Post<ScanTaskModel>(model);
        }

        public ExecuteResult<ScanTaskModel> Incremental(BaseScanApiModel model)
        {
            return _webRequest.CreateRequest(ApiResource.Scans.INCREMENTAL)
                              .Execute()
                              .Post<ScanTaskModel>(model);
        }

        //ToDo : Response bilgisi yok
        public ExecuteResult Delete(List<Guid> scanids)
        {
            return _webRequest.CreateRequest(ApiResource.Scans.DELETE)
                              .Execute()
                              .Post(scanids);
        }

        public ExecuteResult<ApiScanStatusModel> Status(Guid id)
        {
            return _webRequest.CreateRequestWithQueryString(ApiResource.Scans.STATUS, new { id = id })
                              .Execute()
                              .Get<ApiScanStatusModel>();
        }

        public ExecuteResult<List<VulnerabilityModel>> Result(Guid id)
        {
            return _webRequest.CreateRequestWithQueryString(ApiResource.Scans.RESULT, new { id = id })
                              .Execute()
                              .Get<List<VulnerabilityModel>>();
        }

        public ExecuteResult<PagedListApiResult<ScanTaskModel>> List(int page = 1, int pageSize = 20)
        {
            return _webRequest.CreateRequestWithQueryString(ApiResource.Scans.LIST, new { page = page, pageSize = pageSize })
                              .Execute()
                              .Get<PagedListApiResult<ScanTaskModel>>();
        }

        public ExecuteResult<UpdateScheduledScanModel> Schedule(NewScheduledScanApiModel model)
        {
            return _webRequest.CreateRequest(ApiResource.Scans.SCHEDULE)
                              .Execute()
                              .Post<UpdateScheduledScanModel>(model);
        }

        public ExecuteResult Unschedule(Guid id)
        {
            return _webRequest.CreateRequest(ApiResource.Scans.UNSCHEDULE)
                              .Execute()
                              .Post(id);
        }

        public ExecuteResult<UpdateScheduledScanApiModel> UpdateSchedule(UpdateScheduledScanApiModel model)
        {
            return _webRequest.CreateRequest(ApiResource.Scans.UPDATE_SCHEDULED)
                              .Execute()
                              .Post<UpdateScheduledScanApiModel>(model);
        }

        //ToDo : Dökümanda sayfa dönmüyor. Parametre almasının anlamı ne ? Sorulacak.
        public ExecuteResult<PagedListApiResult<UpdateScheduledScanModel>> ListScheduled(int page = 1, int pageSize = 20)
        {
            return _webRequest.CreateRequestWithQueryString(ApiResource.Scans.LIST_SCHEDULED, new { page = page, pageSize = pageSize })
                              .Execute()
                              .Get<PagedListApiResult<UpdateScheduledScanModel>>();
        }

        //ToDo: Ne döndürecek Reponse da bir bilgi yok
        public ExecuteResult Report(Guid id, ReportType type, ReportFormat format, ContentFormat contentFormat = ContentFormat.Html)
        {
            return _webRequest.CreateRequestWithQueryString(ApiResource.Scans.REPORT,
                                                                new
                                                                {
                                                                    Id = id,
                                                                    Type = type,
                                                                    Format = format,
                                                                    ContentFormat = contentFormat
                                                                })
                                                                .Execute()
                                                                .Get();
        }
    }

    public class ScanPolicy
    {
        IHttpRequest _webRequest;

        public ScanPolicy(IHttpRequest webRequest)
        {
            _webRequest = webRequest;
        }

        public ExecuteResult<PagedListApiResult<ScanPolicySettingItemModel>> List(int page = 1, int pageSize = 20)
        {
            return _webRequest.CreateRequestWithQueryString(ApiResource.ScanPolicy.LIST, new { page = page, pageSize = pageSize })
                              .Execute()
                              .Get<PagedListApiResult<ScanPolicySettingItemModel>>();
        }

        public ExecuteResult<ScanPolicySettingApiModel> New(NewScanPolicySettingModel model)
        {
            return _webRequest.CreateRequest(ApiResource.ScanPolicy.NEW)
                              .Execute()
                              .Post<ScanPolicySettingApiModel>(model);
        }

        public ExecuteResult<ScanPolicyDeleteResult> Delete(string policyName)
        {
            return _webRequest.CreateRequest(ApiResource.ScanPolicy.DELETE)
                              .Execute()
                              .Post<ScanPolicyDeleteResult>(policyName);
        }

        public ExecuteResult<ScanPolicySettingApiModel> Update(UpdateScanPolicySettingModel model)
        {
            return _webRequest.CreateRequest(ApiResource.ScanPolicy.UPDATE)
                              .Execute()
                              .Post<ScanPolicySettingApiModel>(model);
        }

        public ExecuteResult<ScanPolicySettingApiModel> Get(string name)
        {
            return _webRequest.CreateRequestWithQueryString(ApiResource.ScanPolicy.GET, new { name = name })
                              .Execute()
                              .Get<ScanPolicySettingApiModel>();
        }

        public ExecuteResult<ScanPolicySettingApiModel> Get(Guid id)
        {
            return _webRequest.CreateRequestWithQueryString(ApiResource.ScanPolicy.GET, new { id = id })
                              .Execute()
                              .Get<ScanPolicySettingApiModel>();
        }
    }
}