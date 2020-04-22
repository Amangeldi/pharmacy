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
    public class CountryController : ControllerBase
    {
        readonly ICountryService serv;
        public CountryController(ICountryService service)
        {
            serv = service;
        }
        [HttpPost("CreateCountry")]
        public async Task<ActionResult<CountryRegistrationDTO>> CreateCountry(CountryRegistrationDTO CountryRegistrationDTO)
        {
            try
            {
                await serv.CreateCountry(CountryRegistrationDTO);
            }
            catch (Exception ex)
            {
                var err = new { error = ex.Message };
                return Ok(err);
            }
            return Ok(CountryRegistrationDTO);
        }
        [HttpGet("GetCountry")]
        public ActionResult<CountryLangDTO> GetCountry(int CountryId, int LangId)
        {
            try
            {
                CountryLangDTO country = serv.GetCountry(CountryId, LangId);
                return Ok(country);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpGet("GetCountries/{LangId}")]
        public IEnumerable<CountryLangDTO> GetCountries(int LangId)
        {
            return serv.GetCountries(LangId);
        }
        [HttpDelete("DeleteCountry")]
        public ActionResult DeleteCountry(int CountryId)
        {
            if (serv.DeleteCountry(CountryId))
            {
                return Ok("Страна " + CountryId + " удалена");
            }
            var err = new { error = "Страна " + CountryId + " не была удалена" };
            return Ok(err);
        }
    }
}