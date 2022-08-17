using MyFin.v2.Models.Entities.database;
using MyFin.v2.Models.Entities.finance;
using MyFin.v2.Models.Entities.repo.ifaces;

namespace MyFin.v2.Models.Entities.repo
{
    public class CreditRepo : ICreditRepo
    {
        private FinContext financeContext;
        public CreditRepo(FinContext financeContext)
        {
            this.financeContext = financeContext;
        }

        public void add(Credit credit)
        {
            financeContext.credits.Add(credit);
            financeContext.SaveChanges();
        }

        public void reduce(int idCredit, double value, string idUser)
        {
            Credit credit = financeContext.credits.Where(i => i.id == idCredit && i.idUser == idUser).First();
            credit.returnedAmount += value;
            financeContext.SaveChanges();
        }

        public int count(string idUser)
        {
            return financeContext.credits.Where(i => i.idUser == idUser).Count();
        }

        public List<Credit> creditsByUserId(string id)
        {
            return financeContext.credits.Where(i => i.idUser == id).ToList();
        }

        public Credit get(int id, string idUser)
        {
            return financeContext.credits.Where(i => i.id == id && i.idUser == idUser).First();
        }

        public void rename(string name, int id, string idUser)
        {
            financeContext.credits.Where(i => i.id == id && i.idUser == idUser).First().comment = name;
            financeContext.SaveChanges();
        }

        public void delete(int id, string idUser)
        {
            financeContext.credits.Remove(financeContext.credits.Where(i => i.id == id && i.idUser == idUser).First());
            financeContext.SaveChanges();
        }

        public void deleteAll(string idUser)
        {
            financeContext.credits.RemoveRange(financeContext.credits.Where(i => i.idUser == idUser).ToList());
            financeContext.SaveChanges();
        }

        public void closeCredit(int idCredit)
        {
            financeContext.credits.Where(i => i.id == idCredit).Single().closeDate = DateTime.Now;
            financeContext.SaveChanges();
        }
    }
}
