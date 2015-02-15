using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaestroPanel.NetsparkerClient.Model
{
    public class WebsiteGroupApiModel
    {
        /// <summary>
        /// Required
        /// </summary>
        public Guid Id { get; set; }
        public int TotalWebsites { get; set; }

        /// <summary>
        /// Required
        /// Max length: 99
        /// </summary>
        public string Name { get; set; }
    }

    public class WebsiteGroupModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class NewWebsiteGroupApiModel
    {
        /// <summary>
        /// Required
        /// Max length: 99
        /// </summary>
        public string Name { get; set; }
    }

    public class DeleteWebsiteGroupApiModel
    {
        /// <summary>
        /// Required
        /// Max length: 99
        /// </summary>
        public string Name { get; set; }
    }
}
