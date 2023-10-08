using AspNetCoreMvc_ETicaret_Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreMvc_ETicaret_Entity.ViewModels
{
    public class SaleViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string ThumbnailImage { get; set; }
        public virtual List<SaleDetails> SaleDetails { get; set; }
    }
}
