using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using ServicosGlobais.Model;

namespace ServicosGlobais.Services
{
    public class VpsService : IVpsService
    {
        ILogger<VpsService>? _logger;

        public VpsService(ILogger<VpsService> logger)
        {
            _logger = logger;
        }
        public async Task<UploadResult> Upload(string localBackupFilePath, Uri url)
        {
            if (!File.Exists(localBackupFilePath))
            {
                return new UploadResult(false, "Arquivo local de banco de dados não foi encontrado!");
            }
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (MultipartFormDataContent formData = new MultipartFormDataContent())
                    {
                        byte[] fileBytes = await File.ReadAllBytesAsync(localBackupFilePath);
                        ByteArrayContent fileContent = new ByteArrayContent(fileBytes);
                        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/zip");
                        formData.Add(fileContent, "file", Path.GetFileName(localBackupFilePath));
                        HttpResponseMessage response = await client.PostAsync(url.AbsoluteUri, formData);

                        if (response.IsSuccessStatusCode)
                        {
                            return new UploadResult(true, $"Arquivo enviado com sucesso ao VPS!.{response.ReasonPhrase}");
                        }
                        else
                        {
                            return new UploadResult(false, $"Erro ao enviar arquivo: {response.StatusCode}, {response.ReasonPhrase}");
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
