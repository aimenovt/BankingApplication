using BankingApplication.Banking.Domain.Commands;
using BankingApplicaton.Domain;
using BankingData.Context;
using BankingData.Interfaces;
using BankingData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingData.Repositories
{
    public class TransferRepository : ITransferRepository
    {
        private readonly BankingDbContext _bankingDbContext;
        private readonly IEventBus _bus;

        public TransferRepository(BankingDbContext bankingDbContext, IEventBus bus)
        {
            _bankingDbContext = bankingDbContext;
            _bus = bus;
        }

        public async Task<ServiceResponse<string>> Transfer(Transfering transfering)
        {
            var fromAccount = _bankingDbContext.Accounts.Where(a => a.Id == transfering.FromAccountId).FirstOrDefault();
            var toAccount = _bankingDbContext.Accounts.Where(a => a.Id == transfering.ToAccountId).FirstOrDefault();

            if (fromAccount != null && toAccount != null)
            {
                if (fromAccount.Balance >= transfering.TransferAmount)
                {
                    fromAccount.Balance -= transfering.TransferAmount;
                    toAccount.Balance += transfering.TransferAmount;
                    _bankingDbContext.SaveChanges();

                    var createTransferCommand = new CreateTransferCommand(
                        transfering.FromAccountId,
                        transfering.ToAccountId,
                        transfering.TransferAmount);

                    _bus.SendCommand(createTransferCommand);

                    var response = new ServiceResponse<string>();
                    response.Message = "Sent successfully";

                    return response;
                }

                else
                {
                    var response = new ServiceResponse<string>();
                    response.Success = false;
                    response.Message = "Not enough funds to transfer";

                    return response;
                }
            }

            else
            {
                var response = new ServiceResponse<string>();
                response.Success = false;
                response.Message = "There are no such account(s)";

                return response;
            }
        }
    }
}
