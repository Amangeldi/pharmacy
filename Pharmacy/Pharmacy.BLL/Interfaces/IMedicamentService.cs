using Pharmacy.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.BLL.Interfaces
{
    public interface IMedicamentService
    {
        Task<MedicamentRegistrationDTO> CreateMedicament (MedicamentRegistrationDTO MedicamentRegistrationDTO);
        MedicamentLangDTO GetMedicament(int MedicamentId, int LangId);
        IEnumerable<MedicamentLangDTO> GetMedicaments(int LangId);
        bool DeleteMedicament(int MedicamentId);
        IEnumerable<MedicamentLangDTO> GetMedicamentsOfManufacturer(int LangId, int ManufacturerId);
        IEnumerable<MedicamentLangDTO> GetMedicamentsOfCountry(int LangId, int CountryId);
    }
}
