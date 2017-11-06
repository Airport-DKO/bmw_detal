using System.Collections.Generic;
using WebApplication.DatabaseEntities;

namespace WebApplication.Repositories.Interfaces
{
    public interface IDetailRepository
    {
        void Create(Detail detail);
        void Delete(int internalId);
        Detail Get(int internalId);
        List<Detail> GetDetails();
        void Update(Detail detail);
    }
}