using PagedList;
using SwiftSearch.Config;
using SwiftSearch.Data;
using SwiftSearch.Helpers;
using SwiftSearch.Interfaces;
using SwiftSearch.Models;
using SwiftSearch.RemitaResources.Infrastructure;
using System;
using System.Globalization;
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
        private readonly IVehicleRepository _vehicleRepository;
        private IGateWayIntegrator Integrator { get; set; }

        private IntegrateConfig config;

        private RemitaHashGenerator _hasher;

        public VehiclesController(IUnitOfWork unitOfWork, IVehicleRepository vehicleRepository)
        {
            #region MyRegion
            //demo credentails provided by remita for use in development.
            //substitute with actual live credentials.
            config = new SampleConfig("2547916", "4430731", "1946");
            _hasher = new RemitaHashGenerator(config);
            //Better way would be to use DI to inject everything rather than newing up thing in the controller
            Integrator = new RemitaGateWayIntegrator(_hasher);
            #endregion


            _unitOfWork = unitOfWork;
            _vehicleRepository = vehicleRepository;

        }
       
        // GET: Vehicles
        public async Task<ActionResult> Index()
        {
           var allData = await _vehicleRepository.GetAllDataAsync();
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
            var list = _vehicleRepository.GetVehiclesBySearch().ToList();

            var finalList = list.ToPagedList(page.Value, recordsPerPage);
            return View(finalList);
        }

        //GET: Vehicle/MakePayment

        public ActionResult MakePayment(Vehicle model)
        {
            var listPay = _vehicleRepository.MakeVehiclePayment();
            if(listPay != null)
            {
                return RedirectToAction("CreatePayment", "Vehicles", new { id = listPay.ID});
            }
            return View();
        }


        public ActionResult CreatePayment(int Id)
        {
            var paymentType = from PaymentType p in Enum.GetValues(typeof(PaymentType))
                              select new { ID = p, Name = p.GetDescription() };
            ViewBag.PaymentType = new SelectList(paymentType, "Name", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePayment(Payment model)
        {
            var post = new VehiclePayment
            {
                CarPrice = Convert.ToInt32(model.PaymentAmount),
                PayerEmail = model.PayerEmail,
                PayerName = model.PayerName,
                PayerPhone = model.PayerPhoneNumber,
                PayerId = Guid.NewGuid().ToString(),
                OrderId = DateTimeOffset.UtcNow.UtcTicks.ToString(),
                MerchantId = config.MerchantId,
                ServiceTypeId = config.ServiceTypeId,
                RemitaPaymentType = model.PaymentType.ToString(),
                ResponseUrl = "http://localhost:1572/Vehicles/Listing"
            };

            post.Hash = _hasher.HashRemitaRequest(post);

            var paymentType = from PaymentType p in Enum.GetValues(typeof(PaymentType))
                              select new { ID = p, Name = p.GetDescription() };
            ViewBag.PaymentType = new SelectList(paymentType, "Name", "Name");
            return View("ProcessPayment", post);
        }

        // GET: Vehicles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }
            var vehicleDetail = await _vehicleRepository.FindAsync(id);
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
                    _vehicleRepository.Insert(vehicle);
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
            var vehicleEdit = await _vehicleRepository.FindAsync(id);
    
            return View(vehicleEdit);
        }

        // POST: Vehicles/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int? id, Vehicle vehicle)
        {

            try
            {
                // TODO: Add update logic here

                await _vehicleRepository.FindAsync(id);
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
                _vehicleRepository.Update(vehicle);
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
            var deleteVehicle = await _vehicleRepository.FindAsync(id);
            return View(deleteVehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id, Vehicle vehicle)
        {
            try
            {

                // TODO: Add delete logic here
                _vehicleRepository.Delete(id);
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
