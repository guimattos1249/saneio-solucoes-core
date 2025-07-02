namespace SaneioSolucoes.Communication.Responses
{
    public class ResponseRegisteredBankJson
    {
        public Guid Id { get; set; }
        public string LegalName { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }
}
