using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace SwiftSearch.Data
{
    public class Vehicle
    {
        [Key]
        public int ID { get; set; }
        public string CarName { get; set; }
        public string CarModel { get; set; }
        public string CarColor { get; set; }
        public int CarPrice { get; set; }
        public string CarDealer { get; set; }
        public string CarAddress { get; set; }
        [Display(Name ="Upload First Image")]
        public string CarImage { get; set; }
      
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

    }
}
