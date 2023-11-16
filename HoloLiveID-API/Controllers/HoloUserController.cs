using HoloLiveID_API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace HoloLiveID_API.Controllers
{
    [ApiController]
    [Route("hololive")]
    public class HoloUserController : ControllerBase
    {
        private readonly HoloLiveIDContext _dbContext;
        private static bool _ensureCreated { get; set; } = false;

        public HoloUserController(HoloLiveIDContext dbContext)
        {
            _dbContext = dbContext;

            if (!_ensureCreated)
            {
                _dbContext.Database.EnsureCreated();
                _ensureCreated = true;
            }
        }

        [HttpGet("user")]
        [Produces("application/json")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _dbContext.HoloUsers.ToListAsync());
        }
    }
}
