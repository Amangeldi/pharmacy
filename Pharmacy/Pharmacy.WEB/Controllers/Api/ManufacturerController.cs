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
    public class ManufacturerController : ControllerBase
    {
        readonly IManufacturerService serv;
        public ManufacturerController(IManufacturerService service)
        {
            serv = service;
        }
        [HttpPost("CreateManufacturer")]
        public async Task<ActionResult<ManufacturerDTO>> CreateManufacturer(ManufacturerDTO ManufacturerDTO)
        {
            try
            {
                await serv.CreateManufacturer(ManufacturerDTO);
            }
            catch (Exception ex)
            {
                var err = new { error = ex.Message };
                return Ok(err);
            }
            return Ok(ManufacturerDTO);
        }
        [HttpGet("GetManufacturer/{ManufacturerId}")]
        public ActionResult<ManufacturerDTO> GetManufacturer(int ManufacturerId)
        {
            try
            {
                ManufacturerDTO manufacturer = serv.GetManufacturer(ManufacturerId);
                return Ok(manufacturer);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpGet("GetManufacturers/")]
        public IEnumerable<ManufacturerDTO> GetManufacturers()
        {
            return serv.GetManufacturers();
        }
        [HttpDelete("DeleteManufacturer/{ManufacturerId}")]
        public ActionResult DeleteManufacturer(int ManufacturerId)
        {
            if (serv.DeleteManufacturer(ManufacturerId))
            {
                return Ok("Проиводитель " + ManufacturerId + " удален");
            }
            var err = new { error = "Проиводитель " + ManufacturerId + " не был удален" };
            return Ok(err);
        }
        [HttpGet("GetManufacturersOfCountry/{countryId}")]
        public IEnumerable<ManufacturerDTO> GetManufacturersOfCountry(int countryId)
        {
            return serv.GetManufacturersOfCountry(countryId);
        }
    }
}