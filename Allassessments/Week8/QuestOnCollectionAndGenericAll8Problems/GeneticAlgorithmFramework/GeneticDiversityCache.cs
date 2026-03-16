using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithmFramework;

public class GeneticDiversityCache<TGene>
{
    private readonly HashSet<string> _geneSignatures = new();

    public double MeasureDiversity(IEnumerable<IReadOnlyList<TGene>> chromosomes)
    {
        var list = chromosomes.ToList();
        if (list.Count == 0) return 0;
        int unique = list.Select(GenesToSignature).Distinct().Count();
        return (double)unique / list.Count;
    }

    private static string GenesToSignature(IReadOnlyList<TGene> genes)
    {
        return string.Join(",", genes);
    }
}
