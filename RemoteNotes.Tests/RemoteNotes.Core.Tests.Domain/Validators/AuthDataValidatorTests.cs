using System;
using System.IO;
using FluentAssertions;
using RemoteNotes.Rules;
using Xunit;

namespace RemoteNotes.Core.Tests.Domain.Validators
{
    public class AuthDataValidatorTests
    {
        [Theory]
        [InlineData(".user")]
        [InlineData("_user")]
        [InlineData("Jonie...Ji29")]
        [InlineData("Jonie...Ji29__")]
        [InlineData("JonieJi29__")]
        public void UserName_WrongStyle_Validation(string userName)
        {
            // Arrange
            var authValidator = new AuthorizationDataValidator();
            var exception = default(Exception);
            
            //Act
            try
            {
                authValidator.ValidateLogin(userName);
            }
            catch (Exception e)
            {
                exception = e;
            }
            
            //Assert
            exception.Should().BeOfType<InvalidDataException>();
        }
        
        [Theory]
        [InlineData("2user2user")]
        [InlineData("JonieJi29")]
        [InlineData("Jonie.Ji29")]
        [InlineData("Jonie_Ji29")]
        public void UserName_Pass_Validation(string userName)
        {
            // Arrange
            var authValidator = new AuthorizationDataValidator();
            var exception = default(Exception);
            
            //Act
            try
            {
                authValidator.ValidateLogin(userName);
            }
            catch (Exception e)
            {
                exception = e;
            }
            
            //Assert
            exception.Should().BeNull();
        }

        [Fact]
        public void UserName_Null_Check()
        {
            // Arrange
            var authValidator = new AuthorizationDataValidator();
            var exception = default(Exception);
            
            //Act
            try
            {
                authValidator.ValidateLogin(null);
            }
            catch (Exception e)
            {
                exception = e;
            }
            
            //Assert
            exception.Should().BeOfType<ArgumentNullException>();
        }

        [Theory]
        [InlineData(".pass")]
        [InlineData("233")]
        [InlineData("34256777654324")]
        [InlineData("JJi29_")]
        public void Password_WrongStyle_Validation(string password)
        {
            // Arrange
            var authValidator = new AuthorizationDataValidator();
            var exception = default(Exception);
            
            //Act
            try
            {
                authValidator.ValidatePassword(password);
            }
            catch (Exception e)
            {
                exception = e;
            }
            
            //Assert
            exception.Should().BeOfType<InvalidDataException>();
        }

        [Theory]
        [InlineData(".pass")]
        [InlineData("233")]
        [InlineData("34256777654324")]
        [InlineData("JJi29_")]
        public void Password_Pass_Validation(string password)
        {
            // Arrange
            var authValidator = new AuthorizationDataValidator();
            var exception = default(Exception);
            
            //Act
            try
            {
                authValidator.ValidatePassword(password);
            }
            catch (Exception e)
            {
                exception = e;
            }
            
            //Assert
            exception.Should().BeOfType<InvalidDataException>();
        }

        [Fact]
        public void Password_Null_Check()
        {
            // Arrange
            var authValidator = new AuthorizationDataValidator();
            var exception = default(Exception);
            
            //Act
            try
            {
                authValidator.ValidatePassword(null);
            }
            catch (Exception e)
            {
                exception = e;
            }
            
            //Assert
            exception.Should().BeOfType<ArgumentNullException>();
        }
    }
}