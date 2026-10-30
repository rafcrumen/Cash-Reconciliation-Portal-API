using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Deposit
{
    public class DepositService : IDepositService
    {
        private readonly Repository.Deposit.IDepositRepository depositRepository;
        public DepositService(Repository.Deposit.IDepositRepository _depositRepository)
        {
            depositRepository = _depositRepository;
        }   

        public async Task<Domain.Model.Deposit?> AddDeposit(Domain.Model.Deposit deposit)
        {
            return await depositRepository.AddDeposit(deposit);
        }

        public decimal CalculateVariance(decimal expectedAmount, decimal? actualAmount)
        {
            if (actualAmount.HasValue)
            {
                return actualAmount.Value - expectedAmount;
            }
            else
            {
                return 0;
            }
        }

        public void DeleteDeposit(int depositId)
        {
            depositRepository.DeleteDeposit(depositId);
        }

        public async Task<Domain.Model.Deposit?> GetDepositById(int depositId)
        {
            return await this.depositRepository.GetDepositById(depositId);
        }

        public async Task<IEnumerable<Domain.Model.Deposit>> ListDeposits()
        {
            return await this.depositRepository.GetAllDeposits();
        }

        public void ReconciliateDeposit(Domain.Model.Deposit deposit)
        {
            this.depositRepository.ReconciliateDeposit(deposit);
        }

        public async Task<Domain.Model.Deposit?> UpdateDeposit(Domain.Model.Deposit deposit)
        {
            return await this.depositRepository.UpdateDeposit(deposit);
        }
        public async Task<Domain.Model.Deposit?> GetDepositByDepositNumber(string depositNumber)
        {
            return await this.depositRepository.GetDepositByDepositNumber(depositNumber);
        }
        public async Task<DashboardStatsDto> GetDashboardStatsAsync()
        {
            return await this.depositRepository.GetDashboardStatsAsync();
        }
    }
}
