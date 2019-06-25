using PetSharing.Data.Contexts;
using PetSharing.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetSharing.Data.UnitOfWorks
{
    public class UnitOfWork : IDisposable
    {
        private PetSharingDbContext db/* = new PetSharingDbContext(Options)*/;
        private UserRepository userRepository;
        private PetProfileRepository petRepository;
        public UserRepository Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public PetProfileRepository Pets
        {
            get
            {
                if (petRepository == null)
                    petRepository = new PetProfileRepository(db);
                return petRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}