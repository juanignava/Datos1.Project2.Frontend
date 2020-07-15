

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
    }
}
