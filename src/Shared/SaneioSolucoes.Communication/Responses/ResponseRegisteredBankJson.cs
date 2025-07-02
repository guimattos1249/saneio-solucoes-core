using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaneioSolucoes.Communication.Responses
{
    public class ResponseRegisteredBankJson
    {
        public Guid Id { get; set; }
        public string LegalName { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }
}
