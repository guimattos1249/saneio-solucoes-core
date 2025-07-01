using Microsoft.EntityFrameworkCore;
using SaneioSolucoes.Domain.Dtos;
using SaneioSolucoes.Domain.Entities;
using SaneioSolucoes.Domain.Repositories.Transaction;
using SaneioSolucoes.Infrastructure.Services.Hashs;

namespace SaneioSolucoes.Infrastructure.DataAccess.Repositories
{
    public class TransactionRepository : ITransactionWriteOnlyRepository
    {
        private readonly SaneioSolucoesDBContext _dbContext;
        public TransactionRepository(SaneioSolucoesDBContext dbContext) => _dbContext = dbContext;

        public async Task AddRange(List<TransactionDto> transactions)
        {
            var transactionsWithHash = GenerateTransactionsWithHash(transactions);

            var hashes = transactionsWithHash.Select(t => t.Hash).ToList();

            var existentHashes = await FindExistentHashes(hashes);

            var newTransactions = FilterNewTransactions(transactionsWithHash, existentHashes);

            if (newTransactions.Count == 0)
                return;

            await _dbContext.Transactions.AddRangeAsync(newTransactions);
        }

        private static List<(TransactionDto Transaction, string Hash)> GenerateTransactionsWithHash(List<TransactionDto> transactions)
        {
            return transactions
                .Select(transaction => (transaction, HashHelper.ComputeTransactionHash(
                        transaction.Date,
                        transaction.AccountId,
                        transaction.TransactionId!,
                        transaction.ServerTransactionId,
                        transaction.UserId,
                        transaction.CompanyId,
                        transaction.TenantId)))
                .ToList();
        }

        private async Task<List<string>> FindExistentHashes(List<string> hashes)
        {
            return await _dbContext.Transactions
                .Where(t => hashes.Contains(t.Hash))
                .Select(t => t.Hash)
                .ToListAsync();
        }

        private static List<Transaction> FilterNewTransactions(
        List<(TransactionDto transaction, string hash)> transactionsWithHash,
        List<string> existentHashes)
        {
            return transactionsWithHash
                .Where(th => !existentHashes.Contains(th.hash))
                .Select(th => new Transaction
                {
                    Date = th.transaction.Date,
                    Memo = th.transaction.Memo,
                    Amount = th.transaction.Amount,
                    Bank = th.transaction.Bank,
                    AccountId = th.transaction.AccountId,
                    TransactionId = th.transaction.TransactionId,
                    ServerTransactionId = th.transaction.ServerTransactionId,
                    UserId = th.transaction.UserId,
                    CompanyId = th.transaction.CompanyId,
                    TenantId = th.transaction.TenantId,
                    Type = th.transaction.Type,
                    Hash = th.hash,
                }).ToList();
        }
    }
}
