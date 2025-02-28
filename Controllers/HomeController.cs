using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
