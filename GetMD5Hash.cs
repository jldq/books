using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace books
{
    class GetMD5Hash
    {
        public static string MD5(string strSource)
        {
            byte[] data = Encoding.Unicode.GetBytes(strSource);//取得输入字符串的字节数组
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();//一个加密对象
            byte[] hash_byte = md5.ComputeHash(data);//用加密对象对字节数组加密,得到字节数组
            string result = System.BitConverter.ToString(hash_byte);//将加密后的字节数组转化为字符串
            return result;
        }
    }
}
