using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
    public class UserRegistrationService<TDTO, TModel> : IUserRegistrationService<TDTO, TModel> where TDTO : UserDTO where TModel : User
    {
        readonly ApiContext dbContext;
        readonly UserManager<User> _userManager;
        public UserRegistrationService(ApiContext context, UserManager<User> userManager)
        {
            dbContext = context;
            _userManager = userManager;
        }
        public async Task CreateUser(TDTO DTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TDTO, TModel>()).CreateMapper();
            TModel model = mapper.Map<TDTO, TModel>(DTO);
            model.UserName = DTO.Email;

            await _userManager.CreateAsync(model, DTO.Password);
            TModel user = dbContext.Set<TModel>().Where(p => p.Email == DTO.Email).FirstOrDefault();
            if (typeof(TModel) == typeof(Administrator))
            {
                await _userManager.AddToRoleAsync(user, "admin");
            }
            else if (typeof(TModel) == typeof(Author))
            {
                await _userManager.AddToRoleAsync(user, "author");
            }
            else if (typeof(TModel) == typeof(Moderator))
            {
                await _userManager.AddToRoleAsync(user, "moderator");
            }
        }

        public async Task DeleteUser(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);

        }

        public IEnumerable<TDTO> FindUser(Func<TModel, bool> predicate)
        {
            IEnumerable<TModel> model = dbContext.Set<TModel>().Where(predicate);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TDTO, TModel>()).CreateMapper();
            IEnumerable<TDTO> DTO = mapper.Map<IEnumerable<TModel>, IEnumerable<TDTO>>(model);
            return DTO;
        }

        public TDTO GetCurrent(string currentUserName)
        {
            TModel model = dbContext.Set<TModel>().Where(p => p.UserName == currentUserName).FirstOrDefault();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TDTO, TModel>()).CreateMapper();
            TDTO DTO = mapper.Map<TModel, TDTO>(model);
            return DTO;
        }

        public TDTO GetUser(string id)
        {
            TModel model = dbContext.Set<TModel>().Where(p => p.Id == id).FirstOrDefault();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TModel, TDTO>());
            var mapper = new Mapper(config);
            TDTO DTO = mapper.Map<TDTO>(model);
            return DTO;
        }

        public IEnumerable<TDTO> Getusers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TModel, TDTO>()).CreateMapper();
            IEnumerable<TDTO> DTO = mapper.Map<IEnumerable<TDTO>>(dbContext.Set<TModel>().ToList());
            return DTO;
        }

        public async Task<TDTO> UpdateUser(TDTO DTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TModel, TDTO>()).CreateMapper();
            TModel model = mapper.Map<TDTO, TModel>(DTO);
            await _userManager.UpdateAsync(model);
            return DTO;
        }
    }
}
