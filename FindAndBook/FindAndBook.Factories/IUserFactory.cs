using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindAndBook.Models;

namespace FindAndBook.Factories
{
    public interface IUserFactory
    {
        User CreateUser(string username, string email, string firstName, string lastName);
    }
}
