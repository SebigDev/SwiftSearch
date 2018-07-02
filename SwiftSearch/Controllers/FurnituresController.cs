using SwiftSearch.Data;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using PagedList;
using SwiftSearch.Interfaces;
using System.Web.Helpers;
using SwiftSearch.RemitaResources.Infrastructure;

namespace SwiftSearch.Controllers
{
    public class FurnituresController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFurnitureRepository _furnitureRepository;

        private IGateWayIntegrator Integrator { get; set; }
        private IntegrateConfig config;
        private RemitaHashGenerator _hasher;

        public FurnituresController(IUnitOfWork unitOfWork, IFurnitureRepository furnitureRepository)
        {
            #region RemitaConfig
            //demo credentails provided by remita for use in development.
            //substitute with actual live credentials.
            config = new SampleConfig("2547916", "4430731", "1946");
            _hasher = new RemitaHashGenerator(config);
            //Better way would be to use DI to inject everything rather than newing up thing in the controller
            Integrator = new RemitaGateWayIntegrator(_hasher); 
            #endregion

           
            _unitOfWork = unitOfWork;
            _furnitureRepository = furnitureRepository; 
        }
        // GET: Furnitures
        public async Task<ActionResult> Index()
        {
            var list = await _furnitureRepository.GetAllDataAsync();
            return View(list);
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
           
            var list = _furnitureRepository.GetFurnituresBySearch().ToList();
            
            var finalList = list.ToPagedList(page.Value, recordsPerPage);
            
            return View(finalList);
        }
        // GET: Furnitures/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var detail = await _furnitureRepository.FindAsync(id);
            return View(detail);
        }

        // GET: Furnitures/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Furnitures/Create
        [HttpPost]
        public ActionResult Create(Furniture furniture)
        {

                if (ModelState.IsValid)
                {

                    var fileName = Path.GetFileNameWithoutExtension(furniture.ImageFile.FileName);
                    var extension = Path.GetExtension(furniture.ImageFile.FileName);
                    fileName = fileName + Guid.NewGuid().ToString() + extension;
                    furniture.FurnitureImage = "~/PhotoUploads/Furnitures/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/PhotoUploads/Furnitures/"), fileName);
                    furniture.ImageFile.SaveAs(fileName);
                    //resizing image
                    MemoryStream ms = new MemoryStream();
                    WebImage webImage = new WebImage(fileName);
                    if (webImage.Width > 700)
                    {
                        webImage.Resize(700, 564, false);
                        webImage.Save(fileName);
                    }
                    _furnitureRepository.Insert(furniture);
                    _unitOfWork.Complete();
                    return RedirectToAction("Index");
                }
           
            return View(furniture);
        }

        // GET: Furnitures/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var edit = await _furnitureRepository.FindAsync(id);
            return View(edit);
        }

        // POST: Furnitures/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Furniture furniture)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {

                    var fileName = Path.GetFileNameWithoutExtension(furniture.ImageFile.FileName);
                    var extension = Path.GetExtension(furniture.ImageFile.FileName);
                    fileName = fileName + Guid.NewGuid().ToString() + extension;
                    furniture.FurnitureImage = "~/PhotoUploads/Vehicles/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/PhotoUploads/Vehicles/"), fileName);
                    furniture.ImageFile.SaveAs(fileName);
                    //resizing image
                    MemoryStream ms = new MemoryStream();
                    WebImage webImage = new WebImage(fileName);
                    if (webImage.Width > 700)
                    {
                        webImage.Resize(700, 564, false);
                        webImage.Save(fileName);
                    }
                    _furnitureRepository.Update(furniture);
                    _unitOfWork.Complete();
                    return RedirectToAction("Index");
                }
               
            }
            catch
            {
                throw new ArgumentNullException(String.Format($"Cannot Update {furniture.FurnitureName} "));
            }
            return View(furniture);
        }

        // GET: Furnitures/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var delete = await _furnitureRepository.FindAsync(id);

            return View(delete);
        }

        // POST: Furnitures/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id, Furniture furniture)
        {
            try
            {
                // TODO: Add delete logic here
                _furnitureRepository.FindAsync(id);
                _furnitureRepository.Delete(furniture);
                return RedirectToAction("Index");
            }
            catch
            {
                throw new ArgumentException(String.Format($"The {furniture.FurnitureName} could not be deleted"));
            }
           // return View();
        }
    }
}
