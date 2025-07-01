using SaneioSolucoes.Domain.Dtos;

namespace SaneioSolucoes.Domain.Services.OFX
{
    public interface IOFXParser
    {
        List<TransactionDto> Parse(string ofxContent);
    }
}
