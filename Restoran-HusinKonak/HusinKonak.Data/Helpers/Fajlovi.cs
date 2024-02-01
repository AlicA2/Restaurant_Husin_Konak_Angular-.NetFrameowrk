using System;
using System.IO;

namespace FIT_Api_Examples.Helper
{
    public class Fajlovi
    {
        public static byte[]? Ucitaj(string path)
        {
            try
            {
                return System.IO.File.ReadAllBytes(path);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static void Snimi(byte[] bajtoviBytes, string path)
        {
            var directoryName = Path.GetDirectoryName(path);
            if (directoryName != null)
                System.IO.Directory.CreateDirectory(directoryName);

            using var fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            fs.Write(bajtoviBytes, 0, bajtoviBytes.Length);
            fs.Close();
        }
        public static byte[] ConcatenateByteArrays(List<byte[]> arrays)
        {
            int totalLength = arrays.Sum(array => array.Length);
            byte[] result = new byte[totalLength];
            int offset = 0;

            foreach (var array in arrays)
            {
                Buffer.BlockCopy(array, 0, result, offset, array.Length);
                offset += array.Length;
            }

            return result;
        }

    }
}