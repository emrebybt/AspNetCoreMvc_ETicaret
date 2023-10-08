using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreMvc_ETicaret_Entity.Entities
{
    public class CartLine : BaseEntity
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrince { get; set; }
        public decimal Price { get; set; }
        public int CartId { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual Products Product { get; set; }
    }
}
