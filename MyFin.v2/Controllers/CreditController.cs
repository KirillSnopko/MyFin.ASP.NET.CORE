using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFin.v2.Models.Entities.finance;
using MyFin.v2.Models.services.ifaces;

namespace MyFin.v2.Controllers
{
    [Authorize]
    public class CreditController : Controller
    {
        private ICreditService creditService;
        public CreditController(ICreditService creditService)
        {
            this.creditService = creditService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            var idUser = User.Claims.First().Value;
            var response = creditService.creditsByUserId(idUser);
            return Json(response);
        }

        public ActionResult Create(double value, string comment, DateTime closeDate, TypeOfMoney typeOfMoney)
        {
            string idUser = User.Claims.First().Value;
            creditService.add(value, comment, closeDate, idUser, typeOfMoney);
            return Json(new { status = 200 });
        }

        public ActionResult Delete(int idCredit)
        {
            var idUser = User.Claims.First().Value;
            creditService.delete(idCredit, idUser);
            return Json(new { status = 200 });
        }

        public ActionResult Reduce(int idCredit, double value, string comment)
        {
            var idUser = User.Claims.First().Value;
            creditService.reduce(idCredit, value, idUser, comment);
            return Json(new { status = 200 });
        }

        public ActionResult HistoryById(int id)
        {
            var idUser = User.Claims.First().Value;
            var response = creditService.historyById(id, idUser);
            return Json(response);
        }

      /*  protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception != null)
            {
                var response = new { status = 500, message = filterContext.Exception.Message };
                filterContext.Result = new JsonResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = response
                };
                filterContext.ExceptionHandled = true;
            }
        }*/
    }
}
