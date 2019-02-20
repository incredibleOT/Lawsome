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

            JsonSerializer serializer = new JsonSerializer();
            serializer.TypeNameHandling = TypeNameHandling.Auto;
            serializer.Formatting = Formatting.Indented;
            var builder = new StringBuilder();

            using (var writer = new StringWriter(builder))
            {
                serializer.Serialize(writer, contract, typeof(Contract));

            }

            Console.WriteLine("Serialized:");
            Console.Write(builder.ToString());

            Contract deserialized = null;

            using (var reader = new StringReader(builder.ToString()))
            {
                deserialized = serializer.Deserialize(reader, typeof(Contract)) as Contract;
            }

            result = evaluator.Evaluate(deserialized, deserialized.InterestedParty[0]);

            foreach (var kvp in result)
            {
                Console.WriteLine($"{kvp.Key.GetType()}: {kvp.Value}");
            }
            Console.ReadLine();

        }
    }
}
