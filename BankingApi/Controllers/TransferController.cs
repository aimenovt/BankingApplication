using BankingData.Interfaces;
using BankingData.Models;
using BankingData.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace BankingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransferController : ControllerBase
    {
        private readonly ITransferRepository _transferRepository;

        public TransferController(ITransferRepository transferRepository)
        {
            _transferRepository = transferRepository;
        }

        [HttpPost("transfering_funds")]
        public async Task<IActionResult> TransferingFunds([FromBody] Transfering transfering)
        {
            return Ok(await _transferRepository.Transfer(transfering));
        }
    }
}
