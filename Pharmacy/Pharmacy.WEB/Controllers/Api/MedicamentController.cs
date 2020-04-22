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
    public class MedicamentController : ControllerBase
    {
        readonly IMedicamentService serv;
        public MedicamentController(IMedicamentService service)
        {
            serv = service;
        }
        [HttpPost("CreateMedicament")]
        public async Task<ActionResult<MedicamentRegistrationDTO>> CreateMedicament(MedicamentRegistrationDTO MedicamentRegistrationDTO)
        {
            try
            {
                await serv.CreateMedicament(MedicamentRegistrationDTO);
            }
            catch (Exception ex)
            {
                var err = new { error = ex.Message };
                return Ok(err);
            }
            return Ok(MedicamentRegistrationDTO);
        }
        [HttpGet("GetMedicament")]
        public ActionResult<MedicamentLangDTO> GetMedicament(int MedicamentId, int LangId)
        {
            try
            {
                MedicamentLangDTO country = serv.GetMedicament(MedicamentId, LangId);
                return Ok(country);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpGet("GetMedicaments/{LangId}")]
        public IEnumerable<MedicamentLangDTO> GetMedicaments(int LangId)
        {
            return serv.GetMedicaments(LangId);
        }
        [HttpDelete("DeleteMedicament")]
        public ActionResult DeleteMedicament(int MedicamentId)
        {
            if (serv.DeleteMedicament(MedicamentId))
            {
                return Ok("Лекарственный препарат " + MedicamentId + " удален");
            }
            var err = new { error = "Лекарственный препарат " + MedicamentId + " не был удален" };
            return Ok(err);
        }
        [HttpGet("GetMedicamentsOfCountry")]
        public IEnumerable<MedicamentLangDTO> GetMedicamentsOfCountry(int LangId, int CountryId)
        {
            return serv.GetMedicamentsOfCountry(LangId, CountryId);
        }
        [HttpGet("GetMedicamentsOfManufacturer")]
        public IEnumerable<MedicamentLangDTO> GetMedicamentsOfManufacturer(int LangId, int ManufacturerId)
        {
            return serv.GetMedicamentsOfManufacturer(LangId, ManufacturerId);
        }
    }
}