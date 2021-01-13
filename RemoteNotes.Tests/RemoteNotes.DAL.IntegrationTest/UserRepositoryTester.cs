using System;
using NUnit.Framework;
using RemoteNotes.BL.Security.Password;
using RemoteNotes.DAL.Contract;
using RemoteNotes.DAL.IntegrationTest.Base;
using RemoteNotes.Domain.Entity;

namespace RemoteNotes.DAL.IntegrationTest
{
    public class UserRepositoryTester : RepositoryTesterBase<IUserRepository, User>
    {
        [Test]
        public async void GetByLoginTest()
        {
            // Arrange
            User element = this.BuildObject();
            repository.Add(element, true);

            // Act
            User elementAfter = await repository.GetUserAsync(element.Username);

            // Assert
            Assert.IsFalse(elementAfter.Id.Equals(Guid.Empty));
        }

        protected override void ModifyProperties(User element)
        {
            element.Username = "user";
            element.EncryptedPassword = GetPasswordCryptor().ToPassword("newPassword");
        }

        protected override User BuildObject()
        {
           return new User() {Username = "login", EncryptedPassword = GetPasswordCryptor().ToPassword("newPassword")};
        }

        private PasswordCryptor GetPasswordCryptor()
        {
            return new PasswordCryptor();
        }
    }
}
