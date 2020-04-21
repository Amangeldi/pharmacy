using Microsoft.AspNetCore.Identity;
using Pharmacy.DAL.EF;
using Pharmacy.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.DAL.Data
{
    public class SampleData
    {
        public static async Task Initialize(UserManager<User> userManager, ApiContext context, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@gmail.com";
            string password = "_Aa123456";
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
                await roleManager.CreateAsync(new IdentityRole("author"));
                await roleManager.CreateAsync(new IdentityRole("moderator"));
                User admin = new Administrator { Email = adminEmail, UserName = adminEmail };
                await userManager.CreateAsync(admin, password);
                await userManager.AddToRoleAsync(admin, "admin");
                User author = new Author { Email = "dustin.carroll@example.com", UserName = "dustin.carroll@example.com" };
                await userManager.CreateAsync(author, password);
                await userManager.AddToRoleAsync(author, "author");
                Moderator moderator = new Moderator { Email = "john.hale@example.com", UserName = "john.hale@example.com" };
                await userManager.CreateAsync(moderator, password);
                await userManager.AddToRoleAsync(moderator, "moderator");
                Lang ENG = new Lang { Name = "English" };
                Lang RUS = new Lang { Name = "Русский" };
                Lang TKM = new Lang { Name = "Türkmençe" };
                await context.Langs.AddRangeAsync(ENG, RUS, TKM);
                Category Analgesics = new Category { Status = true };
                Category Anesthetics = new Category { Status = true };
                await context.Categories.AddRangeAsync(Analgesics, Anesthetics);
                CategoryLangLink AnalgesicENG = new CategoryLangLink { Category = Analgesics, Lang = ENG, Name = "Analgesics", Description = @"A group of medications that are used to relieve pain associated with inflammation or damage to tissues and organs.
Unlike anesthetics, analgesics act selectively. They relieve or eliminate pain without reducing the sensitivity of a specific area of the body." };
                CategoryLangLink AnalgesicRUS = new CategoryLangLink { Category = Analgesics, Lang = RUS, Name = "Анальгетики", Description = @"Группа медицинских препаратов, которые применяются для облегчения болевого синдрома, связанного с воспалением или повреждением тканей и органов.
В отличие от анестетиков, анальгезирующее средства действуют избирательно. Они ослабляют или устраняют боль, не снижая чувствительность определенной области тела." };
                CategoryLangLink AnalgesicTKM = new CategoryLangLink { Category = Analgesics, Lang = TKM, Name = "Analgetikler", Description = @"Analgetikler barada Türkmençe." };

                CategoryLangLink AnestheticENG = new CategoryLangLink { Category = Anesthetics, Lang = ENG, Name = "Anesthetics", Description = "Drugs with the ability to cause anesthesia." };
                CategoryLangLink AnestheticRUS = new CategoryLangLink { Category = Anesthetics, Lang = RUS, Name = "Анестетики", Description = "Лекарственные средства, обладающие способностью вызывать анестезию." };
                CategoryLangLink AnestheticTKM = new CategoryLangLink { Category = Anesthetics, Lang = ENG, Name = "Anesthetics", Description = "Anestetikler barada Türkmençe." };
                await context.CategoryLangLinks.AddRangeAsync(AnalgesicENG, AnalgesicRUS, AnalgesicTKM, AnestheticENG, AnestheticRUS, AnestheticTKM);
                Country Russia = new Country();
                await context.Countries.AddAsync(Russia);
                CountryLangLink RussiaENG = new CountryLangLink { Capital = "Moscow", Country = Russia, Name= "Russia", Lang = ENG };
                CountryLangLink RussiaRUS = new CountryLangLink { Capital = "Москва", Country = Russia, Name = "Россия", Lang = RUS };
                CountryLangLink RussiaTKM = new CountryLangLink { Capital = "Moskwa", Country = Russia, Name = "Russiýa", Lang = TKM };
                await context.CountryLangLinks.AddRangeAsync(RussiaENG, RussiaRUS, RussiaTKM);
                Manufacturer Werofarm = new Manufacturer { Country = Russia, Name = "Верофарм" };
                await context.Manufacturers.AddAsync(Werofarm);
                Medicament Amigrenin = new Medicament { Category = Analgesics, Manufacturer = Werofarm };
                await context.Medicaments.AddAsync(Amigrenin);
                MedicamentLangLink AmigreninENG = new MedicamentLangLink { Medicament = Amigrenin, Lang = ENG, Description = "Code ATC: N02CC01", MedicamentName = "Amigrenin" };
                MedicamentLangLink AmigreninRUS = new MedicamentLangLink { Medicament = Amigrenin, Lang = RUS, Description = "Код ATX: N02CC01", MedicamentName = "АМИГРЕНИН" };
                MedicamentLangLink AmigreninTKM = new MedicamentLangLink { Medicament = Amigrenin, Lang = TKM, Description = "ATH Kod: N02CC01", MedicamentName = "Amigrenin" };
                await context.MedicamentLangLinks.AddRangeAsync(AmigreninENG, AmigreninRUS, AmigreninTKM);
                await context.SaveChangesAsync();
            }
        }
    }
}
