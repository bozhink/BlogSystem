namespace M101DotNet.Services.Contracts
{
    using System.Threading.Tasks;
    using Models;

    public interface IUsersDataService
    {
        Task<UserServiceModel> GetUser(string email);

        Task<object> AddNewUser(UserServiceModel user);
    }
}