using Pharmacy.BLL.DTO;
using Pharmacy.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.BLL.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryRegistrationDTO> CreateCategory(CategoryRegistrationDTO categoryRegistrationDTO);
        CategoryLangDTO GetCategory(int CategoryId, int LangId);
        IEnumerable<CategoryLangLink> GetCategories(int LangId);
        bool DeleteCategory(int CategoryId);
        CategoryLangDTO EditCategory(CategoryLangDTO categoryLangDTO);
    }
}
