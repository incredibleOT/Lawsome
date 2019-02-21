using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lawsome.Data;

namespace Lawsome.Logic
{
    class ArbitrationEvaluator : ClauseEvaluator
    {
        //TODO useEnums
        public List<string> InvalidCountriesOfArbitration { get; set; } = new List<string>();

        public List<string> InvalidGoverningLaw { get; set; } = new List<string>();

        public bool AllowDifferingCounties { get; set; } = true;

        public ArbitrationEvaluator()
        {
            ID = IDs.Arbitration;
        }

        public override EvaluationResult Evaluate(Contract contract, Clause clause, int whoAmI)
        {
            if (!(clause is ArbitrationClause arbitrationClause))
            {
                throw new InvalidOperationException("Unexpected Clause");
            }

            if (InvalidCountriesOfArbitration.Contains(arbitrationClause.PlaceOfArbirtration.Country))
            {
                return new EvaluationResult(false, "Invalid Place of arbitration");
            }

            if(InvalidGoverningLaw.Contains(arbitrationClause.GoverningLaw))
            {
                return new EvaluationResult(false, "Invalid governing Law");
            }

            if (!AllowDifferingCounties)
            {
                if (!GetCountriesForGoverningLaw(arbitrationClause.GoverningLaw).Contains(
                    arbitrationClause.PlaceOfArbirtration.Country))
                {
                    return new EvaluationResult(false, "Counties of governing law and arbitration differ.");
                }
            }
            return new EvaluationResult(true);
        }

        private IEnumerable<string> GetCountriesForGoverningLaw(string governingLaw)
        {
            if(string.Equals(governingLaw, "swiss", StringComparison.InvariantCultureIgnoreCase))
            {
                return new[] { "Switzerland" };
            }
            else if (string.Equals(governingLaw, "french", StringComparison.InvariantCultureIgnoreCase))
            {
                return new[] { "France" };
            }
            else if (string.Equals(governingLaw, "german", StringComparison.InvariantCultureIgnoreCase))
            {
                return new[] { "Germany"};
            }
            else if (string.Equals(governingLaw, "dutch", StringComparison.InvariantCultureIgnoreCase))
            {
                return new[] { "Netherlands", "Holland" };
            }
            else if (string.Equals(governingLaw, "british", StringComparison.InvariantCultureIgnoreCase))
            {
                return new[] { "Great Britain", "GB", "United Kingom", "UK" };
            }
            else if (string.Equals(governingLaw, "hungarian", StringComparison.InvariantCultureIgnoreCase))
            {
                return new[] { "Hungary" };
            }
            return new string[] { };
        }


    }
}
