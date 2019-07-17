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

                if (TempData["State"] != null) ViewBag.State = TempData["State"];
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
        public ActionResult Edit(int id)
        {
            using (var db = new IsskDb())
            {
                var vehicle = db.Vehicles.Include("VehicleType").FirstOrDefault(v => v.VehicleId == id);
                if (vehicle == null)
                {
                    TempData["State"] = "InvalidId";
                    return RedirectToAction("Index", "Vehicle");
                }
                var vehicleEdit = new VehicleCreateView
                {
                    Manufacturer = vehicle.Manufacturer,
                    Model = vehicle.Model,
                    SideNo = vehicle.SideNo,
                    IsCoupleable = vehicle.IsCoupleable,
                    VehicleTypeId = vehicle.TypeId,
                    _vehicleTypes = db.VehicleTypes.Select(vt => vt).ToList()
                };
                return View(vehicleEdit);

            }
        }

        [CustomAuthorize(Roles = "Moderator, Administrator")]
        [HttpPost]
        public ActionResult Edit(VehicleCreateView vehicleCreateView, int id)
        {
            if (ModelState.IsValid)
            {
                using (var db = new IsskDb())
                {
                    var vehicle = db.Vehicles.Include("VehicleType").FirstOrDefault(v => v.VehicleId == id);
                    if (vehicle == null) return RedirectToAction("Index", "Vehicle");
                    vehicle.SideNo = vehicleCreateView.SideNo;
                    vehicle.Model = vehicleCreateView.Model;
                    vehicle.Manufacturer = vehicleCreateView.Manufacturer;
                    vehicle.IsCoupleable = vehicleCreateView.IsCoupleable;
                    vehicle.TypeId = vehicleCreateView.VehicleTypeId;
                    db.SaveChanges();
                    TempData["State"] = "EditSuccess";
                    return RedirectToAction("Index", "Vehicle");
                }
            }

            ModelState.AddModelError("", "Błąd: Coś poszło nie tak, skontatkuj się z administratorem.");
            return View(vehicleCreateView);
        }

        [CustomAuthorize(Roles = "Moderator, Administrator")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (var db = new IsskDb())
            {
                var vehicle = db.Vehicles.Include("VehicleType").FirstOrDefault(v => v.VehicleId == id);
                if (vehicle == null)
                {
                    TempData["State"] = "InvalidId";
                    return RedirectToAction("Index", "Vehicle");
                }
                var vehicleView = new VehilceDeleteView
                {
                    Manufacturer = vehicle.Manufacturer,
                    Model = vehicle.Model,
                    SideNo = vehicle.SideNo,
                    VehilceTypeName = vehicle.VehicleType.Name
                };
                return View(vehicleView);
            }
        }

        [CustomAuthorize(Roles = "Moderator, Administrator")]
        [HttpPost]
        public ActionResult Delete(VehilceDeleteView vehicleDeleteView, int id)
        {
            if (ModelState.IsValid)
            {
                using (var db = new IsskDb())
                {
                    var vehicle = db.Vehicles.FirstOrDefault(v => v.VehicleId == id);
                    if (vehicle == null)
                    {
                        TempData["State"] = "InvalidId";
                        return RedirectToAction("Index", "Vehicle");
                    }
                    db.Vehicles.Remove(vehicle);
                    db.SaveChanges();
                    TempData["State"] = "DeleteSuccess";
                    return RedirectToAction("Index", "Vehicle");
                }
            }
            ModelState.AddModelError("", "Błąd: Coś poszło nie tak, skontaktuj się z administratorem.");
            return View(vehicleDeleteView);
        }
    }
}