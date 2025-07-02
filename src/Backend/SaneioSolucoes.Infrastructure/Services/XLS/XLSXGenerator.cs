using ClosedXML.Excel;
using SaneioSolucoes.Domain.Dtos;
using SaneioSolucoes.Domain.Services.XLS;
using SaneioSolucoes.Domain.Utils;
using System.Data;

namespace SaneioSolucoes.Infrastructure.Services.XLS
{
    public class XLSXGenerator : IXLSGenerator
    {
        public byte[] GenerateXLS(List<TransactionExportDto> transactions)
        {
            var datas = GetDataTable(transactions);
            using (XLWorkbook workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(datas, "Dados");
                worksheet.Columns().AdjustToContents();
                worksheet.Column(4).Style.NumberFormat.Format = "#,##0.00";

                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.SaveAs(ms);
                    return ms.ToArray();
                }
            }
        }

        private DataTable GetDataTable(List<TransactionExportDto> transactions)
        {
            DataTable dataTable = new DataTable();

            dataTable.TableName = "Dados Transferência";
            dataTable.Columns.Add("Data", typeof(DateTime));
            dataTable.Columns.Add("Descrição 1", typeof(string));
            dataTable.Columns.Add("Descrição 2", typeof(string));
            dataTable.Columns.Add("Valor", typeof(decimal));
            dataTable.Columns.Add("Id da Transação", typeof(string));
            dataTable.Columns.Add("Nome Fantasia", typeof(string));
            dataTable.Columns.Add("Operador", typeof(string));
            dataTable.Columns.Add("Banco", typeof(string));

            if (transactions.Count > 0)
            {
                transactions.ForEach(transaction =>
                {
                    dataTable.Rows.Add(
                        transaction.Date, 
                        transaction.Memo, 
                        "",
                        ((decimal)transaction.Amount) / Constants.MoneyScaleConverter,
                        transaction.TransactionId,
                        transaction.CompanyName,
                        transaction.UserName,
                        transaction.BankName);
                });
            }

            return dataTable;
        }
    }
}
