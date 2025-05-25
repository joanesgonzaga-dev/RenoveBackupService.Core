using FluentFTP;
using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using ServicosGlobais.Data;
using ServicosGlobais.Model;
using ServicosGlobais.Model.Enum;
using System.Text;
using Serilog;

namespace ServicosGlobais.Services
{
    public class ConfigService : IConfigService
    {
        private static ILogger _fileLogger;
        public ConfigService()
        {
            string fileName = DateTime.Now.ToString("ddMMyyyy");
            _fileLogger = new LoggerConfiguration().WriteTo.Console().WriteTo.File($"logs\\{fileName}.txt", rollingInterval: RollingInterval.Day).CreateLogger();
        }

        public int SalvarCaminhoMySqlDumpExe(FileInfo fileInfo)
        {
            try
            {
                if (fileInfo.Exists)
                {
                    using (var con = new ConectaDb().RetornaSqliteConexao())
                    {
                        con.Open();
                        using (var cmd = con.CreateCommand())
                        {
                            cmd.Connection = con;
                            string sql = @"INSERT OR REPLACE INTO MysqldumpConfig (Id, MysqldumpPath) VALUES (1, @MysqldumpPath);";
                            cmd.CommandText = sql;
                            cmd.Parameters.AddWithValue("@MysqldumpPath", fileInfo.FullName);
                            return cmd.ExecuteNonQuery();
                        }
                    }
                }
                return 0;
            }
            catch (Exception)
            {
                _fileLogger.Error("Ocorreu um erro ao tentar salvar o caminho do executável MySqldump");
                throw;
            }
        }

