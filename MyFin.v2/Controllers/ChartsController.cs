using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFin.v2.Models.services.ifaces;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace MyFin.v2.Controllers
{
    [Authorize]
    public class ChartsController : Controller
    {
        private IChartsService chartsService;

        public ChartsController(IChartsService chartsService)
        {
            this.chartsService = chartsService;
        }

        [HttpGet]
        [Route("api/Charts/Spending/CurrentDepository/CurrentMonth/{idDepository}")]
        public ActionResult getSpendingDataCurDepCurMonth(int idDepository)
        {
            var idUser = User.Claims.First().Value;
            return Json(chartsService.getSpendingDataCurDepCurMonth(idDepository, idUser));
        }

        [HttpGet]
        [Route("api/Charts/Addition/CurrentDepository/CurrentMonth/{idDepository}")]
        public ActionResult getAddDataCurDepCurMonth(int idDepository)
        {
            var idUser = User.Claims.First().Value;
            return Json(chartsService.getAddDataCurDepCurMonth(idDepository, idUser));
        }

        [HttpGet]
        [Route("api/Charts/Spending/allDepository/CurMonth")]
        public ActionResult getSpendDataAllDepCurMonth()
        {
            var idUser = User.Claims.First().Value;
            return Json(chartsService.getSpendDataAllDepCurMonth(idUser));
        }

        [HttpGet]
        [Route("api/Charts/Addition/allDepository/CurMonth")]
        public ActionResult getAddDataAllDepCurMonth()
        {
            var idUser = User.Claims.First().Value;
            return Json(chartsService.getAddDataAllDepCurMonth(idUser));
        }

        [HttpGet]
        [Route("api/Charts/Spending/AllDepository/AllTime")]
        public ActionResult getSpendDataAllDepAllTime()
        {
            var idUser = User.Claims.First().Value;
            return Json(chartsService.getSpendDataAllDepAllTime(idUser));
        }

        [HttpGet]
        [Route("api/Charts/Addition/allDepository/AllTime")]
        public ActionResult getAddDataAllDepAllTime()
        {
            var idUser = User.Claims.First().Value;
            return Json(chartsService.getAddDataAllDepAllTime(idUser));
        }
    }
}
