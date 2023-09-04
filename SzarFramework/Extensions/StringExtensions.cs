using System;
using System.IO;
using System.Reflection;

namespace SzarFramework
{
    public static class StringExtensions 
    {
        public static Type[] GetTypesFromAssembly(this string assemblyName)
        {
            return Assembly.Load(assemblyName).GetTypes();
        }

        /// <summary>
        /// Converte o arquivo passado no caminho para string base 64
        /// </summary>
        /// <param name="path">Caminho do arquivo</param>
        /// <returns></returns>
        public static string ConvertObjectToString(this string path)
        {
            FileStream stream = File.OpenRead(path);
            byte[] fileBytes = new byte[stream.Length];
            stream.Read(fileBytes, 0, fileBytes.Length);
            stream.Close();
            return Convert.ToBase64String(fileBytes);

        }

        /// <summary>
        /// Converte a string base 64 para byte[]
        /// </summary>
        /// <param name="fileString">String do arquivo</param>
        /// <returns></returns>
        public static byte[] ConvertStringToByte(this string fileString)
        {
            return Convert.FromBase64String(fileString);
        }
    }
}
