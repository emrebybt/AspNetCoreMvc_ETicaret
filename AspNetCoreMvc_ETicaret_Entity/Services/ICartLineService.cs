using AspNetCoreMvc_ETicaret_Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreMvc_ETicaret_Entity.Services
{
    public interface ICartLineService
    {
        Task<List<CartLineViewModel>> AddCart(List<CartLineViewModel> cartline, int id, int quantity);
        List<CartLineViewModel> RemoveCart(List<CartLineViewModel> cartline, int id);
        decimal TotalPrince(List<CartLineViewModel> cartline);
        void CreateCartLine(List<CartLineViewModel> model, int id, int quantity, int productId);
        List<CartLineViewModel> GetCartLines(int id);
        void DecreaseCartLine(List<CartLineViewModel> model, int productId);
        void DecreaseUserCartLine(List<CartLineViewModel> model, int productId);
        CartLineViewModel Get(int id);
        void DeleteCartLine(int id);
        void MoveCartLineSessionToDb(List<CartLineViewModel> model, int id, List<CartLineViewModel> dbcart);
        decimal CartTotalPrice(int id);
        void Delete(int id);



    }
}
