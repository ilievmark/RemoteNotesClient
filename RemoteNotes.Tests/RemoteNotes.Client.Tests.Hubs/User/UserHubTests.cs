using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using RemoteNotes.Client.Tests.Hubs.Notes;
using RemoteNotes.Domain.Extensions;
using RemoteNotes.Domain.Models;
using RemoteNotes.Service.Domain.User;
using Xunit;

namespace RemoteNotes.Client.Tests.Hubs.User
{
    public class UserHubTests
    {
        [Fact]
        public async Task ProfileUpdated_Check()
        {
            //Arrange
            var hub = new UserHub();
            var count = 0;
            hub.UserDataUpdated += model => count++;
            
            //Act
            var profile = new UserModel();
            var json = profile.ToJson();
            await hub.HandleMessageAsync(json);
            
            //Assert
            count.Should().Be(1);
        }
    }
}