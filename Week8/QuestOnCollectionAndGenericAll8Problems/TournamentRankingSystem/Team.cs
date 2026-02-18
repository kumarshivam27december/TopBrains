namespace TournamentRankingSystem;

public class Team : IComparable<Team>
{
    public string Name { get; set; } = string.Empty;
    public int Points { get; set; }

    public int CompareTo(Team? other)
    {
        if (other is null) return 1;
        int pointsCompare = other.Points.CompareTo(Points); // Descending by points
        if (pointsCompare != 0) return pointsCompare;
        return string.Compare(Name, other.Name, StringComparison.Ordinal); // Then by name
    }
}
