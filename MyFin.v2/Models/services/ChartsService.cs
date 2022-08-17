using MyFin.v2.Models.Entities.finance;
using MyFin.v2.Models.Entities.repo.ifaces;
using MyFin.v2.Models.services.ifaces;

namespace MyFin.v2.Models.services
{
    public class ChartsService : IChartsService
    {
        private IOperationRepo operationRepo;
        private IDepositoryRepo depositoryRepo;

        public ChartsService(IOperationRepo _operationRepo, IDepositoryRepo depositoryRepo)
        {
            operationRepo = _operationRepo;
            this.depositoryRepo = depositoryRepo;
        }

        public dynamic getAddDataAllDepAllTime(string idUser)
        {
            List<Operation> financeOperations = operationRepo.getByIdUser(idUser)
                .Where(i => i.isSpending == false && i.category != Category.Credit).ToList();
            var data = financeOperations
                .GroupBy(i => i.idDepository)
                .Select(i => new
                {
                    Depository = depositoryRepo.get(i.Key, idUser).name,
                    Sum = i.Sum(x => x.amountOfMoney)
                }).ToList();
            return data;
        }

        public dynamic getAddDataAllDepCurMonth(string idUser)
        {
            List<Operation> financeOperations = operationRepo.getByIdUser(idUser)
                .Where(i => i.isSpending == false && i.category != Category.Credit).
                Where(i => i.created.Month == DateTime.Now.Month).ToList();

            var data = financeOperations
                .GroupBy(i => i.idDepository)
                .Select(i => new
                {
                    Depository = depositoryRepo.get(i.Key, idUser).name,
                    Sum = i.Sum(x => x.amountOfMoney)
                }).ToList();

            return data;
        }

        public dynamic getAddDataCurDepCurMonth(int idDepository, string idUser)
        {
            List<Operation> financeOperations = operationRepo.getByIdDepository(idDepository, idUser)
                .Where(i => i.isSpending == false && i.category != Category.Credit)
                .Where(i => i.created.Month == DateTime.Now.Month).ToList();

            var data = financeOperations
                .GroupBy(i => i.category)
                .Select(i => new
                {
                    Category = Enum.GetName(typeof(Category), i.Key),
                    Sum = i.Sum(x => x.amountOfMoney)
                }).ToList();
            return data;
        }

        public dynamic getSpendDataAllDepAllTime(string idUser)
        {
            List<Operation> financeOperations = operationRepo.getByIdUser(idUser)
                .Where(i => i.isSpending == true && i.category != Category.Credit).ToList();
            var data = financeOperations
                .GroupBy(i => i.idDepository)
                .Select(i => new
                {
                    Depository = depositoryRepo.get(i.Key, idUser).name,
                    Sum = i.Sum(x => x.amountOfMoney)
                }).ToList();
            return data;
        }

        public dynamic getSpendDataAllDepCurMonth(string idUser)
        {
            List<Operation> financeOperations = operationRepo.getByIdUser(idUser)
                .Where(i => i.isSpending == true && i.category != Category.Credit)
                .Where(i => i.created.Month == DateTime.Now.Month).ToList();
            var data = financeOperations
                .GroupBy(i => i.idDepository)
                .Select(i => new
                {
                    Depository = depositoryRepo.get(i.Key, idUser).name,
                    Sum = i.Sum(x => x.amountOfMoney)
                }).ToList();
            return data;
        }

        public dynamic getSpendingDataCurDepCurMonth(int idDepository, string idUser)
        {
            List<Operation> financeOperations = operationRepo.getByIdDepository(idDepository, idUser)
                .Where(i => i.isSpending == true && i.category != Category.Credit)
                .Where(i => i.created.Month == DateTime.Now.Month).ToList();
            var data = financeOperations
                .GroupBy(i => i.category)
                .Select(i => new
                {
                    Category = Enum.GetName(typeof(Category), i.Key),
                    Sum = i.Sum(x => x.amountOfMoney)
                }).ToList();
            return data;
        }
    }
}
