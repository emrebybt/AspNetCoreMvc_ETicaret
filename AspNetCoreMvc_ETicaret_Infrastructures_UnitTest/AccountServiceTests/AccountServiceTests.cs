using AspNetCoreMvc_ETicaret_DataAccess.Identity;
using AspNetCoreMvc_ETicaret_Entity.Services;
using AspNetCoreMvc_ETicaret_Entity.UnitOfWorks;
using AspNetCoreMvc_ETicaret_Entity.ViewModels;
using AspNetCoreMvc_ETicaret_Service.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreMvc_ETicaret_Infrastructures_UnitTest.AccountServiceTests
{
    public class AccountServiceTests
    {
        [Fact]
        public async Task FindByNameAsync_ValidUsername_ReturnsOK()
        {
            // Arrange
            var userManagerMock = new Mock<UserManager<AppUser>>();
            var signInManagerMock = new Mock<SignInManager<AppUser>>();
            var mapperMock = new Mock<IMapper>();
            var cartServiceMock = new Mock<ICartService>();
            var lineServiceMock = new Mock<ICartLineService>();
            var uowMock = new Mock<IUnitOfWorks>();

            var accountService = new AccountService(userManagerMock.Object, null, signInManagerMock.Object, mapperMock.Object, cartServiceMock.Object, lineServiceMock.Object, uowMock.Object);

            var model = new LoginViewModel
            {
                Username = "testuser",
                Password = "testpassword",
                RememberMe = false
            };

            userManagerMock.Setup(m => m.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new AppUser()); // Bu kullanıcı adıyla ilgili bir kullanıcı döndürmesi gerekiyor.

            signInManagerMock.Setup(m => m.PasswordSignInAsync(It.IsAny<AppUser>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Success);

            // Act
            var result = await accountService.FinByNameAsync(model, null);

            // Assert
            Assert.Equal("OK", result); // Beklenen sonucun "OK" olup olmadığını kontrol edin.
            
        }
    }
}
