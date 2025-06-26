namespace SaneioSolucoes.Exceptions.ExceptionBase
{
    public class ErrorOnValidationException : SaneioSolucoesException
    {
        public IList<string> ErrorMessages { get; set; }

        public ErrorOnValidationException(IList<string> errorMessage) : base(string.Empty) => ErrorMessages = errorMessage;
    }
}
