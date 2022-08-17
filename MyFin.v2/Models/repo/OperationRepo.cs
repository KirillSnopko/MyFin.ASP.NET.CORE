using MyFin.v2.Models.Entities.database;
using MyFin.v2.Models.Entities.finance;
using MyFin.v2.Models.Entities.repo.ifaces;

namespace MyFin.v2.Models.Entities.repo
{
    public class OperationRepo : IOperationRepo
    {
        private FinContext financeContext;

        public OperationRepo(FinContext financeContext)
        {
            this.financeContext = financeContext;
        }

        public List<Operation> getByIdDepository(int idDepository, string idUser)
        {
            return financeContext.operations.Where(i => i.idDepository == idDepository && i.idUser == idUser).ToList();
        }

        public List<Operation> getByIdUser(string idUser)
        {
            return financeContext.operations.Where(i => i.idUser == idUser).ToList();
        }

        public void SaveToHistory(Operation operation)
        {
            financeContext.operations.Add(operation);
            financeContext.SaveChanges();
        }

        public void delete(int idOperation, string idUser)
        {
            Operation operation = financeContext.operations.Where(i => i.id == idOperation && i.idUser == idUser).First();
            financeContext.operations.Remove(operation);
            financeContext.SaveChanges();
        }

        public void deleteAll(string idUser)
        {
            financeContext.operations.RemoveRange(financeContext.operations.Where(i => i.idUser == idUser).ToList());
            financeContext.SaveChanges();
        }

        public void deleteByIdDepository(int idDepository, string idUser)
        {
            financeContext.operations.RemoveRange(financeContext.operations.Where(i => i.idDepository == idDepository && i.idUser == idUser).ToList());
            financeContext.SaveChanges();
        }
    }
}
