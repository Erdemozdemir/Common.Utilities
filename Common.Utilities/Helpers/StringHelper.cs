using Common.Utilities.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utilities.Helpers
{
    public class StringHelper
    {
        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }
        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }

            return false;
            //return ByteArraysEqual(buffer3, buffer4);
        }

        public static Int64 ConvertDoubleToInt64(string value)
        {
            return Convert.ToInt64(value.Substring(0, value.IndexOf('.') > 0 ? value.IndexOf('.') : value.Length));
        }

        public static string ToShortDateString(DateTime dateTime)
        {
            return CastDateToStringOrEmpty(dateTime);
        }

        public static string ToShortDateString(DateTime? dateTime)
        {
            return CastDateToStringOrEmpty(dateTime);
        }


        private static string CastDateToStringOrEmpty(DateTime? datetime)
        {
            if (datetime.HasValue)
                return datetime.Value.ToString(ParameterConsts.ShortDateFormat);

            return "";
        }
    }
}
