using ServicosGlobais.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicosGlobais.Services
{
    public interface ISftpService
    {
        public Task<UploadResult> Upload(string localBackupFilePath, SftpSettings ftpSettings);
    }
}
