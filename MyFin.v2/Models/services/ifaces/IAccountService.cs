using MyFin.v2.Models.Entities;

namespace MyFin.v2.Models.services.ifaces
{
    public interface IAccountService
    {
        Task<Response> login(string email, string password);
        Task<Response> register(string name, string password, string email);
        void logout();
        Task<Response> rename(string name, string idUser);
        Task<Response> changePassword(string old_password, string new_password, string idUser);
        Task<Response> removeAccount(string password, string userName);
    }
}
