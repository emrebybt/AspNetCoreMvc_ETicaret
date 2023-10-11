using AspNetCoreMvc_ETicaret_DataAccess.Context;
using AspNetCoreMvc_ETicaret_DataAccess.Identity;
using AspNetCoreMvc_ETicaret_DataAccess.Repositories;
using AspNetCoreMvc_ETicaret_DataAccess.UnitOfWorks;
using AspNetCoreMvc_ETicaret_Entity.Repositories;
using AspNetCoreMvc_ETicaret_Entity.Services;
using AspNetCoreMvc_ETicaret_Entity.UnitOfWorks;
using AspNetCoreMvc_ETicaret_Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreMvc_ETicaret_Service.Extensions
{
    public static class DependencyExtensions
    {
        public static void AddExtensions(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(
            opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 3;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireDigit = false;

                //opt.User.AllowedUserNameCharacters = "abcdefghijklmnoprstuvyzqw0123456789";

                opt.User.RequireUniqueEmail = true;

                opt.Lockout.MaxFailedAccessAttempts = 3;    //default : 5
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1); //default 5 dk
            }
            ).AddEntityFrameworkStores<ECommerceDbContext>();

            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = new PathString("/Account/Login");
                opt.LogoutPath = new PathString("/Account/Logout");
                opt.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                opt.SlidingExpiration = true; //10 dk dolmadan kullanıcı login olursa süre baştan başlar.
                opt.Cookie = new CookieBuilder()
                {
                    Name = "Identity.App.Cookie",
                    HttpOnly = true
                };
            });

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped(typeof(IAccountService), typeof(AccountService));
            services.AddScoped<ICartLineService, CartLineService>();
            services.AddScoped<IProductSpecsService, ProductSpecsService>();
            services.AddScoped<IUnitOfWorks, UnitOfWork>();
            services.AddScoped<IFilterSpecService, FilterSpecService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ISaleService, SaleService>();
            services.AddScoped<ISaleDetailsService, SaleDetailsService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
