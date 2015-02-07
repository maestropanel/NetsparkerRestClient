using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaestroPanel.NetsparkerClient.Model
{
    public class WebsiteNewOrUpdate
    {
        public string Name { get; set; }
        public string RootUrl { get; set; }
        public List<string> Groups { get; set; }
        public BasicAuthentication BasicAuthentication { get; set; }
        public FormAuthentication FormAuthentication { get; set; }
        public ClientCertificateAuthentication ClientCertificateAuthentication { get; set; }
    }

    public class Website
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

    public abstract class Authentication
    {
        public bool IsConfigured { get; set; }
    }

    public class BasicAuthentication : Authentication
    {
        public string Username { get; set; }
    }

    public class ClientCertificateAuthentication : Authentication
    {

    }

    public class FormAuthentication : Authentication
    {
        public FormAuthentication()
        {
            Personas = new List<Person>();
        }

        public string LoginUrl { get; set; }
        public List<Person> Personas { get; set; }
    }

    public class Person
    {
        public string Username { get; set; }
        public bool IsDefault { get; set; }
    }
}
