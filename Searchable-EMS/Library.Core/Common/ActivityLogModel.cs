using Library.Core.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core
{
    public class ActivityLogModel
    {
        public ActivityLogModel()
        {
            SecurityPolicy policy = new SecurityPolicy();
            UserId = new Guid(policy.UserId());
            IpAddress= IpHelper.GetPublicIPAddress();
        }
        public Guid UserId { get; set; } 
        public string IpAddress { get; set; }
    }
}
