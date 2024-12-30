using Explorer.Stakeholders.Core.Domain.Users;
using System.Collections.Generic;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IAchievementRepository
    {
        Achievement GetById(int id);
        IEnumerable<Achievement> GetAll();
        void Add(Achievement achievement); 
        void Update(Achievement achievement);
        void Delete(int id); 
    }
}
