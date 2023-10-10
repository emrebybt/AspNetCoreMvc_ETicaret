using AspNetCoreMvc_ETicaret_Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreMvc_ETicaret_Entity.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ürün adı boş geçilemez")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Ürün fiyatı boş geçilemez")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Ürün stoğu boş geçilemez")]
        public int Stock { get; set; }
        public bool IsDeleted { get; set; }
        [Required(ErrorMessage = "Ürün markası boş geçilemez")]
        public string Brand { get; set; }
        [Required(ErrorMessage = "Ürünün kapak resmi boş geçilemez")]
        public string ThumbnailImage { get; set; }
        [Required(ErrorMessage = "Ürünün içerik resmi boş geçilemez")]
        public string ContentImage { get; set; }
        [Required(ErrorMessage = "Ürünün içerik resmi boş geçilemez")]
        public string ContentImage2 { get; set; }
        [Required(ErrorMessage = "Ürünün içerik resmi boş geçilemez")]
        public string ContentImage3 { get; set; }
        [Required(ErrorMessage = "Ürünün içerik resmi boş geçilemez")]
        public string ContentImage4 { get; set; }
        [Required(ErrorMessage = "Ürünün içerik resmi boş geçilemez")]
        public int CategoryId { get; set; }
        public Categories Category { get; set; }
    }
}
