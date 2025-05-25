using Microsoft.Data.Sqlite;

namespace ServicosGlobais.Data
{
    public class ConectaDb
    {
        string conString = string.Empty;
        public ConectaDb()
        {
            string dbPath = Utils.DatabaseConfig.GetDatabasePath();
            conString = $"Data Source={dbPath}";
        }

        public SqliteConnection RetornaSqliteConexao()
        {
            try
            {
                SqliteConnection sqliteConnection = new SqliteConnection(conString);
                return sqliteConnection;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool TestaConexao()
        {
            using (var connection = new SqliteConnection(conString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
