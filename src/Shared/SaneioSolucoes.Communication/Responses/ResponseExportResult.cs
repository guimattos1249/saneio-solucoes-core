namespace SaneioSolucoes.Communication.Responses
{
    public class ResponseExportResult
    {
        public string FileName { get; set; } = default!;
        public byte[] FileContent { get; set; } = default!;
    }
}
