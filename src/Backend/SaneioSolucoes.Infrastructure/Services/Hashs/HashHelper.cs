using System.Security.Cryptography;
using System.Text;

namespace SaneioSolucoes.Infrastructure.Services.Hashs
{
    public class HashHelper
    {
        public static string ComputeTransactionHash(
            DateTime? date, 
            string accountId, 
            string transactionId, 
            string serverTransactionId,
            Guid userId,
            Guid companyId,
            Guid tenantId)
        {
            var input = $"{date}{accountId}{transactionId}{serverTransactionId}{userId}{companyId}{tenantId}";
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToHexString(hash);
        }
    }
}
