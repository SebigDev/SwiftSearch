using PagedList;
using SwiftSearch.Data;
using SwiftSearch.Models;
using SwiftSearch.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SwiftSearch.Controllers
{
    public class VehiclesController : Controller
    {
        private UnitOfWork _unitOfWork;

        public VehiclesController()
        {
            _unitOfWork = new UnitOfWork(new SwiftSearchDbContext());

        }
        // GET: Vehicles
        public async Task<ActionResult> Index()
        {
           var allData = await _unitOfWork.Vehicle.GetAllDataAsync();
            return View(allData);
        
        }
        public ActionResult Listing(string sortOn, string orderBy, string pSortOn, int? page)
        {
            int recordsPerPage = 3;
            if (!page.HasValue)
            {
                page = 1; // set initial page value
                if (string.IsNullOrWhiteSpace(orderBy) || orderBy.Equals("asc"))
                {
                    orderBy = "desc";
                }
                else
                {
                    orderBy = "asc";
                }
            }


            ViewBag.OrderBy = orderBy;
            ViewBag.SortOn = sortOn;
            var list = _unitOfWork.Vehicle.GetVehiclesBySearch().ToList();

            var finalList = list.ToPagedList(page.Value, recordsPerPage);
            return View(finalList);
        }
        // GET: Vehicles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }
            var vehicleDetail = await _unitOfWork.Vehicle.FindAsync(id);
            return View(vehicleDetail);
        }

        // GET: Vehicles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vehicles/Create
        [HttpPost]
        public ActionResult Create(Vehicle vehicle)
        {

              // TODO: Add insert logic here
                if (ModelState.IsValid)
                {

                var fileName = Path.GetFileNameWithoutExtension(vehicle.ImageFile.FileName);
                var extension = Path.GetExtension(vehicle.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                vehicle.CarImage = "~/PhotoUploads/Vehicles/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/PhotoUploads/Vehicles/"), fileName);
                vehicle.ImageFile.SaveAs(fileName);
                _unitOfWork.Vehicle.Insert(vehicle);
                _unitOfWork.Vehicle.Save();
                return RedirectToAction("Index");
                }  
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var vehicleEdit = await _unitOfWork.Vehicle.FindAsync(id);
    
            return View(vehicleEdit);
        }

        // POST: Vehicles/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int? id, Vehicle vehicle)
        {

            try
            {
                // TODO: Add update logic here

               await _unitOfWork.Vehicle.FindAsync(id);
                var fileName = Path.GetFileNameWithoutExtension(vehicle.ImageFile.FileName);
                var extension = Path.GetExtension(vehicle.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                vehicle.CarImage = "~/PhotoUploads/Vehicles/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/PhotoUploads/Vehicles/"), fileName);
                vehicle.ImageFile.SaveAs(fileName);
                _unitOfWork.Vehicle.Update(vehicle);
                _unitOfWork.Vehicle.Save();
                return RedirectToAction("Index");
            }
            catch
            {
               //
            }
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var deleteVehicle = await _unitOfWork.Vehicle.FindAsync(id);
            return View(deleteVehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, Vehicle vehicle)
        {
           
            // TODO: Add delete logic here
             _unitOfWork.Vehicle.Delete(id);
             _unitOfWork.Vehicle.Save();
             return RedirectToAction("Index");
           
        }
    }
}
