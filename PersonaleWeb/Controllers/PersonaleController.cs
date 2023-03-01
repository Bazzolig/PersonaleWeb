using PersonaleWeb.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonaleWeb.Helpers;
using PersonaleWeb.Models.Entities;

namespace PersonaleWeb.Controllers
{
    public class PersonaleController : Controller
    {
        // GET: PersonaleController
        public ActionResult Index()
        {
            var listaPersonale = DatabaseHelper.GetAllPersonale();
            return View(listaPersonale);
        }

        // GET: PersonaleController/Details/5
        public ActionResult Details(int id)
        {
            var personale = DatabaseHelper.GetPersonaleById(id);
            return View(personale);
        }

        // GET: PersonaleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonaleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Personale p)
        {
            try
            {
                DatabaseHelper.InsertPersonale(p);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonaleController/Edit/5
        public ActionResult Edit(int id)
        {
            var personale = DatabaseHelper.GetPersonaleById(id);
            return View(personale);
        }

        // POST: PersonaleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Personale p)
        {
            try
            {
                DatabaseHelper.UpdatePersonale(p);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PersonaleController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PersonaleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
