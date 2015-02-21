using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MaestroPanel.NetsparkerClient.Model
{
    public class NewWebsiteApiModel
    {
        /// <summary>
        /// Required 
        /// Max length: 99
        /// </summary>                
        public string Name { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        public string RootUrl { get; set; }
        public List<string> Groups { get; set; }
        public BasicAuthentication BasicAuthentication { get; set; }
        public FormAuthentication FormAuthentication { get; set; }
        public ClientCertificateAuthentication ClientCertificateAuthentication { get; set; }
    }

    public class UpdateWebsiteApiModel
    {
        /// <summary>
        /// Required
        /// Max length: 99
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        public string RootUrl { get; set; }
        public List<string> Groups { get; set; }
        public BasicAuthentication BasicAuthentication { get; set; }
        public FormAuthentication FormAuthentication { get; set; }
        public ClientCertificateAuthentication ClientCertificateAuthentication { get; set; }
    }

    public class WebsiteApiModel
    {
        public Guid Id { get; set; }
        public string RootUrl { get; set; }
        public string Name { get; set; }
        public List<string> Groups { get; set; }
        public bool IsVerified { get; set; }
        public BasicAuthentication BasicAuthentication { get; set; }
        public FormAuthentication FormAuthentication { get; set; }
        public ClientCertificateAuthentication ClientCertificateAuthentication { get; set; }
    }

    public class DeleteWebsiteApiModel
    {
        /// <summary>
        /// Required
        /// </summary>
        public string RootUrl { get; set; }
    }

    public class VerifyApiModel
    {
        public VerificationMethod VerificationMethod { get; set; }
        public string VerificationSecret { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        public string WebsiteUrl { get; set; }
    }

    public enum VerificationMethod
    {
        File,
        Tag,
        Dns,
        Email,
    }

    public class StartVerificationApiModel
    {
        /// <summary>
        /// Required
        /// </summary>
        public VerificationMethod VerificationMethod { get; set; }

        /// <summary>
        /// Required
        /// </summary>
        public string WebsiteUrl { get; set; }
    }

    public abstract class Authentication
    {
        public bool IsConfigured { get; set; }
    }

    public class BasicAuthentication : Authentication
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Domain { get; set; }
    }

    public class ClientCertificateAuthentication : Authentication
    {
        /// <summary>
        /// Required
        /// </summary>
        public HttpPostedFileBase Certificate { get; set; }

        public string Password { get; set; }
    }

    public class FormAuthentication : Authentication
    {
        public FormAuthentication()
        {
            Personas = new List<FormAuthenticationPerson>();
        }

        /// <summary>
        /// Required
        /// </summary>
        public string LoginFormUrl { get; set; }
        public List<FormAuthenticationPerson> Personas { get; set; }
    }

    public class FormAuthenticationPerson
    {
        /// <summary>
        /// Required
        /// </summary>
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsDefault { get; set; }
    }

    public enum VerifyOwnershipResult
    {
        Verified,
        NotVerified,
        VerificationLimitExceed
    }

    public enum DeleteWebsiteResult
    {
        Ok,
        NotFound,
        OnGoingScanExisting
    }
}
