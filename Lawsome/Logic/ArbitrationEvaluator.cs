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

        public List<GoverningLaw> InvalidGoverningLaw { get; set; } = new List<GoverningLaw>();

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

        private IEnumerable<string> GetCountriesForGoverningLaw(GoverningLaw governingLaw)
        {
            switch (governingLaw)
            {
                case GoverningLaw.Swiss:
                    return new[] { "Switzerland" };
                case GoverningLaw.French:
                    return new[] { "France" };
                case GoverningLaw.German:
                    return new[] { "Germany" };
                case GoverningLaw.Dutch:
                    return new[] { "Netherlands", "Holland" };
                case GoverningLaw.British:
                    return new[] { "Great Britain", "GB", "United Kingom", "UK" };
                case GoverningLaw.Hungarian:
                    return new[] { "Hungary" };
                default:
                    return new string[] { };
            }
        }


    }
}
