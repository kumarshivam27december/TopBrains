using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithmFramework;

public class SelectionStrategy<TGene, TFitness, TChromosome>
    where TChromosome : class, IChromosome<TGene, TFitness>
    where TFitness : struct, IComparable<TFitness>
{
    public IEnumerable<TChromosome> Select(IEnumerable<TChromosome> population, int count)
    {
        return population.OrderByDescending(c => c.Fitness).Take(count);
    }
}

public class CrossoverStrategy<TGene, TFitness, TChromosome>
    where TChromosome : class, IChromosome<TGene, TFitness>
    where TFitness : struct, IComparable<TFitness>
{
    public TChromosome? Crossover(TChromosome parent1, TChromosome parent2)
    {
        return parent1.Crossover(parent2) as TChromosome;
    }
}
