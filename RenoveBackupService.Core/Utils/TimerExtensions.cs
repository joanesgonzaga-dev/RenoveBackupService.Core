using Quartz;
using ServicosGlobais.Model;
using ServicosGlobais.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenoveBackupService.Core.Utils
{
    public static class TimerExtensions
    {
        public static void AddJobAndTrigger<T>(
            this IServiceCollectionQuartzConfigurator quartz,
            IConfiguration config)
            where T : IJob
        {
            //Pegar nome da classe - mesmo nome da chave do config
            string nomeJob = typeof(T).Name;

            var configKey = $"Quartz:{nomeJob}";
            var cronHorarioExecucao = config[configKey]; //5seg

            if (string.IsNullOrEmpty(cronHorarioExecucao))
            {
                throw new Exception($"No Quartz.NET Cron schedule found for job in configuration at {configKey}");
            }

            //registrando o job
            var jobKey = new JobKey(nomeJob);
            quartz.AddJob<T>(opts => opts.WithIdentity(jobKey));

            quartz.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity(nomeJob + "-trigger")
                .WithCronSchedule(cronHorarioExecucao));
        }

        public static void AddJobAndTrigger<T>(this IServiceCollectionQuartzConfigurator quartz, ConfiguracaoBancoDeDadosRenove dbConfig, string cronExpression)
        where T : IJob
        {
            var jobKey = new JobKey(typeof(T).Name);
            quartz.AddJob<T>(opts => opts.WithIdentity(jobKey));

            quartz.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity($"{typeof(T).Name}-trigger")
                .WithCronSchedule(cronExpression));
        }
    }
}
