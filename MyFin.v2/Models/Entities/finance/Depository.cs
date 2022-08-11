using System.ComponentModel.DataAnnotations;

namespace MyFin.v2.Models.Entities.finance
{
    public class Depository
    {
        public int id { get; set; }
        public string idUser { get; set; }
        public string name { get; set; }
        public double amount { get; set; }
        public TypeDep typeDep { get; set; }
        public TypeOfMoney typeMoney { get; set; }
    }

    public enum TypeDep
    {
        DEPOSIT, CARD, CASH
    }
}

