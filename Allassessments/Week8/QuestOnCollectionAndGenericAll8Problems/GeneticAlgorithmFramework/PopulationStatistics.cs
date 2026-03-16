namespace GeneticAlgorithmFramework;

public class PopulationStatistics
{
    public double AverageFitness { get; set; }
    public double MaxFitness { get; set; }
    public double MinFitness { get; set; }
    public int Count { get; set; }

    public PopulationStatistics Accumulate<TGene, TFitness, TChromosome>(TChromosome chromosome)
        where TChromosome : IChromosome<TGene, TFitness>
        where TFitness : struct, IComparable<TFitness>
    {
        var f = Convert.ToDouble(chromosome.Fitness);
        AverageFitness += f;
        if (f > MaxFitness) MaxFitness = f;
        if (Count == 0 || f < MinFitness) MinFitness = f;
        Count++;
        return this;
    }

    public PopulationStatistics Combine(PopulationStatistics other)
    {
        AverageFitness += other.AverageFitness;
        Count += other.Count;
        if (other.MaxFitness > MaxFitness) MaxFitness = other.MaxFitness;
        if (other.MinFitness < MinFitness || Count == 0) MinFitness = other.MinFitness;
        return this;
    }

    public PopulationStatistics Normalize()
    {
        if (Count > 0) AverageFitness /= Count;
        return this;
    }
}
