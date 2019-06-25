using PetSharing.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetSharing.Domain.Services
{ 
    public interface IPostService
    {
        IEnumerable<Post> GetPosts(PetProfile pet);
        Post GetById(int id);
        Post Create(Post post);
        void Delete(int id);
    }
}