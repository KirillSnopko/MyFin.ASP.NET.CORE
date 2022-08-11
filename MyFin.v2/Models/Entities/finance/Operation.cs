namespace MyFin.v2.Models.Entities.finance
{
    public class Operation
    {
        public int id { get; set; }
        public string idUser { get; set; }
        public int idDepository { get; set; }
        public double amountOfMoney { get; set; }
        public TypeOfMoney TypeOfMoney { get; set; }
        public Category category { get; set; }
        public string comment { get; set; }
        public DateTime created { get; init; } = DateTime.Now;
        public bool isSpending { get; set; }
    }

    public enum Category
    {
        Home, Repair, Supermarkets, Pharmacy, Entertainment, Transport, Clothing, Electronics, Others, Addition, Credit
    }
}

