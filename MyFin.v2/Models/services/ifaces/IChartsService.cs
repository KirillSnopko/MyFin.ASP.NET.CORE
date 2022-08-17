using MyFin.v2.Models.Entities.finance;

namespace MyFin.v2.Models.services.ifaces
{
    public interface IChartsService
    {
        dynamic getSpendingDataCurDepCurMonth(int idDepository, string idUser);
        dynamic getAddDataCurDepCurMonth(int idDepository, string idUser);
        dynamic getSpendDataAllDepCurMonth(string idUser);
        dynamic getAddDataAllDepCurMonth(string idUser);
        dynamic getSpendDataAllDepAllTime(string idUser);
        dynamic getAddDataAllDepAllTime(string idUser);
    }
}
