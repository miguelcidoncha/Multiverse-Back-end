using Entities;

namespace Multiverse.IServices
{
    public interface IUserService
    {
        UserItem GetUserById(int userId);
        List<UserItem> GetAllUsers();
        int InsertUser(UserItem userItem);
        void UpdateUser(UserItem userItem);
        void DeleteUser(int userId);
        UserItem AuthenticateUser(string userName, string password);
        UserItem GetUserByUserName(string userName); // Agregamos este método
    }
}
