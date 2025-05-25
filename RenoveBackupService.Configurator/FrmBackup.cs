using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace RenoveBackupService.Configurator
{
    public partial class FrmBackup : Form
    {
        public FrmBackup()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hand, int wmsg, int wparam, int lparam);

        private void pictureBoxAsButtonMenu_Click(object sender, EventArgs e)
        {
            if (MenuVerticalPanel.Width == 217)
            {
                MenuVerticalPanel.Width = 70;
            }
            else
            {
                MenuVerticalPanel.Width = 217;
            }
        }

        private void pictureBoxAsButtonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBoxAsButtonMaximize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            pictureBoxAsRestoreButton.Visible = true;
            pictureBoxAsButtonMaximize.Visible = false;
            //pictureBoxAsButtonMaximize.Image = 
        }

        private void pictureBoxAsMinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBoxAsRestoreButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            pictureBoxAsRestoreButton.Visible = false;
            pictureBoxAsButtonMaximize.Visible = true;
        }

        private void BarraDeTituloPanel_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        //private void btnInicio_MouseHover(object sender, EventArgs e)
        //{
        //    btnInicio.ForeColor = Color.Red;
        //}

        //private void btnInicio_MouseLeave(object sender, EventArgs e)
        //{
        //    btnInicio.ForeColor = Color.White;
        //}

        private void btnConfiguracoes_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new FrmConfiguracoes());
        }

        private void AbrirFormInPanel(object FormFilho)
        {
            string FormName = FormFilho.GetType().Name;
            var openedForm = Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.Name == FormName);

            if (openedForm != null)
            {
                openedForm.Show();
                openedForm.BringToFront();
                openedForm.Focus();
            }

            else
            {
                var formType = Type.GetType($"RenoveBackupService.Configurator.{FormName}");
                var formInstance = (Form)Activator.CreateInstance(formType);
                formInstance.TopLevel = false;
                formInstance.Dock = DockStyle.Fill;
                this.ConteudoPanel.Controls.Add(formInstance);
                formInstance.Show();
                formInstance.BringToFront();
                formInstance.Focus();
            }
        }

        private void btnServico_Click(object sender, EventArgs e)
        {
            string FormName = nameof(FrmServico);
            var openedForm = Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.Name == FormName);

            if (openedForm != null)
            {
                openedForm.Show();
                openedForm.BringToFront();
                openedForm.Focus();
            }

            else
            {
                var formType = Type.GetType($"RenoveBackupService.Configurator.{FormName}");
                var formInstance = (Form)Activator.CreateInstance(formType);
                formInstance.TopLevel = false;
                formInstance.Dock = DockStyle.Fill;
                this.ConteudoPanel.Controls.Add(formInstance);
                formInstance.Show();
                formInstance.BringToFront();
                formInstance.Focus();
            }
        }

        private void FrmBackup_Load(object sender, EventArgs e)
        {
            string FormName = nameof(FrmConfiguracoes);
            var openedForm = Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.Name == FormName);
            var formType = Type.GetType($"RenoveBackupService.Configurator.{FormName}");
            var formInstance = (Form)Activator.CreateInstance(formType);
            formInstance.TopLevel = false;
            formInstance.Dock = DockStyle.Fill;
            this.ConteudoPanel.Controls.Add(formInstance);
            formInstance.Show();
            formInstance.BringToFront();
            formInstance.Focus();
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            //string FormName = nameof(FrmDashboard);
            //var openedForm = Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.Name == FormName);

            //if (openedForm != null)
            //{
            //    openedForm.Show();
            //    openedForm.BringToFront();
            //    openedForm.Focus();
            //}

            //else
            //{
            //    var formType = Type.GetType($"RenoveBackupService.Configurator.{FormName}");
            //    var formInstance = (Form)Activator.CreateInstance(formType);
            //    formInstance.TopLevel = false;
            //    formInstance.Dock = DockStyle.Fill;
            //    this.ConteudoPanel.Controls.Add(formInstance);
            //    formInstance.Show();
            //    formInstance.BringToFront();
            //    formInstance.Focus();
            //}
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new FrmUpload());
        }
    }
}
