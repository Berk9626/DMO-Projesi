using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ProductsDTO
    {
        public int ProductsID { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public bool Discontinued { get; set; }

        public int SupplierID { get; set; }

    }
}
