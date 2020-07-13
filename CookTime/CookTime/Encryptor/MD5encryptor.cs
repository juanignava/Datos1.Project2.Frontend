

namespace CookTime.Encryptor
{
    using System.Security.Cryptography;
    using System.Text;
    using Xamarin.Forms;

    public static class MD5encryptor
    {
        /*
         * The only method of this class encrypts a text in Hash MD5
         */
        public static string MD5Hash (string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //Compute hash from the bytes of the text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < result.Length; i++)
            {
                //Change it into 2 hexadecimal digits

                stringBuilder.Append(result[i].ToString("x2"));
            }

            return stringBuilder.ToString();
        }
    }
}
