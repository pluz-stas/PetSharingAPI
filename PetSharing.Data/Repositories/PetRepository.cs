using Microsoft.EntityFrameworkCore;
using PetSharing.Data.Contexts;
using System.Collections.Generic;

namespace PetSharing.Data.Repositories
{
    public class PetProfileRepository : IRepository<PetProfile>
    {
        private PetSharingDbContext db;

        public PetProfileRepository(PetSharingDbContext context)
        {
            this.db = context;
        }

        public IEnumerable<PetProfile> GetAll()
        {
            return db.Pets;
        }

        public PetProfile Get(int id)
        {
            return db.Pets.Find(id);
        }

        public void Create(PetProfile pet)
        {
            db.Pets.Add(pet);
        }

        public void Update(PetProfile pet)
        {
            db.Entry(pet).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            PetProfile pet = db.Pets.Find(id);
            if (pet != null)
                db.Pets.Remove(pet);
        }
    }
}
