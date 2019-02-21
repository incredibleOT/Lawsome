using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lawsome.Data;

namespace Lawsome.Logic
{
    class NonDisclosureEvaluator:ClauseEvaluator
    {
        public bool AllowUnilateralBindingAgainstMe { get; set; } = true;

        public bool AlsoAllowOnlyMarked { get; set; } = true;

        public NonDisclosureEvaluator()
        {
            ID = IDs.NonDisclosure;
        }

        public override EvaluationResult Evaluate(Contract contract, Clause clause, int whoAmI)
        {
            if(! (clause is NonDisclosureClause nonDisclosureClause))
            {
                throw new InvalidOperationException("Unexpected Clause");
            }

            if (!AllowUnilateralBindingAgainstMe)
            {
                if(nonDisclosureClause.WhoIsBound.Count==1 &&
                    nonDisclosureClause.WhoIsBound[0].Name.Equals(contract.InterestedParty[whoAmI].Name))
                {
                    return new EvaluationResult(false, "Unilaterally binding to own disadvandtage");
                }
            }

            if (!AlsoAllowOnlyMarked)
            {
                if (nonDisclosureClause.OnlyMarked)
                {
                    return new EvaluationResult(false, "Only marked informations are labeld confidential.");
                }
            }
            return new EvaluationResult(true);
        }
    }
}
