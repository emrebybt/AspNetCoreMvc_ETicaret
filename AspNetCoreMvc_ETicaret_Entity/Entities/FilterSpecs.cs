using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreMvc_ETicaret_Entity.Entities
{
    public class FilterSpecs : BaseEntity
    {
        public string Spec { get; set; }
        public string Value1 { get; set; }
        public string? Value2 { get; set; }
        public string? Value3 { get; set; }
        public string? Value4 { get; set; }
        public string? Value5 { get; set; }
        public string? Value6 { get; set; }
        public int CategoryId { get; set; }
        public virtual Categories Category { get; set; }
    }
}
