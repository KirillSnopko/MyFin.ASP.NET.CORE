using MyFin.v2.Models.Entities.finance;

namespace MyFin.v2.Models.services.ifaces
{
    public interface ICreditService
    {
        dynamic creditsByUserId(string idUser);
        void add(double value, string comment, DateTime closeDate, string idUser, TypeOfMoney typeOfMoney);
        Credit get(int idCredit, string idUser);
        void rename(string newName, int idCredit, string idUser);
        int count(string idUser);
        dynamic historyById(int idCredit, string idUser);
        void reduce(int idCredit, double value, string idUser, string comment);
        void delete(int idCredit, string idUser);
        void deleteAll(string idUser);
    }
}
