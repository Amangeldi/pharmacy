using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.BLL.DTO;
using Pharmacy.BLL.Interfaces;
using Pharmacy.DAL.Entities;

namespace Pharmacy.WEB.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        readonly IUserRegistrationService<AdministratorDTO, Administrator> userService;
        public AdminController(IUserRegistrationService<AdministratorDTO, Administrator> serv)
        {
            userService = serv;
        }
        [HttpGet("GetCurrentAdmin")]
        public IActionResult GetCurrentAdmin()
        {
            return Ok(userService.GetCurrent(User.Identity.Name));
        }
        [HttpPost]
        public async Task<ActionResult<AdministratorDTO>> Post(AdministratorDTO AdministratorDTO)
        {
            try
            {
                await userService.CreateUser(AdministratorDTO);
            }
            catch (Exception ex)
            {
                var er = new { error = ex.Message };
                return Ok(er);
            }
            return Ok();
        }
        [HttpGet("GetAdmin/{id}")]
        public ActionResult<AdministratorDTO> GetAdmin(string id)
        {
            try
            {
                AdministratorDTO user = userService.GetUser(id);
                return Ok(user);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpGet("GetAdmins")]
        public IEnumerable<AdministratorDTO> GetAdmins()
        {
            return userService.Getusers();
        }
        [HttpPut("EditAdmin")]
        public async Task<ActionResult<AdministratorDTO>> EditAdmin(AdministratorDTO AdministratorDTO)
        {
            try
            {
                await userService.UpdateUser(AdministratorDTO);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
            return Ok(AdministratorDTO);
        }
        [HttpDelete("DeleteAdmin")]
        public async Task<ActionResult<AdministratorDTO>> DeleteAdmin(string id)
        {
            try
            {
                await userService.DeleteUser(id);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
            return Ok();
        }

    }
}