using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreMvc_ETicaret_Entity.Entities
{
    public class ProductSpecs : BaseEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public int ProductId { get; set; }
        public Products Product { get; set; }
    }
}
