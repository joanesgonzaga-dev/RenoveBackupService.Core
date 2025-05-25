using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenoveBackupService.Core.Services;

namespace RenoveBackupService.Core.Utils
{
    /// <summary>
    /// Classe estática para extender funcionalidades de um DirectoryInfo
    /// </summary>
    public static class DirectoryExtensions
    {
        /// <summary>
        /// Faz x tentativas de exclusão de um diretório, aguardando o tempo definido entre elas
        /// </summary>
        /// <param name="directory">diretório a ser excluído</param>
        /// <param name="_fileLogger">arquivo de log monitorado pelo FileSystemWatcher</param>
        /// <param name="retrials">Quantidade de tentativas de exclusão do diretório</param>
        /// <param name="delay">tempo de espera entre as tentativas (Padrão = 500ms (meio segundo))</param>
        /// <param name="recursive">deve apagar todos os subdiretórios?</param>
        /// <returns>IOException, UnauthorizedAccessException, Exception</returns>
        public static bool DeleteWithRetrials(this DirectoryInfo directory, ref ILogger<BackupService> _fileLogger, int retrials = 5, TimeSpan? delay = null, bool recursive = true)
        {
            delay ??= TimeSpan.FromMilliseconds(500);

            for (int i = 0; i < retrials; i++)
            {
                try
                {
                    directory.Delete(recursive);
                    return true;
                }
                catch (IOException ex)
                {
                    _fileLogger.LogError($"Erro ao acessar o diretório temporário \"{directory.FullName.ToUpper()}\" para exclusão: {ex.Message}");
                }

                catch (UnauthorizedAccessException ex)
                {
                    _fileLogger.LogError($"Acesso Negado ao diretório temporário \"{directory.FullName.ToUpper()}\" para exclusão: {ex.Message}");
                }

                catch (Exception ex)
                {
                    _fileLogger.LogError($"Erro ao tentar excluir o arquivo temporário \"{directory.FullName.ToUpper()}\": {ex.Message}");
                } 
            }

            return false;
        }
    }
}
