using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreMvc_ETicaret_Entity.Entities
{
    public class Products : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Brand { get; set; }
        public string ThumbnailImage { get; set; }
        public string ContentImage { get; set; }
        public string ContentImage2 { get; set; }
        public string ContentImage3 { get; set; }
        public string ContentImage4 { get; set; }
        public int CategoryId { get; set; }
        public Categories Category { get; set; }
        public List<ProductSpecs> ProductSpecs { get; set; }
    }
}
