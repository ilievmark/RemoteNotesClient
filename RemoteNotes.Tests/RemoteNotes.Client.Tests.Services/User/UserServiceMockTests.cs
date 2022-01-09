using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace RemoteNotes.Client.Tests.Services.User
{
    public class UserServiceMockTests
    {
        [Fact]
        public async Task GetUser()
        {
            //Arrange
            var userService = UserServiceMockFactory.GetUserService();
            
            //Act
            var userData = await userService.GetUserDataAsync();
            
            //Assert
            userData.Should().NotBeNull();
            userData.Id.Should().Be(0);
            userData.Email.Should().Be("jhon.doe.2925@gmail.com");
            userData.UserName.Should().Be("JonieJi29");
            userData.Surname.Should().Be("Doe");
            userData.Name.Should().Be("Jhon");
            userData.PhotoUrl.Should().Be("https://i1.mosconsv.ru/287/400/800/90/kotek_iosif.jpg");
        }

        [Fact]
        public async Task UpdateUser()
        {
            //Arrange
            var userService = UserServiceMockFactory.GetUserService();
            
            //Act
            var userData = await userService.GetUserDataAsync();
            userData.Email = "valek.commisiion@mail.ru";
            userData.Name = "valek";
            userData.PhotoUrl = "";
            
            //Assert
            userData.Should().NotBeNull();
            userData.Id.Should().Be(0);
            userData.Email.Should().Be("valek.commisiion@mail.ru");
            userData.UserName.Should().Be("JonieJi29");
            userData.Surname.Should().Be("Doe");
            userData.Name.Should().Be("valek");
            userData.PhotoUrl.Should().BeEmpty();
        }
    }
}