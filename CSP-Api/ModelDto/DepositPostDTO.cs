namespace CSP_Api.ModelDto
{
    public class DepositPostDTO
    {
        public int DepositId { get; set; }
            public string DepositNumber { get; set; }

            public string StoreNumber { get; set; }

            public decimal ExpectedAmount { get; set; }
        public decimal ActualAmount { get; set; }

        public DateTime DepositDate { get; set; }
    }
}
