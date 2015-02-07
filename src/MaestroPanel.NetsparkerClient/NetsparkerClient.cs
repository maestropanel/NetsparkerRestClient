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

        /// <summary>
        /// List of website
        /// </summary>
        /// <param name="page">List page number</param>
        /// <param name="pageSize">Page row count</param>
        /// <returns></returns>
        public ExecuteResult<Page<Website>> WebsiteList(int page = 1, int pageSize = 20)
        {
            return _api.Get<Page<Website>>(new { page = page, pageSize = pageSize }, ApiResource.Website.LIST);
        }

        /// <summary>
        /// Create web site
        /// </summary>
        /// <param name="webSite">Website model</param>
        public ExecuteResult<Website> WebSiteAdd(WebsiteNewOrUpdate webSite)
        {
            return _api.Post<Website>(webSite, ApiResource.Website.NEW);
        }

        /// <summary>
        /// Update web site
        /// </summary>
        /// <param name="webSite">Website model</param>
        public ExecuteResult<Website> WebSiteUpdate(WebsiteNewOrUpdate webSite)
        {
            return _api.Post<Website>(webSite, ApiResource.Website.UPDATE);
        }

        /// <summary>
        /// Delete web site
        /// </summary>
        /// <param name="webSite">website rootUrl ex: http://demo.maestrodemo.com</param>
        public ExecuteResult<string> WebSiteDelete(string rootUrl)
        {
            return _api.Post<string>(new { RootUrl = rootUrl }, ApiResource.Website.DELETE);
        }
 
        ///<summary>
        /// List of website group
        /// </summary>
        /// <param name="page">List page number</param>
        /// <param name="pageSize">Page row count</param>
        /// <returns></returns>
        public ExecuteResult<Page<WebsiteGroup>> WebsiteGroupList(int page = 1, int pageSize = 20)
        {
            return _api.Get<Page<WebsiteGroup>>(new { page = page, pageSize = pageSize }, ApiResource.WebsiteGroup.LIST);
        }

        public ExecuteResult<WebsiteGroup> WebsiteGroupAdd(WebsiteGroup websiteGroup)
        {
            return _api.Post<WebsiteGroup>(websiteGroup, ApiResource.WebsiteGroup.NEW);
        }
    }
}