using Microsoft.Extensions.Logging;
using RenoveBackupService.Core.Utils;
using ServicosGlobais.Model;
using ServicosGlobais.Services;
using System.Diagnostics;
using System.IO.Compression;

namespace RenoveBackupService.Core.Services
{
    public class BackupService : IBackupService
    {
        internal ILogger<BackupService> _fileLogger;
        ISftpService _ftpService;
        IVpsService _vpsService;
        IConfigService _configService;
        public BackupService(ISftpService sftpService, IVpsService vpsService, ILogger<BackupService> fileLogger, IConfigService configService)
        {
            _ftpService = sftpService;
            _fileLogger = fileLogger;
            _configService = configService;
            _vpsService = vpsService;
        }
        public async Task ExecuteBackup(string dbUser, string dbPassword, List<string>? dbNameS)
        {
            try
            {
                string? timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                var localArquivoBackup =_configService?.RetornaCaminhoArquivoBackup(false);
                var localArquivoBackupAlternativo = _configService?.RetornaCaminhoArquivoBackup(true);
                bool islocalBackupPadraoExiste = true;
                bool islocalBackupAlternativoExiste = true;
                if (!Directory.Exists(localArquivoBackup))
                {
                    islocalBackupPadraoExiste = false;
                    _fileLogger.LogWarning($"Local para arquivo de backup PADRÃO não encontrado/definido. Caminho atual configurado:{localArquivoBackup}");
                }

                if (!Directory.Exists(localArquivoBackupAlternativo))
                {
                    islocalBackupAlternativoExiste=false;
                    _fileLogger.LogWarning($"Local para arquivo de backup ALTERNATIVO não encontrado/definido. Caminho atual configurado:{localArquivoBackup}");
                }

                if (!islocalBackupPadraoExiste && !islocalBackupAlternativoExiste)
                {
                    _fileLogger.LogWarning("Nenhum local definido para backups. Configure caminhos Padrão e Alternativo!");
                    throw new DirectoryNotFoundException();
                }


                string? localExeMySqlDump = _configService?.RetornaMysqldumpExeInfoFromDb().FullName;
                string? cnpj = await _configService?.RetornaCnpjCliente();

                var currentTempBackupDir = Directory.CreateDirectory($"{localArquivoBackup}\\temp_{timestamp}");

                foreach (string dbName in dbNameS)
                {
                    string backupFile = Path.Combine(currentTempBackupDir.FullName, $"{dbName}.sql");
                    string mysqldumpPath = Path.Combine(localExeMySqlDump);

                    if (!File.Exists(mysqldumpPath))
                    {
                        _fileLogger.LogError($"Erro: O arquivo '{mysqldumpPath}' não foi encontrado. A execução será encerrada!");
                        return;
                    }

                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = mysqldumpPath,
                        Arguments = $" -u {dbUser} -p{dbPassword} {dbName}",
                        UseShellExecute = false, // Necessário para redirecionamento funcionar
                        CreateNoWindow = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    };

                    
                    using (Process? process = Process.Start(psi))
                    {
                        using (StreamWriter? writer = new StreamWriter(backupFile))
                        {
                            writer.Write(process?.StandardOutput.ReadToEnd());
                        }
                        process?.WaitForExit();
                        _fileLogger.LogInformation($"Arquivo temporário de backup CRIADO: {backupFile}"); //Diretório??
                    }
                }

                var arquivoZipFinal = Path.Combine(localArquivoBackup, $"{cnpj}-{(int)DateTime.Today.DayOfWeek}.zip");

                var arquivoZipFinalCopia = Path.Combine(localArquivoBackupAlternativo, $"{cnpj}-{DateTime.Now.ToString("MM-dd")}.zip");

                //verifica se existe arquivo homônimo referente à execução anterior e exclui
                //para criação do novo
                if (File.Exists(arquivoZipFinal))
                {
                    File.Delete(arquivoZipFinal);
                    _fileLogger.LogInformation($"Arquivo de backup anterior EXCLUÍDO \"{arquivoZipFinal.ToUpper()}\" ");
                }

                #region Código para compactação com WinRar
                //var winRarExePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "WinRar", "WinRar.exe");

                //ProcessStartInfo psiCopy = new ProcessStartInfo
                //{
                //    FileName = winRarExePath,
                //    Arguments = $"a -r \"{arquivoFinal}\" \"*\"",
                //    WorkingDirectory = currentTempBackupDir.FullName,
                //    WindowStyle = ProcessWindowStyle.Hidden
                //};

                //using (var rarProcess = Process.Start(psiCopy))
                //{
                //    rarProcess.WaitForExit();
                //    _logger.LogInformation($"Arquivo RAR final criado: {arquivoFinal}");
                //}
                #endregion

                ZipFile.CreateFromDirectory(currentTempBackupDir.FullName, arquivoZipFinal, CompressionLevel.Optimal, includeBaseDirectory: false);

                File.Copy(arquivoZipFinal, arquivoZipFinalCopia, true);
                
                if (Directory.Exists(currentTempBackupDir.FullName))
                {
                    currentTempBackupDir.DeleteWithRetrials(ref _fileLogger, 5);
                    _fileLogger.LogInformation($"Diretório temporário de backup EXCLUÍDO: {currentTempBackupDir.FullName.ToUpper()}");
                }
                else
                {
                    _fileLogger.LogWarning($"Diretório temporário de backup não encontrado para exclusão: {currentTempBackupDir.FullName.ToUpper()}");
                }

                    VpsServerSettings? vpsServerSettings = await _configService.RetornaDadosServidorVps();
                if (vpsServerSettings.isAtivo)
                {
                    UploadResult? uploadResult = await _vpsService.Upload(arquivoZipFinal, new Uri(vpsServerSettings.Url));
                    _fileLogger.LogInformation(uploadResult.Message);
                }

                SftpSettings? sftpSettings = _configService.RetornaDadosAcessoFTP();
                if (sftpSettings.isAtivo)
                {
                    UploadResult? uploadResult = await _ftpService.Upload(arquivoZipFinal, sftpSettings);
                    _fileLogger.LogInformation(uploadResult.Message);
                }

                currentTempBackupDir = null;
            }
            catch (Exception ex)
            {
                _fileLogger.LogError($"Erro ao executar backup: {ex.Message}");
            }
        }
    }
}
