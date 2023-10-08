using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreMvc_ETicaret_Entity.Entities
{
    public class Categories : BaseEntity
    {
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        List<Products> Products { get; set; }
    }
}
