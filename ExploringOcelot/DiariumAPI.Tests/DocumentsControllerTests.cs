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

        [Test]
        public void GetNoneExistingItem()
        {
            // Arrange
            var documents = _fixture.CreateMany<IDocument>(9).ToList();
            var repository = new Mock<IRepository<IDocument, Guid>>();
            repository.Setup(r => r.Get(It.IsAny<Guid>())).Returns((Guid g) => documents.FirstOrDefault(d => d.Id == g));
            var controller = new DocumentsController(repository.Object);
            var id = Guid.NewGuid();

            // Act
            var actual = controller.Get(id);

            // Assert
            Assert.IsNull(actual);
            repository.Verify(r => r.Get(It.Is<Guid>(g => g == id)), Times.Once);
        }

        [Test]
        public void AddItemToEmptyList()
        {
            // Arrange
            var documents = _fixture.CreateMany<IDocument>(0).ToList();
            var document = _fixture.Create<IDocument>();
            var repository = new Mock<IRepository<IDocument, Guid>>();
            repository.Setup(r => r.Add(It.IsAny<IDocument>())).Callback((IDocument d) => documents.Add(d));
            var controller = new DocumentsController(repository.Object);

            // Act
            controller.Post(document);

            // Assert
            Assert.AreEqual(1, documents.Count);
            repository.Verify(r => r.Add(It.Is<IDocument>(d => d.Id == document.Id)), Times.Once);
        }

        [Test]
        public void AddItemToPopulatedList()
        {
            // Arrange
            var documents = _fixture.CreateMany<IDocument>(9).ToList();
            var document = _fixture.Create<IDocument>();
            var repository = new Mock<IRepository<IDocument, Guid>>();
            repository.Setup(r => r.Add(It.IsAny<IDocument>())).Callback((IDocument d) => documents.Add(d));
            var controller = new DocumentsController(repository.Object);

            // Act
            controller.Post(document);

            // Assert
            Assert.AreEqual(10, documents.Count);
            repository.Verify(r => r.Add(It.Is<IDocument>(d => d.Id == document.Id)), Times.Once);
        }

        [Test]
        public void UpdateItem()
        {
            // Arrange
            var documents = _fixture.CreateMany<IDocument>(9).ToList();
            var original = documents[5];
            var document = _fixture.Create<IDocument>();
            document.Id = original.Id;
            var repository = new Mock<IRepository<IDocument, Guid>>();
            repository.Setup(r => r.Get(It.IsAny<Guid>())).Returns((Guid g) => documents.FirstOrDefault(d => d.Id == g));
            repository.Setup(r => r.Update(It.IsAny<IDocument>())).Callback((IDocument d) => documents[documents.FindIndex(r => r.Id == d.Id)] = d);
            var controller = new DocumentsController(repository.Object);

            // Act
            controller.Put(document.Id, document);
            var actual = documents[5];

            // Assert
            repository.Verify(r => r.Update(It.Is<IDocument>(d => d.Id == document.Id)), Times.Once);
            Assert.AreEqual(document, actual);
            
        }

        [Test]
        public void DeleteExistingItem()
        {
            // Arrange
            var documents = _fixture.CreateMany<IDocument>(9).ToList();
            var document = documents[5];
            var repository = new Mock<IRepository<IDocument, Guid>>();
            repository.Setup(r => r.Get(It.IsAny<Guid>())).Returns((Guid g) => documents.FirstOrDefault(d => d.Id == g));
            repository.Setup(r => r.Delete(It.IsAny<Guid>()))
                      .Callback((Guid g) =>
                      {
                          if (documents.Any(d => d.Id == g))
                          {
                              documents.Remove(documents.FirstOrDefault(d => d.Id == g));
                          }
                      });
            var controller = new DocumentsController(repository.Object);

            // Act
            controller.Delete(document.Id);

            // Assert
            Assert.AreEqual(8,documents.Count);
            repository.Verify(r => r.Delete(It.Is<Guid>(g => g == document.Id)), Times.Once);
        }

        [Test]
        public void DeleteNoneExistingItem()
        {
            // Arrange
            var documents = _fixture.CreateMany<IDocument>(9).ToList();
            var document = _fixture.Create<IDocument>();
            var repository = new Mock<IRepository<IDocument, Guid>>();
            repository.Setup(r => r.Get(It.IsAny<Guid>())).Returns((Guid g) => documents.FirstOrDefault(d => d.Id == g));
            repository.Setup(r => r.Delete(It.IsAny<Guid>()))
                      .Callback((Guid g) =>
                      {
                          if (documents.Any(d => d.Id == g))
                          {
                              documents.Remove(documents.FirstOrDefault(d => d.Id == g));
                          }
                      });
            var controller = new DocumentsController(repository.Object);
            
            // Act
            controller.Delete(document.Id);

            // Assert
            Assert.AreEqual(9,documents.Count);
            repository.Verify(r => r.Delete(It.Is<Guid>(g => g == document.Id)), Times.Once);
        }
    }
}
