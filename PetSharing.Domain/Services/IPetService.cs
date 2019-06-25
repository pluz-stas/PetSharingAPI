using PetSharing.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetSharing.Domain.Services
{
    public interface IPetService
    {
        IEnumerable<PetProfile> GetAll(User owner);
        PetProfile GetById(int id);
        PetProfile Create(PetProfile pet);
        void Update(PetProfile pet);
        void Delete(int id);
    }
}
