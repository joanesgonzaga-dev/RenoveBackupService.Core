using Quartz;
using RenoveBackupService.Core;
using RenoveBackupService.Core.Services;
using RenoveBackupService.Core.Utils;
using ServicosGlobais.Model;
using ServicosGlobais.Services;
using Serilog;
using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using System.Runtime.ConstrainedExecution;
using System;

//string logFileName = DateTime.Now.ToString("ddMMyyyy");
string logsDir = Path.Combine(AppContext.BaseDirectory, "logs");
Directory.CreateDirectory(logsDir);

//Configura o logger globalmente
Log.Logger = new LoggerConfiguration().WriteTo.File(Path.Combine(logsDir, $".txt"), rollingInterval: RollingInterval.Day).CreateLogger();
try
{
    Log.Information("Iniciando o serviço RenoveBackup service...");

    #region Detalhes sobre implementação do Serilog por D.I
    //No contexto de uma aplicação WinForms com IHost, o uso de UseSerilog() já faz a ponte entre o Serilog global(Log.Logger)
    //e o DI do Host.Ou seja, ao chamar UseSerilog(), o ILogger<T> passa a ser resolvido com base no Serilog, sem necessidade
    //de registrar manualmente no ConfigureServices().
    //UserSerilog PRECISA ficar logo após CreateDefaultBuilder
    #endregion
    IHost host = Host.CreateDefaultBuilder(args).UseSerilog().UseWindowsService().ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        ConfigService? _configService = new ConfigService();
        ConfiguracaoBancoDeDadosRenove? databaseBackupSettings = _configService.RetornaDadosDeAcessoAoBancoRenove().Result;
        string? cronExpression = _configService.RetornaExpressaoCronEmString(null);

        // Registrando no DI como Singleton (para reuso)
        services?.AddSingleton(databaseBackupSettings);
        services?.AddSingleton(cronExpression);
        services?.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionScopedJobFactory();
            q.AddJobAndTrigger<Worker>(databaseBackupSettings, cronExpression);
        });
        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        services.AddScoped<IConfigService, ConfigService>();
        services.AddScoped<IBackupService, BackupService>();
        services.AddScoped<ISftpService, SftpService>();
        services.AddScoped<IVpsService, VpsService>();
        
    })
    .Build();
    await host.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex.Message, "Erro fatal ao iniciar o serviço RenoveBackup");
}

finally
{
    Log.CloseAndFlush();
}