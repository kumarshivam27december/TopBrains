using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithmFramework;

public class BitChromosome : IChromosome<int, double>
{
    private static readonly Random Rnd = new();
    public IReadOnlyList<int> Genes { get; private set; }
    public double Fitness { get; private set; }

    public BitChromosome(IReadOnlyList<int> genes)
    {
        Genes = genes.ToList();
        Fitness = genes.Sum();
    }

    public int CompareTo(IChromosome<int, double>? other) => other == null ? 1 : Fitness.CompareTo(other.Fitness);

    public IChromosome<int, double> Crossover(IChromosome<int, double> other)
    {
        var childGenes = new int[Genes.Count];
        for (int i = 0; i < Genes.Count; i++)
            childGenes[i] = Rnd.NextDouble() < 0.5 ? Genes[i] : other.Genes[i];
        return new BitChromosome(childGenes);
    }

    public void Mutate(double mutationRate)
    {
        var list = Genes.ToList();
        for (int i = 0; i < list.Count; i++)
            if (Rnd.NextDouble() < mutationRate)
                list[i] = 1 - list[i];
        Genes = list;
        Fitness = list.Sum();
    }

    public static BitChromosome Random(int length)
    {
        var genes = Enumerable.Range(0, length).Select(_ => Rnd.Next(2)).ToList();
        return new BitChromosome(genes);
    }
}
