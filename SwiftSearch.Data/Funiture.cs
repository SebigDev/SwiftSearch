using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SwiftSearch.Data
{
    public class Furniture
    {
        [Key]
        public int ID { get; set; }
        public string FurnitureName { get; set; }
        public string FurnitureModel { get; set; }
        public string FurnitureColor { get; set; }
        public int FurniturePrice { get; set; }
        public string FurnitureDealer { get; set; }
        public string FurnitureAddress { get; set; }
        public string FurnitureImage { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }
       
    }
}
