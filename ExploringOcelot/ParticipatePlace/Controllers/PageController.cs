namespace ParticipatePlace.Controllers
{
    using System;
    using System.Collections.Generic;

    using Framework.Storage;

    using Microsoft.AspNetCore.Mvc;

    using ParticipatePlace.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class PageController : Controller
    {
        private readonly IRepository<IPage, Guid> _pageRepository;

        public PageController(IRepository<IPage, Guid> pageRepository)
        {
            _pageRepository = pageRepository;
        }

        [HttpGet]
        public IEnumerable<IPage> Get()
        {
            return _pageRepository.Get();
        }

        [HttpGet("{id}", Name = "Get")]
        public IPage Get(Guid id)
        {
            return _pageRepository.Get(id);
        }

        [HttpPost]
        public void Post([FromBody] IPage value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (!value.Validate(out var propertyNames))
            {
                throw new ArgumentException($"The following properties are invalid: {string.Join(", ", propertyNames)}", nameof(value));
            }

            _pageRepository.Add(value);
        }

        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] IPage value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (!value.Validate(out var propertyNames))
            {
                throw new ArgumentException($"The following properties are invalid: {string.Join(", ", propertyNames)}", nameof(value));
            }

            var page = _pageRepository.Get(id);
            page.Title = value.Title;
            page.Url = value.Url;
            page.Created = value.Created;
            page.Modified = value.Modified;
            page.Content = value.Content;
            _pageRepository.Update(value);
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _pageRepository.Delete(id);
        }
    }
}
