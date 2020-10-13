namespace Neo.DataBaseHelpers.Configurations
{
    public static class NeoDataBaseonfiguration
    {
        private static string localDataRepository;
        public static string LocalDataRepository
        {
            get => localDataRepository ?? @"NeoDataBase\";
            private set => localDataRepository = value;
        }
        public static void SetLocalDataBaseRepository(string local) => LocalDataRepository = local;
    }
}
