namespace ServicosGlobais.Utils
{
    public class DatabaseConfig
    {
        public static string GetDatabasePath()
        {
            //Navega até a pasta Db, na raíz da Solution
            var path = Path.Combine(AppContext.BaseDirectory, "..", "Db", "RenoveBackupConfigDb.db");
            if (File.Exists(path))
            {
                return path;
            }
            return path;
        }
    }
}
