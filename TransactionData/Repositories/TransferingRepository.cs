using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionData.Context;
using TransactionData.Dto;
using TransactionData.Interfaces;
using TransactionData.Models;

namespace TransactionData.Repositories
{
    public class TransferingRepository : ITransferingRepository
    {
        private readonly TransactionDbContext _transactionDbContext;
        private readonly IMapper _mapper;

        public TransferingRepository(TransactionDbContext transactionDbContext, IMapper mapper)
        {
            _transactionDbContext = transactionDbContext;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<string>> AddTransferLog(AddTransferLogDto addingTransferLogDto)
        {
            var transferLogToAdd = _mapper.Map(addingTransferLogDto, new TransferLog());
            _transactionDbContext.TransferLogs.Add(transferLogToAdd);
            _transactionDbContext.SaveChanges();

            var response = new ServiceResponse<string>();
            response.Data = "Successfully added log";
            return response;
        }

        public async Task<ServiceResponse<List<GetTransferLogDto>>> GetTransferLogs()
        {
            var logs = _transactionDbContext.TransferLogs.OrderBy(l => l.Id).ToList();
            var logsToShow = _mapper.Map(logs, new List<GetTransferLogDto>());

            var response = new ServiceResponse<List<GetTransferLogDto>>();  
            response.Data = logsToShow;
            return response;
        }
    }
}
