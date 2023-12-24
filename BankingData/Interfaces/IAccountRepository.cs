using BankingData.Dto.Account;
using BankingData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingData.Interfaces
{
    public interface IAccountRepository
    {
        Task<ServiceResponse<List<GetAccountDto>>> GetAccounts();
        Task<ServiceResponse<GetAccountDto>> GetAccount(int accountId);
        Task<bool> AccountExists(int accountId);
        Task<ServiceResponse<int>> Register(RegisterAccountDto accountToRegister);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<ServiceResponse<GetAccountDto>> UpdateAccount(UpdateAccountDto updatedAccount);
        Task<ServiceResponse<List<GetAccountDto>>> DeleteAccount(int accountId);
    }
}
