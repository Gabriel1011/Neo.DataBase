using JsonDataBase.Helpers.Configurations;
using JsonDataBase.Helpers.Extensions;
using System;

namespace JsonDataBase.Helpers.Vadations
{
    public static class Validation
    {
        public static void ValidateConnection()
        {
            if (JsonDataBaseConfiguration.LocalDataRepository.IsNullOrEmpty())
                throw new Exception("LocalDataRepository is requerid to use this package, use JsonDataBaseConfiguration.SetLocalDataBaseRepository(local) to set LocalDataRepository.");

            if (!JsonDataBaseConfiguration.LocalDataRepository.EndsWith(@"\"))
                JsonDataBaseConfiguration.SetLocalDataBaseRepository(JsonDataBaseConfiguration.LocalDataRepository + @"\");
        }
    }
}
