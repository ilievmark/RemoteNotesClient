using System;
using System.IO;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using RemoteNotes.Domain.Exceptions.Authorization;
using Xunit;

namespace RemoteNotes.Client.Tests.Services.Authorization
{
    public class AuthorizationServiceTests
    {
        [Theory]
        [InlineData("user", "password")]
        [InlineData("JonieJi29", "password")]
        [InlineData("user", "12345t67890")]
        public async Task SignIn_ForUser_NoRegistered(string userName, string password)
        {
            // Arrange
            var authHolder = AuthorizationMockFactory.GetAuthHolder();
            var authService = AuthorizationMockFactory.GetAuthService(authHolder);
            var exception = default(Exception);
            
            //Act
            try
            {
                await authService.SignInAsync(userName, password, CancellationToken.None);
            }
            catch (Exception e)
            {
                exception = e;
            }
            
            //Assert
            exception.Should().BeOfType<AuthenticationException>();
            authHolder.GetLastSession().Should().BeNull();
        }
        
        [Fact]
        public async Task SignIn_ForUser_Registered()
        {
            // Arrange
            var userName = "JonieJi29";
            var password = "12345t67890";
            var authHolder = AuthorizationMockFactory.GetAuthHolder();
            var authService = AuthorizationMockFactory.GetAuthService(authHolder);
            var exception = default(Exception);
            
            //Act
            try
            {
                await authService.SignInAsync(userName, password, CancellationToken.None);
            }
            catch (Exception e)
            {
                exception = e;
            }
            
            //Assert
            exception.Should().BeNull();
            authHolder.GetLastSession().Should().NotBeNull();
            authHolder.GetLastSession().Token.Should().NotBeNullOrEmpty();
            authHolder.GetLastSession().ExpireAt.Should().BeAfter(DateTimeOffset.Now);
            authHolder.GetLastSession().CanBeUpdatedTill.Should().BeAfter(DateTimeOffset.Now);
        }
        
        [Theory]
        [InlineData("user", "password")]
        [InlineData("JonieJi29", "password")]
        [InlineData("user", "12345t67890")]
        public async Task SignUp_ForUser_NoRegistered_Validation(string userName, string password)
        {
            // Arrange
            var authHolder = AuthorizationMockFactory.GetAuthHolder();
            var authService = AuthorizationMockFactory.GetAuthService(authHolder);
            var exception = default(Exception);
            
            //Act
            try
            {
                await authService.SignUpAsync(userName, password, CancellationToken.None);
            }
            catch (Exception e)
            {
                exception = e;
            }
            
            //Assert
            exception.Should().BeOfType<InvalidDataException>();
        }
        
        [Theory]
        [InlineData("user99dsn", "pass4word2")]
        [InlineData("Jonieki29", "password34")]
        [InlineData("user112dd", "12345t67890")]
        public async Task SignUp_ForUser_NoRegistered(string userName, string password)
        {
            // Arrange
            var authHolder = AuthorizationMockFactory.GetAuthHolder();
            var authService = AuthorizationMockFactory.GetAuthService(authHolder);
            var exception = default(Exception);
            
            //Act
            try
            {
                await authService.SignUpAsync(userName, password, CancellationToken.None);
            }
            catch (Exception e)
            {
                exception = e;
            }
            
            //Assert
            exception.Should().BeNull();
            authHolder.GetLastSession().Should().NotBeNull();
            authHolder.GetLastSession().Token.Should().NotBeNullOrEmpty();
            authHolder.GetLastSession().ExpireAt.Should().BeAfter(DateTimeOffset.Now);
            authHolder.GetLastSession().CanBeUpdatedTill.Should().BeAfter(DateTimeOffset.Now);
        }
        
        [Fact]
        public async Task SignUp_ForUser_Registered()
        {
            // Arrange
            var userName = "JonieJi29";
            var password = "12345t67890";
            var authHolder = AuthorizationMockFactory.GetAuthHolder();
            var authService = AuthorizationMockFactory.GetAuthService(authHolder);
            var exception = default(Exception);
            
            //Act
            try
            {
                await authService.SignUpAsync(userName, password, CancellationToken.None);
            }
            catch (Exception e)
            {
                exception = e;
            }
            
            //Assert
            exception.Should().BeOfType<AuthorizationException>();
            authHolder.GetLastSession().Should().BeNull();
        }
        
        [Fact]
        public async Task RefreshAuth_ForUser_Registered_JustSignedIn()
        {
            // Arrange
            var userName = "JonieJi29";
            var password = "12345t67890";
            var authHolder = AuthorizationMockFactory.GetAuthHolder();
            var authService = AuthorizationMockFactory.GetAuthService(authHolder);
            var exception = default(Exception);
            
            //Act 1
            try
            {
                await authService.SignInAsync(userName, password, CancellationToken.None);
            }
            catch (Exception e)
            {
                exception = e;
            }
            
            //Assert 1
            exception.Should().BeNull();
            authHolder.GetLastSession().Should().NotBeNull();
            authHolder.GetLastSession().Token.Should().NotBeNullOrEmpty();
            authHolder.GetLastSession().ExpireAt.Should().BeAfter(DateTimeOffset.Now);
            authHolder.GetLastSession().CanBeUpdatedTill.Should().BeAfter(DateTimeOffset.Now);
            
            //Act 2
            try
            {
                await authService.UpdateSessionAsync(authHolder.GetLastSession().Token, CancellationToken.None);
            }
            catch (Exception e)
            {
                exception = e;
            }
            
            //Assert 2
            exception.Should().BeNull();
            authHolder.GetLastSession().Should().NotBeNull();
            authHolder.GetLastSession().Token.Should().NotBeNullOrEmpty();
            authHolder.GetLastSession().ExpireAt.Should().BeAfter(DateTimeOffset.Now);
            authHolder.GetLastSession().CanBeUpdatedTill.Should().BeAfter(DateTimeOffset.Now);
        }
    }
}