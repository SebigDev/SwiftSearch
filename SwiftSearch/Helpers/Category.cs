using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwiftSearch.Helpers
{
    public class Category : Enumeration
    {
        public static readonly Category Vehicles = new Category(1, "Vehicles");
        public static readonly Category Furnitures = new Category(2, "Furnitures");
        public static readonly Category CantenatedVehicles = new Category(3, "Cantenated Vehicles");

        public Category()
        {

        }
        public Category(int value, string displayName) 
            : base(value, displayName)
        {

        }

        public static IEnumerable<Category> ListCategory()
        {
            return new[] { Vehicles, Furnitures, CantenatedVehicles };
        }
    }
}