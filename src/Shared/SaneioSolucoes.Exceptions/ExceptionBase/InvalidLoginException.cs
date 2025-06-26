namespace SaneioSolucoes.Exceptions.ExceptionBase
{
    public class InvalidLoginException : SaneioSolucoesException
    {
        public InvalidLoginException() : base(ResourceMessageExceptions.EMAIL_OR_PASSWORD_INDVALID)
        {
        }
    }
}
