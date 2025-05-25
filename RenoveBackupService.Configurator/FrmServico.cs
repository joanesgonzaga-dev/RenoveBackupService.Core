using System.Diagnostics;
using System.ServiceProcess;
using System.Text;

namespace RenoveBackupService.Configurator
{
    public partial class FrmServico : Form
    {
        #region Timers
        private System.Windows.Forms.Timer _debounceTimer;
        private System.Windows.Forms.Timer _pollingTimer;
        private bool _pendingUpdate = false; //indica se existem atualizações no arquivo pendentes de exibição no form
        private DateTime _lastWriteTime;
        #endregion

        private FileSystemWatcher _fileWatcher;
        StringBuilder sbLogServico;
        string logFileName = string.Empty;
        FileInfo logFileInfo;
        private long _lastReadPosition = 0;
        DirectoryInfo _logDirectoryInfo = new DirectoryInfo("C:\\SistemaRenove\\RenoveBackup\\RenoveBackupService.Core\\logs");
        string nomeDoServico = "RenoveBackupService";
        ServiceController sc = null;
        ServiceControllerStatus currentServiceControllerStatus;
        bool isExecutou = false;
        public FrmServico()
        {

            sc = new ServiceController(nomeDoServico);
            InitializeComponent();
            logFileName = GetDatedLogFilePath(DateTime.Now); //precisa ser chamado antes dos demais
            if (File.Exists(logFileName))
            {
                lblDataLogs.Text = logFileName;
            }
            else
            {
                lblDataLogs.Text = "Não foi gerado logo para a data de hoje";
            }
            
            StartFileWatcher();
            LoadLog(logFileName);
            SetupDebounceTimer();
        }

        /// <summary>
        /// Faz o monitoramento de alterações no arquivo de log do dia e exibe na tela (Richtextbox.Text)
        /// </summary>
        private void StartFileWatcher()
        {
            try
            {
                _fileWatcher = new FileSystemWatcher
                {
                    Path = _logDirectoryInfo.FullName,
                    Filter = "*.txt",
                    NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.LastAccess | NotifyFilters.Size
                };

                _fileWatcher.Changed += OnLogFileChanged;
                _fileWatcher.Created += OnLogFileChanged; // captura rotação de log
                _fileWatcher.Renamed += OnLogFileChanged;
                _fileWatcher.Deleted += OnLogFileChanged;
                _fileWatcher.IncludeSubdirectories = false;
                _fileWatcher.EnableRaisingEvents = true;
            }
            catch (Exception ex)
            {
                richTextBox1.Text = ex.Message;
            }

        }

        /// <summary>
        /// Abre o arquivo mesmo que ele esteja em uso por outro processo e faz a carga inicial do log do dia atual no componente RichTextBox.Text
        /// </summary>
        /// <param name="path">caminho completo ate o arquivo de log</param>
        private void LoadLog(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    using var stream = new FileStream(
                        path: path,
                        mode: FileMode.Open,
                        access: FileAccess.Read,
                        share: FileShare.ReadWrite // ← Permite ler enquanto outro processo escreve
                    );

                    using var reader = new StreamReader(stream);
                    var content = reader.ReadToEnd();
                    richTextBox1.Text = content;
                    _lastReadPosition = stream.Length;
                    ScrollToEnd();
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"Erro ao ler o arquivo de log:\n{ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            else
            {
                lblDataLogs.Text = "Não foi encontrado arquivo de log para a data selecionada.";
            }
        }

