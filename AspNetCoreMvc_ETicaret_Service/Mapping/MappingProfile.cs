using AspNetCoreMvc_ETicaret_DataAccess.Identity;
using AspNetCoreMvc_ETicaret_Entity.Entities;
using AspNetCoreMvc_ETicaret_Entity.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreMvc_ETicaret_Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Products,ProductViewModel>().ReverseMap();
            CreateMap<CartLine,CartLineViewModel>().ReverseMap();
            CreateMap<AppUser,UserViewModel>().ReverseMap();
            CreateMap<FilterSpecs,FilterSpecsViewModel>().ReverseMap();
            CreateMap<ProductSpecs,ProductSpecsViewModel>().ReverseMap();
            CreateMap<Cart,CartViewModel>().ReverseMap();
            CreateMap<Sale,SaleViewModel>().ReverseMap();
            CreateMap<Categories,CategoryViewModel>().ReverseMap();
            CreateMap<SaleDetails,SaleDetailsViewModel>().ReverseMap();
        }
    }
}
