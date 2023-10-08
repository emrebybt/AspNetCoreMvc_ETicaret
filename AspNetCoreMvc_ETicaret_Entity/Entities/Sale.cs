using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreMvc_ETicaret_Entity.Entities
{
    public class Sale : BaseEntity
    {
        public int UserId { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual List<SaleDetails> SaleDetails { get; set; }
    }
}
