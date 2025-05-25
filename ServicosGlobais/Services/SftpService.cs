using FluentFTP;
using Microsoft.Extensions.Logging;
using ServicosGlobais.Model;

namespace ServicosGlobais.Services
{
    public class SftpService : ISftpService
    {
        ILogger<SftpService>? _logger;
        public SftpService(ILogger<SftpService> logger)
        {
            _logger = logger;
        }
        public async Task<UploadResult> Upload(string localBackupFilePath, SftpSettings ftpSettings)
        {
            if (!File.Exists(localBackupFilePath))
            {
                return new UploadResult(false, "Arquivo local de banco de dados não foi encontrado!");
            }
            using (var ftpClient = new FtpClient())
            {
                try
                {
                    ftpClient.Host = ftpSettings?.Host;
                    ftpClient.Port = ftpSettings.Port;
                    ftpClient.Credentials.UserName = ftpSettings.Username;
                    ftpClient.Credentials.Password = ftpSettings.Password;

                    ftpClient.Config.EncryptionMode = FtpEncryptionMode.Explicit; //Para a cofiguração "Explicit FTP over TLS and insecure plain FTP" do servidor FileZilla
                    ftpClient.ValidateCertificate += (control, e) => { e.Accept = true; }; //aceita o certificado do Servidor Ftp
                    ftpClient.Config.DataConnectionType = FtpDataConnectionType.AutoPassive; // Melhor para NAT e firewalls
                    ftpClient.Config.ConnectTimeout = 60000; //Aumenta o timeout para 60 segundos
                    ftpClient.Config.SocketKeepAlive = true; //Mantém conexão viva para grades uploads
                    ftpClient.Config.RetryAttempts = 2; // Tentativas extras caso ocorra falha

                    ftpClient.Connect();
                    bool existe = ftpClient.DirectoryExists(ftpSettings.RemoteDirectoryPath);
                    string remoteDirectory = ftpSettings.RemoteDirectoryPath.TrimEnd('/') + "/";
                    string remoteFilePath = remoteDirectory + Path.GetFileName(localBackupFilePath);
                    using (var fileStream = new FileStream(localBackupFilePath, FileMode.Open, FileAccess.Read))
                    {
                        var statusResult = ftpClient.UploadStream(fileStream, remoteFilePath, FtpRemoteExists.Overwrite, true);
                        return new UploadResult(true, $"Arquivo enviado com sucesso ao FTP!.{statusResult}");
                    }
                    
                }
                catch (Exception ex)
                {
                    return new UploadResult(false, $"Upload não realizado!: {ex.Message}, {ex.InnerException?.Message}");
                }
                finally
                {
                    if (ftpClient.IsConnected)
                    {
                        ftpClient.Disconnect();
                        ftpClient.Dispose();
                    }
                }
            }
        }
        private void FtpClient_ValidateCertificate(FluentFTP.Client.BaseClient.BaseFtpClient control, FtpSslValidationEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
