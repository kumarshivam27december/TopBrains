using System.Collections.Generic;
using System.Linq;

namespace TournamentRankingSystem;

public class Tournament
{
    private readonly List<Team> _rankings = new List<Team>();
    private readonly LinkedList<Match> _schedule = new LinkedList<Match>();
    private readonly Stack<Match> _undoStack = new Stack<Match>();

    public void ScheduleMatch(Match match)
    {
        _schedule.AddLast(match);
        EnsureTeamInRankings(match.Team1);
        EnsureTeamInRankings(match.Team2);
    }

    private void EnsureTeamInRankings(Team team)
    {
        if (!_rankings.Contains(team))
            _rankings.Add(team);
    }

    public void RecordMatchResult(Match match, int team1Score, int team2Score)
    {
        match.Team1ScoreAdded = team1Score;
        match.Team2ScoreAdded = team2Score;
        _undoStack.Push(match.Clone());

        match.Team1.Points += team1Score;
        match.Team2.Points += team2Score;
        SortRankings();
    }

    private void SortRankings()
    {
        _rankings.Sort();
    }

    public void UndoLastMatch()
    {
        if (_undoStack.Count == 0) return;
        var last = _undoStack.Pop();
        last.Team1.Points -= last.Team1ScoreAdded;
        last.Team2.Points -= last.Team2ScoreAdded;
        SortRankings();
    }

    public int GetTeamRanking(Team team)
    {
        var idx = _rankings.BinarySearch(team);
        if (idx < 0) idx = ~idx;
        return idx + 1; // 1-based ranking
    }

    public List<Team> GetRankings() => new List<Team>(_rankings);
}
