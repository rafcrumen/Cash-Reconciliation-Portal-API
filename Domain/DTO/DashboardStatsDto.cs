using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO
{
    public class DashboardStatsDto
    {
        public int TotalDeposits { get; set; }
        public int MatchedDeposits { get; set; }
        public int Shortages { get; set; }
        public int Overages { get; set; }
        public decimal TotalVarianceAmount { get; set; }
    }
}
