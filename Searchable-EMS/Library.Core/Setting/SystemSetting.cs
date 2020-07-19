using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core
{
    public partial class SystemSetting
    {
        public static string ObjectQualifer = GetObjectQualifer();
        public static string DataBaseOwner = GetDataBaseOwner();
        public static string SageFrameConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DataBaseConnectionString"].ToString();
        /// <summary>
        /// Return database owner.
        /// </summary>
        /// <returns>Database owner.</returns>
        public static string GetDataBaseOwner()
        {
            string _databaseOwner = System.Configuration.ConfigurationManager.AppSettings["databaseOwner"].ToString();
            if (_databaseOwner != "" && _databaseOwner.EndsWith(".") == false)
            {
                _databaseOwner += ".";
            }
            return _databaseOwner;
        }
        /// <summary>
        /// Return object qualifer.
        /// </summary>
        /// <returns>Object qualifer.</returns>
        public static string GetObjectQualifer()
        {
            string _objectQualifier = System.Configuration.ConfigurationManager.AppSettings["objectQualifier"].ToString();
            if ((_objectQualifier != "") && (_objectQualifier.EndsWith("_") == false))
            {
                _objectQualifier += "_";
            }
            return _objectQualifier;
        }
    }
}
