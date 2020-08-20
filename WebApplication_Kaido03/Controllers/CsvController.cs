using System;
using System.Web;
using System.Web.Mvc;
using WebApplication_Kaido03.Services;
using System.Text;
using System.Web.UI.WebControls;
using WebApplication_Kaido03.Models;
using System.Linq;

namespace WebApplication_Kaido03.Controllers
{

    public class CsvController : Controller
    {
        private WebApplication_Kaido03Context db = new WebApplication_Kaido03Context();


        // GET: Csv
        public ActionResult Index()
        {
            return RedirectToAction("Export");
        }

        // GET: Parents/Export
        public ActionResult Export()
        {
            return View();
        }

        // POST: Parents/Export
        [HttpPost, ActionName("Export")]
        [ValidateAntiForgeryToken]
        public ActionResult ExportDownloaded()
        {
            // DB からデータ取得
            var parentList = db.Parents.ToList();

            // CSV 内容生成
            var csvString = CsvServices.CreateCsv(parentList);

            // クライアントにダウンロードさせる形で CSV 出力
            var fileName = string.Format("マスタデータ_{0}.csv", DateTime.Now.ToString("yyyyMMddHHmmss"));
            // IE で全角が文字化けするため、ファイル名を UTF-8 でエンコーディング
            Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", HttpUtility.UrlEncode(fileName, Encoding.UTF8)));
            return Content(csvString, "text/csv", Encoding.GetEncoding("Shift_JIS"));
        }
    }
}