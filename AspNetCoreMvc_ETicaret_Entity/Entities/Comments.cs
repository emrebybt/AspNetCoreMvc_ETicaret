using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreMvc_ETicaret_Entity.Entities
{
    public class Comments : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public Products Product { get; set; }

    }
}
