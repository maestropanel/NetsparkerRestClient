using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaestroPanel.NetsparkerClient.Model
{
    public class WebsiteGroupApiModel
    {
        public Guid Id { get; set; }
        public int TotalWebsites { get; set; }
        public string Name { get; set; }
    }

    public class WebsiteGroupModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class NewWebsiteGroupApiModel
    {
        public string Name { get; set; }
    }

    public class DeleteWebsiteGroupApiModel
    {
        public string Name { get; set; }
    }
}
