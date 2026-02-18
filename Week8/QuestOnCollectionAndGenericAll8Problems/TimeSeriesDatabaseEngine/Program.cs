using TimeSeriesDatabaseEngine;

var db = new TimeSeriesDatabase<DateTime, double>();
await db.AppendAsync(new[]
{
    new TimeSeriesPoint<DateTime, double> { Timestamp = DateTime.UtcNow.AddMinutes(-10), Value = 1.0 },
    new TimeSeriesPoint<DateTime, double> { Timestamp = DateTime.UtcNow.AddMinutes(-5), Value = 2.0 },
    new TimeSeriesPoint<DateTime, double> { Timestamp = DateTime.UtcNow, Value = 1.5 }
});
Console.WriteLine($"Count: {db.Count}");
var windows = db.RollingWindow(DateTime.UtcNow.AddHours(-1), DateTime.UtcNow.AddMinutes(1), TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(1), vals => vals.Average()).Take(3);
foreach (var w in windows)
    Console.WriteLine($"{w.Start:HH:mm} - {w.End:HH:mm}: {w.Aggregate}");
