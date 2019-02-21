using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lawesome.Data
{
    class Contract
    {
        public List<Party> InterestedParty { get; set; } = new List<Party>();

        public List<Clause> Clauses { get; set; } = new List<Clause>();

    }
}
