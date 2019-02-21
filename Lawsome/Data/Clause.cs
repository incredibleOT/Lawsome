using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lawsome.Data
{
    abstract class Clause //: IClause
    {
        public string ID { get; set; }
        //TODO get rid of property
        //public List<IProperty> Properties { get; } = new List<IProperty>();

        //public Contract ParentContract { get; }

        //protected Clause(Contract parentContract)
        //{
        //    ParentContract = parentContract ?? throw new ArgumentNullException(nameof(parentContract));
        //}
    }
}
