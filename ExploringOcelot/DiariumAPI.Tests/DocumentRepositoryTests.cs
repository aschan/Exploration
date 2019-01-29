namespace DiariumAPI
{
    using System.Linq;

    using AutoFixture;
    using AutoFixture.Kernel;

    using DiariumAPI.Entities;
    using DiariumAPI.Models;
    using DiariumAPI.Storage;

    using NUnit.Framework;

    [TestFixture]
    public class DocumentRepositoryTests
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

        // Ordered tests because of the persistent storage implementation in the repository

        [Test]
        [Order(1)]
        public void AddItems()
        {
            //Arrange
            var document = _fixture.Create<IDocument>();
            var repository = new DocumentRepository();

            // Act
            var actual = repository.Add(document);

            // Assert
            Assert.AreEqual(document, actual);
        }

        [Test]
        [Order(4)]
        public void DeleteExistingItem()
        {
            // Arrange
            var repository = new DocumentRepository();
            var document = repository.Get().First();

            // Act
            repository.Delete(document);
            var documents = repository.Get();

            // Assert
            Assert.AreEqual(0, documents.Count());
        }

        [Test]
        [Order(5)]
        public void DeleteNoneExistingItem()
        {
            // Arrange
            var repository = new DocumentRepository();
            var originalDocuments = _fixture.CreateMany<IDocument>(9).Select(repository.Add);
            var document = repository.Get().First();

            // Act
            repository.Delete(document);
            var documents = repository.Get();

            // Assert
            Assert.AreEqual(originalDocuments.Count(), documents.Count());
        }

        [Test]
        [Order(2)]
        public void GetAllItemsAfterAdd()
        {
            // Arrange
            var repository = new DocumentRepository();

            // Act
            var documents = repository.Get();

            // Assert
            Assert.AreEqual(1, documents.Count());
        }

        [Test]
        [Order(0)]
        public void GetAllItemsFromEmptyRepository()
        {
            // Arrange
            var repository = new DocumentRepository();

            // Act
            var documents = repository.Get();

            // Assert
            Assert.AreEqual(0, documents.Count());
        }

        [Test]
        [Order(3)]
        public void UpdateExistingItem()
        {
            // Arrange
            var repository = new DocumentRepository();
            var original = repository.Get().First();
            var document = _fixture.Create<IDocument>();
            document.Id = original.Id;

            // Act
            repository.Update(document);
            var documents = repository.Get();

            // Assert
            Assert.AreEqual(1, documents.Count());
            Assert.AreNotEqual(original, document);
            Assert.AreEqual(document, documents.FirstOrDefault());
        }
    }
}
