using System.Collections.Generic;
using SelectCountry.Models;
using System.Linq;
using System.Web.Mvc;

namespace SelectCountry.Controllers
{


    public class MultiSelectVMController : Controller
    {
        // GET: MultiSelectVM
        public ActionResult Index()
        {
            using (dbLocationEntities db = new dbLocationEntities())
            {
                List<country> listCountries = db.countries.ToList();
                List<departement> listDepartement = db.departements.ToList();
                List<city> listCities = db.cities.ToList();

                var query = (from lc in listCountries
                             join ld in listDepartement on lc.countryId equals ld.countryId into tab1
                             from ld in tab1
                             join lcity in listCities on ld.departementId equals lcity.departementId
                             select new MultiSelectVM
                             {
                                 countryId = lc.countryId,
                                 countryName = lc.countryName,
                                 departementId = ld.departementId,
                                 departementName = ld.departementName,
                                 cityId = lcity.cityId,
                                 cityName = lcity.cityName

                             }).ToList();
                return View(query);
            }
        }
        [HttpGet]
        public ActionResult createCountrie()
        {
            return View();
        }
        [HttpPost]
        public ActionResult createCountrie(CreateCountrieVM Newcountrie)
        {
            using (dbLocationEntities  db = new dbLocationEntities())
            {
                country newCountry = new country()
                {
                    countryName = Newcountrie.countryName
                };

                db.countries.Add(newCountry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }


        public JsonResult IsExistingCountryName(string countryName)
        {
            using (dbLocationEntities db = new dbLocationEntities())
            {
                string data;
                var isExist = db.countries.FirstOrDefault(s => s.countryName == countryName);
                if (isExist == null)
                {
                    data = "Valid Name";
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    data = "Invalid Name";
                    return Json(data, JsonRequestBehavior.AllowGet);
                }

            }
        }

        public ActionResult join3SelectList()
        {
            using (dbLocationEntities db = new dbLocationEntities())
            {
                List<country> listCountries = db.countries.ToList();
                ViewBag.listofcountries = new SelectList(listCountries, "countryId", "countryName");

                return View();
            }
        }
        public JsonResult getDepListByCountry(int countryId)
        {
            using (dbLocationEntities db = new dbLocationEntities())
            {
                //db.Configuration.ProxyCreationEnabled = false;
                var data = db.departements.Where(s => s.countryId == countryId).Select(c => new {
                    departementId = c.departementId,
                    departementName = c.departementName
                }).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult getCitiesListByDep(int depId)
        {
            using (dbLocationEntities db = new dbLocationEntities())
            {
                //db.Configuration.ProxyCreationEnabled = false;
                var data = db.cities.Where(s => s.departementId == depId).Select(c => new {
                    idCity = c.cityId,
                    nameCity = c.cityName
                }).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

    }

   
}