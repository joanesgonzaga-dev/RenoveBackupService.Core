using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicosGlobais.Model;

namespace ServicosGlobais.Services
{
    public interface IVpsService
    {
        public Task<UploadResult> Upload(string? localBackupFilePath, Uri? url);
    }
}
