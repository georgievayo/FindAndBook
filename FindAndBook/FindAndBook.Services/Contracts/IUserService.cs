using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindAndBook.Models;

namespace FindAndBook.Services.Contracts
{
    public interface IUserService
    {
        User GetUserById(string id);

        IQueryable<User> GetUserByUsername(string username);

        User GetUserWithBookings(string id);

        User AddUser(string username, string email, string firstName, string lastName, string phoneNumber);
    }
}
