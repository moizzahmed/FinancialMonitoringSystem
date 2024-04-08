using FinancialMonitoringSystem.Interfaces;
using FinancialMonitoringSystem.Models;
using FinancialMonitoringSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinancialMonitoringSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] TransactionEvent transactionEvent)
        {
            var isPassed = _transactionService.ProcessTransaction(transactionEvent);

            if (isPassed)
            {
                return Ok(new { status = "SUCCESS", transaction = transactionEvent });
            }
            
            return Ok(new { status = "FAIL", transaction = transactionEvent });
        }
    }
}
