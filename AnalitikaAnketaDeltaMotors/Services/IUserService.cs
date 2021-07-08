using System.Collections.Generic;
using UnitOfWorkExample.UnitOfWork.Models;

namespace UnitOfWorkExample.Services
{
    public interface IUserService
    {
        List<User> GetUsersAsync(string term);
        User CheckUser(string username,string password);
    }
}