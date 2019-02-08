namespace ParticipatePlace.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using ParticipatePlace.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        // POST: api/DocumentSearch
        [HttpPost]
        public IResult Post([FromBody] IQuery query)
        {
            return null;
        }
    }
}
