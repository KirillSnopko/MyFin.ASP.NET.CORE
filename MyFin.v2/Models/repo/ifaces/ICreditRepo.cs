using MyFin.v2.Models.Entities.finance;

namespace MyFin.v2.Models.Entities.repo.ifaces
{
    public interface ICreditRepo
    {
        List<Credit> creditsByUserId(string idUser);
        void add(Credit credit);
        Credit get(int idCredit, string idUser);
        void rename(string newName, int idCredit, string idUser);
        int count(string idUser);
        void delete(int idCredit, string idUser);
        void deleteAll(string idUser);
        void reduce(int idCredit, double value, string idUser);
        void closeCredit(int idCredit);
    }
}
