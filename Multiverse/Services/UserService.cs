using Data;
using Entities;
using Microsoft.EntityFrameworkCore;
using Multiverse.IServices;


namespace Multiverse.Services
{
    public class UserService : IUserService
    {
        private readonly ServiceContext _serviceContext;

        public UserService(ServiceContext serviceContext)
        {
            _serviceContext = serviceContext;
        }

        public UserItem GetUserById(int userId)
        {
            return _serviceContext.UserItems.FirstOrDefault(u => u.IdUsuario == userId);
        }

        public List<UserItem> GetAllUsers()
        {
            return _serviceContext.UserItems.ToList();
        }

        public int InsertUser(UserItem userItem)
        {
            _serviceContext.UserItems.Add(userItem);
            _serviceContext.SaveChanges();
            return userItem.IdUsuario;
        }

        public void UpdateUser(UserItem userItem)
        {
            _serviceContext.Entry(userItem).State = EntityState.Modified;
            _serviceContext.SaveChanges();
        }

        public void DeleteUser(int userId)
        {
            var user = _serviceContext.UserItems.FirstOrDefault(u => u.IdUsuario == userId);
            if (user != null)
            {
                _serviceContext.UserItems.Remove(user);
                _serviceContext.SaveChanges();
            }
        }

        public UserItem AuthenticateUser(string userName, string password)
        {
            // Realiza la autenticación del usuario según tu lógica personalizada aquí
            return _serviceContext.UserItems.FirstOrDefault(u => u.UserName == userName && u.Password == password && u.IdRoll == 1);
        }
    }
}


