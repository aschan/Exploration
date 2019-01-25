namespace DiariumAPI.Controllers
{
    using DiariumAPI.Models;

    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class DocumentSearchController : ControllerBase
    {
        // POST: api/DocumentSearch
        [HttpPost]
        public IResult Post([FromBody] IQuery query)
        {
            return null;
        }
    }
}
