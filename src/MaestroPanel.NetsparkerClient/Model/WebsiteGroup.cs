using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaestroPanel.NetsparkerClient.Model
{
    public class WebsiteGroup
    {
        public Guid Id { get; set; }
        public int TotalWebsites { get; set; }
        public string Name { get; set; }
    }
}
