using BankingData.Dto.Account;
using BankingData.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BankingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterAccountDto AccountToRegister)
        {
            var response = await _accountRepository.Register(AccountToRegister);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginAccountDto AccountToLogin)
        {
            var response = await _accountRepository.Login(AccountToLogin.Username, AccountToLogin.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet("get-accounts")]
        public async Task<IActionResult> GetAccounts()
        {
            return Ok(await _accountRepository.GetAccounts());
        }

        [HttpGet("get-account/{accountId}")]
        public async Task<IActionResult> GetAccount(int accountId)
        {
            return Ok(await _accountRepository.GetAccount(accountId));
        }

        [HttpPut("update-account")]
        public async Task<IActionResult> UpdateAccount(UpdateAccountDto accountToUpdate)
        {
            var response = await _accountRepository.UpdateAccount(accountToUpdate);

            if (response.Data == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpDelete("delete-account/{accountId}")]
        public async Task<IActionResult> DeleteAccount(int accountId)
        {
            var response = await _accountRepository.DeleteAccount(accountId);

            if (response.Data == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
