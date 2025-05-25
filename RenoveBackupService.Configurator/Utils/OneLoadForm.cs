using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenoveBackupService.Configurator.Utils
{
    public class OneLoadForm
    {
        public static void TargetForm(string FormName)
        {
            var openedForm = Application.OpenForms.Cast<Form>().FirstOrDefault(f => f.Name == FormName);

            if (openedForm != null) {

                openedForm.Show();
                openedForm.BringToFront();
                openedForm.Focus();
            }

            else
            {
                var formType = Type.GetType($"RenoveBackupService.Configurator.{FormName}");
                var formInstance = (Form)Activator.CreateInstance(formType);
                formInstance.Show();
                formInstance.BringToFront();
                formInstance.Focus();
            }
        }
    }
}
