using SwiftSearch.Data;
using SwiftSearch.Models;
using SwiftSearch.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace SwiftSearch.Controllers
{
    public class FurnituresController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        public FurnituresController()
        {
            _unitOfWork = new UnitOfWork(new SwiftSearchDbContext());
        }
        // GET: Furnitures
        public async Task<ActionResult> Index()
        {
            var list = await _unitOfWork.Furniture.GetAllDataAsync();
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
           
            var list = _unitOfWork.Furniture.GetFurnituresBySearch().ToList();
            
            var finalList = list.ToPagedList(page.Value, recordsPerPage);
            return View(finalList);
        }
        // GET: Furnitures/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var detail = await _unitOfWork.Furniture.FindAsync(id);
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
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    furniture.FurnitureImage = "~/PhotoUploads/Furnitures/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/PhotoUploads/Furnitures/"), fileName);
                    furniture.ImageFile.SaveAs(fileName);
                    _unitOfWork.Furniture.Insert(furniture);
                    _unitOfWork.Furniture.Save();
                    return RedirectToAction("Index");
                }
           
            return View();
        }

        // GET: Furnitures/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var edit = await _unitOfWork.Furniture.FindAsync(id);
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
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    furniture.FurnitureImage = "~/PhotoUploads/Vehicles/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/PhotoUploads/Vehicles/"), fileName);
                    furniture.ImageFile.SaveAs(fileName);
                    _unitOfWork.Furniture.Update(furniture);
                    _unitOfWork.Furniture.Save();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Furnitures/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var delete = await _unitOfWork.Furniture.FindAsync(id);

            return View(delete);
        }

        // POST: Furnitures/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, Furniture furniture)
        {
            try
            {
                // TODO: Add delete logic here
                _unitOfWork.Furniture.FindAsync(id);
                _unitOfWork.Furniture.Delete(furniture);
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("","Cannot Delete Furniture");
            }
            return View();
        }
    }
}
