namespace RenoveBackupService.Configurator
{
    partial class FrmBackup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBackup));
            MenuVerticalPanel = new Panel();
            btnUpload = new Button();
            btnServico = new Button();
            btnConfiguracoes = new Button();
            pictureBox1 = new PictureBox();
            BarraDeTituloPanel = new Panel();
            pictureBoxAsMinimizeButton = new PictureBox();
            pictureBoxAsRestoreButton = new PictureBox();
            pictureBoxAsButtonMaximize = new PictureBox();
            pictureBoxAsButtonClose = new PictureBox();
            pictureBoxAsButtonMenu = new PictureBox();
            ConteudoPanel = new Panel();
            MenuVerticalPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            BarraDeTituloPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAsMinimizeButton).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAsRestoreButton).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAsButtonMaximize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAsButtonClose).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAsButtonMenu).BeginInit();
            SuspendLayout();
            // 
            // MenuVerticalPanel
            // 
            MenuVerticalPanel.BackColor = Color.Firebrick;
            MenuVerticalPanel.Controls.Add(btnUpload);
            MenuVerticalPanel.Controls.Add(btnServico);
            MenuVerticalPanel.Controls.Add(btnConfiguracoes);
            MenuVerticalPanel.Controls.Add(pictureBox1);
            MenuVerticalPanel.Dock = DockStyle.Left;
            MenuVerticalPanel.Location = new Point(0, 0);
            MenuVerticalPanel.Name = "MenuVerticalPanel";
            MenuVerticalPanel.Size = new Size(217, 836);
            MenuVerticalPanel.TabIndex = 0;
            // 
            // btnUpload
            // 
            btnUpload.FlatAppearance.BorderSize = 0;
            btnUpload.FlatAppearance.MouseDownBackColor = Color.Gainsboro;
            btnUpload.FlatAppearance.MouseOverBackColor = Color.White;
            btnUpload.FlatStyle = FlatStyle.Flat;
            btnUpload.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnUpload.ForeColor = Color.White;
            btnUpload.Image = (Image)resources.GetObject("btnUpload.Image");
            btnUpload.ImageAlign = ContentAlignment.MiddleLeft;
            btnUpload.Location = new Point(-3, 384);
            btnUpload.Name = "btnUpload";
            btnUpload.Size = new Size(217, 70);
            btnUpload.TabIndex = 3;
            btnUpload.Text = "Upload";
            btnUpload.UseVisualStyleBackColor = true;
            btnUpload.Click += btnUpload_Click;
            // 
            // btnServico
            // 
            btnServico.FlatAppearance.BorderSize = 0;
            btnServico.FlatAppearance.MouseDownBackColor = Color.Gainsboro;
            btnServico.FlatAppearance.MouseOverBackColor = Color.White;
            btnServico.FlatStyle = FlatStyle.Flat;
            btnServico.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnServico.ForeColor = Color.White;
            btnServico.Image = (Image)resources.GetObject("btnServico.Image");
            btnServico.ImageAlign = ContentAlignment.MiddleLeft;
            btnServico.Location = new Point(0, 308);
            btnServico.Name = "btnServico";
            btnServico.Size = new Size(217, 70);
            btnServico.TabIndex = 2;
            btnServico.Text = "Serviço";
            btnServico.UseVisualStyleBackColor = true;
            btnServico.Click += btnServico_Click;
            // 
            // btnConfiguracoes
            // 
            btnConfiguracoes.FlatAppearance.BorderSize = 0;
            btnConfiguracoes.FlatAppearance.MouseDownBackColor = Color.Gainsboro;
            btnConfiguracoes.FlatAppearance.MouseOverBackColor = Color.White;
            btnConfiguracoes.FlatStyle = FlatStyle.Flat;
            btnConfiguracoes.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnConfiguracoes.ForeColor = Color.White;
            btnConfiguracoes.Image = (Image)resources.GetObject("btnConfiguracoes.Image");
            btnConfiguracoes.ImageAlign = ContentAlignment.MiddleLeft;
            btnConfiguracoes.Location = new Point(0, 232);
            btnConfiguracoes.Name = "btnConfiguracoes";
            btnConfiguracoes.Size = new Size(217, 70);
            btnConfiguracoes.TabIndex = 1;
            btnConfiguracoes.Text = "Configurações";
            btnConfiguracoes.TextAlign = ContentAlignment.MiddleRight;
            btnConfiguracoes.UseVisualStyleBackColor = true;
            btnConfiguracoes.Click += btnConfiguracoes_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.renove_banner;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(217, 126);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // BarraDeTituloPanel
            // 
            BarraDeTituloPanel.BackColor = Color.Gainsboro;
            BarraDeTituloPanel.Controls.Add(pictureBoxAsMinimizeButton);
            BarraDeTituloPanel.Controls.Add(pictureBoxAsRestoreButton);
            BarraDeTituloPanel.Controls.Add(pictureBoxAsButtonMaximize);
            BarraDeTituloPanel.Controls.Add(pictureBoxAsButtonClose);
            BarraDeTituloPanel.Controls.Add(pictureBoxAsButtonMenu);
            BarraDeTituloPanel.Dock = DockStyle.Top;
            BarraDeTituloPanel.Location = new Point(217, 0);
            BarraDeTituloPanel.Name = "BarraDeTituloPanel";
            BarraDeTituloPanel.Size = new Size(830, 50);
            BarraDeTituloPanel.TabIndex = 1;
            BarraDeTituloPanel.MouseDown += BarraDeTituloPanel_MouseDown;
            // 
            // pictureBoxAsMinimizeButton
            // 
            pictureBoxAsMinimizeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBoxAsMinimizeButton.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxAsMinimizeButton.Cursor = Cursors.Hand;
            pictureBoxAsMinimizeButton.Image = (Image)resources.GetObject("pictureBoxAsMinimizeButton.Image");
            pictureBoxAsMinimizeButton.Location = new Point(746, 12);
            pictureBoxAsMinimizeButton.Name = "pictureBoxAsMinimizeButton";
            pictureBoxAsMinimizeButton.Size = new Size(20, 20);
            pictureBoxAsMinimizeButton.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxAsMinimizeButton.TabIndex = 4;
            pictureBoxAsMinimizeButton.TabStop = false;
            pictureBoxAsMinimizeButton.Click += pictureBoxAsMinimizeButton_Click;
            // 
            // pictureBoxAsRestoreButton
            // 
            pictureBoxAsRestoreButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBoxAsRestoreButton.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxAsRestoreButton.Cursor = Cursors.Hand;
            pictureBoxAsRestoreButton.Image = (Image)resources.GetObject("pictureBoxAsRestoreButton.Image");
            pictureBoxAsRestoreButton.Location = new Point(771, 12);
            pictureBoxAsRestoreButton.Name = "pictureBoxAsRestoreButton";
            pictureBoxAsRestoreButton.Size = new Size(20, 20);
            pictureBoxAsRestoreButton.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxAsRestoreButton.TabIndex = 3;
            pictureBoxAsRestoreButton.TabStop = false;
            pictureBoxAsRestoreButton.Visible = false;
            pictureBoxAsRestoreButton.Click += pictureBoxAsRestoreButton_Click;
            // 
            // pictureBoxAsButtonMaximize
            // 
            pictureBoxAsButtonMaximize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBoxAsButtonMaximize.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxAsButtonMaximize.Cursor = Cursors.Hand;
            pictureBoxAsButtonMaximize.Image = (Image)resources.GetObject("pictureBoxAsButtonMaximize.Image");
            pictureBoxAsButtonMaximize.Location = new Point(771, 12);
            pictureBoxAsButtonMaximize.Name = "pictureBoxAsButtonMaximize";
            pictureBoxAsButtonMaximize.Size = new Size(20, 20);
            pictureBoxAsButtonMaximize.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxAsButtonMaximize.TabIndex = 2;
            pictureBoxAsButtonMaximize.TabStop = false;
            pictureBoxAsButtonMaximize.Click += pictureBoxAsButtonMaximize_Click;
            // 
            // pictureBoxAsButtonClose
            // 
            pictureBoxAsButtonClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBoxAsButtonClose.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxAsButtonClose.Cursor = Cursors.Hand;
            pictureBoxAsButtonClose.Image = Properties.Resources.close;
            pictureBoxAsButtonClose.Location = new Point(798, 12);
            pictureBoxAsButtonClose.Name = "pictureBoxAsButtonClose";
            pictureBoxAsButtonClose.Size = new Size(20, 20);
            pictureBoxAsButtonClose.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxAsButtonClose.TabIndex = 1;
            pictureBoxAsButtonClose.TabStop = false;
            pictureBoxAsButtonClose.Click += pictureBoxAsButtonClose_Click;
            // 
            // pictureBoxAsButtonMenu
            // 
            pictureBoxAsButtonMenu.Cursor = Cursors.Hand;
            pictureBoxAsButtonMenu.Image = Properties.Resources.menu;
            pictureBoxAsButtonMenu.Location = new Point(6, 9);
            pictureBoxAsButtonMenu.Name = "pictureBoxAsButtonMenu";
            pictureBoxAsButtonMenu.Size = new Size(35, 35);
            pictureBoxAsButtonMenu.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxAsButtonMenu.TabIndex = 0;
            pictureBoxAsButtonMenu.TabStop = false;
            pictureBoxAsButtonMenu.Click += pictureBoxAsButtonMenu_Click;
            // 
            // ConteudoPanel
            // 
            ConteudoPanel.AutoScroll = true;
            ConteudoPanel.AutoScrollMargin = new Size(0, 30);
            ConteudoPanel.AutoScrollMinSize = new Size(1, 1);
            ConteudoPanel.Dock = DockStyle.Fill;
            ConteudoPanel.Location = new Point(217, 50);
            ConteudoPanel.Name = "ConteudoPanel";
            ConteudoPanel.Size = new Size(830, 786);
            ConteudoPanel.TabIndex = 2;
            // 
            // FrmBackup
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1047, 836);
            Controls.Add(ConteudoPanel);
            Controls.Add(BarraDeTituloPanel);
            Controls.Add(MenuVerticalPanel);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            Name = "FrmBackup";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmBackup";
            Load += FrmBackup_Load;
            MenuVerticalPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            BarraDeTituloPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxAsMinimizeButton).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAsRestoreButton).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAsButtonMaximize).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAsButtonClose).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxAsButtonMenu).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel MenuVerticalPanel;
        private Panel BarraDeTituloPanel;
        private Panel ConteudoPanel;
        private PictureBox pictureBoxAsButtonMenu;
        private PictureBox pictureBox1;
        private PictureBox pictureBoxAsButtonClose;
        private PictureBox pictureBoxAsButtonMaximize;
        private PictureBox pictureBoxAsRestoreButton;
        private PictureBox pictureBoxAsMinimizeButton;
        private Button btnConfiguracoes;
        private Button btnServico;
        private Button btnUpload;
    }
}