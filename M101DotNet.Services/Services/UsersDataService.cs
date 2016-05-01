namespace M101DotNet.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Data.Common.Repositories.Contracts;
    using Data.Models;
    using Models;

    public class UsersDataService : IUsersDataService
    {
        private readonly IGenericRepository<User> repository;

        public UsersDataService(IGenericRepository<User> repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            this.repository = repository;
        }

        public async Task<object> AddNewUser(UserServiceModel user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var entity = new User
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name
            };

            var result = await this.repository.Add(entity);

            await this.repository.SaveChanges();

            return result;
        }

        public async Task<UserServiceModel> GetUser(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            var entity = (await this.repository.All())
                .FirstOrDefault(u => u.Email == email);

            return new UserServiceModel
            {
                Id = entity.Id,
                Email = entity.Email,
                Name = entity.Name
            };
        }
    }
}