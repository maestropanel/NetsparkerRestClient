using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaestroPanel.NetsparkerClient
{
    public static class ApiResource
    {
        public static class Website
        {
            private const string PREFIX = "/websites";

            public const string LIST = PREFIX + "/list";

            public const string NEW = PREFIX + "/new";
            public const string UPDATE = PREFIX + "/update";
            public const string DELETE = PREFIX + "/delete";

            public const string VERIFY = PREFIX + "/verify";
            public const string START_VERIFICATION = PREFIX + "/startverification";
            public const string VERIFICATION_FILE = PREFIX + "/verificationfile";
            public const string SEND_VERIFICATION_FILE = PREFIX + "/sendverificationemail";
        }

        public static class WebsiteGroup 
        {
            private const string PREFIX = "/websitegroups";

            public const string LIST = PREFIX + "/list";

            public const string NEW = PREFIX + "/new";
            public const string UPDATE = PREFIX + "/update";
            public const string DELETE = PREFIX + "/delete";
        }
    }
}
