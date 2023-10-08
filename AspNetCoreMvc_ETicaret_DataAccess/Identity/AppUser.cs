using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreMvc_ETicaret_DataAccess.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? AdressName { get; set; }
        public string? Adress { get; set; }
        public string? Adress2 { get; set; }
        public string? Adress3 { get; set; }
    }
}
