namespace GeneticAlgorithmFramework;

public class EvolutionConfiguration
{
    public int Generations { get; set; } = 100;
    public int PopulationSize { get; set; } = 50;
    public double MutationRate { get; set; } = 0.01;
    public int EliteCount { get; set; } = 5;
}
