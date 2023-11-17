using HoloLiveID_API.Data;
using HoloLiveID_API.Input;
using HoloLiveID_API.Model;
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

        [HttpGet("user/{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await _dbContext.HoloUsers
                .Where(x => x.HoloId.Equals(id, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefaultAsync();

            if (user != null)
            {
                return Ok(user);
            }

            return NotFound("User is not exist");
        }

        [HttpPost("user")]
        [Produces("application/json")]
        public async Task<IActionResult> CreateUser([FromBody] HoloUser data)
        {
            var newHoloUser = new HoloUser
            {
                HoloId = data.HoloId,
                Name = data.Name,
                Gen = data.Gen,
                Description = data.Description,
                Birthdate = data.Birthdate,
                Height = data.Height,
                Zodiac = data.Zodiac,
            };

            _dbContext.HoloUsers.Add(newHoloUser);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPatch("user/{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] HoloUserInput data)
        {
            try
            {
                var user = await _dbContext.HoloUsers
                    .Where(x => x.HoloId.Equals(id, StringComparison.OrdinalIgnoreCase))
                    .FirstOrDefaultAsync();

                if (user != null)
                {
                    user.Name = data.Name ?? user.Name;
                    user.Gen = data.Gen ?? user.Gen;
                    user.Description = data.Description ?? user.Description;
                    user.Birthdate = data.Birthdate ?? user.Birthdate;
                    user.Height = data.Height ?? user.Height;
                    user.Zodiac = data.Zodiac ?? user.Zodiac;

                    _dbContext.HoloUsers.Update(user);
                    await _dbContext.SaveChangesAsync();

                    return Ok();
                }

                return NotFound("User is not exist");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.InnerException, stackTrace = ex.StackTrace });
            }
        }

        [HttpDelete("user/{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _dbContext.HoloUsers
                .Where(x => x.HoloId.Equals(id, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefaultAsync();

            if (user != null)
            {
                _dbContext.HoloUsers.Remove(user);
                await _dbContext.SaveChangesAsync();
                return Ok();
            }

            return NotFound("User is not exist");
        }
    }
}
