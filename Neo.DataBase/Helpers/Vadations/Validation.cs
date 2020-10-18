using Neo.DataBaseHelpers.Configurations;
using Neo.DataBaseHelpers.Extensions;
using System;

namespace Neo.DataBaseHelpers.Vadations
{
    internal static class Validation
    {
        internal static void ValidateConnection()
        {
            if (NeoDataBaseConfiguration.LocalDataRepository.IsNullOrEmpty())
                throw new Exception("LocalDataRepository is requerid to use this package, use Neo.DataBaseonfiguration.SetLocalDataBaseRepository(local) to set LocalDataRepository.");

            if (!NeoDataBaseConfiguration.LocalDataRepository.EndsWith(@"\"))
                NeoDataBaseConfiguration.SetLocalDataBaseRepository(NeoDataBaseConfiguration.LocalDataRepository + @"\");
        }
    }
}
