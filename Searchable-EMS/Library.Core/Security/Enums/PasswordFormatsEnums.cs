using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Security
{
    /// <summary>
    /// 
    /// </summary>
    public enum PasswordFormats
    {
        CLEAR = 1,
        ONE_WAY_HASHED = 2,
        ENCRYPTED_AES = 3,
        ENCRYPTED_RSA = 4
    }
}
