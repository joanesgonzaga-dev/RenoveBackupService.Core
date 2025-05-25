using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicosGlobais.Model
{
    public class SftpSettings
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; } = 21;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string RemoteDirectoryPath { get; set; } = string.Empty;

        public bool isAtivo { get; set; }
    }
}
