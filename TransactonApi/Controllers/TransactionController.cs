using Microsoft.AspNetCore.Mvc;
using TransactionData.Interfaces;

namespace TransactonApi.Controllers
{
    public class TransactionController : ControllerBase
    {
        private readonly ITransferingRepository _transferingRepository;

        public TransactionController(ITransferingRepository transferingRepository)
        {
            _transferingRepository = transferingRepository;
        }

        [HttpGet("get-logs")]
        public IActionResult GetLogs()
        {
            return Ok(_transferingRepository.GetTransferLogs());
        }
    }
}
