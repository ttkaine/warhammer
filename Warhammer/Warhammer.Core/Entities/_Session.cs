using System.Collections.Generic;
using System.Linq;

namespace Warhammer.Core.Entities
{
    public partial class Session
    {
        public IEnumerable<Person> People
        {
            get { return Related.OfType<Person>(); }
        }
    }
}
