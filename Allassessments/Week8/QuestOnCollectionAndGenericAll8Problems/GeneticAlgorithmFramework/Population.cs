using System.Collections.Generic;

namespace GeneticAlgorithmFramework;

public class Population<TGene, TFitness, TChromosome>
    where TChromosome : class, IChromosome<TGene, TFitness>
    where TFitness : struct, IComparable<TFitness>
{
    public List<TChromosome> Chromosomes { get; } = new List<TChromosome>();
}
