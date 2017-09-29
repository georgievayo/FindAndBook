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
    }
}
