using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CookTime.FileHelpers
{
    public class FileHelper
    {
        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
