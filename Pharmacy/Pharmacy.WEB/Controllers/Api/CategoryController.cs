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
                var err = new { error = ex.Message };
                return Ok(err);
            }
            return Ok(CategoryRegistrationDTO);
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
        [HttpGet("GetCategories/{LangId}")]
        public IEnumerable<CategoryLangLink> GetCategories(int LangId)
        {
            return serv.GetCategories(LangId);
        }
        [HttpDelete("DeleteCategory")]
        public ActionResult DeleteCategory(int CategoryId)
        {
            if(serv.DeleteCategory(CategoryId))
            {
                return Ok("Категория "+CategoryId+" удалена");
            }
            var err = new { error = "Категория " + CategoryId + " не была удалена" };
            return Ok(err);
        }
    }

}