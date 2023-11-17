using HoloLiveID_API.Data;
using HoloLiveID_API.Helper;
using HoloLiveID_API.Input;
using HoloLiveID_API.Model;
using HoloLiveID_API.Output;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace HoloLiveID_API.Controllers
{
    [ApiController]
    [Route("hololive")]
    public class HoloUserController : ControllerBase
    {
        private HoloUserHelper holoUserHelper;
        public HoloUserController(HoloUserHelper holoUserHelper)
        {
            this.holoUserHelper = holoUserHelper;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var objJSON = new ListHoloUserOutput();
                objJSON.payload = holoUserHelper.GetAllUsers();
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var objJSON = new HoloUserOutput();
                objJSON.payload = holoUserHelper.GetUser(id);
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }   
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> Create([FromBody] HoloUser data)
        {
            try
            {
                var objJSON = holoUserHelper.CreateUser(data);
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> Update(string id, [FromBody] HoloUserInput data)
        {
            try
            {
                var objJSON = holoUserHelper.UpdateUser(id, data);
                return new OkObjectResult(objJSON);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> Delete(string id)
        {
            var objJSON = holoUserHelper.DeleteUser(id);
            return new OkObjectResult(objJSON);
        }
    }
}