        private void btnInstalarServico_Click(object sender, EventArgs e)
        {
            string nomeServico = "RenoveBackupService";

            DirectoryInfo backupDir = new DirectoryInfo(Path.Combine("C:\\SistemaRenove", "RenoveBackup"));
            DirectoryInfo renoveServiceCoreDir = new DirectoryInfo(Path.Combine(backupDir.FullName, "RenoveBackupService.Core"));
            string caminhoExe = Path.Combine(renoveServiceCoreDir.FullName, "RenoveBackupService.Core.exe");

            if (!File.Exists(caminhoExe))
            {
                MessageBox.Show($"Arquivo NÂO localizado!: {caminhoExe}", "Renove backup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Confirma a instalação no diretório: {caminhoExe} ?", "Renove Backup", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            var psi = new ProcessStartInfo("sc", $"create {nomeServico} binPath= \"{caminhoExe}\" start= auto")
            {
                Verb = "runas", // Executar como administrador
                UseShellExecute = true
            };

            try
            {
                Process.Start(psi);
                MessageBox.Show("Serviço de backup instalado com sucesso!", "Renove backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao instalar serviço de backup: " + ex.Message, "Renove backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDesinstalarServico_Click(object sender, EventArgs e)
        {
            string nomeServico = "RenoveBackupService";

            var psi = new ProcessStartInfo("sc", $"delete {nomeServico}")
            {
                Verb = "runas", // Executar como administrador
                UseShellExecute = true
            };

            try
            {
                Process.Start(psi);
                MessageBox.Show("Serviço desinstalado com sucesso!", "Renove backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao desinstalar serviço: " + ex.Message, "Renove backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIniciarServico_Click(object sender, EventArgs e)
        {
            try
            {
                using (ServiceController sc = new ServiceController("RenoveBackupService"))
                {
                    if (sc.Status == ServiceControllerStatus.Stopped)
                    {
                        sc.Start();
                        sc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(30));
                    }
                }

                MessageBox.Show("Serviço de backup iniciado com sucesso!", "Renove backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao iniciar o serviço! " + ex.Message, "Renove backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReiniciarServico_Click(object sender, EventArgs e)
        {
            try
            {
                using (ServiceController sc = new ServiceController("RenoveBackupService"))
                {
                    if (sc.Status != ServiceControllerStatus.Stopped)
                    {
                        sc.Stop();
                        sc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30));
                    }

                    sc.Start();
                    sc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(30));
                }

                MessageBox.Show("Serviço de backup reiniciado com sucesso!", "Renove backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao reiniciar o serviço! " + ex.Message, "Renove backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPararServico_Click(object sender, EventArgs e)
        {
            try
            {
                using (ServiceController sc = new ServiceController("RenoveBackupService"))
                {
                    if (sc.Status != ServiceControllerStatus.Stopped)
                    {
                        sc.Stop();
                        sc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30));
                    }
                }

                MessageBox.Show("Serviço de backup parado com sucesso!", "Renove backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao parar o serviço! " + ex.Message, "Renove backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDesativarServico_Click(object sender, EventArgs e)
        {
            string nomeServico = "RenoveBackupService";
            var psi = new ProcessStartInfo("sc", $"config {nomeServico} start= disabled")
            {
                Verb = "runas",
                UseShellExecute = true
            };

            try
            {
                Process.Start(psi);
                MessageBox.Show("Serviço de backup desativado com sucesso!", "Renove backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao desativar serviço de backup: " + ex.Message, "Renove backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReativarServico_Click(object sender, EventArgs e)
        {
            string nomeServico = "RenoveBackupService";
            var psi = new ProcessStartInfo("sc", $"config {nomeServico} start= auto")
            {
                Verb = "runas", // Executa como administrador
                UseShellExecute = true
            };

            try
            {
                Process.Start(psi);
                MessageBox.Show("Serviço de backup reativado com sucesso!", "Renove backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao reativar serviço de backup: " + ex.Message, "Renove backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AtualizarStatusDoServico()
        {
            try
            {
                if (sc == null)
                {
                    sc = new ServiceController(nomeDoServico);
                }
                sc.Refresh();

                if (currentServiceControllerStatus != sc.Status)
                {
                    if (sc.Status == ServiceControllerStatus.Running)
                    {
                        progressBar1.Style = ProgressBarStyle.Marquee;
                        progressBar1.MarqueeAnimationSpeed = 25; // velocidade do "giro"
                        progressBar1.Value = 60;
                        lblStatusServico.Text = "Serviço de backup em execução";
                        currentServiceControllerStatus = ServiceControllerStatus.Running;
                    }
                    else
                    {
                        //aqui é possível e recomendável avaliar outros status...futuramente.
                        progressBar1.Style = ProgressBarStyle.Marquee;
                        progressBar1.MarqueeAnimationSpeed = 0;
                        progressBar1.Value = 60;
                        lblStatusServico.Text = "Serviço de backup parado";
                        currentServiceControllerStatus = ServiceControllerStatus.Stopped;
                    }
                }
            }

            catch (InvalidOperationException ex)
            {
                lblStatusServico.Text = "Serviço não localizado";
                //txtLogFileViewer.Text = sbLogServico.Append(ex.Message).ToString();
            }

            catch (Exception ex)
            {
                lblStatusServico.Text = "Erro ao acessar o serviço";
                //txtLogFileViewer.Text = sbLogServico.Append(ex.Message).ToString();
            }
        }

        private void FrmServico_Load(object sender, EventArgs e)
        {
            VerificaStatusServicoTimer();
            CheckFilePollingTimer();
        }
        private void OnLogFileChanged(object sender, FileSystemEventArgs e)
        {
            //// Aguarda um pouco para o arquivo ficar disponível para leitura
            System.Threading.Thread.Sleep(100);
            try
            {
                // Como estamos em uma thread diferente, invocamos o UI thread
                Invoke(new Action(() =>
                {
                    if (File.Exists(logFileInfo.FullName))
                        using (var stream = new FileStream(logFileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            using (var reader = new StreamReader(stream))
                            {
                                var content = reader.ReadToEnd();
                                richTextBox1.Text = content;
                                _lastReadPosition = stream.Length;

                                _pendingUpdate = true;
                                _debounceTimer.Stop();
                                _debounceTimer.Start();

                                richTextBox1.Refresh();
                                ScrollToEnd();
                            }
                        }
                }));
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Debounce contra mudança em cascata
        /// </summary>
        private void SetupDebounceTimer()
        {
            _debounceTimer = new System.Windows.Forms.Timer();
            _debounceTimer.Interval = 300; // 300ms debounce
            _debounceTimer.Tick += (s, e) =>
            {
                _debounceTimer.Stop();
                if (_pendingUpdate)
                {
                    _pendingUpdate = false;
                    AppendNewLines();
                }
            };
        }

        /// <summary>
        /// Método para adicionar apenas novas linhas
        /// </summary>
        private void AppendNewLines()
        {
            try
            {
                using (var fs = new FileStream(logFileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var reader = new StreamReader(fs))
                {
                    fs.Seek(_lastReadPosition, SeekOrigin.Begin);
                    string newContent = reader.ReadToEnd();
                    _lastReadPosition = fs.Position;

                    if (!string.IsNullOrWhiteSpace(newContent))
                    {
                        Invoke(new Action(() =>
                        {
                            richTextBox1.AppendText(newContent);
                            ScrollToEnd();
                        }));
                    }
                }
            }
            catch (Exception)
            {
                // Arquivo ainda sendo usado, ignore temporariamente
            }
        }

        /// <summary>
        /// Faz o scroll automático para o fim
        /// </summary>
        private void ScrollToEnd()
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        /// <summary>
        /// Obtém o caminho do log do dia atual
        /// </summary>
        /// <returns>o caminho completo até o arquivo de logo do dia atual</returns>
        private string GetDatedLogFilePath(DateTime fileDate)
        {
            // parão de data compatível com Serilog padrão
            logFileName = fileDate.ToString("yyyyMMdd") + ".txt";
            logFileInfo = new FileInfo(Path.Combine("C:\\SistemaRenove\\RenoveBackup\\RenoveBackupService.Core\\logs", $"{logFileName}"));
            return logFileInfo.FullName;
        }

        /// <summary>
        /// Método para forçar uma verificação de mudanças no arquivo de log selecionado, pois o FileSystemWatcher
        /// apresentou falhas intermitentes
        /// </summary>
        private void CheckFilePollingTimer()
        {
            _lastWriteTime = File.GetLastWriteTime(logFileInfo.FullName);

            _pollingTimer = new System.Windows.Forms.Timer();
            _pollingTimer.Interval = 1000; // Verifica a cada 1 segundo
            _pollingTimer.Tick += (s, e) =>
            {
                DateTime currentWriteTime = File.GetLastWriteTime(logFileInfo.FullName);
                if (currentWriteTime != _lastWriteTime)
                {
                    _lastWriteTime = currentWriteTime;
                    _pendingUpdate = true;
                    _debounceTimer.Stop(); // Reinicia debounce
                    _debounceTimer.Start();
                }
            };
            _pollingTimer.Start();
        }

        private void VerificaStatusServicoTimer()
        {
            System.Windows.Forms.Timer statusCheckerTimer = new System.Windows.Forms.Timer();
            statusCheckerTimer.Interval = 3000;
            statusCheckerTimer.Tick += (s, e) =>
            {
                AtualizarStatusDoServico();
            };
            statusCheckerTimer.Start();
        }

        private void FrmServico_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_fileWatcher != null)
            {
                _fileWatcher.EnableRaisingEvents = false;
                _fileWatcher.Dispose();
            }
        }

        private void FrmServico_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_fileWatcher != null)
            {
                _fileWatcher.EnableRaisingEvents = false;
                _fileWatcher.Dispose();
            }
        }

        private void dateTimePickerLogs_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                string logFileSelected = GetDatedLogFilePath(dateTimePickerLogs.Value);
                if (!File.Exists(logFileSelected))
                {
                    lblDataLogs.Text = "Não foi encontrado arquivo de log para a data selecionada.";
                    return;
                }
                logFileInfo = new FileInfo(logFileSelected);
                logFileName = logFileInfo.FullName;
                StartFileWatcher();
                LoadLog(logFileName);
                lblDataLogs.Text = logFileInfo.FullName;
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
