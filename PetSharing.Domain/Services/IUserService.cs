using AutoMapper;
using PetSharing.Data.Contexts;
using PetSharing.Data.UnitOfWorks;
using PetSharing.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetSharing.Domain.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
        User Create(User user, string password);
        void Update(User user, string password = null);
        void Delete(int id);
    }
    public class UserService : IUserService
    {
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;

        public UserService(PetSharingDbContext context, IMapper mapper)
        {
            _unitOfWork = new UnitOfWork();
            _mapper = mapper;
        }

        public User Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;
            var user = _unitOfWork.Users.Authenticate(email, password);
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;
            // authentication successful
            return _mapper.Map<User>(user);
        }

        public IEnumerable<User> GetAll()
        {
            var userList = _mapper.Map<IList<User>>(_unitOfWork.Users.GetAll());
            return userList;
        }

        public User GetById(int id)
        {
            var user = _mapper.Map<User>(_unitOfWork.Users.Get(id));
            return user;
        }

        public User Create(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is required");
            _unitOfWork.Users.Create(_mapper.Map<PetSharing.Data.Entities.User>(user), password);
            _unitOfWork.Save();
            return user;
        }

        public void Update(User userParam, string password = null)
        {
            if (string.IsNullOrWhiteSpace(password))
                return;
            _unitOfWork.Users.Update(_mapper.Map<Data.Entities.User>(userParam), password);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            _unitOfWork.Users.Delete(id);
            _unitOfWork.Save();
        }

        // private helper methods



        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }
    }
}
