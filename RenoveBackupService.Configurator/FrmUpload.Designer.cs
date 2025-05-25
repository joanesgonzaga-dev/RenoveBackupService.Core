namespace RenoveBackupService.Configurator
{
    partial class FrmUpload
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
            progressBarUpload = new ProgressBar();
            checkedListBox1 = new CheckedListBox();
            btnEnviarBackup = new Button();
            cbTipoEnvio = new ComboBox();
            SuspendLayout();
            // 
            // progressBarVPS
            // 
            progressBarUpload.Location = new Point(235, 342);
            progressBarUpload.Name = "progressBarVPS";
            progressBarUpload.Size = new Size(337, 35);
            progressBarUpload.TabIndex = 3;
            // 
            // checkedListBox1
            // 
            checkedListBox1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new Point(235, 77);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(337, 144);
            checkedListBox1.TabIndex = 0;
            // 
            // btnEnviarBackup
            // 
            btnEnviarBackup.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnEnviarBackup.Location = new Point(235, 276);
            btnEnviarBackup.Name = "btnEnviarBackup";
            btnEnviarBackup.Size = new Size(337, 50);
            btnEnviarBackup.TabIndex = 1;
            btnEnviarBackup.Text = "Gerar e Enviar";
            btnEnviarBackup.UseVisualStyleBackColor = true;
            btnEnviarBackup.Click += btnEnviarBackup_Click;
            // 
            // cbTipoEnvio
            // 
            cbTipoEnvio.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTipoEnvio.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            cbTipoEnvio.FormattingEnabled = true;
            cbTipoEnvio.Items.AddRange(new object[] { "Enviar para VPS", "Enviar para FTP" });
            cbTipoEnvio.Location = new Point(235, 229);
            cbTipoEnvio.Name = "cbTipoEnvio";
            cbTipoEnvio.Size = new Size(337, 33);
            cbTipoEnvio.TabIndex = 4;
            // 
            // FrmUpload
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(880, 706);
            Controls.Add(cbTipoEnvio);
            Controls.Add(checkedListBox1);
            Controls.Add(btnEnviarBackup);
            Controls.Add(progressBarUpload);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmUpload";
            Text = "FrmUpload";
            ResumeLayout(false);
        }

        #endregion
        private ProgressBar progressBarUpload;
        private CheckedListBox checkedListBox1;
        private Button btnEnviarBackup;
        private ComboBox cbTipoEnvio;
    }
}