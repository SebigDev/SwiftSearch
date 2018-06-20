
using PagedList;
using SwiftSearch.Data;
using SwiftSearch.Models;
using SwiftSearch.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwiftSearch.Controllers
{
    public class HomeController : Controller
    {
        private UnitOfWork _unitOfWork;

        public HomeController()
        {
            _unitOfWork = new UnitOfWork(new SwiftSearchDbContext()); 
        }
        

        public ActionResult Index(string sortOn, string orderBy,string pSortOn, string keyword, int? page)
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
            ViewBag.Keyword = keyword;

            var list = _unitOfWork.Vehicle.GetVehiclesBySearch();
            var list2 = _unitOfWork.Furniture.GetFurnituresBySearch();
 
            if (!string.IsNullOrWhiteSpace(keyword))
            {
              
                list = list.Where(
                                  f => f.CarName.ToLower().Contains(keyword) ||
                                  f.CarModel.ToLower().Contains(keyword) ||
                                  f.CarDealer.ToLower().Contains(keyword) ||
                                  f.CarPrice.ToString().ToLower().Contains(keyword) ||
                                  f.CarAddress.ToLower().Contains(keyword) ||
                                  f.CarColor.ToLower().Contains(keyword)
                                  );
                list2 = list2.Where(
                                    f => f.FurnitureName.ToLower().Contains(keyword) ||
                                    f.FurnitureModel.ToLower().Contains(keyword) ||
                                    f.FurnitureDealer.ToLower().Contains(keyword) ||
                                    f.FurniturePrice.ToString().ToLower().Contains(keyword)||
                                    f.FurnitureAddress.ToLower().Contains(keyword) ||
                                    f.FurnitureColor.ToLower().Contains(keyword)
                                    );
            }
            else
            {
                ViewBag.NullSearch = "Your Search Returned no Result";  
            }
            var finalList = list.ToPagedList(page.Value, recordsPerPage);
            var finalList1 = list2.ToPagedList(page.Value, recordsPerPage);

            var flist = new Tuple<IPagedList<Vehicle>, IPagedList<Furniture>>(finalList, finalList1);
           
            return View(flist);
        }
       
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}