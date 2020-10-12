using JsonDataBase.Helpers.Configurations;
using JsonDataBase.Helpers.Extensions;
using JsonDataBase.Helpers.Vadations;
using JsonDataBase.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JsonDataBase.DataBase
{
    public static class DataBaseGenerator
    {
        public static async Task Generate()
        {
            Validation.ValidateConnection();
            var entitys = await GetEntityNames();
            await JsonDataBaseConfiguration.LocalDataRepository.CheckAndCreateDirectory();
            entitys.ToList().ForEach(async (entity) => { await (entity + FileFormat.Json).CheckAndCreateFile(JsonDataBaseConfiguration.LocalDataRepository); });
            
        }

        private static async Task<IEnumerable<string>> GetEntityNames() =>
            await Task.FromResult(
                AppDomain.CurrentDomain.GetAssemblies()
                       .SelectMany(assembly => assembly.GetTypes())
                       .Where(type => type.IsSubclassOf(typeof(JsonEntity)))
                       .Select(p => p.Name));
    }
}
