using PagedList;
using SwiftSearch.Data;
using SwiftSearch.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;

namespace SwiftSearch.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VehiclesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
       
        // GET: Vehicles
        public async Task<ActionResult> Index()
        {
           var allData = await _unitOfWork.VehicleRepo.GetAllDataAsync();
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
            var list = _unitOfWork.VehicleRepo.GetVehiclesBySearch().ToList();

            var finalList = list.ToPagedList(page.Value, recordsPerPage);
            return View(finalList);
        }

        //GET: Vehicle/MakePayment

        public ActionResult MakePayment(Vehicle model)
        {
            var listPay = _unitOfWork.VehicleRepo.MakeVehiclePayment();
            return View(listPay);
        }
        // GET: Vehicles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }
            var vehicleDetail = await _unitOfWork.VehicleRepo.FindAsync(id);
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
                fileName = fileName + Guid.NewGuid().ToString() + extension;
                vehicle.CarImage = "~/PhotoUploads/Vehicles/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/PhotoUploads/Vehicles/"), fileName);
                vehicle.ImageFile.SaveAs(fileName);

                //resizing image
                MemoryStream ms = new MemoryStream();
                WebImage webImage = new WebImage(fileName);
                if (webImage.Width > 700)
                {
                    webImage.Resize(700, 564, false);
                    webImage.Save(fileName);
                }
                _unitOfWork.VehicleRepo.Insert(vehicle);
                _unitOfWork.Complete();            
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
            var vehicleEdit = await _unitOfWork.VehicleRepo.FindAsync(id);
    
            return View(vehicleEdit);
        }

        // POST: Vehicles/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int? id, Vehicle vehicle)
        {

            try
            {
                // TODO: Add update logic here

                await _unitOfWork.VehicleRepo.FindAsync(id);
                var fileName = Path.GetFileNameWithoutExtension(vehicle.ImageFile.FileName);
                var extension = Path.GetExtension(vehicle.ImageFile.FileName);
                fileName = fileName + Guid.NewGuid().ToString() + extension;
                vehicle.CarImage = "~/PhotoUploads/Vehicles/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/PhotoUploads/Vehicles/"), fileName);
                vehicle.ImageFile.SaveAs(fileName);
                //resizing image
                MemoryStream ms = new MemoryStream();
                WebImage webImage = new WebImage(fileName);
                if (webImage.Width > 700)
                {
                    webImage.Resize(700, 564, false);
                    webImage.Save(fileName);
                }
                _unitOfWork.VehicleRepo.Update(vehicle);
                _unitOfWork.Complete();
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
            var deleteVehicle = await _unitOfWork.VehicleRepo.FindAsync(id);
            return View(deleteVehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id, Vehicle vehicle)
        {
            try
            {

                // TODO: Add delete logic here
                _unitOfWork.VehicleRepo.Delete(id);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw new ArgumentException(String.Format($"The {vehicle.CarName} could not be deleted"));
            }
           
        }
    }
}
