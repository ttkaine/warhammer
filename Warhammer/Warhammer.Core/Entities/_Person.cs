using System.Collections.Generic;
using System.Linq;

namespace Warhammer.Core.Entities
{
    public partial class Person
    {
        public IEnumerable<Session> Sessions
        {
            get { return Related.OfType<Session>(); }
        }
    }
}
