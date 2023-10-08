using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreMvc_ETicaret_Entity.Entities
{
    public class Cart : BaseEntity
    {
        public int UserId { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual List<CartLine> CartLines { get; set; }
    }
}
