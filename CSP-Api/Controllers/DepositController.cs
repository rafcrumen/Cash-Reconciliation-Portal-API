using CSP_Api.ModelDto;
using Domain.DTO;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;
using DomainModel = Domain.Model;
using services = Business.Deposit;
namespace CSP_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositController : ControllerBase
    {
        private readonly services.IDepositService depositService;
        public DepositController(services.IDepositService _depositService)
        {
            depositService = _depositService;
        }
        [HttpGet]
        public async Task<IActionResult> ListDeposits()
        {
            var deposits = await depositService.ListDeposits();
            return Ok(deposits);
        }
        [HttpPost]
        public async Task<IActionResult> AddDeposit(DepositPostDTO depositDTO)
        {
            var deposit = new Deposit
            {
                DepositNumber = depositDTO.DepositNumber,
                StoreNumber = depositDTO.StoreNumber,
                ExpectedAmount = depositDTO.ExpectedAmount,
                DepositDate = depositDTO.DepositDate,

                Status = 1,

                ActualAmount = null,

                Variance = null
            };
            var result = await depositService.AddDeposit(deposit);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var deposit = await depositService.GetDepositById(id);
            if (deposit == null)
            {
                return NotFound();
            }
            return Ok(deposit);
        }
        [HttpGet("{GetByDepositNumber}/{depositNumber}")]
        public async Task<IActionResult> GetByDepositNumber(string depositNumber)
        {
            var deposit = await depositService.GetDepositByDepositNumber(depositNumber);
            if (deposit == null)
            {
                return NotFound();
            }
            return Ok(deposit);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateDeposit(DomainModel.Deposit deposit)
        {
            var result = await depositService.UpdateDeposit(deposit);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [HttpDelete("{depositId}")]
        public IActionResult DeleteDeposit(int depositId)
        {
            depositService.DeleteDeposit(depositId);
            return Ok();
        }
        [HttpPut("{id}/reconcile")]
        public IActionResult ReconciliateDeposit(int id,
        [FromBody] DepositReconcilePut deposit)
        {
            Deposit depositToUpdate = new Deposit
            {
                DepositId = id,
                ActualAmount = deposit.ActualAmount
            };
            depositService.ReconciliateDeposit(depositToUpdate);
            return Ok(deposit);
        }
        [HttpGet("dashboard-stats")]
        public async Task<IActionResult> GetDashboardStats()
        {
            var stats = await depositService.GetDashboardStatsAsync();
            return Ok(stats);
        }
        [HttpGet("GetFilteredDeposits")]
        public async Task<IActionResult> GetFilteredDeposits([FromQuery] DepositFilterDto filters)
        {
            var result = await this.depositService.GetFilteredDeposits(filters);
            return Ok(result);
        }
    }
}
