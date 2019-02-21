using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lawesome.Data
{

    interface IProperty
    {
        /// <summary>
        /// Empty indicates for all
        /// </summary>
        List<Party> AppliesTo { get; }

        object Value { get; set; }
    }

    interface IProperty<T> : IProperty
    {
        new T Value { get; set; }
    }
}
