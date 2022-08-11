using MyFin.v2.Models.Entities.finance;

namespace MyFin.v2.Models.Entities.repo.ifaces
{
    public interface IDepositoryRepo
    {
        List<Depository> depositoriesByUserId(string idUser);
        void add(Depository depository);
        Depository get(int idDepository, string idUser);
        void rename(string newName, int idDepository, string idUser);
        int count(string idUser);
        void delete(int idDepository, string idUser);
        void deleteAll(string idUser);
        void change(int idDepository, bool isSpending, double amountOfMoney, string idUser);
    }
}
