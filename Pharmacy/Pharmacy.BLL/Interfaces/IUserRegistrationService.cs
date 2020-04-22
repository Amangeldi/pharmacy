using Pharmacy.BLL.DTO;
using Pharmacy.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.BLL.Interfaces
{
    public interface IUserRegistrationService<TDTO, TModel> where TDTO : UserDTO where TModel : User
    {
        Task CreateUser(TDTO DTO);
        TDTO GetUser(string id);
        IEnumerable<TDTO> Getusers();
        Task DeleteUser(string id);
        IEnumerable<TDTO> FindUser(Func<TModel, bool> predicate);
        public TDTO GetCurrent(string currentUserName);
        public Task<TDTO> UpdateUser(TDTO DTO);
    }
}
