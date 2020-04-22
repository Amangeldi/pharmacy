using Pharmacy.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.BLL.Interfaces
{
    public interface IManufacturerService
    {
        Task<ManufacturerDTO> CreateManufacturer(ManufacturerDTO ManufacturerDTO);
        ManufacturerDTO GetManufacturer(int ManufacturerId);
        IEnumerable<ManufacturerDTO> GetManufacturers();
        bool DeleteManufacturer(int ManufacturerId);
        IEnumerable<ManufacturerDTO> GetManufacturersOfCountry(int CountryId);
    }
}
