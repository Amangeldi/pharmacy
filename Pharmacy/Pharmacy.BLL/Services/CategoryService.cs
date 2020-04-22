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
    public class CategoryService : ICategoryService
    {
        readonly ApiContext db;
        public CategoryService(ApiContext context)
        {
            db = context;
        }
        public async Task<CategoryRegistrationDTO> CreateCategory(CategoryRegistrationDTO categoryRegistrationDTO)
        {
            Category category = new Category { Status = true };
            await db.Categories.AddAsync(category);
            List<Lang> langs = db.Langs.ToList();
            CategoryLangLink categoryENG = new CategoryLangLink
            {
                Category = category,
                Description = categoryRegistrationDTO.DescriptionENG,
                Name = categoryRegistrationDTO.NameENG,
                Lang = langs.Where(p => p.Name == "ENG").First()
            };
            CategoryLangLink categoryRUS = new CategoryLangLink
            {
                Category = category,
                Description = categoryRegistrationDTO.DescriptionRUS,
                Name = categoryRegistrationDTO.NameRUS,
                Lang = langs.Where(p => p.Name == "RUS").First()
            };
            CategoryLangLink categoryTKM = new CategoryLangLink
            {
                Category = category,
                Description = categoryRegistrationDTO.DescriptionRUS,
                Name = categoryRegistrationDTO.NameRUS,
                Lang = langs.Where(p => p.Name == "TKM").First()
            };
            await db.CategoryLangLinks.AddRangeAsync(categoryENG, categoryRUS, categoryTKM);
            await db.SaveChangesAsync();
            return categoryRegistrationDTO;
        }

        public bool DeleteCategory(int CategoryId)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    Category category = db.Categories.Find(CategoryId);
                    db.Categories.Remove(category);
                    db.SaveChanges();
                    List<CategoryLangLink> categoryLangLinks = db.CategoryLangLinks.Where(p => p.CategoryId == CategoryId).ToList();
                    db.CategoryLangLinks.RemoveRange(categoryLangLinks);
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

        public CategoryLangDTO EditCategory(CategoryLangDTO categoryLangDTO)
        {
#warning Нужно сделать
            throw new NotImplementedException();
        }

        public IEnumerable<CategoryLangLink> GetCategories(int LangId)
        {
            List<CategoryLangLink> categories = db.CategoryLangLinks.Where(p => p.LangId == LangId).ToList();
            return categories;
        }

        public CategoryLangDTO GetCategory(int CategoryId, int LangId)
        {
            CategoryLangLink categoryLangLink = db.CategoryLangLinks.Where(c => c.CategoryId == CategoryId && c.LangId == LangId).FirstOrDefault();
            if (categoryLangLink == null)
            {
                throw new Exception("Категория не существует");
            }
            Category category = db.Categories.Find(categoryLangLink.CategoryId);
            CategoryLangDTO categoryLangDTO = new CategoryLangDTO
            {
                Description = categoryLangLink.Description,
                Name = categoryLangLink.Name,
                LangId = categoryLangLink.LangId,
                CategoryId = CategoryId,
                Status = category.Status
            };
            return categoryLangDTO;
        }
    }
}
