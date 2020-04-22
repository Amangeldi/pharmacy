using Pharmacy.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.BLL.Interfaces
{
    public interface ICountryService
    {
        Task<CountryRegistrationDTO> CreateCountry(CountryRegistrationDTO CountryRegistrationDTO);
        CountryLangDTO GetCountry(int CountryId, int LangId);
        IEnumerable<CountryLangDTO> GetCountries(int LangId);
        bool DeleteCountry(int CountryId);
    }
}