        /// <summary>
        /// Retorna do banco de dados as informações do executável Mysqldump.exe
        /// </summary>
        /// <returns></returns>
        public FileInfo? RetornaMysqldumpExeInfoFromDb()
        {
            string filePath = string.Empty;
            try
            {
                using (var con = new ConectaDb().RetornaSqliteConexao())
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "SELECT MysqldumpPath FROM MysqldumpConfig WHERE Id = 1";
                        using (SqliteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                filePath = Convert.IsDBNull(dr.GetString(0)) ? string.Empty : dr.GetString(0);
                            }
                            // Verifica se o caminho não está vazio antes de retornar
                            return !string.IsNullOrWhiteSpace(filePath) ? new FileInfo(filePath) : null;
                        }
                    }
                }
            }
            catch (Exception)
            {
                _fileLogger.Error("Ocorreu um erro ao recuperar o caminho do MySqlDump.exe a partir do banco de dados");
                throw;
            }
        }

        public List<DiaDaSemana>? RetornaDiasDaSemanaAgendamento()
        {
            try
            {
                using (var con = new ConectaDb().RetornaSqliteConexao())
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.Connection = con;
                        string query = "SELECT Ordem, DiaDaSemana, isExecutar FROM DiasDaSemana ORDER BY Ordem";
                        cmd.CommandText = query;
                        SqliteDataReader reader = cmd.ExecuteReader();

                        List<DiaDaSemana> dias = new List<DiaDaSemana>();
                        while (reader.Read())
                        {
                            DiaDaSemana dia = new DiaDaSemana
                            {
                                Ordem = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                isAtivo = reader.GetBoolean(2)
                            };

                            dias.Add(dia);
                        }

                        return dias;
                    }
                }
            }
            catch (Exception)
            {
                _fileLogger.Error("Ocorreu um erro ao tentar ler os dias da semana");
                throw;
            }
            
        }

        public CronExpressionModel RetornaExpressaoCronDoBd()
        {
            try
            {
                using (var con = new ConectaDb().RetornaSqliteConexao())
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.Connection = con;
                        string query = "SELECT CronDescription, CronExpression, Second, Minute, Hour, DayOfMonth, Month, DaysOfWeek, RecurrenceTime, RecurrenceType FROM CronQuartzSettings";
                        cmd.CommandText = query;
                        using (SqliteDataReader reader = cmd.ExecuteReader())
                        {
                            reader.Read();
                            CronExpressionModel cronExpression = new CronExpressionModel()
                            {
                                Seconds = reader.GetString(2) ?? string.Empty,
                                Minutes = reader.GetString(3) ?? string.Empty,
                                Hours = reader.GetString(4) ?? string.Empty,
                                DayOfMonth = reader.GetString(5) ?? string.Empty,
                                Month = reader.GetString(6) ?? string.Empty,
                                DayOfWeek = reader.GetString(7) ?? string.Empty,
                                RecurrenceTime = reader.GetString(8) ?? string.Empty,
                                RecurrenceType = reader.GetString(9) ?? string.Empty
                            };

                            return cronExpression;
                        }
                    }
                }
            }
            catch (Exception)
            {
                _fileLogger.Error("Ocorreu um erro ao tentar ler a expressão CRON a partir do banco de dados");
                throw;
            }
        }

        public string RetornaExpressaoCronEmString(CronExpressionModel? cronExpressionModel)
        {
            try
            {
                if (cronExpressionModel == null)
                {
                    cronExpressionModel = RetornaExpressaoCronDoBd();
                }

                EnumRecorrenciaBackup tipoDeRecorrencia = (EnumRecorrenciaBackup)int.Parse(cronExpressionModel.RecurrenceType);
                StringBuilder cronStringBuinder = new StringBuilder();
                cronStringBuinder.Append(cronExpressionModel.Seconds).Append(" ");
                cronStringBuinder.Append(cronExpressionModel.Minutes).Append(tipoDeRecorrencia == EnumRecorrenciaBackup.Minutos ? $"/{cronExpressionModel.RecurrenceTime} " : " ");
                cronStringBuinder.Append(cronExpressionModel.Hours).Append(tipoDeRecorrencia == EnumRecorrenciaBackup.Horas ? $"/{cronExpressionModel.RecurrenceTime} " : " ");
                cronStringBuinder.Append(cronExpressionModel.DayOfMonth).Append(" ");
                cronStringBuinder.Append(cronExpressionModel.Month).Append(" ");
                cronStringBuinder.Append(cronExpressionModel.DayOfWeek);
                return cronStringBuinder.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void GravaCronExpressionNoBd(CronExpressionModel model)
        {
            try
            {
                using (SqliteConnection con = new ConectaDb().RetornaSqliteConexao())
                {
                    con.Open();
                    string cronExpressionString = RetornaExpressaoCronEmString(model);
                    using (SqliteTransaction _transaction = con.BeginTransaction())
                    {
                        using (SqliteCommand cmd = con.CreateCommand())
                        {
                            cmd.Connection = con;
                            cmd.CommandText = @"UPDATE CronQuartzSettings SET CronExpression = @cronExpressionString, Second = @segundo, Minute = @minuto, Hour = @hora, DayOfMonth = @diaDoMes, Month = @mes, DaysOfWeek = @diasDaSemana, RecurrenceTime = @TempoDeRepeticao, RecurrenceType = @tipoDeRepeticao WHERE Id = 1;";
                            cmd.Parameters.AddWithValue("@cronExpressionString", cronExpressionString);
                            cmd.Parameters.AddWithValue("@segundo", model.Seconds);
                            cmd.Parameters.AddWithValue("@minuto", model.Minutes);
                            cmd.Parameters.AddWithValue("@hora", model.Hours);
                            cmd.Parameters.AddWithValue("@diaDoMes", model.DayOfMonth);
                            cmd.Parameters.AddWithValue("@mes", model.Month);
                            cmd.Parameters.AddWithValue("@diasDaSemana", model.DayOfWeek);
                            cmd.Parameters.AddWithValue("@TempoDeRepeticao", model.RecurrenceTime);
                            cmd.Parameters.AddWithValue("@tipoDeRepeticao", model.RecurrenceType);

                            cmd.ExecuteNonQuery();
                        }

                        string[] diasSelecionados = model.DayOfWeek.Split(',');
                        List<DiaDaSemana>? diasDaSemanaDb = RetornaDiasDaSemanaAgendamento();
                        foreach (DiaDaSemana? dia in diasDaSemanaDb)
                        {
                            for (int i = 0; i < diasSelecionados.Length; i++)
                            {
                                using (SqliteCommand cmdDiasSemana = con.CreateCommand())
                                {
                                    cmdDiasSemana.Connection = con;
                                    cmdDiasSemana.CommandText = "UPDATE DiasDaSemana SET isExecutar = @isExecutar WHERE Ordem = @ordem ;";
                                    int diaSelecionado = int.Parse(diasSelecionados[i]);
                                    if (dia.Ordem == diaSelecionado)
                                    {
                                        cmdDiasSemana.Parameters.AddWithValue("@isExecutar", 1);
                                        cmdDiasSemana.Parameters.AddWithValue("@ordem", dia.Ordem);
                                        cmdDiasSemana.ExecuteNonQuery();
                                        break;
                                    }
                                    else
                                    {
                                        cmdDiasSemana.Parameters.AddWithValue("@isExecutar", 0);
                                        cmdDiasSemana.Parameters.AddWithValue("@ordem", dia.Ordem);
                                        cmdDiasSemana.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                        _transaction.Commit();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public int SalvarCaminhoArquivoBackupLocal(DirectoryInfo directory, bool isAlternative)
        {
            try
            {
                using (var con = new ConectaDb().RetornaSqliteConexao())
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.Connection = con;
                        string sql = @"UPDATE BackupFileSettings SET DbFilePath = @dirPath WHERE AlternativeCopyPath = @alternative ;";
                        cmd.CommandText = sql;
                        cmd.Parameters.AddWithValue("@dirPath", directory.FullName);
                        cmd.Parameters.AddWithValue("@alternative", isAlternative ? 1 : 0);
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        /// <summary>
        /// Retorna o caminho do arquivo físico de backup no sistema de arqiuvos
        /// </summary>
        /// <param name="isAlternative">Define se o caminho a ser retornado é da cópia alternativa do backup</param>
        /// <returns></returns>
        public string? RetornaCaminhoArquivoBackup(bool isAlternative)
        {
            try
            {
                using (var con = new ConectaDb().RetornaSqliteConexao())
                {
                    con.Open();
                    using (SqliteCommand cmd = new SqliteCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "SELECT DbFilePath FROM BackupFileSettings WHERE AlternativeCopyPath = @alternative;";
                        cmd.Parameters.AddWithValue("alternative", isAlternative ? 1 : 0);
                        using (SqliteDataReader reader = cmd.ExecuteReader())
                        {
                            reader.Read();
                            var caminho = Convert.IsDBNull(reader.GetString(0)) ? null : reader.GetString(0);

                            return caminho;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ConfiguracaoBancoDeDadosRenove?> RetornaDadosDeAcessoAoBancoRenove()
        {
            try
            {
                using (var con = new ConectaDb().RetornaSqliteConexao())
                {
                    con.Open();
                    using (SqliteCommand cmd = con.CreateCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "SELECT Server, User, Password, Porta, Databasename FROM LocalDbSettings LIMIT 1;";
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            reader.Read();
                            ConfiguracaoBancoDeDadosRenove databaseSettings = new ConfiguracaoBancoDeDadosRenove()
                            {
                                Server = reader.GetString(0) ?? string.Empty,
                                User = reader.GetString(1) ?? string.Empty,
                                Password = reader.GetString(2) ?? string.Empty,
                                Porta = Convert.IsDBNull(reader.GetInt32(3)) ? 0 : reader.GetInt32(3),
                                Database = reader.GetString(4) ?? string.Empty
                            };
                            return databaseSettings;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<VpsServerSettings?> RetornaDadosServidorVps()
        {
            try
            {
                using (var con = new ConectaDb().RetornaSqliteConexao())
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "SELECT Url, Ativo FROM RemoteBackupServerSettings LIMIT 1;";
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                string url = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                                bool isAtivo = !reader.IsDBNull(1) && reader.GetInt32(1) == 1;

                                return new VpsServerSettings()
                                {
                                    Url = url,
                                    isAtivo = isAtivo
                                };
                            }
                        }
                    }
                }
                return new VpsServerSettings()
                {
                    Url = string.Empty,
                    isAtivo = false
                };
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Retorna dados de acesso ao servidor/diretório FTP salvos no banco do aplicativo de backup
        /// </summary>
        /// <returns></returns>
        public SftpSettings? RetornaDadosAcessoFTP()
        {
            try
            {
                using (var con = new ConectaDb().RetornaSqliteConexao())
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.Connection = con;
                        string query = "SELECT Host, Username, Password, Port, RemoteDirectoryPath, Ativo FROM FtpServerSettings LIMIT 1;";
                        cmd.CommandText = query;
                        using (SqliteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                SftpSettings ftpSettings = new SftpSettings()
                                {
                                    Host = reader.IsDBNull(0)? string.Empty : reader.GetString(0),
                                    Username = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                                    Password = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                    Port = reader.IsDBNull(3) ? 21 : reader.GetInt32(3),
                                    RemoteDirectoryPath = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                                    isAtivo = !reader.IsDBNull(5) && reader.GetInt32(5) == 1,
                                };
                                return ftpSettings;
                            }
                            return null;
                        }
                    }
                }
            }

            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Testa conexão ao banco Renove a partir dos dados recebidos em databaseSettings
        /// </summary>
        /// <param name="dadosBancoRenove"></param>
        /// <returns></returns>
        public bool TestaConexaoBancoRenove(ConfiguracaoBancoDeDadosRenove dadosBancoRenove, IProgress<int>? progress)
        {
            progress?.Report(33);
            string connectionString = $"Server={dadosBancoRenove.Server};Database={dadosBancoRenove.Database};User Id={dadosBancoRenove.User};Password={dadosBancoRenove.Password};";
            using (MySqlConnection _mariaDbConnection = new MySqlConnection(connectionString))
            {
                try
                {
                    progress?.Report(66);
                    _mariaDbConnection.Open();
                    var statusConexao = _mariaDbConnection.State;

                    if (statusConexao == System.Data.ConnectionState.Open)
                    {
                        progress?.Report(100);
                        return true;
                    }

                    return false;
                }
                catch (Exception)
                {

                    throw;
                }

                finally
                {
                    _mariaDbConnection.Close();
                }
                
            }
        }


        /// <summary>
        /// Testa conexão ao servidor/diretório FTP usando os dados digitados pelo usuário
        /// </summary>
        /// <param name="ftpSettings"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        public async Task<bool> TestaConexaoFtpAsync(SftpSettings? ftpSettings, IProgress<int>? progress)
        {
            await Task.Delay(1000);
            progress?.Report(30);
            await Task.Delay(1000);

            
            progress?.Report(45);
            await Task.Delay(1000);


            
            progress?.Report(65);
            await Task.Delay(1000);

            using (FtpClient cliente = new FtpClient())
            {
                try
                {
                    progress?.Report(75);
                    await Task.Delay(1000);
                    cliente.Host = ftpSettings?.Host;
                    cliente.Port = ftpSettings.Port;
                    cliente.Credentials.UserName = ftpSettings.Username;
                    cliente.Credentials.Password = ftpSettings.Password;

                    cliente.Config.EncryptionMode = FtpEncryptionMode.Explicit; //Para a cofiguração "Explicit FTP over TLS and insecure plain FTP" do servidor FileZilla
                    cliente.ValidateCertificate += (control, e) => { e.Accept = true; }; //aceita o certificado do Servidor Ftp
                    cliente.Config.DataConnectionType = FtpDataConnectionType.AutoPassive; // Melhor para NAT e firewalls
                    cliente.Config.ConnectTimeout = 10000; //Confgura o timeout para 10 segundos
                    cliente.Config.SocketKeepAlive = true; //Mantém conexão viva para grades uploads
                    cliente.Config.RetryAttempts = 0; // Tentativas extras caso ocorra falha

                    progress?.Report(85);
                    await Task.Delay(1000);

                    cliente.Connect();

                    progress?.Report(100);
                    return cliente.DirectoryExists(ftpSettings.RemoteDirectoryPath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                    return false;
                }
                finally
                {
                    cliente.Disconnect();
                }
            }
        }

        /// <summary>
        /// Salva os parâmetro de acesso ao banco de dados do sistema Renove ERP
        /// </summary>
        /// <param name="dadosBancoRenove"></param>
        /// <returns></returns>
        public int SalvarDadosDeAcessoAoBancoRenove(ConfiguracaoBancoDeDadosRenove dadosBancoRenove)
        {
            try
            {
                using (var con = new ConectaDb().RetornaSqliteConexao())
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.Connection = con;
                        string sql = @"UPDATE LocalDbSettings SET Server=@server, User=@user, Password=@password, DatabaseName=@databaseName, Porta=@porta;";
                        cmd.CommandText = sql;
                        cmd.Parameters.AddWithValue("@server", dadosBancoRenove.Server);
                        cmd.Parameters.AddWithValue("@user", dadosBancoRenove.User);
                        cmd.Parameters.AddWithValue("@password", dadosBancoRenove.Password);
                        cmd.Parameters.AddWithValue("@databaseName", dadosBancoRenove.Database);
                        cmd.Parameters.AddWithValue("@porta", dadosBancoRenove.Porta);
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int SalvarDadosDeConexaoFtp(SftpSettings ftpSettings)
        {
            try
            {
                using (var con = new ConectaDb().RetornaSqliteConexao())
                {
                    con.Open();
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.Connection = con;
                        string sql = @"UPDATE FtpServerSettings SET Host=@host, Username=@username, Password=@password, Port=@port, RemoteDirectoryPath=@remoteDir, Ativo = @ativado;";
                        cmd.CommandText = sql;
                        cmd.Parameters.AddWithValue("@host", ftpSettings.Host);
                        cmd.Parameters.AddWithValue("@username", ftpSettings.Username);
                        cmd.Parameters.AddWithValue("@password", ftpSettings.Password);
                        cmd.Parameters.AddWithValue("@port", ftpSettings.Port);
                        cmd.Parameters.AddWithValue("@remoteDir", ftpSettings.RemoteDirectoryPath);
                        cmd.Parameters.AddWithValue("@ativado", ftpSettings.isAtivo == true ? 1 : 0);
                        
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Salva os dados de configuração de acesso ao Servidor de Backup remoto da Renove
        /// </summary>
        /// <param name="remotoSettings"></param>
        /// <returns></returns>
        public int SalvaDadosServidorRemotoDeBackup(VpsServerSettings remotoSettings)
        {
            try
            {
                using (var connection = new ConectaDb().RetornaSqliteConexao())
                {
                    connection.Open();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.Connection = connection;
                        cmd.CommandText = "UPDATE RemoteBackupServerSettings SET Url = @url, Ativo = @isAtivo";
                        cmd.Parameters.AddWithValue("@url", remotoSettings.Url);
                        cmd.Parameters.AddWithValue("@isAtivo", remotoSettings.isAtivo ? 1 : 0);
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<string?> RetornaCnpjCliente()
        {
            try
            {
                string cnpj = string.Empty;
                ConfiguracaoBancoDeDadosRenove? dadosAcessoBanco = await RetornaDadosDeAcessoAoBancoRenove();

                using (var _mySqlConnection = new MySqlConnection())
                {
                    _mySqlConnection.ConnectionString = $"Server={dadosAcessoBanco?.Server};Database={dadosAcessoBanco?.Database};User Id={dadosAcessoBanco?.User};Password={dadosAcessoBanco?.Password};";
                    _mySqlConnection.Open();
                    using (var cmdCapturaCnpj = _mySqlConnection.CreateCommand())
                    {
                        cmdCapturaCnpj.CommandText = "SELECT cnpj FROM renove.empresa;";
                        using (var reader = await cmdCapturaCnpj.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                cnpj = reader.GetString(0);

                                cnpj = cnpj.Replace(".", "").Replace("/", "").Replace("-", "");
                            }
                        }
                    }
                }

                return cnpj;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<string>?> RetornaNomesBancosDeDadosRenove()
        {
            List<string> nomesbancos = new List<string>(); ;
            ConfiguracaoBancoDeDadosRenove? dadosAcessoBanco = await RetornaDadosDeAcessoAoBancoRenove();

            using (var _mySqlConnection = new MySqlConnection())
            {
                try
                {
                    _mySqlConnection.ConnectionString = $"Server={dadosAcessoBanco?.Server};Database={dadosAcessoBanco?.Database};User Id={dadosAcessoBanco?.User};Password={dadosAcessoBanco?.Password};";
                    _mySqlConnection.Open();
                    using (var cmdListarBancos = _mySqlConnection.CreateCommand())
                    {
                        cmdListarBancos.CommandText = "SHOW DATABASES;";
                        using (var reader = await cmdListarBancos.ExecuteReaderAsync())
                        {
                            if (reader.HasRows)
                            {
                                nomesbancos = new List<string>();
                                while (reader.Read())
                                {
                                    string dbName = reader.GetString(0);
                                    if (!IsSystemDatabase(dbName))
                                    {
                                        nomesbancos.Add(dbName);
                                    }
                                }
                            }
                        }
                    }
                    return nomesbancos;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private bool IsSystemDatabase(string dbName)
        {
            return dbName.Equals("information_schema", StringComparison.OrdinalIgnoreCase) ||
               dbName.Equals("performance_schema", StringComparison.OrdinalIgnoreCase) ||
               dbName.Equals("mysql", StringComparison.OrdinalIgnoreCase) ||
               dbName.Equals("sys", StringComparison.OrdinalIgnoreCase) || dbName.Equals("test", StringComparison.OrdinalIgnoreCase);
        }
    }
}
