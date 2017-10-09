using System;
using System.Data.Entity;
using System.Linq;
using FindAndBook.Data.Contracts;
using FindAndBook.Factories;
using FindAndBook.Models;
using FindAndBook.Services.Contracts;

namespace FindAndBook.Services
{
    public class UserService : IUserService
    {
        private IRepository<User> userRepository;

        private IUnitOfWork unitOfWork;

        private IUserFactory userFactory;

        public UserService(IRepository<User> userRepository, IUnitOfWork unitOfWork, IUserFactory userFactory)
        {
            if (userRepository == null)
            {
                throw new ArgumentNullException(nameof(userRepository));
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }

            if (userFactory == null)
            {
                throw new ArgumentNullException(nameof(userFactory));
            }

            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
            this.userFactory = userFactory;
        }

        public User GetUserById(string id)
        {
            var foundUser = this.userRepository.GetById(id);

            return foundUser;
        }

        public IQueryable<User> GetUserByUsername(string username)
        {
            return this.userRepository
                .All
                .Where(u => u.UserName == username)
                .Include(x => x.Bookings);
        }

        public User GetUserWithBookings(string id)
        {
            return this.userRepository
                .All
                .Where(x => x.Id == id)
                .Include(x => x.Bookings)
                .FirstOrDefault();
        }

        public User AddUser(string username, string email, string firstName, string lastName, string phoneNumber)
        {
            var user = this.userFactory.CreateUser(username, email, firstName, lastName, phoneNumber);

            return user;
        }

        public IQueryable<User> GetAll()
        {
            return this.userRepository.All;
        }

        public void DeleteUser(string id)
        {
            var user = this.userRepository.GetById(id);
            this.userRepository.Delete(user);
            this.unitOfWork.Commit();
        }
    }
}
