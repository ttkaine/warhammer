using System.Collections.Generic;
using Warhammer.Core.Entities;

namespace Warhammer.Core.Abstract
{
    public interface IAuthenticatedDataProvider
    {
        ICollection<Person> MyPeople();
        void AddLog(int sessionId, int personId, string title, string description);
    }
}
