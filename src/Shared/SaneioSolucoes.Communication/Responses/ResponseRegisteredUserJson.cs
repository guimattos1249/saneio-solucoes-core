namespace SaneioSolucoes.Communication.Responses
{
    public class ResponseRegisteredUserJson
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public ResponseTokenJson Tokens { get; set; } = default!;
    }
}
