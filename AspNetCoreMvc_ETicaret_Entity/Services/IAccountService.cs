using AspNetCoreMvc_ETicaret_Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreMvc_ETicaret_Entity.Services
{
    public interface IAccountService
    {
        Task<string> CreateUserAsync(RegisterViewModel model);
        Task<string> FinByNameAsync(LoginViewModel model, List<CartLineViewModel> cartline);
        Task<UserViewModel> Find(string name);
        Task LogoutAsync();
        Task <UserViewModel> FindByIdAsync(int id);
        void UpdateCartPrice(int cartId);
        Task<List<UserViewModel>> GetAll();
        Task<List<RoleViewModel>> GetAllRoles();
        Task<string> CreateRoleAsync(RoleViewModel role);
        Task<UsersInOrOutViewModel> GetAllUsersWithRole(string id);
        Task<RoleViewModel> FindRoleByIdAsync(string id);
        Task<string> EditRoleListAsync(EditRoleViewModel model);
    }
}
