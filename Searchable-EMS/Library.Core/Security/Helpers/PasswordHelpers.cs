using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Core.Security;
namespace Library.Core.Security.Helpers
{
    public class PasswordHelpers
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PasswordFormat"></param>
        /// <param name="passwordText"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        /// <returns></returns>
        public static bool ValidateUser(int PasswordFormat, string passwordText, string passwordHash, string passwordSalt)
        {
            Crypto crypto = new Crypto();
            bool verificationStatus = false;
            switch (PasswordFormat)
            {
                case (int)PasswordFormats.CLEAR:
                case (int)PasswordFormats.ONE_WAY_HASHED:
                    verificationStatus = crypto.VerifyHashString(passwordText, passwordHash, passwordSalt);
                    break;
                case (int)PasswordFormats.ENCRYPTED_AES:
                    verificationStatus = Crypto.Decrypt(passwordHash) == passwordText ? true : false;
                    break;
                case (int)PasswordFormats.ENCRYPTED_RSA:
                    break;

            }
            return verificationStatus;
        }
    }
}
