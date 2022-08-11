namespace MyFin.v2.Models.Entities.finance
{
    public class Credit
    {
        public int id { get; set; }
        public string idUser { get; set; }
        public double initialAmount { get; set; }
        public double returnedAmount { get; set; } = 0;
        public DateTime openDate { get; init; } = DateTime.Now;
        public DateTime closeDate { get; set; }
        public string comment { get; set; }
        public TypeOfMoney typeOfMoney { get; set; }
    }
}
