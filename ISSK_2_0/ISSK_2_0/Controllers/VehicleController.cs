using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISSK_2_0.Models;

namespace ISSK_2_0.Controllers
{
    public class VehicleController : Controller
    {
        [CustomAuthorize(Roles = "Moderator, Administrator")]
        // GET: Vehicle
        public ActionResult Index()
        {
            using (var db = new IsskDb())
            {
                var vehicles = db.Vehicles.Include("VehicleType").Select(v => v).ToList();
                var vehiclesView = new List<VehicleListView>();
                foreach (var vehicle in vehicles)
                {
                    vehiclesView.Add(new VehicleListView
                    {
                        VehicleId = vehicle.VehicleId,
                        Manufacturer = vehicle.Manufacturer,
                        Model = vehicle.Model,
                        IsCoupleable = vehicle.IsCoupleable,
                        SideNo = vehicle.SideNo,
                        VehicleTypeName = vehicle.VehicleType.Name
                    });
                }
                return View(vehiclesView);
            }
        }

        [CustomAuthorize(Roles = "Moderator, Administrator")]
        [HttpGet]
        public ActionResult Create()
        {
            using (var db = new IsskDb())
            {
                var vehicleTypes = db.VehicleTypes.Select(vt => vt).ToList();
                var viewModel = new VehicleCreateView
                {
                    _vehicleTypes = vehicleTypes
                };
                return View(viewModel);
            }
        }

        [CustomAuthorize(Roles = "Moderator, Administrator")]
        [HttpPost]
        public ActionResult Create(VehicleCreateView vehicleCreateView)
        {
            if (ModelState.IsValid)
            {
                using (var db = new IsskDb())
                {
                    var vehicle = new Vehicle
                    {
                        Manufacturer = vehicleCreateView.Manufacturer,
                        Model = vehicleCreateView.Model,
                        IsCoupleable = vehicleCreateView.IsCoupleable,
                        SideNo = vehicleCreateView.SideNo,
                        TypeId = vehicleCreateView.VehicleTypeId
                    };
                    db.Vehicles.Add(vehicle);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Vehicle");
                }
            }
            ModelState.AddModelError("", "Błąd: Wystąpił nieoczekiwany błąd, skontaktuj się z administratorem.");
            return View(vehicleCreateView);
        }

        [CustomAuthorize(Roles = "Moderator, Administrator")]
        [HttpGet]
        public ActionResult Edit()
        {
            return View();
        }

        [CustomAuthorize(Roles = "Moderator, Administrator")]
        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }
    }
}