namespace JsonDataBase.Helpers.Configurations
{
    public static class JsonDataBaseConfiguration
    {
        private static string localDataRepository;
        public static string LocalDataRepository
        {
            get => localDataRepository ?? @"JsonDataBase\";
            private set => localDataRepository = value;
        }
        public static void SetLocalDataBaseRepository(string local) => LocalDataRepository = local;
    }
}
