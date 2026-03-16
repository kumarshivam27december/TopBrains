using GeneticAlgorithmFramework;

var population = new Population<int, double, BitChromosome>();
for (int i = 0; i < 20; i++)
    population.Chromosomes.Add(BitChromosome.Random(10));

var algo = new EvolutionaryAlgorithm<int, double, BitChromosome>(population);
var config = new EvolutionConfiguration { Generations = 5, PopulationSize = 20, MutationRate = 0.1, EliteCount = 2 };
var (best, history) = await algo.EvolveAsync(config);
Console.WriteLine($"Best fitness: {best?.Fitness ?? 0}");
var stats = algo.GetStatistics();
Console.WriteLine($"Stats - Avg: {stats.AverageFitness}, Max: {stats.MaxFitness}");
