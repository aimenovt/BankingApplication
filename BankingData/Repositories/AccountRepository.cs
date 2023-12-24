using AutoMapper;
using BankingData.Context;
using BankingData.Dto.Account;
using BankingData.Interfaces;
using BankingData.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BankingData.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankingDbContext _bankingDbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AccountRepository(BankingDbContext bankingDbContext, IMapper mapper, IConfiguration configuration)
        {
            _bankingDbContext = bankingDbContext;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> AccountExists(int accountId)
        {
            var exists = await _bankingDbContext.Accounts.AnyAsync(a => a.Id == accountId);

            return exists;
        }

        public async Task<ServiceResponse<List<GetAccountDto>>> DeleteAccount(int accountId)
        {
            var accountToDelete = _bankingDbContext.Accounts.Where(a => a.Id == accountId).FirstOrDefault();

            _bankingDbContext.Accounts.Remove(accountToDelete);
            await _bankingDbContext.SaveChangesAsync();

            var response = new ServiceResponse<List<GetAccountDto>>();
            response.Data = _mapper.Map(_bankingDbContext.Accounts.ToList(), new List<GetAccountDto>());

            return response;
        }

        public async Task<ServiceResponse<GetAccountDto>> GetAccount(int accountId)
        {
            var account = _bankingDbContext.Accounts.Where(a => a.Id == accountId).FirstOrDefault();

            var response = new ServiceResponse<GetAccountDto>();
            response.Data = _mapper.Map(account, new GetAccountDto());

            return response;
        }

        public async Task<ServiceResponse<List<GetAccountDto>>> GetAccounts()
        {
            var accounts = _bankingDbContext.Accounts.ToList();

            var response = new ServiceResponse<List<GetAccountDto>>();
            response.Data = _mapper.Map(accounts, new List<GetAccountDto>());

            return response;
        }

        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var response = new ServiceResponse<string>();

            var account = await _bankingDbContext.Accounts.Where(u => u.Username == username).FirstOrDefaultAsync();

            if (account == null)
            {
                response.Success = false;
                response.Message = "User not found";
            }

            else if (!VerifyPasswordHash(password, account.PasswordHash, account.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password";
            }

            else
            {
                response.Data = CreateToken(account);
            }

            return response;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(Account account)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.Username),
            };

            // Assign roles based on user ID or any other criteria
            if (account.Id == 100)
            {
                // User with ID 100 gets the "Admin" role
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }

            else
            {
                // Other users get the "Standard" role
                claims.Add(new Claim(ClaimTypes.Role, "Standard"));
            }

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token); //Token
        }

        public async Task<ServiceResponse<int>> Register(RegisterAccountDto accountToRegister)
        {
            var countryExists = _bankingDbContext.Countries.Any(c => c.Id == accountToRegister.countryId);

            if (countryExists)
            {
                var account = _mapper.Map<Account>(accountToRegister);
                account.Country = _bankingDbContext.Countries.Where(c => c.Id == accountToRegister.countryId).FirstOrDefault();

                CreatePasswordHash(accountToRegister.Password, out byte[] passwordHash, out byte[] passwordSalt);

                account.PasswordHash = passwordHash;
                account.PasswordSalt = passwordSalt;

                _bankingDbContext.Accounts.Add(account);
                await _bankingDbContext.SaveChangesAsync();

                var response = new ServiceResponse<int>();
                response.Data = account.Id;

                return response;
            }

            else
            {
                var response = new ServiceResponse<int>();
                response.Success = false;
                response.Message = "This country doesn't exists";
                return response;
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public Task<ServiceResponse<GetAccountDto>> UpdateAccount(UpdateAccountDto updatedAccount)
        {
            throw new NotImplementedException();
        }
    }
}
