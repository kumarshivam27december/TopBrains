using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeneticAlgorithmFramework;

public class EvolutionaryAlgorithm<TGene, TFitness, TChromosome>
    where TChromosome : class, IChromosome<TGene, TFitness>
    where TFitness : struct, IComparable<TFitness>
{
    private Population<TGene, TFitness, TChromosome> _population;
    private readonly SelectionStrategy<TGene, TFitness, TChromosome> _selectionStrategy = new();
    private readonly CrossoverStrategy<TGene, TFitness, TChromosome> _crossoverStrategy = new();
    private readonly AdaptiveSortedList<TFitness, TChromosome> _elitePool = new();
    private readonly GeneticDiversityCache<TGene> _diversityCache = new();

    public EvolutionaryAlgorithm(Population<TGene, TFitness, TChromosome> population)
    {
        _population = population;
    }

    public async Task<(TChromosome? BestSolution, IEnumerable<EvolutionMetrics> History)> EvolveAsync(
        EvolutionConfiguration config,
        CancellationToken cancellationToken = default)
    {
        var history = new List<EvolutionMetrics>();
        TChromosome? best = null;

        for (int gen = 0; gen < config.Generations && !cancellationToken.IsCancellationRequested; gen++)
        {
            var elites = _selectionStrategy.Select(_population.Chromosomes, config.EliteCount).ToList();
            var offspring = new List<TChromosome>();

            await Parallel.ForEachAsync(Enumerable.Range(0, config.PopulationSize - config.EliteCount),
                cancellationToken, async (_, ct) =>
                {
                    var parents = _selectionStrategy.Select(_population.Chromosomes, 2).ToList();
                    if (parents.Count >= 2)
                    {
                        var child = _crossoverStrategy.Crossover(parents[0], parents[1]);
                        if (child != null)
                        {
                            child.Mutate(config.MutationRate);
                            lock (offspring) offspring.Add(child);
                        }
                    }
                    await Task.Yield();
                });

            _population.Chromosomes.Clear();
            _population.Chromosomes.AddRange(elites);
            _population.Chromosomes.AddRange(offspring.Take(config.PopulationSize - config.EliteCount));

            var diversity = _diversityCache.MeasureDiversity(_population.Chromosomes.Select(c => c.Genes));
            best = _population.Chromosomes.OrderByDescending(c => c.Fitness).FirstOrDefault();
            var avgFitness = _population.Chromosomes.Count > 0
                ? _population.Chromosomes.Average(c => Convert.ToDouble(c.Fitness))
                : 0;
            var bestFitness = best != null ? Convert.ToDouble(best.Fitness) : 0;

            history.Add(new EvolutionMetrics
            {
                Generation = gen,
                AverageFitness = avgFitness,
                BestFitness = bestFitness,
                Diversity = diversity
            });
        }

        return (best, history);
    }

    public PopulationStatistics GetStatistics()
    {
        var stats = _population.Chromosomes
            .AsParallel()
            .WithDegreeOfParallelism(Environment.ProcessorCount)
            .Aggregate(
                () => new PopulationStatistics(),
                (acc, chromosome) => acc.Accumulate(chromosome),
                (acc1, acc2) => acc1.Combine(acc2),
                final => final.Normalize());
        return stats;
    }
}