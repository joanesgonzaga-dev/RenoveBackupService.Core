using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicosGlobais.Model
{
    public class UploadResult
    {
        public bool Success { get; }
        public string Message { get;}

        public UploadResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
