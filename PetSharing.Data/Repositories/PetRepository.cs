using Microsoft.EntityFrameworkCore;
using PetSharing.Data.Contexts;
using System.Collections.Generic;
using PetSharing.Data.Entities;


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
            return db.PetProfiles;
        }

        public PetProfile Get(int id)
        {
            return db.PetProfiles.Find(id);
        }

        public void Create(PetProfile pet)
        {
            db.PetProfiles.Add(pet);
        }

        public void Update(PetProfile pet)
        {
            db.Entry(pet).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            PetProfile pet = db.PetProfiles.Find(id);
            if (pet != null)
                db.PetProfiles.Remove(pet);
        }
    }
}
