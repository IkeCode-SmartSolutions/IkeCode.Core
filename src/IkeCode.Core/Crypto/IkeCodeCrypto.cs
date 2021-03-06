﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkeCode.Core.Crypto
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public static class IkeCodeCrypto
    {
        #region MD5

        public static string HashMD5(string phrase)
        {
            if (phrase == null)
                return null;
            var encoder = new UTF8Encoding();
            using (var md5Hasher = MD5.Create())
            {
                var hashedDataBytes = md5Hasher.ComputeHash(encoder.GetBytes(phrase));
                return ByteArrayToHexString(hashedDataBytes);
            }
        }

        #endregion

        #region SHA

        public static string HashSHA1(string phrase)
        {
            if (phrase == null)
                return null;
            var encoder = new UTF8Encoding();
            using (var sha1Hasher = SHA1.Create())
            {
                var hashedDataBytes = sha1Hasher.ComputeHash(encoder.GetBytes(phrase));
                return ByteArrayToHexString(hashedDataBytes);
            }
        }

        public static string HashSHA256(string phrase)
        {
            if (phrase == null)
                return null;
            var encoder = new UTF8Encoding();
            using (var sha256Hasher = SHA256.Create())
            {
                var hashedDataBytes = sha256Hasher.ComputeHash(encoder.GetBytes(phrase));
                return ByteArrayToHexString(hashedDataBytes);
            }
        }

        public static string HashSHA384(string phrase)
        {
            if (phrase == null)
                return null;
            var encoder = new UTF8Encoding();
            using (var sha384Hasher = SHA384.Create())
            {
                var hashedDataBytes = sha384Hasher.ComputeHash(encoder.GetBytes(phrase));
                return ByteArrayToHexString(hashedDataBytes);
            }
        }

        public static string HashSHA512(string phrase)
        {
            if (phrase == null)
                return null;
            var encoder = new UTF8Encoding();
            using (var sha512Hasher = SHA512.Create())
            {
                var hashedDataBytes = sha512Hasher.ComputeHash(encoder.GetBytes(phrase));
                return ByteArrayToHexString(hashedDataBytes);
            }
        }

        #endregion

        #region AES

        public static string EncryptAES(string phrase, string key, bool hashKey = true)
        {
            if (phrase == null || key == null)
                return null;

            var keyArray = HexStringToByteArray(hashKey ? HashMD5(key) : key);
            var toEncryptArray = Encoding.UTF8.GetBytes(phrase);
            byte[] result;

            using (var aes = Aes.Create())
            {
                var cTransform = aes.CreateEncryptor();
                result = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            }
            return ByteArrayToHexString(result);
        }

        public static string DecryptAES(string hash, string key, bool hashKey = true)
        {
            if (hash == null || key == null)
                return null;

            var keyArray = HexStringToByteArray(hashKey ? HashMD5(key) : key);
            var toEncryptArray = HexStringToByteArray(hash);

            using (var aes = Aes.Create())
            {
                var cTransform = aes.CreateDecryptor();
                var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                
                return Encoding.UTF8.GetString(resultArray);
            }
        }

        #endregion

        #region 3DES

        public static string EncryptTripleDES(string phrase, string key, bool hashKey = true)
        {
            var keyArray = HexStringToByteArray(hashKey ? HashMD5(key) : key);
            var toEncryptArray = Encoding.UTF8.GetBytes(phrase);

            using (var tdes = TripleDES.Create())
            {
                var cTransform = tdes.CreateEncryptor();
                var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                
                return ByteArrayToHexString(resultArray);
            }
        }

        public static string DecryptTripleDES(string hash, string key, bool hashKey = true)
        {
            var keyArray = HexStringToByteArray(hashKey ? HashMD5(key) : key);
            var toEncryptArray = HexStringToByteArray(hash);

            using (var tdes = TripleDES.Create())
            {
                var cTransform = tdes.CreateDecryptor();
                var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                
                return Encoding.UTF8.GetString(resultArray);
            }
        }

        #endregion

        #region Helpers

        internal static string ByteArrayToHexString(byte[] inputArray)
        {
            if (inputArray == null)
                return null;
            var o = new StringBuilder("");
            for (var i = 0; i < inputArray.Length; i++)
                o.Append(inputArray[i].ToString("X2"));
            return o.ToString();
        }

        internal static byte[] HexStringToByteArray(string inputString)
        {
            if (inputString == null)
                return null;

            if (inputString.Length == 0)
                return new byte[0];

            if (inputString.Length % 2 != 0)
                throw new Exception("Hex strings have an even number of characters and you have got an odd number of characters!");

            var num = inputString.Length / 2;
            var bytes = new byte[num];
            for (var i = 0; i < num; i++)
            {
                var x = inputString.Substring(i * 2, 2);
                try
                {
                    bytes[i] = Convert.ToByte(x, 16);
                }
                catch (Exception ex)
                {
                    throw new Exception("Part of your \"hex\" string contains a non-hex value.", ex);
                }
            }
            return bytes;
        }

        #endregion
    }
}
