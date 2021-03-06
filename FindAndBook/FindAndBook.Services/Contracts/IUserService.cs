﻿using System;
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

        User CreateUser(string username, string email, string firstName, string lastName, string phoneNumber);

        IQueryable<User> GetAll();

        void DeleteUser(string id);
    }
}
