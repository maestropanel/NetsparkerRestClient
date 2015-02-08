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
    public class NetsparkerClient
    {
        INetsparkerRestApi _api;

        public NetsparkerClient(INetsparkerRestApi api)
        {
            _api = api;
        }

        public Website WebSite()
        {
            return new Website(_api);
        }

        public WebsiteGroup WebSiteGroup()
        {
            return new WebsiteGroup(_api);
        }

        public Scan Scan()
        {
            return new Scan(_api);
        }

        public ScanPolicy ScanPolicy()
        {
            return new ScanPolicy(_api);
        }
    }

    public class Website
    {
        INetsparkerRestApi _api;

        public Website(INetsparkerRestApi api)
        {
            _api = api;
        }

        public ExecuteResult<PagedListApiResult<WebsiteApiModel>> List(int page = 1, int pageSize = 20)
        {
            return _api.Get<PagedListApiResult<WebsiteApiModel>>(new { page = page, pageSize = pageSize }, ApiResource.Website.LIST);
        }

        public ExecuteResult<WebsiteApiModel> New(NewWebsiteApiModel webSite)
        {
            return _api.Post<WebsiteApiModel>(webSite, ApiResource.Website.NEW);
        }

        public ExecuteResult<WebsiteApiModel> Update(UpdateWebsiteApiModel webSite)
        {
            return _api.Post<WebsiteApiModel>(webSite, ApiResource.Website.UPDATE);
        }

        public ExecuteResult<DeleteWebsiteResult> Delete(DeleteWebsiteApiModel model)
        {
            return _api.Post<DeleteWebsiteResult>(model, ApiResource.Website.DELETE);
        }

        public ExecuteResult<VerifyOwnershipResult> Verify(VerifyApiModel model)
        {
            return _api.Post<VerifyOwnershipResult>(model, ApiResource.Website.VERIFY);
        }

        public ExecuteResult<VerifyOwnershipResult> StartVerification(StartVerificationApiModel model)
        {
            return _api.Post<VerifyOwnershipResult>(model, ApiResource.Website.START_VERIFICATION);
        }

        public ExecuteResult<VerifyOwnershipResult> VerificationFile(string websiteUrl)
        {
            return _api.Get<VerifyOwnershipResult>(new { webSiteUrl = websiteUrl }, ApiResource.Website.VERIFICATION_FILE);
        }

        public ExecuteResult<VerifyOwnershipResult> WebSiteSendVerificationEmail(string websiteUrl)
        {
            return _api.Post<VerifyOwnershipResult>(websiteUrl, ApiResource.Website.SEND_VERIFICATION_EMAIL);
        }
    }

    public class WebsiteGroup
    {
        INetsparkerRestApi _api;

        public WebsiteGroup(INetsparkerRestApi api)
        {
            _api = api;
        }

        public ExecuteResult<PagedListApiResult<WebsiteGroupApiModel>> List(int page = 1, int pageSize = 20)
        {
            return _api.Get<PagedListApiResult<WebsiteGroupApiModel>>(new { page = page, pageSize = pageSize }, ApiResource.WebsiteGroup.LIST);
        }

        public ExecuteResult<WebsiteGroupApiModel> New(NewWebsiteGroupApiModel model)
        {
            return _api.Post<WebsiteGroupApiModel>(model, ApiResource.WebsiteGroup.NEW);
        }

        public ExecuteResult<WebsiteApiModel> Update(WebsiteGroupApiModel model)
        {
            return _api.Post<WebsiteApiModel>(model, ApiResource.WebsiteGroup.UPDATE);
        }

        public ExecuteResult<string> Delete(DeleteWebsiteGroupApiModel model)
        {
            return _api.Post<string>(model, ApiResource.WebsiteGroup.DELETE);
        }
    }

    public class Scan
    {
        INetsparkerRestApi _api;

        public Scan(INetsparkerRestApi api)
        {
            _api = api;
        }

        public ExecuteResult<ScanTaskModel> New(NewScanTaskApiModel model)
        {
            return _api.Post<ScanTaskModel>(model, ApiResource.Scans.NEW);
        }

        public ExecuteResult<string> Cancel(Guid scanid)
        {
            return _api.Post<string>(scanid, ApiResource.Scans.NEW);
        }

        public ExecuteResult<ScanTaskModel> Retest(BaseScanApiModel model)
        {
            return _api.Post<ScanTaskModel>(model, ApiResource.Scans.RETEST);
        }

        public ExecuteResult<ScanTaskModel> Incremental(BaseScanApiModel model)
        {
            return _api.Post<ScanTaskModel>(model, ApiResource.Scans.INCREMENTAL);
        }

        public ExecuteResult<string> Delete(List<Guid> scanids)
        {
            return _api.Post<string>(scanids, ApiResource.Scans.DELETE);
        }

        public ExecuteResult<ApiScanStatusModel> Status(Guid id)
        {
            return _api.Get<ApiScanStatusModel>(new { id = id }, ApiResource.Scans.STATUS);
        }

        public ExecuteResult<VulnerabilityModel> Result(Guid id)
        {
            return _api.Get<VulnerabilityModel>(new { id = id }, ApiResource.Scans.RESULT);
        }

        public ExecuteResult<PagedListApiResult<ScanTaskModel>> List(int page = 1, int pageSize = 20)
        {
            return _api.Get<PagedListApiResult<ScanTaskModel>>(new { page = page, pageSize = pageSize }, ApiResource.Scans.LIST);
        }

        public ExecuteResult<UpdateScheduledScanModel> Schedule(NewScheduledScanApiModel model)
        {
            return _api.Post<UpdateScheduledScanModel>(model, ApiResource.Scans.SCHEDULE);
        }

        public ExecuteResult<string> Unschedule(Guid id)
        {
            return _api.Post<string>(id, ApiResource.Scans.UNSCHEDULE);
        }

        public ExecuteResult<UpdateScheduledScanModel> UpdateSchedule(UpdateScheduledScanApiModel model)
        {
            return _api.Post<UpdateScheduledScanModel>(model, ApiResource.Scans.UPDATE_SCHEDULED);
        }

        //ToDo : Dökümanda sayfa dönmüyor. Parametre almasının anlamı ne ? Sorulacak.
        public ExecuteResult<PagedListApiResult<UpdateScheduledScanModel>> ListScheduled(int page = 1, int pageSize = 20)
        {
            return _api.Get<PagedListApiResult<UpdateScheduledScanModel>>(new { page = page, pageSize = pageSize }, ApiResource.Scans.LIST_SCHEDULED);
        }

        //ToDo: Ne döndürecek Reponse da bir bilgi yok
        public ExecuteResult<string> Report(Guid id, ReportType type, ReportFormat format, ContentFormat contentFormat = ContentFormat.Html)
        {
            return _api.Get<string>(new { Id = id, Type = type, Format = format, ContentFormat = contentFormat }, ApiResource.Scans.LIST_SCHEDULED);
        }
    }

    public class ScanPolicy
    {
        INetsparkerRestApi _api;

        public ScanPolicy(INetsparkerRestApi api)
        {
            _api = api;
        }

        public ExecuteResult<PagedListApiResult<ScanPolicySettingItemModel>> List(int page = 1, int pageSize = 20)
        {
            return _api.Get<PagedListApiResult<ScanPolicySettingItemModel>>(new { page = page, pageSize = pageSize }, ApiResource.ScanPolicy.LIST);
        }

        public ExecuteResult<ScanPolicySettingApiModel> New(NewScanPolicySettingModel model)
        {
            return _api.Post<ScanPolicySettingApiModel>(model, ApiResource.ScanPolicy.NEW);
        }

        public ExecuteResult<ScanPolicyDeleteResult> Delete(string policyName)
        {
            return _api.Post<ScanPolicyDeleteResult>(policyName, ApiResource.ScanPolicy.DELETE);
        }

        public ExecuteResult<ScanPolicySettingApiModel> Update(UpdateScanPolicySettingModel webSite)
        {
            return _api.Post<ScanPolicySettingApiModel>(webSite, ApiResource.ScanPolicy.UPDATE);
        }

        public ExecuteResult<ScanPolicySettingApiModel> Get(string name)
        {
            return _api.Get<ScanPolicySettingApiModel>(new { name = name }, ApiResource.ScanPolicy.GET);
        }

        public ExecuteResult<ScanPolicySettingApiModel> Get(Guid id)
        {
            return _api.Get<ScanPolicySettingApiModel>(new { id = id }, ApiResource.ScanPolicy.GET);
        }
    }
}