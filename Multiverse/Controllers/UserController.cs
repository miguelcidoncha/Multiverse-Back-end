using Data;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Cors;
using Multiverse.IServices;
using Multiverse.Services;

namespace Multiverse.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost(Name = "InsertUsers")]
        public IActionResult Post([FromBody] UserItem userItem, [FromQuery] string userUserName, [FromQuery] string userPassword)
        {
            var selectedUser = _userService.AuthenticateUser(userUserName, userPassword);

            if (selectedUser != null)
            {
                int userId = _userService.InsertUser(userItem);
                return Ok(userItem);
            }
            else
            {
                return Unauthorized("El Usuario no Existe");
            }
        }

        [HttpPut("{userId}", Name = "UpdateUser")]
        public IActionResult UpdateUser(int userId, [FromBody] UserItem updatedUser, [FromQuery] string userUserName, [FromQuery] string userPassword)
        {
            var selectedUser = _userService.AuthenticateUser(userUserName, userPassword);

            if (selectedUser != null)
            {
                var user = _userService.GetUserById(userId);

                if (user != null)
                {
                    user.UserName = updatedUser.UserName;
                    user.IdRoll = updatedUser.IdRoll;
                    user.Password = updatedUser.Password;
                    user.Email = updatedUser.Email;

                    _userService.UpdateUser(user);

                    return Ok("El Usuario se ha modificado correctamente");
                }
                else
                {
                    return NotFound("No se ha encontrado el Usuario");
                }
            }
            else
            {
                return Unauthorized("El usuario no está autorizado");
            }
        }

        [HttpDelete("{userId}", Name = "DeleteUser")]
        public IActionResult Delete(int userId, [FromQuery] string userUserName, [FromQuery] string userPassword)
        {
            var selectedUser = _userService.AuthenticateUser(userUserName, userPassword);

            if (selectedUser != null)
            {
                var user = _userService.GetUserById(userId);

                if (user != null)
                {
                    _userService.DeleteUser(userId);

                    return Ok("El usuario se ha eliminado");
                }
                else
                {
                    return NotFound("No se ha encontrado el Usuario");
                }
            }
            else
            {
                return Unauthorized("El usuario no está autorizado");
            }
        }
    }

}
