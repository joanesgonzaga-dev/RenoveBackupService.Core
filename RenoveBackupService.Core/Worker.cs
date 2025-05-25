using Quartz;
using RenoveBackupService.Core.Services;
using ServicosGlobais.Model;
using ServicosGlobais.Services;
using Serilog;
using static System.Net.Mime.MediaTypeNames;

namespace RenoveBackupService.Core
{
    [DisallowConcurrentExecution]
    public class Worker : IJob
    {
        private readonly ILogger<Worker> _logger;
        private readonly ILogger<Worker> _fileLogger;
        private readonly ConfiguracaoBancoDeDadosRenove _dbSettings;
        IBackupService _backupService;
        public Worker(ILogger<Worker> logger, IBackupService backupService, ConfiguracaoBancoDeDadosRenove dbSettings, ILogger<Worker> fileLogger)
        {
            _logger = logger;
            _backupService = backupService;
            _dbSettings = dbSettings;
            _fileLogger = fileLogger;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _fileLogger.LogInformation($"O serviço iniciou uma execução");
                List<string>? dbNames = await new ConfigService().RetornaNomesBancosDeDadosRenove();
                await _backupService.ExecuteBackup(_dbSettings.User, _dbSettings.Password, dbNames);
                _fileLogger.LogInformation($"Execução concluída com sucesso!");
            }
            catch (Exception ex)
            {
                _fileLogger.LogCritical(ex.Message, "Erro crítico!");
                throw;
            }
            
        }
    }
}