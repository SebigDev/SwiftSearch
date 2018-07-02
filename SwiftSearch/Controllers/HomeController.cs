
using PagedList;
using SwiftSearch.Data;
using SwiftSearch.Helpers;
using SwiftSearch.Interfaces;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IFurnitureRepository _furnitureRepository;

        public HomeController(IUnitOfWork unitOfWork,IVehicleRepository vehicleRepository,IFurnitureRepository furnitureRepository)
        {
            _unitOfWork = unitOfWork;
            _vehicleRepository = vehicleRepository;
            _furnitureRepository = furnitureRepository;
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

            var list = _vehicleRepository.GetVehiclesBySearch();
            var list2 = _furnitureRepository.GetFurnituresBySearch();
 
            if (!string.IsNullOrWhiteSpace(keyword))
            {
              
                list = list.Where(
                                  f => f.CarName.ToLower().Contains(keyword.ToLower()) ||
                                  f.CarModel.ToLower().Contains(keyword.ToLower()) ||
                                  f.CarDealer.ToLower().Contains(keyword.ToLower()) ||
                                  f.CarPrice.ToString().ToLower().Contains(keyword.ToLower()) ||
                                  f.CarAddress.ToLower().Contains(keyword.ToLower()) ||
                                  f.CarColor.ToLower().Contains(keyword.ToLower())
                                  );
                list2 = list2.Where(
                                    f => f.FurnitureName.ToLower().Contains(keyword.ToLower()) ||
                                    f.FurnitureModel.ToLower().Contains(keyword.ToLower()) ||
                                    f.FurnitureDealer.ToLower().Contains(keyword.ToLower()) ||
                                    f.FurniturePrice.ToString().ToLower().Contains(keyword.ToLower()) ||
                                    f.FurnitureAddress.ToLower().Contains(keyword.ToLower()) ||
                                    f.FurnitureColor.ToLower().Contains(keyword.ToLower())
                                    );
            }
           if(!list.Any() || !list2.Any())
            {
                var error = "No Item(s) Matches your Search";
                ViewBag.Error = error;
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