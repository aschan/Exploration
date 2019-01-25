namespace DiariumAPI.Controllers
{
    using System;
    using System.Collections.Generic;

    using DiariumAPI.Models;
    using DiariumAPI.Storage;

    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IRepository<IDocument, Guid> _documentRepository;

        public DocumentsController(IRepository<IDocument, Guid> documentRepository)
        {
            _documentRepository = documentRepository;
        }

        [HttpGet]
        public IEnumerable<IDocument> Get()
        {
            return _documentRepository.Get();
        }

        [HttpGet("{id}", Name = "Get")]
        public IDocument Get(Guid id)
        {
            return _documentRepository.Get(id);
        }

        [HttpPost]
        public void Post([FromBody] IDocument value)
        {
            // TODO: Add validation
            _documentRepository.Add(value);
        }

        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] IDocument value)
        {
            // TODO: Add validation
            // TODO: Check if exists
            _documentRepository.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _documentRepository.Delete(id);
        }
    }
}
