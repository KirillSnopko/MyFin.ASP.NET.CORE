using Microsoft.AspNetCore.Mvc;
using MyFin.v2.Models.Entities.finance;
using MyFin.v2.Models.services.ifaces;
using System.Globalization;

namespace MyFin.v2.Controllers
{
    public class DepositoryController : Controller
    {
        private IDepositoryService depositoryService;
        private ICreditService creditService;

        public object JsonRequestBehavior { get; private set; }

        public DepositoryController(IDepositoryService depositoryService, ICreditService creditService)
        {
            this.depositoryService = depositoryService;
            this.creditService = creditService;
        }

        [HttpGet]
        public ActionResult List()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetData()
        {
            var idUser = User.Claims.First().Value;
            var response = depositoryService.depositoriesByUserId(idUser);
            return Json(response);
        }

        [HttpGet]
        public ActionResult GetById(int id)
        {
            var idUser = User.Claims.First().Value;
            var response = depositoryService.get(id, idUser);
            return Json(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TypeDep tDep, TypeOfMoney tMoney, string name, double amount)
        {
            var idUser = User.Claims.First().Value;
            depositoryService.add(tDep, tMoney, name, amount, idUser);
            return Json(new { status = 200 });
        }

        [HttpGet]

        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rename(string name, int id)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Json(new { status = 500, message = "invalid value" });
            }
            var idUser = User.Claims.First().Value;
            depositoryService.rename(name, id, idUser);
            return Json(new { status = 200 });
        }

        [HttpGet]
        public ActionResult Count()
        {
            int dep_count = 0;
            int credit_count = 0;
            if (User.Identity.IsAuthenticated)
            {
                var idUser = User.Claims.First().Value;
                dep_count = depositoryService.count(idUser);
                credit_count = creditService.count(idUser);
            }
            return Json(new { dep_count, credit_count });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var idUser = User.Claims.First().Value;
            depositoryService.delete(id, idUser);
            return Json(new { status = 200 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Change(int idDepository, bool isSpending, string amountOfMoney, string comment, Category category, TypeOfMoney typeOfMoney)
        {
            var idUser = User.Claims.First().Value;
            double amount = Double.Parse(amountOfMoney, CultureInfo.InvariantCulture);
            depositoryService.change(idDepository, isSpending, amount, idUser, comment, category, typeOfMoney);
            return Json(new { status = 200 });
        }

        [HttpGet]
        public ActionResult HistoryById(int id)
        {
            var idUser = User.Claims.First().Value;
            var response = depositoryService.historyById(id, idUser);
            return Json(response);
        }
    }
}
