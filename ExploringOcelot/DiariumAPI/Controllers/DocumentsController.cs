using System;
using System.Collections.ObjectModel;
using DiariumAPI.Models;

namespace DiariumAPI.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        // GET: api/Documents
        [HttpGet]
        public IEnumerable<IDocument> Get()
        {
            return new Collection<IDocument>();
        }

        // GET: api/Documents/5
        [HttpGet("{id}", Name = "Get")]
        public IDocument Get(Guid id)
        {
            return (IDocument)null;
        }

        // POST: api/Documents
        [HttpPost]
        public void Post([FromBody] IDocument value)
        {
        }

        // PUT: api/Documents/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] IDocument value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
        }
    }
}