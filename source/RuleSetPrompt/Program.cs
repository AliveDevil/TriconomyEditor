using System;
using System.IO;
using System.Text;
using RuleSet.Effects;
using RuleSet.Elements;

namespace RuleSetPrompt
{
    class Program
    {
        static void Main(string[] args)
        {
            RuleSet.RuleSet ruleSet = new RuleSet.RuleSet();
            ruleSet.Name = "Test";
            Building building = new Building() { Name = "New Building" };
            building.Effects.Add(new StorageEffect());
            ruleSet.Elements.Add(building);
            Resource resource = new Resource() { Name = "New Resource" };
            ruleSet.Elements.Add(resource);
            ruleSet.Elements.Add(new Resource() { Name = "Second Resource" });
            ruleSet.Elements.Add(new WorldResource() { Name = "New World Resource", Resource = resource });
            Job job = new Job() { Name = "Some Job" };
            building = new Building() { Name = "Workplace" };
            building.Effects.Add(new StorageEffect() { PublicAccessible = true });
            building.Effects.Add(new WorkplaceEffect() { Job = job });
            ruleSet.Elements.Add(building);
            ruleSet.Elements.Add(job);
            StringBuilder builder = new StringBuilder();
            using (var writer = new StringWriter(builder))
                RuleSet.RuleSet.Save(ruleSet, writer);
            Console.WriteLine(builder.ToString());
            Console.ReadLine();

            using (var reader = new StringReader(builder.ToString()))
            {
                RuleSet.RuleSet.Load(reader);
            }
        }
    }
}
