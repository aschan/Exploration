namespace DiariumAPI
{
    using System.Linq;

    using DiariumAPI.Storage;

    using NUnit.Framework;

    [TestFixture]
    public class DocumentRepositoryTests
    {
        [Test]
        public void GetAllItems()
        {
            var repository = new DocumentRepository();
            var documents = repository.Get();
            Assert.AreEqual(0, documents.Count());
        }
    }
}
