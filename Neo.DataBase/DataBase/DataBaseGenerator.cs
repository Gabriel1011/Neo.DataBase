using Neo.DataBaseHelpers.Configurations;
using Neo.DataBaseHelpers.Extensions;
using Neo.DataBaseHelpers.Vadations;
using Neo.DataBaseRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Neo.DataBaseDataBase
{
    public static class DataBaseGenerator
    {
        public static async Task Generate()
        {
            Validation.ValidateConnection();
            var entitys = await GetEntityNames();
            await NeoDataBaseonfiguration.LocalDataRepository.CheckAndCreateDirectory();
            entitys.ToList().ForEach(async (entity) => { await (entity + FileFormat.Json).CheckAndCreateFile(NeoDataBaseonfiguration.LocalDataRepository); });
            
        }

        private static async Task<IEnumerable<string>> GetEntityNames() =>
            await Task.FromResult(
                AppDomain.CurrentDomain.GetAssemblies()
                       .SelectMany(assembly => assembly.GetTypes())
                       .Where(type => type.IsSubclassOf(typeof(NeoEntity)))
                       .Select(p => p.Name));
    }
}
