using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using RemoteNotes.DAL.Contract;
using RemoteNotes.DAL.IntegrationTest.Context;
using RemoteNotes.DAL.IntegrationTest.Extensions;
using RemoteNotes.Domain.Entity;

namespace RemoteNotes.DAL.IntegrationTest.Base
{
    [TestFixture]
    public abstract class RepositoryTesterBase<I, T> where I : ICRUDRepository<T> where T : IIdentificable
    {
        /// <summary>
        /// The dal manager.
        /// </summary>
        protected I repository = TestingContext.GetUnitOfWork().GetRepository<I>();

        /// <summary>
        /// Set up test method which gets executed before each test.
        /// </summary>
        [SetUp]
        public virtual void SetUp()
        {
            this.repository.Clear();
        }

        /// <summary>
        /// Basic story of add object function.
        /// </summary>
        [Test]
        public virtual void AddBasicTest()
        {
            // Arrange
            T element = this.BuildObject();

            // Act
            this.repository.Add(element, true);

            // Assert
            Assert.IsFalse(element.Id.Equals(Guid.Empty));
        }

        /// <summary>
        /// Basic story of get object function.
        /// </summary>
        [Test]
        public virtual void GetBasicTest()
        {
            // Arrange
            T element = this.BuildObject();

            this.repository.Add(element, true);

            // Act
            T elementAfter = this.repository.GetById(element.Id);

            // Assert
            Assert.IsFalse(elementAfter.Id.Equals(Guid.Empty));
        }

        [Test]
        public async virtual Task GetByIdAsyncTest()
        {
            // Arrange
            T element = this.BuildObject();

            this.repository.Add(element, true);

            // Act
            T elementAfter = await this.repository.GetByIdAsync(element.Id);

            // Assert
            Assert.IsFalse(elementAfter.Id.Equals(Guid.Empty));
        }

        /// <summary>
        /// An alternative story of getting object: object was not found.
        /// </summary>
        [Test]
        public virtual void GetNotFoundTest()
        {
            try
            {
                this.repository.GetById(Guid.Empty);
                Assert.Fail("Found an object that should not exist.");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(true);
            }
        }

        /// <summary>
        /// The get collection test.
        /// </summary>
        [Test]
        public virtual void GetCollectionTest()
        {
            // Arrange
            T element = this.BuildObject();

            this.repository.Add(element, true);

            // Act
            List<T> collection = this.repository.GetCollection();

            // Assert
            Assert.That(collection.Count, Is.GreaterThanOrEqualTo(1));
        }


        /// <summary>
        /// Basic story of update object function.
        /// </summary>
        [Test]
        public virtual void UpdateBasicTest()
        {
            // Arrange
            T element = this.BuildObject();

            // Add object
            this.repository.Add(element, true);

            // TODO: Update the object. Fill up fields with testing information using acceptance tests.
            this.ModifyProperties(element);

            // Act
            this.repository.Update(element, true);

            T elementAfter = this.repository.GetById(element.Id);

            // Assert
            element.FieldValuesAreEqual(elementAfter);
        }

        /// <summary>
        /// Basic story of delete object function.
        /// </summary>
        [Test]
        public virtual void DeleteBasicTest()
        {
            // Arrange
            T element = this.BuildObject();

            this.repository.Add(element, true);
            Assert.IsFalse(element.Id.Equals(Guid.Empty));

            // Act
            this.repository.Delete(element.Id, true);
        }

        /// <summary>
        /// The clear all.
        /// </summary>
        [TearDown]
        public virtual void TearDown()
        {
            this.repository.Clear(true);
        }

        /// <summary>
        /// The modify properties.
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        protected abstract void ModifyProperties(T element);

        /// <summary>
        /// The build object.
        /// </summary>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        protected abstract T BuildObject();
    }
}
