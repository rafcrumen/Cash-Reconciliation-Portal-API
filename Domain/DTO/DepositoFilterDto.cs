using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTO
{
    public class DepositFilterDto
    {
        public string? DepositNumber { get; set; }
        public string? StoreNumber { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}
