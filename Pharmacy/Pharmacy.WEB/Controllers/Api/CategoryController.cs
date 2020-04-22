using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.BLL.DTO;
using Pharmacy.BLL.Interfaces;

namespace Pharmacy.WEB.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        readonly ICategoryService serv;
        public CategoryController(ICategoryService service)
        {
            serv = service;
        }
        [HttpPost("CreateCategory")]
        public async Task<ActionResult<CategoryRegistrationDTO>> CreateCategory(CategoryRegistrationDTO CategoryRegistrationDTO)
        {
            try
            {
                await serv.CreateCategory(CategoryRegistrationDTO);
            }
            catch (Exception ex)
            {
                var er = new { error = ex.Message };
                return Ok(er);
            }
            return Ok();
        }
        [HttpGet("GetCategory")]
        public ActionResult<CategoryLangDTO> GetCategory(int CategoryId, int LangId)
        {
            try
            {
                CategoryLangDTO category = serv.GetCategory(CategoryId,LangId);
                return Ok(category);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
    }

}