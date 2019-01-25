namespace DiariumAPI
{
    using System;
    using System.Linq;

    using AutoFixture;
    using AutoFixture.Kernel;

    using DiariumAPI.Controllers;
    using DiariumAPI.Entities;
    using DiariumAPI.Models;
    using DiariumAPI.Storage;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    internal class DocumentsControllerTests
    {
        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
            _fixture.Customizations.Add(new TypeRelay(typeof(IDocumentMetaData), typeof(DocumentMetaData)));
            _fixture.Customizations.Add(new TypeRelay(typeof(IDocument), typeof(Document)));
        }

        [TearDown]
        public void TearDown()
        {
            _fixture = null;
        }

        private Fixture _fixture;

        [Test]
        public void GetEmptyList()
        {
            // Arrange
            var documents = _fixture.CreateMany<IDocument>(0);
            var repository = new Mock<IRepository<IDocument, Guid>>();
            repository.Setup(r => r.Get()).Returns(documents);
            var controller = new DocumentsController(repository.Object);

            // Act
            var actual = controller.Get();

            // Assert
            Assert.AreEqual(0, actual.Count());
            repository.Verify(r => r.Get(), Times.Once);
        }

        [Test]
        public void GetPopulatedList()
        {
            // Arrange
            var documents = _fixture.CreateMany<IDocument>(9);
            var repository = new Mock<IRepository<IDocument, Guid>>();
            repository.Setup(r => r.Get()).Returns(documents);
            var controller = new DocumentsController(repository.Object);

            // Act
            var actual = controller.Get().ToList();

            // Assert
            Assert.AreEqual(9, actual.Count());
            Assert.IsTrue(documents.SequenceEqual(actual));
            repository.Verify(r => r.Get(), Times.Once);
        }

        [Test]
        public void GetExistingItem()
        {
            // Arrange
            var documents = _fixture.CreateMany<IDocument>(9).ToList();
            var candidate = documents[5];
            var repository = new Mock<IRepository<IDocument, Guid>>();
            repository.Setup(r => r.Get(It.IsAny<Guid>())).Returns((Guid g) => documents.FirstOrDefault(d => d.Id == g));
            var controller = new DocumentsController(repository.Object);

            // Act
            var actual = controller.Get(candidate.Id);

            // Assert
            Assert.AreEqual(candidate, actual);
            repository.Verify(r => r.Get(It.IsAny<Guid>()), Times.Once);
        }
    }
}
