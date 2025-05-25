namespace RenoveBackupService.Configurator
{
    partial class FrmConfiguracoes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            btnMysqlSumpSearch = new Button();
            txtMysqlDumpPath = new TextBox();
            label1 = new Label();
            groupBox2 = new GroupBox();
            label3 = new Label();
            label2 = new Label();
            btnGravarAgendamento = new Button();
            btnLimparTodosOsDias = new Button();
            btnMarcarTodosOsDias = new Button();
            checkedListBoxDiasDaSemana = new CheckedListBox();
            cbMinutoHora = new ComboBox();
            numericUpDown1 = new NumericUpDown();
            dateTimePicker1 = new DateTimePicker();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            pictureBoxStatusDb = new PictureBox();
            btnTestarConexao = new Button();
            txtDbPorta = new TextBox();
            label6 = new Label();
            txtDbSenha = new TextBox();
            label5 = new Label();
            txtDbUsuario = new TextBox();
            label4 = new Label();
            tabPage2 = new TabPage();
            checkBoxFtpAtivado = new CheckBox();
            pictureBoxStatusFtp = new PictureBox();
            btnTestarFtp = new Button();
            label11 = new Label();
            txtFtpRemoteDirectory = new TextBox();
            label10 = new Label();
            txtFtpPort = new TextBox();
            label9 = new Label();
            txtFtpPassword = new TextBox();
            label8 = new Label();
            txtFtpUser = new TextBox();
            label7 = new Label();
            txtFtpHost = new TextBox();
            tabPage3 = new TabPage();
            btnGravarUrl = new Button();
            txtUrlServidorBackup = new TextBox();
            label12 = new Label();
            checkBoxServidorRemotoAtivo = new CheckBox();
            groupBox3 = new GroupBox();
            label14 = new Label();
            label13 = new Label();
            btnLocalDbFileAlternativePath = new Button();
            txtCaminhoArquivoBackupAlternativo = new TextBox();
            btnLocalDbFilePath = new Button();
            txtCaminhoArquivoBackup = new TextBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxStatusDb).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxStatusFtp).BeginInit();
            tabPage3.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(btnMysqlSumpSearch);
            groupBox1.Controls.Add(txtMysqlDumpPath);
            groupBox1.Controls.Add(label1);
            groupBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(887, 128);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // btnMysqlSumpSearch
            // 
            btnMysqlSumpSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMysqlSumpSearch.Location = new Point(704, 63);
            btnMysqlSumpSearch.Name = "btnMysqlSumpSearch";
            btnMysqlSumpSearch.Size = new Size(160, 35);
            btnMysqlSumpSearch.TabIndex = 2;
            btnMysqlSumpSearch.Text = "Procurar";
            btnMysqlSumpSearch.UseVisualStyleBackColor = true;
            btnMysqlSumpSearch.Click += btnMysqlSumpSearch_Click;
            // 
            // txtMysqlDumpPath
            // 
            txtMysqlDumpPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtMysqlDumpPath.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            txtMysqlDumpPath.Location = new Point(31, 63);
            txtMysqlDumpPath.Name = "txtMysqlDumpPath";
            txtMysqlDumpPath.Size = new Size(667, 35);
            txtMysqlDumpPath.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(31, 39);
            label1.Name = "label1";
            label1.Size = new Size(305, 21);
            label1.TabIndex = 0;
            label1.Text = "Definir local do MySqlDump.exe (opcional)";
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(btnGravarAgendamento);
            groupBox2.Controls.Add(btnLimparTodosOsDias);
            groupBox2.Controls.Add(btnMarcarTodosOsDias);
            groupBox2.Controls.Add(checkedListBoxDiasDaSemana);
            groupBox2.Controls.Add(cbMinutoHora);
            groupBox2.Controls.Add(numericUpDown1);
            groupBox2.Controls.Add(dateTimePicker1);
            groupBox2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox2.Location = new Point(12, 146);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(887, 195);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Agendamento:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(31, 56);
            label3.Name = "label3";
            label3.Size = new Size(47, 21);
            label3.TabIndex = 8;
            label3.Text = "Hora:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(127, 55);
            label2.Name = "label2";
            label2.Size = new Size(111, 21);
            label2.TabIndex = 7;
            label2.Text = "Repetir a cada:";
            // 
            // btnGravarAgendamento
            // 
            btnGravarAgendamento.Location = new Point(263, 130);
            btnGravarAgendamento.Name = "btnGravarAgendamento";
            btnGravarAgendamento.Size = new Size(171, 44);
            btnGravarAgendamento.TabIndex = 6;
            btnGravarAgendamento.Text = "Gravar agendamento";
            btnGravarAgendamento.UseVisualStyleBackColor = true;
            btnGravarAgendamento.Click += btnGravarAgendamento_Click;
            // 
            // btnLimparTodosOsDias
            // 
            btnLimparTodosOsDias.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLimparTodosOsDias.Enabled = false;
            btnLimparTodosOsDias.Location = new Point(784, 117);
            btnLimparTodosOsDias.Name = "btnLimparTodosOsDias";
            btnLimparTodosOsDias.Size = new Size(80, 32);
            btnLimparTodosOsDias.TabIndex = 5;
            btnLimparTodosOsDias.Text = "Limpar";
            btnLimparTodosOsDias.UseVisualStyleBackColor = true;
            btnLimparTodosOsDias.Click += btnLimparTodosOsDias_Click;
            // 
            // btnMarcarTodosOsDias
            // 
            btnMarcarTodosOsDias.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMarcarTodosOsDias.Enabled = false;
            btnMarcarTodosOsDias.Location = new Point(784, 79);
            btnMarcarTodosOsDias.Name = "btnMarcarTodosOsDias";
            btnMarcarTodosOsDias.Size = new Size(80, 32);
            btnMarcarTodosOsDias.TabIndex = 4;
            btnMarcarTodosOsDias.Text = "Todos";
            btnMarcarTodosOsDias.UseVisualStyleBackColor = true;
            btnMarcarTodosOsDias.Click += btnTodosOsDias_Click;
            // 
            // checkedListBoxDiasDaSemana
            // 
            checkedListBoxDiasDaSemana.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            checkedListBoxDiasDaSemana.Enabled = false;
            checkedListBoxDiasDaSemana.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            checkedListBoxDiasDaSemana.FormattingEnabled = true;
            checkedListBoxDiasDaSemana.Location = new Point(440, 58);
            checkedListBoxDiasDaSemana.Name = "checkedListBoxDiasDaSemana";
            checkedListBoxDiasDaSemana.Size = new Size(338, 116);
            checkedListBoxDiasDaSemana.TabIndex = 3;
            // 
            // cbMinutoHora
            // 
            cbMinutoHora.Enabled = false;
            cbMinutoHora.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            cbMinutoHora.FormattingEnabled = true;
            cbMinutoHora.Items.AddRange(new object[] { "Minuto(s)", "Hora(s)" });
            cbMinutoHora.Location = new Point(216, 79);
            cbMinutoHora.Name = "cbMinutoHora";
            cbMinutoHora.Size = new Size(218, 33);
            cbMinutoHora.TabIndex = 2;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            numericUpDown1.Location = new Point(129, 79);
            numericUpDown1.Maximum = new decimal(new int[] { 24, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(81, 33);
            numericUpDown1.TabIndex = 1;
            numericUpDown1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CustomFormat = "HH:mm";
            dateTimePicker1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new Point(31, 79);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.ShowUpDown = true;
            dateTimePicker1.Size = new Size(92, 33);
            dateTimePicker1.TabIndex = 0;
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            tabControl1.Location = new Point(12, 521);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(887, 243);
            tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(pictureBoxStatusDb);
            tabPage1.Controls.Add(btnTestarConexao);
            tabPage1.Controls.Add(txtDbPorta);
            tabPage1.Controls.Add(label6);
            tabPage1.Controls.Add(txtDbSenha);
            tabPage1.Controls.Add(label5);
            tabPage1.Controls.Add(txtDbUsuario);
            tabPage1.Controls.Add(label4);
            tabPage1.Location = new Point(4, 30);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(879, 209);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Acesso ao Banco de dados";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // pictureBoxStatusDb
            // 
            pictureBoxStatusDb.Location = new Point(645, 62);
            pictureBoxStatusDb.Name = "pictureBoxStatusDb";
            pictureBoxStatusDb.Size = new Size(70, 72);
            pictureBoxStatusDb.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxStatusDb.TabIndex = 7;
            pictureBoxStatusDb.TabStop = false;
            // 
            // btnTestarConexao
            // 
            btnTestarConexao.Location = new Point(503, 77);
            btnTestarConexao.Name = "btnTestarConexao";
            btnTestarConexao.Size = new Size(136, 44);
            btnTestarConexao.TabIndex = 6;
            btnTestarConexao.Text = "Testar conexão";
            btnTestarConexao.UseVisualStyleBackColor = true;
            btnTestarConexao.Click += btnTestarConexao_Click;
            // 
            // txtDbPorta
            // 
            txtDbPorta.Location = new Point(397, 86);
            txtDbPorta.Name = "txtDbPorta";
            txtDbPorta.Size = new Size(100, 29);
            txtDbPorta.TabIndex = 5;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(397, 62);
            label6.Name = "label6";
            label6.Size = new Size(49, 21);
            label6.TabIndex = 4;
            label6.Text = "Porta:";
            // 
            // txtDbSenha
            // 
            txtDbSenha.Location = new Point(212, 86);
            txtDbSenha.Name = "txtDbSenha";
            txtDbSenha.PasswordChar = '*';
            txtDbSenha.PlaceholderText = "********";
            txtDbSenha.Size = new Size(179, 29);
            txtDbSenha.TabIndex = 3;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(212, 62);
            label5.Name = "label5";
            label5.Size = new Size(56, 21);
            label5.TabIndex = 2;
            label5.Text = "Senha:";
            // 
            // txtDbUsuario
            // 
            txtDbUsuario.Location = new Point(27, 86);
            txtDbUsuario.Name = "txtDbUsuario";
            txtDbUsuario.Size = new Size(179, 29);
            txtDbUsuario.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(27, 62);
            label4.Name = "label4";
            label4.Size = new Size(67, 21);
            label4.TabIndex = 0;
            label4.Text = "Usuário:";
            // 
            // tabPage2
            // 
            tabPage2.AutoScroll = true;
            tabPage2.Controls.Add(checkBoxFtpAtivado);
            tabPage2.Controls.Add(pictureBoxStatusFtp);
            tabPage2.Controls.Add(btnTestarFtp);
            tabPage2.Controls.Add(label11);
            tabPage2.Controls.Add(txtFtpRemoteDirectory);
            tabPage2.Controls.Add(label10);
            tabPage2.Controls.Add(txtFtpPort);
            tabPage2.Controls.Add(label9);
            tabPage2.Controls.Add(txtFtpPassword);
            tabPage2.Controls.Add(label8);
            tabPage2.Controls.Add(txtFtpUser);
            tabPage2.Controls.Add(label7);
            tabPage2.Controls.Add(txtFtpHost);
            tabPage2.Location = new Point(4, 30);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(879, 209);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Servidor FTP";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // checkBoxFtpAtivado
            // 
            checkBoxFtpAtivado.AutoSize = true;
            checkBoxFtpAtivado.Location = new Point(27, 25);
            checkBoxFtpAtivado.Name = "checkBoxFtpAtivado";
            checkBoxFtpAtivado.Size = new Size(82, 25);
            checkBoxFtpAtivado.TabIndex = 12;
            checkBoxFtpAtivado.Text = "Ativado";
            checkBoxFtpAtivado.UseVisualStyleBackColor = true;
            // 
            // pictureBoxStatusFtp
            // 
            pictureBoxStatusFtp.Location = new Point(401, 119);
            pictureBoxStatusFtp.Name = "pictureBoxStatusFtp";
            pictureBoxStatusFtp.Size = new Size(70, 72);
            pictureBoxStatusFtp.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxStatusFtp.TabIndex = 11;
            pictureBoxStatusFtp.TabStop = false;
            // 
            // btnTestarFtp
            // 
            btnTestarFtp.Location = new Point(235, 137);
            btnTestarFtp.Name = "btnTestarFtp";
            btnTestarFtp.Size = new Size(160, 39);
            btnTestarFtp.TabIndex = 10;
            btnTestarFtp.Text = "Testar Conexão FTP";
            btnTestarFtp.UseVisualStyleBackColor = true;
            btnTestarFtp.Click += btnTestarFtp_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(24, 119);
            label11.Name = "label11";
            label11.Size = new Size(127, 21);
            label11.TabIndex = 9;
            label11.Text = "Diretório remoto";
            // 
            // txtFtpRemoteDirectory
            // 
            txtFtpRemoteDirectory.Location = new Point(24, 143);
            txtFtpRemoteDirectory.Name = "txtFtpRemoteDirectory";
            txtFtpRemoteDirectory.Size = new Size(205, 29);
            txtFtpRemoteDirectory.TabIndex = 8;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(657, 59);
            label10.Name = "label10";
            label10.Size = new Size(46, 21);
            label10.TabIndex = 7;
            label10.Text = "Porta";
            // 
            // txtFtpPort
            // 
            txtFtpPort.Location = new Point(657, 83);
            txtFtpPort.Name = "txtFtpPort";
            txtFtpPort.Size = new Size(114, 29);
            txtFtpPort.TabIndex = 6;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(446, 59);
            label9.Name = "label9";
            label9.Size = new Size(53, 21);
            label9.TabIndex = 5;
            label9.Text = "Senha";
            // 
            // txtFtpPassword
            // 
            txtFtpPassword.Location = new Point(446, 83);
            txtFtpPassword.Name = "txtFtpPassword";
            txtFtpPassword.PasswordChar = '*';
            txtFtpPassword.PlaceholderText = "******";
            txtFtpPassword.Size = new Size(205, 29);
            txtFtpPassword.TabIndex = 4;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(235, 59);
            label8.Name = "label8";
            label8.Size = new Size(64, 21);
            label8.TabIndex = 3;
            label8.Text = "Usuário";
            // 
            // txtFtpUser
            // 
            txtFtpUser.Location = new Point(235, 83);
            txtFtpUser.Name = "txtFtpUser";
            txtFtpUser.Size = new Size(205, 29);
            txtFtpUser.TabIndex = 2;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(24, 59);
            label7.Name = "label7";
            label7.Size = new Size(42, 21);
            label7.TabIndex = 1;
            label7.Text = "Host";
            // 
            // txtFtpHost
            // 
            txtFtpHost.Location = new Point(27, 83);
            txtFtpHost.Name = "txtFtpHost";
            txtFtpHost.Size = new Size(202, 29);
            txtFtpHost.TabIndex = 0;
            // 
            // tabPage3
            // 
            tabPage3.AutoScroll = true;
            tabPage3.Controls.Add(btnGravarUrl);
            tabPage3.Controls.Add(txtUrlServidorBackup);
            tabPage3.Controls.Add(label12);
            tabPage3.Controls.Add(checkBoxServidorRemotoAtivo);
            tabPage3.Location = new Point(4, 30);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(879, 209);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Servidor de Backup";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnGravarUrl
            // 
            btnGravarUrl.Location = new Point(512, 77);
            btnGravarUrl.Name = "btnGravarUrl";
            btnGravarUrl.Size = new Size(160, 39);
            btnGravarUrl.TabIndex = 11;
            btnGravarUrl.Text = "Gravar URL";
            btnGravarUrl.UseVisualStyleBackColor = true;
            btnGravarUrl.Click += btnGravarUrl_Click;
            // 
            // txtUrlServidorBackup
            // 
            txtUrlServidorBackup.Location = new Point(27, 83);
            txtUrlServidorBackup.Name = "txtUrlServidorBackup";
            txtUrlServidorBackup.Size = new Size(479, 29);
            txtUrlServidorBackup.TabIndex = 2;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(24, 59);
            label12.Name = "label12";
            label12.Size = new Size(178, 21);
            label12.TabIndex = 1;
            label12.Text = "URL de envio (Endpoint)";
            // 
            // checkBoxServidorRemotoAtivo
            // 
            checkBoxServidorRemotoAtivo.AutoSize = true;
            checkBoxServidorRemotoAtivo.Location = new Point(27, 25);
            checkBoxServidorRemotoAtivo.Name = "checkBoxServidorRemotoAtivo";
            checkBoxServidorRemotoAtivo.Size = new Size(82, 25);
            checkBoxServidorRemotoAtivo.TabIndex = 0;
            checkBoxServidorRemotoAtivo.Text = "Ativado";
            checkBoxServidorRemotoAtivo.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox3.Controls.Add(label14);
            groupBox3.Controls.Add(label13);
            groupBox3.Controls.Add(btnLocalDbFileAlternativePath);
            groupBox3.Controls.Add(txtCaminhoArquivoBackupAlternativo);
            groupBox3.Controls.Add(btnLocalDbFilePath);
            groupBox3.Controls.Add(txtCaminhoArquivoBackup);
            groupBox3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            groupBox3.Location = new Point(12, 347);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(887, 168);
            groupBox3.TabIndex = 3;
            groupBox3.TabStop = false;
            groupBox3.Text = "Local para arquivo de backup:";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(31, 95);
            label14.Name = "label14";
            label14.Size = new Size(221, 21);
            label14.TabIndex = 7;
            label14.Text = "caminho alternativo (opcional)";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(28, 27);
            label13.Name = "label13";
            label13.Size = new Size(126, 21);
            label13.TabIndex = 6;
            label13.Text = "caminho padrão:";
            // 
            // btnLocalDbFileAlternativePath
            // 
            btnLocalDbFileAlternativePath.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLocalDbFileAlternativePath.Location = new Point(704, 116);
            btnLocalDbFileAlternativePath.Name = "btnLocalDbFileAlternativePath";
            btnLocalDbFileAlternativePath.Size = new Size(160, 39);
            btnLocalDbFileAlternativePath.TabIndex = 5;
            btnLocalDbFileAlternativePath.Text = "Procurar";
            btnLocalDbFileAlternativePath.UseVisualStyleBackColor = true;
            btnLocalDbFileAlternativePath.Click += btnLocalDbFileAlternativePath_Click;
            // 
            // txtCaminhoArquivoBackupAlternativo
            // 
            txtCaminhoArquivoBackupAlternativo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCaminhoArquivoBackupAlternativo.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            txtCaminhoArquivoBackupAlternativo.Location = new Point(31, 118);
            txtCaminhoArquivoBackupAlternativo.Name = "txtCaminhoArquivoBackupAlternativo";
            txtCaminhoArquivoBackupAlternativo.Size = new Size(667, 35);
            txtCaminhoArquivoBackupAlternativo.TabIndex = 4;
            // 
            // btnLocalDbFilePath
            // 
            btnLocalDbFilePath.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLocalDbFilePath.Location = new Point(704, 48);
            btnLocalDbFilePath.Name = "btnLocalDbFilePath";
            btnLocalDbFilePath.Size = new Size(160, 39);
            btnLocalDbFilePath.TabIndex = 3;
            btnLocalDbFilePath.Text = "Procurar";
            btnLocalDbFilePath.UseVisualStyleBackColor = true;
            btnLocalDbFilePath.Click += btnLocalDbFilePath_Click;
            // 
            // txtCaminhoArquivoBackup
            // 
            txtCaminhoArquivoBackup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCaminhoArquivoBackup.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            txtCaminhoArquivoBackup.Location = new Point(31, 51);
            txtCaminhoArquivoBackup.Name = "txtCaminhoArquivoBackup";
            txtCaminhoArquivoBackup.Size = new Size(667, 35);
            txtCaminhoArquivoBackup.TabIndex = 0;
            // 
            // FrmConfiguracoes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(911, 776);
            Controls.Add(groupBox3);
            Controls.Add(tabControl1);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmConfiguracoes";
            Text = "FrmConfiguracoes";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxStatusDb).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxStatusFtp).EndInit();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Label label1;
        private TextBox txtMysqlDumpPath;
        private Button btnMysqlSumpSearch;
        private GroupBox groupBox2;
        private ComboBox cbMinutoHora;
        private NumericUpDown numericUpDown1;
        private DateTimePicker dateTimePicker1;
        private CheckedListBox checkedListBoxDiasDaSemana;
        private Button btnLimparTodosOsDias;
        private Button btnMarcarTodosOsDias;
        private Button btnGravarAgendamento;
        private Label label2;
        private Label label3;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TextBox txtDbPorta;
        private Label label6;
        private TextBox txtDbSenha;
        private Label label5;
        private TextBox txtDbUsuario;
        private Label label4;
        private Button btnTestarConexao;
        private PictureBox pictureBoxStatusDb;
        private GroupBox groupBox3;
        private Button btnLocalDbFilePath;
        private TextBox txtCaminhoArquivoBackup;
        private TextBox txtFtpHost;
        private Label label7;
        private Label label9;
        private TextBox txtFtpPassword;
        private Label label8;
        private TextBox txtFtpUser;
        private Label label10;
        private TextBox txtFtpPort;
        private Label label11;
        private TextBox txtFtpRemoteDirectory;
        private Button btnTestarFtp;
        private PictureBox pictureBoxStatusFtp;
        private TabPage tabPage3;
        private CheckBox checkBoxFtpAtivado;
        private CheckBox checkBoxServidorRemotoAtivo;
        private Label label12;
        private TextBox txtUrlServidorBackup;
        private Button btnGravarUrl;
        private Label label14;
        private Label label13;
        private Button btnLocalDbFileAlternativePath;
        private TextBox txtCaminhoArquivoBackupAlternativo;
    }
}