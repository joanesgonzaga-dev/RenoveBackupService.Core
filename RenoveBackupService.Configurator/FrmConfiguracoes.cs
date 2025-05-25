using Microsoft.Data.Sqlite;
//using Quartz.Util;
using ServicosGlobais.Data;
using ServicosGlobais.Model;
using ServicosGlobais.Model.Enum;
using ServicosGlobais.Services;
using ServicosGlobais.Utils;
using System.Drawing;
using System.Windows.Forms;

namespace RenoveBackupService.Configurator
{
    public partial class FrmConfiguracoes : Form
    {
        string systemRootPath = string.Empty;
        string? systemDriveLetter = string.Empty;
        string RenoveRootPath = string.Empty;
        string mysqlDumpFileAndPath = string.Empty;
        List<DiaDaSemana>? diasDaSemana = null;
        ConfigService _configService { get; set; }

        public FrmConfiguracoes()
        {
            InitializeComponent();
            LocalizaPastaRenove();
            CarregaCheckedListBoxDiasDaSemana();
            CarregaCronExpressionDoBanco();
            CarregaCaminhoArquivoBackup(false);
            CarregaCaminhoArquivoBackup(true);
            LerDadosDeConexaoDoBanco();
            ControlaIconeAcessoBanco();
            ControlaIconeConexaoFtp(false);
            CarregaConfiguracaoAcessoFtp();
            CarregaConfiguracaoServidorRemotoDeBackup();
            cbMinutoHora.SelectedIndex = 1;
        }
        private void btnMysqlSumpSearch_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Title = "Selecione o mysqldump.exe",
                Filter = "Executáveis (*.exe)|*.exe",
                InitialDirectory = RenoveRootPath
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("Seleção cancelada!", "Localizar MySqlDump.exe", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            else
            {
                mysqlDumpFileAndPath = openFileDialog.FileName;

                if (MessageBox.Show($"Salvar o caminho a seguir para MySqldump.exe? \n{mysqlDumpFileAndPath}",
                    "Configurações",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _configService = new ConfigService();
                    int retorno = _configService.SalvarCaminhoMySqlDumpExe(new FileInfo(mysqlDumpFileAndPath));

                    if (retorno > 0)
                    {
                        txtMysqlDumpPath.Text = mysqlDumpFileAndPath;
                    }
                }
            }
        }

        private void LocalizaPastaRenove()
        {
            FileInfo fileInfo = new ConfigService().RetornaMysqldumpExeInfoFromDb();
            if (fileInfo != null)
            {
                txtMysqlDumpPath.Text = fileInfo.FullName;
                return;
            }

            systemRootPath = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            systemDriveLetter = Path.GetPathRoot(systemRootPath);
            RenoveRootPath = Path.Combine(systemDriveLetter, @"SistemaRenove\db\bin\");
            mysqlDumpFileAndPath = Path.Combine(RenoveRootPath, "mysqldump.exe");

            if (Directory.Exists(RenoveRootPath))
            {
                if (File.Exists(mysqlDumpFileAndPath))
                {
                    txtMysqlDumpPath.Text = mysqlDumpFileAndPath;
                }
                else
                {
                    txtMysqlDumpPath.Text = "mysqldump.exe não localizado";
                }
            }
            else
            {
                txtMysqlDumpPath.Text = "Diretório do sistema Renove não localizado";
            }
        }

        private void CarregaCheckedListBoxDiasDaSemana()
        {
            diasDaSemana = new ConfigService().RetornaDiasDaSemanaAgendamento();
            checkedListBoxDiasDaSemana.Items.Clear();
            foreach (var diaDaSemana in diasDaSemana)
            {
                checkedListBoxDiasDaSemana.Items.Add(diaDaSemana, diaDaSemana.isAtivo);
            }
        }

        private void CarregaCronExpressionDoBanco()
        {
            _configService = new ConfigService();
            CronExpressionModel cronExpression = _configService.RetornaExpressaoCronDoBd();
            int ano = DateTime.Now.Year;
            int mes = cronExpression.Month.Equals("*") ? DateTime.Now.Month : int.Parse(cronExpression.Month);
            int dia = DateTime.Now.Day; //Não há suporte para especificar o dia do mês para execução do backup;
            int hora = int.Parse(cronExpression.Hours);
            int minuto = int.Parse(cronExpression.Minutes);
            int sec = int.Parse(cronExpression.Seconds);
            dateTimePicker1.Value = new DateTime(ano, mes, dia, hora, minuto, sec);
            numericUpDown1.Value = decimal.Parse(cronExpression.RecurrenceTime);
            cbMinutoHora.SelectedIndex = int.Parse(cronExpression.RecurrenceType);
        }

        private void btnGravarAgendamento_Click(object sender, EventArgs e)
        {
            if (checkedListBoxDiasDaSemana.CheckedItems.Count == 0)
            {
                MessageBox.Show("Não é possível agendar backups sem selecionar ao menos 1 dia da semana\nNenhuma alteração será salva", "Agendamento de backup", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                CarregaCheckedListBoxDiasDaSemana();
                CarregaCronExpressionDoBanco();
                checkedListBoxDiasDaSemana.Refresh();
                return;
            }
            try
            {
                CronExpressionModel expressionModel = new CronExpressionModel();
                expressionModel.Seconds = "0";
                expressionModel.Minutes = dateTimePicker1.Value.Minute.ToString();
                expressionModel.Hours = dateTimePicker1.Value.Hour.ToString();
                expressionModel.DayOfMonth = "?";
                expressionModel.Month = "*";

                string diasContatenados = string.Empty;
                foreach (var item in checkedListBoxDiasDaSemana.CheckedItems)
                {
                    diasContatenados += ((DiaDaSemana)item).Ordem.ToString() + ",";
                }
                expressionModel.DayOfWeek = diasContatenados.Substring(0, diasContatenados.Length - 1);
                expressionModel.RecurrenceType = cbMinutoHora.SelectedIndex.ToString();
                expressionModel.RecurrenceTime = numericUpDown1.Value.ToString();

                _configService = new ConfigService();
                _configService.GravaCronExpressionNoBd(expressionModel);
                MessageBox.Show("Agendamento gravado com sucesso!", "Agendamento de backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro ao gravar o agendamento! \n {ex.Message}", "Agendamento de backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTodosOsDias_Click(object sender, EventArgs e)
        {
            if (diasDaSemana != null)
            {
                checkedListBoxDiasDaSemana.Items.Clear();
                foreach (var dia in diasDaSemana)
                {
                    dia.isAtivo = true;
                    checkedListBoxDiasDaSemana.Items.Add(dia, dia.isAtivo);
                }
                checkedListBoxDiasDaSemana.Refresh();
            }
        }
        private void btnLimparTodosOsDias_Click(object sender, EventArgs e)
        {
            if (diasDaSemana != null)
            {
                checkedListBoxDiasDaSemana.Items.Clear();
                foreach (var dia in diasDaSemana)
                {
                    dia.isAtivo = false;
                    checkedListBoxDiasDaSemana.Items.Add(dia, dia.isAtivo);
                }
                checkedListBoxDiasDaSemana.Refresh();
            }
        }

        private void btnLocalDbFilePath_Click(object sender, EventArgs e)
        {
            SalvarCaminhoArquivoBackup(false);
        }

        private void CarregaCaminhoArquivoBackup(bool isAlternative)
        {
            try
            {
                _configService = new ConfigService();
                string? diretorioArquivoBackup = _configService.RetornaCaminhoArquivoBackup(isAlternative);
                if (diretorioArquivoBackup != null)
                {
                    if (!isAlternative)
                    {
                        txtCaminhoArquivoBackup.Text = diretorioArquivoBackup;
                    }
                    else
                    {
                        txtCaminhoArquivoBackupAlternativo.Text = diretorioArquivoBackup;
                    }
                    
                }
                else
                {
                    if (!isAlternative)
                    {
                        txtCaminhoArquivoBackup.Text = "Diretório não definido";
                    }
                    else
                    {
                        txtCaminhoArquivoBackupAlternativo.Text = "Diretório não definido";
                    }
                    
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void btnTestarConexao_Click(object sender, EventArgs e)
        {
            try
            {
                ConfiguracaoBancoDeDadosRenove databaseSettings = new ConfiguracaoBancoDeDadosRenove
                {
                    Server = "localhost",
                    User = txtDbUsuario.Text.Trim(),
                    Password = txtDbSenha.Text.Trim(),
                    Porta = int.Parse(txtDbPorta.Text.Trim()),
                    Database = "Renove"
                };

                _configService = new ConfigService();
                if (_configService.TestaConexaoBancoRenove(databaseSettings, null))
                {
                    if (MessageBox.Show("Testes de conexão BEM sucedido!\n Você deseja salvar os dados de acesso?", "Configuração Banco Renove", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        _configService.SalvarDadosDeAcessoAoBancoRenove(databaseSettings);
                    }
                }
                else
                {
                    MessageBox.Show("Testes de conexão MAL sucedido!", "Configuração Banco Renove", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                ControlaIconeAcessoBanco();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Falha ao conectar!\n{ex.Message}", "Configuração Banco Renove", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void LerDadosDeConexaoDoBanco()
        {
            try
            {
                var dadosAcessoBanco = await _configService.RetornaDadosDeAcessoAoBancoRenove();
                if (string.IsNullOrWhiteSpace(dadosAcessoBanco.Server))
                {
                    txtDbUsuario.Text = "usuário não configurado";
                    txtDbSenha.Text = "";
                    txtDbPorta.Text = "porta não configurada";
                }
                else
                {
                    txtDbUsuario.Text = dadosAcessoBanco.User;
                    txtDbSenha.Text = dadosAcessoBanco.Password;
                    txtDbPorta.Text = dadosAcessoBanco.Porta.ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Monitora o estado da conexão e altera o ícone do banco de dados
        /// </summary>
        private async void ControlaIconeAcessoBanco()
        {
            try
            {
                _configService = new ConfigService();
                ConfiguracaoBancoDeDadosRenove? databaseSettings = await _configService.RetornaDadosDeAcessoAoBancoRenove();

                if (_configService.TestaConexaoBancoRenove(databaseSettings, null))
                {
                    //pictureBoxStatusDb.Image = Image.FromFile(Path.Combine("..\\..\\..\\..\\RenoveBackupService.Configurator\\Resources", "database_ok.png"));
                    pictureBoxStatusDb.Image = Image.FromFile(Path.Combine("Resources", "database_ok.png"));
                }
                else
                {
                    pictureBoxStatusDb.Image = Image.FromFile(Path.Combine("Resources", "database_off.png"));
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        private void CarregaConfiguracaoAcessoFtp()
        {
            try
            {
                _configService = new ConfigService();
                SftpSettings? ftpSettings = _configService.RetornaDadosAcessoFTP();

                if (ftpSettings != null)
                {
                    txtFtpHost.Text = string.IsNullOrEmpty(ftpSettings.Host) ? string.Empty : ftpSettings.Host;
                    txtFtpUser.Text = string.IsNullOrEmpty(ftpSettings.Username) ? string.Empty : ftpSettings.Username;
                    txtFtpPassword.Text = string.IsNullOrEmpty(ftpSettings.Password) ? string.Empty : ftpSettings.Password;
                    txtFtpPort.Text = ftpSettings.Port.ToString();
                    checkBoxFtpAtivado.Checked = ftpSettings.isAtivo;
                    txtFtpRemoteDirectory.Text = string.IsNullOrEmpty(ftpSettings.RemoteDirectoryPath) ? string.Empty : ftpSettings.RemoteDirectoryPath;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task CarregaConfiguracaoServidorRemotoDeBackup()
        {
            try
            {
                ConfigService configService = new ConfigService();
                var servidorRemoto = await _configService.RetornaDadosServidorVps();

                if (servidorRemoto != null)
                {
                    txtUrlServidorBackup.Text = servidorRemoto.Url;
                    checkBoxServidorRemotoAtivo.Checked = servidorRemoto.isAtivo;
                }

                else
                {
                    txtUrlServidorBackup.Text = string.Empty;
                    checkBoxServidorRemotoAtivo.Checked = false;
                }

            }
            catch (Exception)
            {
                txtUrlServidorBackup.Text = "Erro ao carregar dados do Servidor de backup remoto";
            }
        }

        private void ControlaIconeConexaoFtp(bool isConectado)
        {
            if (isConectado)
            {
                pictureBoxStatusFtp.Image = Image.FromFile(Path.Combine("Resources", "folder_ok.png"));
            }
            else
            {
                //pictureBoxStatusFtp.Image = Image.FromFile(Path.Combine("..\\..\\..\\..\\RenoveBackupService.Configurator\\Resources", "folder_off.png"));
                pictureBoxStatusFtp.Image = Image.FromFile(Path.Combine("Resources", "folder_off.png"));
            }
        }

        private async void btnTestarFtp_Click(object sender, EventArgs e)
        {
            try
            {
                ConfigService configService = new ConfigService();
                SftpSettings ftpSettings = new SftpSettings()
                {
                    Host = txtFtpHost.Text,
                    Username = txtFtpUser.Text,
                    Password = txtFtpPassword.Text,
                    Port = int.Parse(txtFtpPort.Text),
                    RemoteDirectoryPath = txtFtpRemoteDirectory.Text,
                    isAtivo = checkBoxFtpAtivado.Checked
                };

                using (FrmCustomProgressBar FrmProgressBar = new FrmCustomProgressBar("Tentando conectar ao servidor FTP"))
                {
                    FrmProgressBar.Show();
                    FrmProgressBar.Refresh();
                    FrmProgressBar.progressBarFtp.Value = 10;
                    var progress = new Progress<int>(value =>
                    {
                        FrmProgressBar.progressBarFtp.Value = (int)value;
                    });

                    bool isSuccess = await configService.TestaConexaoFtpAsync(ftpSettings, progress);
                    ControlaIconeConexaoFtp(isSuccess);

                    FrmProgressBar.Close();

                    if (isSuccess)
                    {
                        bool result = MessageBox.Show("Conexão BEM sucedida. Salvar dados fornecidos?", "Renove backup", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes;
                        if (result)
                        {
                            configService.SalvarDadosDeConexaoFtp(ftpSettings);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Conexão MAL sucedida. Reveja os parâmetros de conexão", "Renove backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }


            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnGravarUrl_Click(object sender, EventArgs e)
        {
            try
            {
                ConfigService configService = new ConfigService();

                VpsServerSettings backupRemotoSettings = new VpsServerSettings()
                {
                    Url = txtUrlServidorBackup.Text,
                    isAtivo = checkBoxServidorRemotoAtivo.Checked
                };

                int result = configService.SalvaDadosServidorRemotoDeBackup(backupRemotoSettings);
                if (result == 1)
                {
                    MessageBox.Show("Dados salvos com sucesso!", "Renove backup", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("Não foi possível salvar os dados fornecidos!", "Renove backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar os dados fornecidos! \n{ex.Message}", "Renove backup", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBoxFtpAtivado_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxFtpAtivado_CheckStateChanged(object sender, EventArgs e)
        {
            MessageBox.Show($"Alterou para {checkBoxFtpAtivado.Checked}");
        }

        private void btnLocalDbFileAlternativePath_Click(object sender, EventArgs e)
        {
            SalvarCaminhoArquivoBackup(true);
        }

        private void SalvarCaminhoArquivoBackup(bool isAlternative)
        {
            using (FolderBrowserDialog folderBrowser = new FolderBrowserDialog())
            {
                folderBrowser.Description = "Selecione a pasta para armazenar o backup";
                folderBrowser.ShowNewFolderButton = true;

                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    string arquivoBackupDir = folderBrowser.SelectedPath;
                    _configService = new ConfigService();
                    int isGravar = _configService.SalvarCaminhoArquivoBackupLocal(new DirectoryInfo(arquivoBackupDir), isAlternative: isAlternative);

                    if (isGravar == 1)
                    {
                        if (!isAlternative)
                        {
                            txtCaminhoArquivoBackup.Text = _configService.RetornaCaminhoArquivoBackup(isAlternative);
                        }
                        else
                        {
                            txtCaminhoArquivoBackupAlternativo.Text = _configService.RetornaCaminhoArquivoBackup(isAlternative);
                        }
                            
                    }
                    else
                    {
                        MessageBox.Show("Não foi possível salvar o local definido", "Renove backup", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
    }
}
