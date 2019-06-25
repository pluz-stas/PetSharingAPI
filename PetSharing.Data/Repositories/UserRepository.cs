using Microsoft.EntityFrameworkCore;
using PetSharing.Data.Contexts;
using PetSharing.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetSharing.Data.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private PetSharingDbContext db;

        public UserRepository(PetSharingDbContext context)
        {
            db = context;
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public void Create(User user)
        {
            db.Users.Add(user);
        }

        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
                db.Users.Remove(user);
        }
        public User Authenticate(string email, string password)
        {
            var user = db.Users.SingleOrDefault(x => x.Email == email);
            return user;
        }
        public User Create(User user, string password)
        {
            if (db.Users.Any(x => x.Email == user.Email))
                return null;
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            db.Users.Add(user);
            return user;
        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        public void Update(User userParam, string password = null)
        {
            var user = db.Users.Find(userParam.Id);
            if (userParam.Email != user.Email)
            {
                // username has changed so check if the new username is already taken
                if (db.Users.Any(x => x.Email == userParam.Email))
                    throw new Exception("Username " + userParam.Email + " is already taken");
            }
            user.FullName = userParam.FullName;
            user.Phone = userParam.Phone;
            user.Email = userParam.Email;
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            db.Users.Update(user);
        }
    }
}
