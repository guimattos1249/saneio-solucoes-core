namespace SaneioSolucoes.Communication.Responses
{
    public class ResponseCompanyJson
    {
        public Guid Id { get; set; }
        public string LegalName { get; set; } = string.Empty;
        public string TradeName { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty;
    }
}
