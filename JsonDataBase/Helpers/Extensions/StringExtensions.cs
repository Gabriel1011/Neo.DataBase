using System.IO;
using System.Threading.Tasks;

namespace JsonDataBase.Helpers.Extensions
{
    public static class StringExtensions
    {
        public static async Task CheckAndCreateDirectory(this string caminho) =>
            await Task.Run(() => { if (!Directory.Exists(caminho)) Directory.CreateDirectory(caminho); });

        public static async Task CheckAndCreateFile(this string caminho) =>
           await Task.Run(() => { if (!File.Exists(caminho)) File.Create(caminho); });
    }
}
