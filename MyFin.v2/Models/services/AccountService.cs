using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using MyFin.v2.Models.database;
using MyFin.v2.Models.Entities;
using MyFin.v2.Models.Entities.repo;
using MyFin.v2.Models.Entities.repo.ifaces;
using MyFin.v2.Models.services.ifaces;

namespace MyFin.v2.Models.services
{
    public class AccountService : IAccountService
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;
        private DbTransaction dbTransaction;
        private IDepositoryRepo depositoryRepo;
        private ICreditRepo creditRepo;
        private IOperationRepo operationRepo;

        public AccountService(DbTransaction dbTransaction, IOperationRepo operationRepo, ICreditRepo creditRepo, IDepositoryRepo depositoryRepo, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.dbTransaction = dbTransaction;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.operationRepo = operationRepo;
            this.creditRepo = creditRepo;
            this.depositoryRepo = depositoryRepo;
        }

        public async Task<Response> login(string email, string password)
        {
            IdentityUser user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new Response { status = 403, message = "invalid email" };
            }
            var result = await signInManager.PasswordSignInAsync(user, password, false, false);
            if (!result.Succeeded)
            {
                return new Response { status = 403, message = "invalid password" };
            }
            return new Response { status = 200, message = user.Id };
        }

        public async Task<Response> register(string name, string password, string email)
        {
            IdentityUser user = new IdentityUser { Email = email, UserName = name };
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                var idUser = await userManager.GetUserIdAsync(user);
                await signInManager.SignInAsync(user, false);
                return new Response { status = 200, message = idUser };
            }
            else
            {
                return new Response { status = 400, message =result.Errors.First().Description };
            }
        }

        public async void logout()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<Response> removeAccount(string password, string idUser)
        {
            IdentityUser user = userManager.FindByIdAsync(idUser).Result;
            var result = await userManager.CheckPasswordAsync(user, password);
            if (result == false)
            {
                return new Response { status = 403, message = "invalid password" };
            }
            using (var transaction = dbTransaction.begin())
            {
                try
                {
                    await userManager.DeleteAsync(user);
                    operationRepo.deleteAll(user.Id);
                    depositoryRepo.deleteAll(user.Id);
                    creditRepo.deleteAll(user.Id);
                    transaction.Commit();
                    return new Response { status = 200, message = "OK" };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new Response { status = 500, message = ex.Message };
                }
            }
        }

        public async Task<Response> rename(string name, string idUser)
        {
            IdentityUser user = userManager.FindByIdAsync(idUser).Result;
            user.UserName = name;
            var result = userManager.UpdateAsync(user);
            if (!result.Result.Succeeded)
            {
                return new Response { status = 500, message = "server error" };
            }
            await signInManager.SignOutAsync();
            await signInManager.SignInAsync(user, new AuthenticationProperties { IsPersistent = false }, "default");
            return new Response { status = 200, message = user.Id };
        }



        public async Task<Response> changePassword(string old_password, string new_password, string idUser)
        {
            IdentityUser user = userManager.FindByIdAsync(idUser).Result;
            if (user == null)
            {
                return new Response { status = 400, message = "User does not exist" };
            }
            var result = userManager.ChangePasswordAsync(user, old_password, new_password).Result;
            if (!result.Succeeded)
            {
                return new Response { status = 400, message = "invalid password" };
            }
            return new Response { status = 200, message = user.Id };
        }
    }
}
