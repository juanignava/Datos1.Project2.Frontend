

using System.IO;
using System.Linq;

namespace CookTime.FileHelpers
{
    public class ReadStringConverter
    {
        public static string ChangePostString(string str)
        {
            string result = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ' ')
                {
                    result += "_";
                }
                
                else
                {
                    result += str[i];
                }
            }

            return result;
        }

        public static string ChangeGetString(string str)
        {
            string result = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '_')
                {
                    result += " ";
                }

                else
                {
                    result += str[i];
                }
            }

            return result;
        }

        public static string BytesToString(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }

        public static string Base64toString (string base64)
        {
            string result = "";
            for (int i = 0; i < base64.Length; i++)
            {
                if (base64[i] == '/')
                {
                    result += "_";
                }

                else if (base64[i] == '+')
                {
                    result += '-';
                }
                else
                { 
                    result += base64[i];
                }
            }

            return result;
        }


        public static string StringToBase64(string str)
        {
            string result = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '_')
                {
                    result += "/";
                }

                else if (str[i] == '-')
                {
                    result += '+';
                }
                else
                {
                    result += str[i];
                }
            }

            return result;
        }
    }
}
