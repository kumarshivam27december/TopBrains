namespace GeneticAlgorithmFramework;

public interface IChromosome<TGene, TFitness> : IComparable<IChromosome<TGene, TFitness>>
    where TFitness : IComparable<TFitness>
{
    IReadOnlyList<TGene> Genes { get; }
    TFitness Fitness { get; }
    IChromosome<TGene, TFitness> Crossover(IChromosome<TGene, TFitness> other);
    void Mutate(double mutationRate);
}
