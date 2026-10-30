using Domain.DTO;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using DomainModel = Domain.Model;
namespace Repository.Deposit
{
    public class DepositRepository : IDepositRepository
    {
        private readonly DataContext dataContext;
        public DepositRepository(DataContext _dataContext)
        {
                        this.dataContext = _dataContext;
        }
        public async Task<DomainModel.Deposit?> AddDeposit(DomainModel.Deposit deposit)
        {
            var result = await this.dataContext.AddAsync(deposit);
            await this.dataContext.SaveChangesAsync();
            return result.Entity;
        }


        public void DeleteDeposit(int depositId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DomainModel.Deposit>> GetAllDeposits()
        {
            var result = await this.dataContext.Deposits.ToListAsync();
            return result;
        }

        public async Task<DomainModel.Deposit?> GetDepositById(int depositId)
        {
            var result = await this.dataContext.Deposits.FirstOrDefaultAsync(d => d.DepositId == depositId);
            return result;
        }

        public async Task<DomainModel.Deposit?> GetDepositByDepositNumber(string depositNumber)
        {
            var result = await this.dataContext.Deposits.FirstOrDefaultAsync(d => d.DepositNumber == depositNumber);
            return result;
        }

        public void ReconciliateDeposit(DomainModel.Deposit deposit)
        {
            var depositToUpdate = this.dataContext.Deposits.FirstOrDefault(d => d.DepositId == deposit.DepositId);
            if (depositToUpdate == null)
            {
                throw new Exception("Deposit not found");
            }
            depositToUpdate.ActualAmount = deposit.ActualAmount;
            depositToUpdate.Variance = deposit.ActualAmount - depositToUpdate.ExpectedAmount;
            depositToUpdate.Status = CalculateStatus(depositToUpdate.ExpectedAmount, depositToUpdate.ActualAmount);
            var result = this.dataContext.Deposits.Update(depositToUpdate);
            this.dataContext.SaveChanges();
        }
        private byte CalculateStatus(decimal expectedAmount, decimal? actualAmount)
        {
            if (actualAmount.HasValue)
            {
                if (expectedAmount == actualAmount.Value)
                {
                    return 2; // Matched
                }
                else if (actualAmount.Value > expectedAmount)
                {
                    return 3; // Overage
                }
                else
                {
                    return 4; // Shortage
                }
            }
            else
            {
                return 1; // Pending
            }
        }
        public async Task<DomainModel.Deposit> UpdateDeposit(DomainModel.Deposit deposit)
        {
            deposit.Variance = deposit.ActualAmount - deposit.ExpectedAmount;
            deposit.Status = CalculateStatus(deposit.ExpectedAmount, deposit.ActualAmount);
            var result = this.dataContext.Deposits.Update(deposit).Entity;
            await this.dataContext.SaveChangesAsync();
            return result;
        }

        public async Task<DashboardStatsDto> GetDashboardStatsAsync()
        {
            var stats = await this.dataContext.Deposits
                .GroupBy(_ => 1)
                .Select(g => new DashboardStatsDto
                {
                    TotalDeposits = g.Count(),
                    MatchedDeposits = g.Count(d => d.Status == 2),
                    Shortages = g.Count(d => d.Status == 3),
                    Overages = g.Count(d => d.Status == 4),
                    TotalVarianceAmount = g.Sum(d => d.Variance ?? 0)
                })
                .FirstOrDefaultAsync();

            return stats ?? new DashboardStatsDto();
        }
    }
}
