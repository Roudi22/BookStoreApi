using BookStoreApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace BookStoreApi.Services
{
    public class UserRepository
    {
        private readonly List<User> _users = new();
        private int _nextId = 1;

        public User Register(User user)
        {
            user.Id = _nextId++;
            _users.Add(user);
            return user;
        }

        public User Authenticate(string username, string password)
        {
            return _users.SingleOrDefault(x => x.Username == username && x.Password == password);
        }

        public IEnumerable<User> GetAll() => _users;
    }
}
