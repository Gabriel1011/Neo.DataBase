using System.IO;
using System.Threading.Tasks;

namespace Neo.DataBaseHelpers.Extensions
{
    public static class StringExtensions
    {
        public static async Task CheckAndCreateDirectory(this string path) =>
                await Task.Run(() => { if (!Directory.Exists(path)) Directory.CreateDirectory(path); });

        public static async Task CheckAndCreateFile(this string fileName, string directory) =>
           await Task.Run(() => { if (!File.Exists(directory + fileName)) File.Create(directory + fileName).DisposeAsync(); });

        public static bool IsNullOrEmpty(this string value) => string.IsNullOrEmpty(value);
            
    }
}
