using Lawsome.Data;
using Lawsome.Logic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lawsome
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0].Equals("eval", StringComparison.InvariantCultureIgnoreCase) && args.Length > 2)
                {
                    EvaluateContract(args[1], args.Skip(2));
                    return;
                }

            }


            var contract = new Contract();
            contract.InterestedParty.Add(new Party("Party 1"));
            contract.InterestedParty.Add(new Party("Party 2"));

            var arbitraionClause = new ArbitrationClause();
            arbitraionClause.GoverningLaw = "French";
            arbitraionClause.PlaceOfArbirtration.Country = "Hungary";
            arbitraionClause.PlaceOfArbirtration.City = "Budapest";
            contract.Clauses.Add(arbitraionClause);

            var nonDisclosureClause = new NonDisclosureClause();
            nonDisclosureClause.OnlyMarked = false;
            nonDisclosureClause.WhoIsBound.Add(contract.InterestedParty[0]);
            contract.Clauses.Add(nonDisclosureClause);

            //setup eval
            var evaluator = new Evaluator();

            var arbEval1 = new ArbitrationEvaluator();
            arbEval1.InvalidCountriesOfArbitration.Add("UK");
            arbEval1.InvalidCountriesOfArbitration.Add("Great Britain");
            arbEval1.InvalidCountriesOfArbitration.Add("United Kingdom");
            arbEval1.InvalidGoverningLaw.Add("British");
            evaluator.ClauseEvaluators.Add(arbEval1);

            var arbEval2 = new ArbitrationEvaluator();
            arbEval2.AllowDifferingCounties = false;
            evaluator.ClauseEvaluators.Add(arbEval2);

            var nonDiscEval1 = new NonDisclosureEvaluator();
            nonDiscEval1.AlsoAllowOnlyMarked = false;
            nonDiscEval1.AllowUnilateralBindingAgainstMe = false;
            evaluator.ClauseEvaluators.Add(nonDiscEval1);



            var result = evaluator.Evaluate(contract, 0);

            foreach (var kvp in result)
            {
                Console.WriteLine($"{kvp.Key.GetType()}: {kvp.Value}");
            }

            JsonSerializer serializer = new JsonSerializer();
            serializer.TypeNameHandling = TypeNameHandling.Auto;
            serializer.Formatting = Formatting.Indented;
            var builder = new StringBuilder();



            using (var writer = new StringWriter(builder))
            {
                serializer.Serialize(writer, contract, typeof(Contract));
                builder.AppendLine();
                builder.AppendLine();
                serializer.Serialize(writer, evaluator.ClauseEvaluators);
            }

            File.WriteAllText(@"D:\contractStuff.json", builder.ToString());

            Console.WriteLine("Serialized:");
            Console.Write(builder.ToString());

            Contract deserialized = null;

            using (var reader = new StringReader(builder.ToString()))
            {
                deserialized = serializer.Deserialize(reader, typeof(Contract)) as Contract;
            }


            result = evaluator.Evaluate(deserialized, 0);

            foreach (var kvp in result)
            {
                Console.WriteLine($"{kvp.Key.GetType()}: {kvp.Value}");
            }
            Console.ReadLine();



        }


        static void EvaluateContract(string contractPath, IEnumerable<string> evaluatorPaths)
        {
            if (!evaluatorPaths.Any())
            {
                return;
            }

            Contract contract = null;
            var evaluator = new Evaluator();

            JsonSerializer serializer = new JsonSerializer();
            serializer.TypeNameHandling = TypeNameHandling.Auto;
            serializer.Formatting = Formatting.Indented;

            var rawContract = File.ReadAllText(contractPath);

            using (var reader = new StringReader(rawContract))
            {
                contract = serializer.Deserialize(reader, typeof(Contract)) as Contract;
            }

            foreach (var evaluatorPath in evaluatorPaths)
            {
                var rawEvals = File.ReadAllText(evaluatorPath);
                using (var reader = new StringReader(rawEvals))
                {
                    var evals = serializer.Deserialize(reader, typeof(List<ClauseEvaluator>)) as List<ClauseEvaluator>;
                    evaluator.ClauseEvaluators.AddRange(evals);
                }
            }

            var result = evaluator.Evaluate(contract, 0);
            foreach (var kvp in result)
            {
                Console.WriteLine($"{kvp.Key.GetType()}: {kvp.Value}");
            }

        }
    }
}
