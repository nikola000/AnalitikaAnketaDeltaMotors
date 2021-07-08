using System.Collections.Generic;
using System.Linq;
using UnitOfWorkExample.UnitOfWork;
using UnitOfWorkExample.UnitOfWork.Models;

namespace UnitOfWorkExample.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public UserService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public User CheckUser(string username, string password)
        {
            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                var users = unitOfWork.Repository().Find<User>(x => x.Username == username && x.Password == password);
                if (users.FirstOrDefault() == null)
                {
                    return null;
                }

                return users.FirstOrDefault();
            }
        }

        public List<User> GetUsersAsync(string term)
        {
            using (var unitOfWork = _unitOfWorkFactory.Create())
            {
                var users = unitOfWork.Repository().Find<User>(x => x.Name.Contains(term));
                return users.ToList();
            }
        }
    }
}
