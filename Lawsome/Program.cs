using Lawsome.Data;
using Lawsome.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lawsome
{
    class Program
    {
        static void Main(string[] args)
        {
            var contract = new Contract();
            contract.InterestedParty.Add(new Party("Party 1"));
            contract.InterestedParty.Add(new Party("Party 2"));

            var arbitraionClause = new ArbitrationClause();
            arbitraionClause.GoverningLaw = GoverningLaw.French;
            arbitraionClause.PlaceOfArbirtration.Country = "Hungary";
            arbitraionClause.PlaceOfArbirtration.City = "Budapest";
            contract.Clauses.Add(arbitraionClause);

            var nonDisclosureClause = new NonDisclosureClause();
            nonDisclosureClause.OnlyMarked = false;
            nonDisclosureClause.WhoIsBound.Add(contract.InterestedParty[0]);
            contract.Clauses.Add(nonDisclosureClause);

            var evaluator = new Evaluator();
            var result = evaluator.Evaluate(contract, contract.InterestedParty[0]);

            foreach (var kvp in result)
            {
                Console.WriteLine($"{kvp.Key.GetType()}: {kvp.Value}");
            }
            Console.ReadLine();

        }
    }
}
