using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using MerryChristmas.Models;
using MerryChristmas.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MerryChristmas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishesController : ControllerBase
    {
        private readonly IMongoCollection<Wish> _wishes;

        public WishesController(IMongoClient client, IOptions<MongoDbSettings> settings)
        {
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _wishes = database.GetCollection<Wish>("wishes");
        }

        [HttpPost]
        public async Task<IActionResult> PostWish([FromBody] Wish wish)
        {
            wish.Date = DateTime.UtcNow;
            await _wishes.InsertOneAsync(wish);
            return CreatedAtAction(nameof(GetWishes), new { id = wish.Id }, wish);
        }

        [HttpGet]
        public async Task<ActionResult<List<Wish>>> GetWishes()
        {
            var wishes = await _wishes.Find(w => true).ToListAsync();
            return Ok(wishes);
        }
    }
}