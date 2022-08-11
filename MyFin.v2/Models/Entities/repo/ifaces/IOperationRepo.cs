using MyFin.v2.Models.Entities.finance;

namespace MyFin.v2.Models.Entities.repo.ifaces
{
    public interface IOperationRepo
    {
        void SaveToHistory(Operation operation);
        List<Operation> getByIdDepository(int idDepository, string idUser);
        void delete(int idOperation, string idUser);
        void deleteAll(string idUser);
        void deleteByIdDepository(int idDepository, string idUser);
        List<Operation> getByIdUser(string idUser);
    }
}
