using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lawesome.Data
{
    /// <summary>
    /// Participant in a contract
    /// </summary>
    class Party
    {
        public string Name { get; set; }

        public Party(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }


    }
}
