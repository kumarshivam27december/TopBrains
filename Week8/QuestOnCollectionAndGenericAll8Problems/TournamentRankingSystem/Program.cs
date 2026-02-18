using TournamentRankingSystem;

var tournament = new Tournament();
var teamA = new Team { Name = "Team Alpha", Points = 0 };
var teamB = new Team { Name = "Team Beta", Points = 0 };

var match = new Match(teamA, teamB);
tournament.ScheduleMatch(match);
tournament.RecordMatchResult(match, 3, 1); // Team A wins

var rankings = tournament.GetRankings();
Console.WriteLine(rankings[0].Name); // Should output: Team Alpha

tournament.UndoLastMatch();
Console.WriteLine(teamA.Points); // Should output: 0 (back to original)
