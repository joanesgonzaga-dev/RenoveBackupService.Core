namespace RenoveBackupService.Configurator
{
    public partial class FrmCustomProgressBar : Form
    {
        public FrmCustomProgressBar(string mensagem)
        {
            InitializeComponent();
            lblMensagem.Text = mensagem;
            progressBarFtp.Style = ProgressBarStyle.Continuous;
            
        }
    }
}
