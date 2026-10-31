using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using DomainModel = Domain.Model;
namespace Repository.Deposit
{
    public interface IDepositRepository
    {
        public Task<Domain.Model.Deposit?> AddDeposit(DomainModel.Deposit deposit);
        public Task<DomainModel.Deposit?> GetDepositById(int depositId);
        public Task<DomainModel.Deposit?> GetDepositByDepositNumber(string depositNumber);
        public Task<IEnumerable<DomainModel.Deposit>> GetAllDeposits();
        public Task<DomainModel.Deposit> UpdateDeposit(DomainModel.Deposit deposit);
        public void DeleteDeposit(int depositId);
        public void ReconciliateDeposit(DomainModel.Deposit deposit);
        public Task<DashboardStatsDto> GetDashboardStatsAsync();
        public Task<IEnumerable<DomainModel.Deposit>> GetFilteredDeposits(DepositFilterDto filters);
    }
}
