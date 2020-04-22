using AutoMapper;
using Pharmacy.BLL.DTO;
using Pharmacy.BLL.Interfaces;
using Pharmacy.DAL.EF;
using Pharmacy.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.BLL.Services
{
    public class ManufacturerService : IManufacturerService
    {
        readonly ApiContext db;
        public ManufacturerService(ApiContext context)
        {
            db = context;
        }
        public async Task<ManufacturerDTO> CreateManufacturer(ManufacturerDTO ManufacturerDTO)
        {
            Manufacturer manufacturer = new Manufacturer { CountryId = ManufacturerDTO.CountryId, Name = ManufacturerDTO.Name };
            await db.Manufacturers.AddAsync(manufacturer);
            await db.SaveChangesAsync();
            return ManufacturerDTO;
        }

        public bool DeleteManufacturer(int ManufacturerId)
        {
            try
            {
                Manufacturer manufacturer = db.Manufacturers.Find(ManufacturerId);
                db.Manufacturers.Remove(manufacturer);
                db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public ManufacturerDTO GetManufacturer(int ManufacturerId)
        {
            Manufacturer manufacturer = db.Manufacturers.Find(ManufacturerId);
            return new ManufacturerDTO { CountryId = manufacturer.CountryId, Id = manufacturer.Id, Name = manufacturer.Name };
        }

        public IEnumerable<ManufacturerDTO> GetManufacturers()
        {
            IEnumerable<Manufacturer> manufacturers = db.Manufacturers;
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Manufacturer, ManufacturerDTO>()).CreateMapper();
            IEnumerable<ManufacturerDTO> manufacturerDTOs = mapper.Map<IEnumerable<Manufacturer>, IEnumerable<ManufacturerDTO>>(manufacturers);
            return manufacturerDTOs;
        }

        public IEnumerable<ManufacturerDTO> GetManufacturersOfCountry(int CountryId)
        {
            IEnumerable<ManufacturerDTO> manufacturers = GetManufacturers().Where(p=>p.CountryId==CountryId);
            return manufacturers;
        }
    }
}
