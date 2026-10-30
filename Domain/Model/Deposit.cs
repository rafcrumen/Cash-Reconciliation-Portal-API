using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Model
{
    public class Deposit
    {
        public int DepositId { get; set; }

        [Required]
        [MaxLength(20)]
        public string DepositNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string StoreNumber { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal ExpectedAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ActualAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Variance { get; set; }

        [Required]
        public byte Status { get; set; } = 1;

        public DateTime DepositDate { get; set; }
    }
}
