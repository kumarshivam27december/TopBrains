namespace TournamentRankingSystem;

public class Match
{
    public Team Team1 { get; }
    public Team Team2 { get; }
    public int Team1ScoreAdded { get; set; }
    public int Team2ScoreAdded { get; set; }

    public Match(Team team1, Team team2)
    {
        Team1 = team1;
        Team2 = team2;
    }

    public Match Clone()
    {
        var clone = new Match(Team1, Team2)
        {
            Team1ScoreAdded = Team1ScoreAdded,
            Team2ScoreAdded = Team2ScoreAdded
        };
        return clone;
    }
}
