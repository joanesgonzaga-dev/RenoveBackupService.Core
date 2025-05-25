namespace RenoveBackupService.Configurator
{
    partial class FrmCustomProgressBar
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
            lblMensagem = new Label();
            progressBarFtp = new ProgressBar();
            SuspendLayout();
            // 
            // lblMensagem
            // 
            lblMensagem.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblMensagem.Location = new Point(73, 8);
            lblMensagem.Name = "lblMensagem";
            lblMensagem.Size = new Size(294, 26);
            lblMensagem.TabIndex = 0;
            lblMensagem.Text = "Conectando...";
            lblMensagem.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // progressBarFtp
            // 
            progressBarFtp.Location = new Point(73, 37);
            progressBarFtp.Name = "progressBarFtp";
            progressBarFtp.Size = new Size(294, 23);
            progressBarFtp.TabIndex = 1;
            // 
            // FrmProgressBarFtpTest
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gainsboro;
            ClientSize = new Size(440, 77);
            Controls.Add(progressBarFtp);
            Controls.Add(lblMensagem);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmProgressBarFtpTest";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmProgressBarFtpTest";
            ResumeLayout(false);
        }

        #endregion

        private Label lblMensagem;
        public ProgressBar progressBarFtp;
    }
}