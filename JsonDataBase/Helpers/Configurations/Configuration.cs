namespace JsonDataBase.Helpers.Configurations
{
    public static class Configuration
    {
        public static string  LocalDataRepository { get; private set; }
        public static void SetLocalDataBaseRepository(string local) => LocalDataRepository = local;
    }
}
