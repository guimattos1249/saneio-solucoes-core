using SaneioSolucoes.Domain.Dtos;
using SaneioSolucoes.Domain.Services.OFX;
using SaneioSolucoes.Infrastructure.Services.OFX.FieldParser;
using SaneioSolucoes.Infrastructure.Services.OFX.Sanitizer;
using System.Text.RegularExpressions;

namespace SaneioSolucoes.Infrastructure.Services.OFX
{
    public class OfxParser : IOFXParser
    {
        public List<TransactionDto> Parse(string ofxContent)
        {
            ofxContent = OfxSanitizer.Sanitize(ofxContent);
            var transactionBlocks = Regex.Matches(ofxContent, "<STMTTRN>(.*?)</STMTTRN>", RegexOptions.Singleline);
            var transactions = new List<TransactionDto>();

            foreach (Match block in transactionBlocks)
            {
                var content = block.Groups[1].Value;

                var transaction = new TransactionDto
                {
                    Date = OfxFieldParser.ParseDate(OfxFieldParser.ExtractTag(content, "DTPOSTED")),
                    Amount = OfxFieldParser.ParseAmount(OfxFieldParser.ExtractTag(content, "TRNAMT")),
                    Memo = string.Join(" ", OfxFieldParser.ExtractTags(content, "MEMO")),
                    TransactionId = OfxFieldParser.ExtractTag(content, "FITID"),
                    Bank = OfxFieldParser.ExtractTag(content, "NAME") ?? OfxFieldParser.ExtractTag(content, "CHECKNUM"),
                    AccountId = OfxFieldParser.ExtractTag(content, "ACCTID") ?? string.Empty
                };

                transactions.Add(transaction);
            }

            return transactions;
        }
    }
}
