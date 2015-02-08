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
            public const string SEND_VERIFICATION_EMAIL = PREFIX + "/sendverificationemail";
        }

        public static class WebsiteGroup 
        {
            private const string PREFIX = "/websitegroups";

            public const string LIST = PREFIX + "/list";

            public const string NEW = PREFIX + "/new";
            public const string UPDATE = PREFIX + "/update";
            public const string DELETE = PREFIX + "/delete";
        }

        public static class Scans
        {
            private const string PREFIX = "/scans";

            public const string LIST = PREFIX + "/list";

            public const string NEW = PREFIX + "/new";
            public const string UPDATE = PREFIX + "/update";
            public const string DELETE = PREFIX + "/delete";

            public const string RETEST = PREFIX + "/retest";
            public const string INCREMENTAL = PREFIX + "/incremental";
            public const string STATUS = PREFIX + "/status";
            public const string CANCEL = PREFIX + "/cancel";
            public const string RESULT = PREFIX + "/result";
            public const string SCHEDULE = PREFIX + "/schedule";
            public const string UNSCHEDULE = PREFIX + "/unschedule";
            public const string UPDATE_SCHEDULED = PREFIX + "/update-scheduled";
            public const string LIST_SCHEDULED = PREFIX + "/list-scheduled";
            public const string REPORT = PREFIX + "/report";
        }

        public static class ScanPolicy 
        {
            private const string PREFIX = "/scanpolicies";

            public const string LIST = PREFIX + "/list";

            public const string NEW = PREFIX + "/new";
            public const string UPDATE = PREFIX + "/update";
            public const string DELETE = PREFIX + "/delete";
            public const string GET = PREFIX + "/get";
        }
    }
}
