using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Security
{
    public class SuspendedIPModel
    {
        public SuspendedIPModel()
        {

        }
        /// <summary>
        /// IpAddressID
        /// </summary>
        public int IPAddressID { get; set; }
        /// <summary>
        /// IpAddress
        /// </summary>
        public string IpAddress { get; set; }
        /// <summary>
        /// SuspendedTime
        /// </summary>
        public string SuspendedTime { get; set; }
        /// <summary>
        /// IsSuspended
        /// </summary>
        public bool IsSuspended { get; set; }
    }
}
