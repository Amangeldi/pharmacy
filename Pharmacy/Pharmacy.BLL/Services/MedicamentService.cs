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
    public class MedicamentService : IMedicamentService
    {
        readonly ApiContext db;
        public MedicamentService(ApiContext context)
        {
            db = context;
        }
        public async Task<MedicamentRegistrationDTO> CreateMedicament(MedicamentRegistrationDTO MedicamentRegistrationDTO)
        {
            Medicament medicament = new Medicament { CategoryId = MedicamentRegistrationDTO.CategoryId, ManufacturerId = MedicamentRegistrationDTO.ManufacturerId };
            await db.Medicaments.AddAsync(medicament);
            List<Lang> langs = db.Langs.ToList();
            MedicamentLangLink medicamentLangLinkENG = new MedicamentLangLink
            {
                Description = MedicamentRegistrationDTO.DescriptionENG,
                MedicamentName = MedicamentRegistrationDTO.MedicamentNameENG,
                Medicament = medicament,
                Lang = langs.Where(p => p.Name == "ENG").First()
            };
            MedicamentLangLink medicamentLangLinkRUS = new MedicamentLangLink
            {
                Description = MedicamentRegistrationDTO.DescriptionRUS,
                MedicamentName = MedicamentRegistrationDTO.MedicamentNameRUS,
                Medicament = medicament,
                Lang = langs.Where(p => p.Name == "RUS").First()
            };
            MedicamentLangLink medicamentLangLinkTKM = new MedicamentLangLink
            {
                Description = MedicamentRegistrationDTO.DescriptionTKM,
                MedicamentName = MedicamentRegistrationDTO.MedicamentNameTKM,
                Medicament = medicament,
                Lang = langs.Where(p => p.Name == "TKM").First()
            };
            await db.MedicamentLangLinks.AddRangeAsync(medicamentLangLinkENG, medicamentLangLinkRUS, medicamentLangLinkTKM);
            await db.SaveChangesAsync();
            return MedicamentRegistrationDTO;
        }

        public bool DeleteMedicament(int MedicamentId)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    Medicament medicament = db.Medicaments.Find();
                    db.Medicaments.Remove(medicament);
                    db.SaveChanges();
                    IEnumerable<MedicamentLangLink> medicamentLangLinks = db.MedicamentLangLinks.Where(p => p.MedicamentId == MedicamentId);
                    db.MedicamentLangLinks.RemoveRange(medicamentLangLinks);
                    db.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        public MedicamentLangDTO GetMedicament(int MedicamentId, int LangId)
        {
            MedicamentLangLink medicamentLangLink = db.MedicamentLangLinks.Where(p => p.LangId == LangId&&p.MedicamentId==MedicamentId).FirstOrDefault();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<MedicamentLangLink, MedicamentLangDTO>()).CreateMapper();
            MedicamentLangDTO medicamentLangDTO = mapper.Map<MedicamentLangLink, MedicamentLangDTO>(medicamentLangLink);
            return medicamentLangDTO;
        }

        public IEnumerable<MedicamentLangDTO> GetMedicaments(int LangId)
        {
            IEnumerable<MedicamentLangLink> medicamentLangLinks = db.MedicamentLangLinks.Where(p => p.LangId == LangId);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<MedicamentLangLink, MedicamentLangDTO>()).CreateMapper();
            IEnumerable<MedicamentLangDTO> medicamentLangDTOs = mapper.Map<IEnumerable<MedicamentLangLink>, IEnumerable<MedicamentLangDTO>>(medicamentLangLinks);
            return medicamentLangDTOs;
        }

        public IEnumerable<MedicamentLangDTO> GetMedicamentsOfCountry(int LangId, int CountryId)
        {
            List<MedicamentLangDTO> medicaments = new List<MedicamentLangDTO>();
            IEnumerable<Manufacturer> manufacturers = db.Manufacturers.Where(c => c.CountryId == CountryId);
            foreach(Manufacturer manufacturer in manufacturers)
            {
                MedicamentLangDTO medicament = GetMedicamentsOfManufacturer(LangId, manufacturer.Id).First();
                if(!medicaments.Contains(medicament))
                {
                    medicaments.Add(medicament);
                }
            }
            return medicaments;
        }

        public IEnumerable<MedicamentLangDTO> GetMedicamentsOfManufacturer(int LangId, int ManufacturerId)
        {
            return GetMedicaments(LangId).Where(p => p.ManufacturerId == ManufacturerId);
        }
    }
}
