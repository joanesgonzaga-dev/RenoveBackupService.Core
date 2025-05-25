using ServicosGlobais.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicosGlobais.Services
{
    public interface IConfigService
    {
        public int SalvarCaminhoMySqlDumpExe(FileInfo fileInfo);
        public FileInfo? RetornaMysqldumpExeInfoFromDb();
        public List<DiaDaSemana>? RetornaDiasDaSemanaAgendamento();
        public CronExpressionModel RetornaExpressaoCronDoBd();

        public Task<ConfiguracaoBancoDeDadosRenove?> RetornaDadosDeAcessoAoBancoRenove();
        public string RetornaExpressaoCronEmString(CronExpressionModel model);

        public int SalvarCaminhoArquivoBackupLocal(DirectoryInfo directory, bool isAlternative);

        public string? RetornaCaminhoArquivoBackup(bool isAlternative);

        public Task<bool> TestaConexaoFtpAsync(SftpSettings? ftpSettings, IProgress<int>? progress);

        public Task<string?> RetornaCnpjCliente();

        public Task<List<string>?> RetornaNomesBancosDeDadosRenove();

        public SftpSettings? RetornaDadosAcessoFTP();

        public Task<VpsServerSettings?> RetornaDadosServidorVps();
    }
}
