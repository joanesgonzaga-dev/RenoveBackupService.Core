using MySqlX.XDevAPI.Common;
using ServicosGlobais.Model;
using ServicosGlobais.Services;
using System;
using System.Data;
using System.Diagnostics;
using System.IO.Compression;
//using static Org.BouncyCastle.Math.EC.ECCurve;

namespace RenoveBackupService.Configurator
{
    public partial class FrmUpload : Form
    {
        ConfigService _configService;
        string? timestamp;
        ConfiguracaoBancoDeDadosRenove? dbConfig;
        string? localArquivoBackup;
        string? localExeMySqlDump;
        string? cnpj;
        string winRarExePath;
        DirectoryInfo currentTempBackupDir;
        string arquivoZipFinal = string.Empty;

        public FrmUpload()
        {
            InitializeComponent();
            progressBarUpload.Visible = false;
            carregarCheckedListBoxBancos().ConfigureAwait(false);
            cbTipoEnvio.SelectedIndex = 0;
        }
        private async Task ControlaBarraDeProgresso(IProgress<int>? progress)
        {
            await Task.Delay(1000);
            progress?.Report(10);

            await Task.Delay(1000);
            progress?.Report(25);

            await Task.Delay(1000);
            progress?.Report(50);

            await Task.Delay(1000);
            progress?.Report(75);

            await Task.Delay(1000);
            progress?.Report(100);
            await Task.Delay(1000);

            await Task.CompletedTask;
        }

        private async Task carregarCheckedListBoxBancos()
        {
            try
            {
                List<string>? nomesBancos = await new ConfigService().RetornaNomesBancosDeDadosRenove();
                if (nomesBancos != null && nomesBancos.Count > 0)
                {
                    foreach (var nome in nomesBancos)
                    {
                        checkedListBox1.Items.Add(nome, true);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async void btnEnviarBackup_Click(object sender, EventArgs e)
        {
            try
            {
                progressBarUpload.Visible = true;
                progressBarUpload.Value = 5;
                progressBarUpload.Refresh();

                var progress = new Progress<int>(value =>
                {
                    progressBarUpload.Value = (int)value;
                });

                await InicializaVariaveis(progress);

                var bancosSelecionados = checkedListBox1.Items.Cast<string>().Select(nome => new
                {
                    Nome = nome,
                    isAtivo = true
                }).ToList();

                await GerarArquivoDeBackup(bancosSelecionados, progress);

                if (cbTipoEnvio.SelectedIndex == 0)
                {
                    UploadResult result = await EnviaUploadVps(progress);

                    if (result.Success)
                    {
                        MessageBox.Show($"{result.Message}", "Renove Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"{result.Message}", "Renove Backup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                else if (cbTipoEnvio.SelectedIndex == 1)
                {
                    UploadResult result = await EnviaUploadFtp(progress);
                    if (result.Success)
                    {
                        MessageBox.Show($"{result.Message}", "Renove Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"{result.Message}", "Renove Backup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao enviar backup para o servidor VPS!{ex.Message}", "Renove Backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            finally { progressBarUpload.Visible = false; }
        }
        private async Task<UploadResult> EnviaUploadFtp(IProgress<int> _progress)
        {
            await Task.Delay(500);
            SftpSettings? sftpSettings = _configService.RetornaDadosAcessoFTP();
            _progress?.Report(80);
            SftpService _ftpService = new SftpService(null);
            await Task.Delay(1000);

            UploadResult result = await _ftpService.Upload(arquivoZipFinal, sftpSettings);
            _progress?.Report(100);
            await Task.Delay(1000);
            return result;
        }
        private async Task<UploadResult> EnviaUploadVps(IProgress<int> _progress)
        {
            await Task.Delay(500);
            VpsServerSettings? vpsServerSettings = await _configService.RetornaDadosServidorVps();
            _progress?.Report(80);
            VpsService vpsService = new VpsService(null);
            await Task.Delay(1000);
            UploadResult result = await vpsService.Upload(arquivoZipFinal, new Uri(vpsServerSettings.Url));
            _progress?.Report(100);
            await Task.Delay(1000);
            return result;
        }
        private async Task InicializaVariaveis(IProgress<int> _progress)
        {
            await Task.Delay(500);
            _progress?.Report(15);
            _configService = new ConfigService();
            timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            dbConfig = await _configService.RetornaDadosDeAcessoAoBancoRenove();
            localArquivoBackup = _configService?.RetornaCaminhoArquivoBackup(false);
            localExeMySqlDump = _configService?.RetornaMysqldumpExeInfoFromDb().FullName;
            cnpj = await _configService?.RetornaCnpjCliente();
            winRarExePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "WinRar", "WinRar.exe");
            currentTempBackupDir = Directory.CreateDirectory($"{localArquivoBackup}\\temp_{timestamp}");
            await Task.Delay(1000);
        }

        /// <summary>
        /// Método para gerar o arquivo final (winrar com backups.sql) a ser enviado.
        /// </summary>
        /// <param name="bancosSelecionados"></param>
        /// <returns>Avaliar alterar método para que retorne FilePath ao invés de void</returns>
        /// <exception cref="FileNotFoundException"></exception>
        private async Task GerarArquivoDeBackup(IEnumerable<dynamic> bancosSelecionados, IProgress<int> _progress)
        {
            _progress?.Report(25);
            await Task.Delay(1000);
            foreach (var banco in bancosSelecionados)
            {
                string backupFile = Path.Combine(currentTempBackupDir.FullName, $"{banco.Nome}.sql");
                string mysqldumpPath = Path.Combine(localExeMySqlDump);

                if (!File.Exists(mysqldumpPath))
                {
                    throw new FileNotFoundException("Arquivo mysqldump.exe não localizado. Favor definir em Configurações!");
                }

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = mysqldumpPath,
                    Arguments = $" -u {dbConfig.User} -p{dbConfig.Password} {banco.Nome}",
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
                }
                _progress?.Report(50);
                await Task.Delay(1000);
            }

            arquivoZipFinal = Path.Combine(localArquivoBackup, $"{cnpj}-{(int)DateTime.Today.DayOfWeek}.zip");
            if (File.Exists(arquivoZipFinal))
            {
                File.Delete(arquivoZipFinal);
            }

            //ProcessStartInfo psiCopy = new ProcessStartInfo
            //{
            //    FileName = winRarExePath,
            //    Arguments = $"a -r \"{arquivoFinal}\" \"*\"",
            //    WorkingDirectory = currentTempBackupDir.FullName,
            //    WindowStyle = ProcessWindowStyle.Hidden
            //};

            //_progress?.Report(75);
            //await Task.Delay(1000);
            //using (var rarProcess = Process.Start(psiCopy))
            //{
            //    rarProcess.WaitForExit();
            //}

            ZipFile.CreateFromDirectory(currentTempBackupDir.FullName, arquivoZipFinal, CompressionLevel.Optimal, includeBaseDirectory: false);

            if (Directory.Exists(currentTempBackupDir.FullName))
            {
                Directory.Delete(currentTempBackupDir.FullName, true);
            }
        }
    }
}
