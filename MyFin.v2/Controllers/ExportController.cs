using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFin.v2.Models.Entities.finance;
using MyFin.v2.Models.services.ifaces;
using System.Data;

namespace MyFin.v2.Controllers
{
    [Authorize]
    public class ExportController : Controller
    {
        private IDepositoryService depositoryService;
        public ExportController(IDepositoryService depositoryService)
        {
            this.depositoryService = depositoryService;
        }

        public FileResult ExportById(int idDepository)
        {
            var idUser = User.Claims.First().Value;
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Date"),
                                            new DataColumn("Category"),
                                            new DataColumn("Comment"),
                                            new DataColumn("Value") });
            List<Operation> history = depositoryService.historyById(idDepository, idUser);
            foreach (Operation fp in history)
            {
                dt.Rows.Add(fp.created, Enum.GetName(typeof(Category), fp.category), fp.comment, fp.isSpending ? ("-" + fp.amountOfMoney) : ("+" + fp.amountOfMoney));
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"History{DateTime.Now.ToString("MM-dd-yyyy")}.xlsx");
                }
            }
        }
    }
}
