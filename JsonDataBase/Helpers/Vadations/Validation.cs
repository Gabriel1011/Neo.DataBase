using Neo.DataBaseHelpers.Configurations;
using Neo.DataBaseHelpers.Extensions;
using System;

namespace Neo.DataBaseHelpers.Vadations
{
    public static class Validation
    {
        public static void ValidateConnection()
        {
            if (NeoDataBaseonfiguration.LocalDataRepository.IsNullOrEmpty())
                throw new Exception("LocalDataRepository is requerid to use this package, use Neo.DataBaseonfiguration.SetLocalDataBaseRepository(local) to set LocalDataRepository.");

            if (!NeoDataBaseonfiguration.LocalDataRepository.EndsWith(@"\"))
                NeoDataBaseonfiguration.SetLocalDataBaseRepository(NeoDataBaseonfiguration.LocalDataRepository + @"\");
        }
    }
}
