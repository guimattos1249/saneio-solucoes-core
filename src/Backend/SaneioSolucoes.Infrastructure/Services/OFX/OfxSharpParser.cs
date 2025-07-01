using SaneioSolucoes.Domain.Dtos;
using SaneioSolucoes.Domain.Services.OFX;
using OfxSharp;
using SaneioSolucoes.Exceptions.ExceptionBase;
using SaneioSolucoes.Exceptions;
using SaneioSolucoes.Infrastructure.Utils;
using DocumentFormat.OpenXml.Drawing;


namespace SaneioSolucoes.Infrastructure.Services.OFX
{
    public class OfxSharpParser : IOFXParser
    {
        public List<TransactionDto> Parse(string ofxContent)
        {
            using var ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(ofxContent));
            var ofxDoc = OfxDocumentReader.FromSgmlFile(ms);

            var statement = ofxDoc.Statements.FirstOrDefault() 
                ?? throw new SaneioSolucoesException(ResourceMessageExceptions.NO_TRANSACTIONS_ON_FILE);

            return statement!.Transactions.Select(transaction => new TransactionDto
            {
                Date = transaction.Date.HasValue ? transaction.Date.Value.DateTime : (DateTime?)null,
                Memo = transaction.Memo,
                Amount = (long)(transaction.Amount * Constants.MoneyScaleConverter),
                AccountId = transaction.TransactionSenderAccount.AccountId,
                Bank = transaction.Name,
                TransactionId = transaction.TransactionId,
                ServerTransactionId = transaction.ServerTransactionId,
            }).ToList();
        }
    }
}
