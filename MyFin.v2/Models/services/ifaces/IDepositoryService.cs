using MyFin.v2.Models.Entities.finance;

namespace MyFin.v2.Models.services.ifaces
{
    public interface IDepositoryService
    {
        dynamic depositoriesByUserId(string idUser);
        void add(TypeDep tDep, TypeOfMoney tMoney, string name, double amount, string idUser);
        dynamic get(int idDepository, string idUser);
        void rename(string newName, int idDepository, string idUser);
        int count(string idUser);
        dynamic historyById(int idDepository, string idUser);
        void delete(int idDepository, string idUser);
        void change(int idDepository, bool isSpending, double amountOfMoney, string idUser, string comment, Category category, TypeOfMoney typeOfMoney);
    }
}
