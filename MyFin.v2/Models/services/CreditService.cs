using MyFin.v2.Models.database;
using MyFin.v2.Models.Entities.finance;
using MyFin.v2.Models.Entities.repo.ifaces;
using MyFin.v2.Models.exceptions;
using MyFin.v2.Models.services.ifaces;

namespace MyFin.v2.Models.services
{
    public class CreditService : ICreditService
    {
        private ICreditRepo creditRepo;
        private IOperationRepo operationRepo;
        private DbTransaction dbTransaction;

        public CreditService(ICreditRepo creditRepo, IOperationRepo operationRepo, DbTransaction dbTransaction)
        {
            this.creditRepo = creditRepo;
            this.operationRepo = operationRepo;
            this.dbTransaction = dbTransaction;
        }

        public void add(double value, string comment, DateTime closeDate, string idUser, TypeOfMoney typeOfMoney)
        {
            Credit credit = new Credit
            {
                initialAmount = value,
                returnedAmount = 0,
                openDate = DateTime.Now,
                closeDate = closeDate,
                comment = comment,
                idUser = idUser,
                typeOfMoney = typeOfMoney
            };
            creditRepo.add(credit);
        }

        public int count(string idUser)
        {
            return creditRepo.count(idUser);
        }

        public dynamic creditsByUserId(string id)
        {
            return creditRepo.creditsByUserId(id)
                .Select(i => new
                {
                    id = i.id,
                    initialAmount = i.initialAmount,
                    returnedAmount = i.returnedAmount,
                    comment = i.comment,
                    date1 = i.openDate.ToString("dddd, dd MMMM yyyy"),
                    date2 = i.closeDate.ToString("dddd, dd MMMM yyyy"),
                    typeOfMoney = Enum.GetName(typeof(TypeOfMoney), i.typeOfMoney)
                }).ToList();
        }

        public Credit get(int id, string idUser)
        {
            return creditRepo.get(id, idUser);
        }

        public void rename(string name, int id, string idUser)
        {
            creditRepo.rename(name, id, idUser);
        }

        public dynamic historyById(int idCredit, string idUser)
        {
            return operationRepo.getByIdDepository(idCredit, idUser)
                                .Where(i => i.category == Category.Credit).ToList()
                .Select(i => new { date = i.created.ToString("U"), comment = i.comment, value = i.amountOfMoney + Enum.GetName(typeof(TypeOfMoney), i.TypeOfMoney) }).ToList();
        }


        /*
         * Transactions
         */
        public void reduce(int idCredit, double value, string idUser, string comment)
        {
            Credit credit = creditRepo.get(idCredit, idUser);
            var diff = credit.initialAmount - credit.returnedAmount;
            if (diff == 0)
            {
                throw new FinContextException("credit closed");
            }
            if (value > diff)
            {
                throw new FinContextException("value more then credit balance");
            }
            Operation operation = new Operation { idDepository = idCredit, idUser = idUser, TypeOfMoney = credit.typeOfMoney, amountOfMoney = value, category = Category.Credit, comment = comment, isSpending = false };
            using (var transaction = dbTransaction.begin())
            {
                try
                {
                    creditRepo.reduce(idCredit, value, idUser);
                    operationRepo.SaveToHistory(operation);
                    if (value == diff)
                    {
                        creditRepo.closeCredit(credit.id);
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new FinContextException(ex.Message, ex);
                }
            }
        }

        public void delete(int id, string idUser)
        {
            using (var transaction = dbTransaction.begin())
            {
                try
                {
                    creditRepo.delete(id, idUser);
                    operationRepo.deleteByIdDepository(id, idUser);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new FinContextException(ex.Message, ex);
                }
            }
        }

        public void deleteAll(string idUser)
        {
            using (var transaction = dbTransaction.begin())
            {
                try
                {
                    creditRepo.deleteAll(idUser);
                    operationRepo.deleteAll(idUser);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new FinContextException(ex.Message, ex);
                }
            }
        }
    }
}
