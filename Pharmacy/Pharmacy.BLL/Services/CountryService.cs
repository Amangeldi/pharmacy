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
    public class CountryService : ICountryService
    {
        readonly ApiContext db;
        public CountryService(ApiContext context)
        {
            db = context;
        }
        public async Task<CountryRegistrationDTO> CreateCountry(CountryRegistrationDTO CountryRegistrationDTO)
        {
            Country country = new Country();
            await db.Countries.AddAsync(country);
            List<Lang> langs = db.Langs.ToList();
            CountryLangLink countryLangLinkENG = new CountryLangLink
            {
                Name = CountryRegistrationDTO.NameENG,
                Capital = CountryRegistrationDTO.CapitalENG,
                Country = country,
                Lang = langs.Where(p => p.Name == "ENG").First()
            };
            CountryLangLink countryLangLinkRUS = new CountryLangLink
            {
                Name = CountryRegistrationDTO.NameRUS,
                Capital = CountryRegistrationDTO.CapitalRUS,
                Country = country,
                Lang = langs.Where(p => p.Name == "RUS").First()
            }; 
            CountryLangLink countryLangLinkTKM = new CountryLangLink
            {
                Name = CountryRegistrationDTO.NameTKM,
                Capital = CountryRegistrationDTO.CapitalTKM,
                Country = country,
                Lang = langs.Where(p => p.Name == "TKM").First()
            };
            await db.CountryLangLinks.AddRangeAsync(countryLangLinkENG, countryLangLinkRUS, countryLangLinkTKM);
            await db.SaveChangesAsync();
            return CountryRegistrationDTO;
        }

        public bool DeleteCountry(int CountryId)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    Country country = db.Countries.Find(CountryId);
                    db.Countries.Remove(country);
                    db.SaveChanges();
                    List<CountryLangLink> countryLangLinks = db.CountryLangLinks.Where(p => p.CountryId == CountryId).ToList();
                    db.CountryLangLinks.RemoveRange(countryLangLinks);
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

        public IEnumerable<CountryLangDTO> GetCountries(int LangId)
        {
            IEnumerable<CountryLangLink> countryLangLinks = db.CountryLangLinks.Where(p => p.LangId == LangId);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<CountryLangLink, CountryLangDTO>()).CreateMapper();
            IEnumerable<CountryLangDTO> countryLangDTOs = mapper.Map<IEnumerable<CountryLangLink>, IEnumerable<CountryLangDTO>>(countryLangLinks);
            return countryLangDTOs;
        }

        public CountryLangDTO GetCountry(int CountryId, int LangId)
        {
            return GetCountries(LangId).Where(p => p.CountryId == CountryId).FirstOrDefault();
        }
    }
}
