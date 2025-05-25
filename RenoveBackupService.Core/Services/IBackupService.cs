using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenoveBackupService.Core.Services
{
    public interface IBackupService
    {
        public Task ExecuteBackup(string dbUser, string dbPassword, List<string>? dbNameS);
    }
}
