using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using OfficeOpenXml; // EPPlus için
using System.IO;

namespace FilmProjesi.Controllers
{
    public class HomeController : Controller
    {
        private static List<Film> Filmler = new List<Film>();

        public IActionResult Index()
        {
            return View(Filmler);
        }

        [HttpPost]
        public IActionResult FilmEkle(string filmAdi, string filmAdiIng, int yapimYili, string oyuncular, double imdbPuani)
        {
            Filmler.Add(new Film
            {
                FilmAdi = filmAdi,
                FilmAdiIng = filmAdiIng,
                YapimYili = yapimYili,
                Oyuncular = oyuncular,
                ImdbPuani = imdbPuani
            });

            return RedirectToAction("Index");
        }

        public IActionResult ExcelIndir()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Lisans ayarı

            using (var package = new ExcelPackage())
            {
                var sheet = package.Workbook.Worksheets.Add("Filmler");

                // Başlıkları ekleyelim
                sheet.Cells[1, 1].Value = "Film Adı";
                sheet.Cells[1, 2].Value = "İngilizce Adı";
                sheet.Cells[1, 3].Value = "Yapım Yılı";
                sheet.Cells[1, 4].Value = "Oyuncular";
                sheet.Cells[1, 5].Value = "IMDB Puanı";

                // Film listesini ekleyelim
                for (int i = 0; i < Filmler.Count; i++)
                {
                    sheet.Cells[i + 2, 1].Value = Filmler[i].FilmAdi;
                    sheet.Cells[i + 2, 2].Value = Filmler[i].FilmAdiIng;
                    sheet.Cells[i + 2, 3].Value = Filmler[i].YapimYili;
                    sheet.Cells[i + 2, 4].Value = Filmler[i].Oyuncular;
                    sheet.Cells[i + 2, 5].Value = Filmler[i].ImdbPuani;
                }

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                string excelName = $"FilmListesi_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
        }
    }

    public class Film
    {
        public string FilmAdi { get; set; }
        public string FilmAdiIng { get; set; }
        public int YapimYili { get; set; }
        public string Oyuncular { get; set; }
        public double ImdbPuani { get; set; }
    }
}
