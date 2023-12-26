using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionData.Dto;
using TransactionData.Models;

namespace TransactionData.Interfaces
{
    public interface ITransferingRepository
    {
        Task<ServiceResponse<List<GetTransferLogDto>>> GetTransferLogs();
        Task<ServiceResponse<string>> AddTransferLog(AddTransferLogDto addingTransferLogDto);
    }
}
