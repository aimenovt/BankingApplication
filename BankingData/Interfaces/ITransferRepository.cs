using BankingData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingData.Interfaces
{
    public interface ITransferRepository
    {
        Task<ServiceResponse<string>> Transfer(Transfering transfering);
    }
}
